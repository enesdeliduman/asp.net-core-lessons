using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using BlogApp.Entity;
using BlogApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;

namespace BlogApp.Controllers
{
    public class PostController : Controller
    {
        private IPostRepository _postRepository;
        private ICommentRepository _commentRepository;
        private ITagRepository _tagRepository;
        public PostController(IPostRepository postRepository, ICommentRepository commentRepository, ITagRepository tagRepository)
        {
            _postRepository = postRepository;
            _commentRepository = commentRepository;
            _tagRepository = tagRepository;
        }

        public async Task<IActionResult> Index(string url)
        {
            var postsQuery = _postRepository.Posts.AsQueryable();

            if (!string.IsNullOrEmpty(url))
            {
                postsQuery = postsQuery.Where(x => x.Tags.Any(t => t.Url == url));
            }

            var posts = await postsQuery
                .Include(x => x.Tags)
                .ToListAsync();
            return View(posts);
        }

        public async Task<IActionResult> Details(string url)
        {
            return View(await _postRepository
                .Posts
                .Include(x => x.Tags)
                .Include(x => x.Comments)
                .ThenInclude(x => x.User)
                .FirstOrDefaultAsync(p => p.Url == url));
        }

        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(PostCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "");
            _postRepository.CreatePost(
                new Post
                {
                    Title = model.Title,
                    Content = model.Content,
                    Url = model.Url,
                    UserId = UserId,
                    Image = "1.jpg"
                }
            );
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult AddComment(int PostId, string Text)
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var UserName = User.FindFirstValue(ClaimTypes.Name);
            var Avatar = User.FindFirstValue(ClaimTypes.UserData);

            var entity = new Comment
            {
                PostId = PostId,
                Text = Text,
                PublishedOn = DateTime.Now,
                UserId = int.Parse(UserId ?? "")
            };
            _commentRepository.CreateComment(entity);
            return Json(new
            {
                UserName,
                Text,
                Avatar
            });
        }

        [Authorize]
        public async Task<IActionResult> List()
        {
            var UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var Role = User.FindFirstValue(ClaimTypes.Role);

            var posts = _postRepository.Posts;

            if (string.IsNullOrEmpty(Role))
            {
                posts = posts.Where(i => i.UserId == UserId);
            }

            return View(await posts.ToListAsync());
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var post = _postRepository.Posts.Include(p => p.Tags).FirstOrDefault(p => p.PostId == id);
            if (post == null)
            {
                return NotFound();
            }

            ViewBag.Tags = await _tagRepository.Tags.ToListAsync();
            return View(new PostEditViewModel
            {
                PostId = post.PostId,
                Title = post.Title,
                Description = post.Description,
                Content = post.Content,
                Url = post.Url,
                isActive = post.IsActive,
                Tags = post.Tags
            });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(PostEditViewModel model, int[] tagIds)
        {
            if (ModelState.IsValid)
            {
                var entityUpdate = new Post
                {
                    PostId = model.PostId,
                    Title = model.Title,
                    Content = model.Content,
                    Description = model.Description,
                    Url = model.Url,
                };
                if (User.FindFirstValue(ClaimTypes.Role) == "admin")
                {
                    entityUpdate.IsActive = model.isActive;
                }
                _postRepository.EditPost(entityUpdate, tagIds);
                return RedirectToAction("List");
            }
            ViewBag.Tags = await _tagRepository.Tags.ToListAsync();
            return View(model);
        }
    }
}