// In details page add comment with AJAX
$(document).ready(function () {
    $("#btnYorumKayit").click(function (e) {
        e.preventDefault()
        $("#btnYorumKayit").text("Gönderiliyor")
        $("#btnYorumKayit").prop('disabled', true)
        $("#Text").prop('disabled', true)
            $.ajax({
                type: "POST",
                url: "/Post/AddComment",
                dataType: "json",
                data: {
                    PostId: $("#PostId").val(),
                    Text: $("#Text").val(),
                },
                success: function (comment) {
                    $("#btnYorumKayit").prop('disabled', false)
                    $("#btnYorumKayit").text("Gönder")
                    $("#Text").prop('disabled', false)
                    $("#comments").append(`
                            <div class="my-4 d-flex">
                                <img src="/img/${comment.avatar}" class="avatar rounded-circle float-start me-3">
                                <div>
                                    <div class="mb-2 d-flex">
                                        <h6 class="me-2">${comment.userName}</h6>
                                        <small>imdi</small>
                                    </div>
                                    <p>${comment.text}</p>
                                </div>
                            </div>
                        `);
                    $("#UserName").val('')
                    $("#Text").val('')
                    var adet = parseInt($("#CommentCount").text())
                    $("#CommentCount").text(adet + 1)
                }
            });
        return false;
    });
});