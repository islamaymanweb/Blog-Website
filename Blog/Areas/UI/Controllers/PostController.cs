using Blog.Interfaces;
using Blog.Models.Comments;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Areas.UI.Controllers
{
    [Area("UI")]
    public class PostController : Controller
    {
        private readonly IRepository _repo;

        public PostController(IRepository repo)
        {
            _repo = repo;
        }
        public IActionResult Index()
        {
            var post = _repo.GetAllPosts().ToList();
            return View(post);
        }

        public IActionResult Detail(int id)
        {
            return View(_repo.GetPost(id));
        }

        [HttpPost]
        public async Task<IActionResult> Comment(CommentViewModel vm)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Post", new { id = vm.PostId });
            var post = _repo.GetPost(vm.PostId);

            if (vm.MainCommentId == 0)
            {
                post.MainComments = post.MainComments ?? new List<MainComment>();

                post.MainComments.Add(new MainComment
                {
                    Message = vm.Message,
                    Created = DateTime.Now
                });
                _repo.UpdatePost(post);
            }
            else 
            {
                var comment = new SubComment
                {
                    MainCommentId = vm.MainCommentId,
                    Message = vm.Message,
                    Created = DateTime.Now,
                };
                _repo.AddSubComment(comment);
            }
            await _repo.SaveChangeAsync();
            return RedirectToAction("Detail", new { id = vm.PostId });
        }
    }
}
