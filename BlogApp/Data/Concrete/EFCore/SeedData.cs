using BlogApp.Data.Concrete.EfCore;
using BlogApp.Entity;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Data.Concrete.EFCore
{
    public static class SeedData
    {
        public static void TestVerileriniDoldur(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<BlogContext>();

                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }

                if (!context.Tags.Any())
                {
                    context.Tags.AddRange(
                        new Entity.Tag { Text = "Web programlama", Url = "web-programlama", Color = TagColors.success },
                        new Entity.Tag { Text = "Backend", Url = "backend", Color = TagColors.danger },
                        new Entity.Tag { Text = "Frontend", Url = "frontend", Color = TagColors.warning },
                        new Entity.Tag { Text = "NodeJS", Url = "nodejs", Color = TagColors.primary },
                        new Entity.Tag { Text = "PHP", Url = "php", Color = TagColors.secondary }
                    );
                    context.SaveChanges();
                }

                if (!context.Users.Any())
                {
                    context.Users.AddRange(
                        new Entity.User { UserName = "tugbaduman", Name = "Tuğba", Email = "info@tugba.com", Password = "root", Image = "p1.jpg" },
                        new Entity.User { UserName = "enesduman", Name = "Enes", Email = "info@enes.com", Password = "root", Image = "p2.jpg" }
                    );
                    context.SaveChanges();
                }

                if (!context.Posts.Any())
                {
                    var tags = context.Tags.Take(4).ToList();

                    context.Posts.AddRange(
                        new Entity.Post
                        {
                            Title = "ASP.NET Core",
                            Url = "asp-net-core",
                            Content = "ASP.NET Core dersleri",
                            Description = "ASP.NET ASP.NET ASP.NET ASP.NET ASP.NET ASP.NET ASP.NET ASP.NET ASP.NET ASP.NET ASP.NET ASP.NET ASP.NET ASP.NET ASP.NET ASP.NET Core dersleri",
                            IsActive = true,
                            PublishedOn = DateTime.Now.AddDays(-10),
                            Tags = tags.Take(3).ToList(),
                            Image = "asp.jpg",
                            UserId = 1
                        },
                        new Entity.Post
                        {
                            Title = "NodeJS",
                            Url = "nodejs",
                            Content = "NodeJS dersleri",
                            Description = "NodeJS dersleriNodeJS dersleriNodeJS dersleriNodeJS dersleriNodeJS dersleriNodeJS dersleriNodeJS dersleriNodeJS dersleriNodeJS dersleriNodeJS dersleriNodeJS dersleriNodeJS dersleriNodeJS dersleriNodeJS dersleriNodeJS dersleriNodeJS dersleri",
                            IsActive = true,
                            PublishedOn = DateTime.Now.AddDays(-20),
                            Tags = tags.Take(2).ToList(),
                            Image = "nodejs.jpg",
                            UserId = 1,
                            Comments = new List<Comment>{
                                new Comment{ Text="Iyi bir kurs", PublishedOn=DateTime.Now.AddHours(-15), UserId=1},
                                new Comment{ Text="Cok faydali bir kurs", PublishedOn=DateTime.Now, UserId=2 }
                                }
                        },
                        new Entity.Post
                        {
                            Title = "Django",
                            Url = "django",
                            Content = "Django dersleri",
                            Description = "Django dersleriDjango dersleriDjango dersleriDjango dersleriDjango dersleriDjango dersleriDjango dersleriDjango dersleriDjango dersleriDjango dersleriDjango dersleriDjango dersleriDjango dersleriDjango dersleriDjango dersleriDjango dersleri",
                            IsActive = true,
                            PublishedOn = DateTime.Now.AddDays(-5),
                            Tags = tags.Take(1).ToList(),
                            Image = "django.jpg",
                            UserId = 2,
                            Comments = new List<Comment>{
                                new Comment{ Text="Iyi bir kurs", PublishedOn=DateTime.Now.AddHours(-15), UserId=1},
                                new Comment{ Text="Cok faydali bir kurs", PublishedOn=DateTime.Now, UserId=2 }
                                }
                        },
                        new Entity.Post
                        {
                            Title = "React",
                            Url = "react",
                            Content = "React dersleri",
                            Description = "React dersleriReact dersleriReact dersleriReact dersleriReact dersleriReact dersleriReact dersleriReact dersleriReact dersleriReact dersleriReact dersleriReact dersleriReact dersleriReact dersleriReact dersleriReact dersleri",
                            IsActive = true,
                            PublishedOn = DateTime.Now.AddDays(-3),
                            Tags = tags.Take(2).ToList(),
                            Image = "react.png",
                            UserId = 1,
                            Comments = new List<Comment>{
                                new Comment{ Text="Iyi bir kurs", PublishedOn=DateTime.Now.AddHours(-15), UserId=1},
                                new Comment{ Text="Cok faydali bir kurs", PublishedOn=DateTime.Now, UserId=2 }
                                }
                        },
                        new Entity.Post
                        {
                            Title = "PHP",
                            Url = "php",
                            Content = "PHP dersleri",
                            Description = "PHP dersleriPHP dersleriPHP dersleriPHP dersleriPHP dersleriPHP dersleriPHP dersleriPHP dersleriPHP dersleriPHP dersleriPHP dersleriPHP dersleriPHP dersleriPHP dersleriPHP dersleriPHP dersleri",
                            IsActive = true,
                            PublishedOn = DateTime.Now.AddDays(-30),
                            Tags = tags.Take(4).ToList(),
                            Image = "php.jpg",
                            UserId = 2,
                            Comments = new List<Comment>{
                                new Comment{ Text="Iyi bir kurs", PublishedOn=DateTime.Now.AddHours(-15), UserId=1},
                                new Comment{ Text="Cok faydali bir kurs", PublishedOn=DateTime.Now, UserId=2 }
                                }
                        }
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}
