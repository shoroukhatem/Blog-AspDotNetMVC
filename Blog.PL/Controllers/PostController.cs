using AutoMapper;
using Blog.BLL.Interfaces;
using Blog.BLL.Repositories;
using Blog.DAL.Entities;
using Blog.PL.Helper;
using Blog.PL.Models;
using Blog.PL.Models.PostViewModels;
using Hangfire;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using System.Reflection.Metadata;
using System.Security.Cryptography;

namespace Blog.PL.Controllers
{
    public class PostController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;

        public PostController(IUnitOfWork unitOfWork,UserManager<ApplicationUser>userManager,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
            this.mapper = mapper;
        }
        public IActionResult Index()
        {
            var userId = userManager.GetUserId(HttpContext.User);
            if (userId==null)
            {
                RedirectToAction("Account", "Login");
            }
            else
            {
                RecurringJob.AddOrUpdate(() => unitOfWork.DeletedPostRepository.DeleteDeletedPosts(), Cron.Monthly);
                var posts = unitOfWork.PostRepository.getUserPosts(userId);
                var pinnedPosts = unitOfWork.PostRepository.getPinnedPosts();
                var pinnedPostsViewModel = mapper.Map<IEnumerable<PostTagViewModel>>(pinnedPosts);
                var postsViewModel = mapper.Map<IEnumerable<PostTagViewModel>>(posts);
                var AllPosts = postsViewModel.Concat(pinnedPostsViewModel).Distinct();
                
                return View(AllPosts);
            }
            return View();
           
        }
        public IActionResult Create()
        {
            var Tags =unitOfWork.TagRepository.GetAll();
           
            var selectList = new List<SelectListItem>();
            foreach (var tag in Tags)
            {
                selectList.Add(new SelectListItem(tag.Name,tag.Id.ToString()));
            }
            var posts = new PostCreateViewModel()
            {
                Tags = selectList

            };
            return View(posts);
        }
        [HttpPost]
        public IActionResult Create(PostCreateViewModel postViewModel)
        {
            if(ModelState.IsValid)
            {
                var post = mapper.Map<Post>(postViewModel);
                post.imageURL = DocumentSettings.UploadFile(postViewModel.Image,"Images");
                post.UserId = userManager.GetUserId(HttpContext.User);
               
                foreach(var item in postViewModel.SelectedTags)
                {
                    post.PostTags.Add(new PostTag()
                    {
                        TagId = item
                    });
                }
                unitOfWork.PostRepository.Add(post);
                unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            return View(postViewModel);
        }
        public IActionResult SinglePost(int? id)
        {
            var post= unitOfWork.PostRepository.GetById(id);
            var postViewModel = mapper.Map<PostTagViewModel>(post);
            return View(postViewModel);
        }
        public IActionResult Update(int? id) {
            try
            {
                if (id is null)
                {
                    return BadRequest();
                }
                var posts = unitOfWork.PostRepository.GetByID(id);
                var tags = unitOfWork.TagRepository.GetAll();
                var selectTags = posts.PostTags.Select(x => new Tag()
                {
                    Id = x.TagId,
                    Name = x.tag.Name
                });
               var selectList = new List<SelectListItem>();
                foreach (var item in tags)
                {
                    selectList.Add(new SelectListItem(item.Name, item.Id.ToString(), selectTags.Select(x => x.Id).Contains(item.Id)));
                }
                var postVM = new PostViewModel()
                {
                    Id = posts.Id,
                    Title = posts.Title,
                    Body = posts.Body,
                    Tags = selectList,
                    Pinned = posts.Pinned,
                    imageURL = posts.imageURL
               
                };
                return View(postVM);

            }
            catch (Exception ex)
            {

                return RedirectToAction("Error", "Home");


            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(PostViewModel postVM)
        {
            try
            {
               var post = unitOfWork.PostRepository.GetByID(postVM.Id);
                // var newPost = mapper.Map<Post>(postVM);
                post.Pinned =postVM.Pinned;
               post.Body = postVM.Body;
                post.Title  = postVM.Title;
             
                var selectedTags = postVM.SelectedTags;
                var existingTags = post.PostTags.Select(x => x.TagId).ToList();
                if(selectedTags == null){
                    selectedTags = new int[0] ;
                }
                var toAdd = selectedTags.Except(existingTags).ToList();
                var toRemove = existingTags.Except(selectedTags).ToList();
                post.PostTags = post.PostTags.Where(x=>!toRemove.Contains(x.TagId)).ToList();
                foreach (var item in toAdd)
                {
                    post.PostTags.Add(new PostTag()
                    {
                        TagId= item,
                       PostId=post.Id
                    });
                }
               if (postVM.Image != null)
                {
                    DocumentSettings.DeleteFile(post.imageURL);
                    post.imageURL = DocumentSettings.UploadFile(postVM.Image, "Images");
                   
                }
                unitOfWork.PostRepository.Update(post);
                unitOfWork.Complete();
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {

                return RedirectToAction("Error", "Home");


            }
            return View(postVM);
        }
        public IActionResult SoftDelete(int? id)
        {
            try
            {
                if (id is null)
                {
                    return BadRequest();
                }
                var post = unitOfWork.PostRepository.GetByID(id);

                if (post == null)
                {
                    return NotFound();
                }
               /* if (post.ImageUrl != null)
                {
                    DocumentSettings.DeleteFile(employee.ImageUrl);
                }*/
                var DeletedPost = new DeletedPost() { 
                Title = post.Title,
                Body = post.Body,
                Pinned = post.Pinned,
                PostTags = post.PostTags,
                imageURL = post.imageURL,
                UserId = post.UserId
                };
    
                unitOfWork.DeletedPostRepository.Add(DeletedPost);
                unitOfWork.PostRepository.Delete(post);
                unitOfWork.Complete();
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
  
                return RedirectToAction("Error", "Home");


            }
        }
        public IActionResult DeletedPosts()
        {
            var userId = userManager.GetUserId(HttpContext.User);
            if (userId == null)
            {
                RedirectToAction("Account", "Login");
            }
            else
            {
                var posts = unitOfWork.DeletedPostRepository.getUserPosts(userId);
                var postsViewModel = mapper.Map<IEnumerable<PostTagViewModel>>(posts);
                return View(postsViewModel);
            }
            return View();

        }
        public IActionResult Restore(int? id)
        {
            try
            {
                if (id is null)
                {
                    return BadRequest();
                }
                var post = unitOfWork.DeletedPostRepository.GetByID(id);

                if (post == null)
                {
                    return NotFound();
                }
                
                var RestoredPost = new Post()
                {
                    Title = post.Title,
                    Body = post.Body,
                    Pinned = post.Pinned,
                    PostTags = post.PostTags,
                    imageURL = post.imageURL,
                    UserId = post.UserId
                };
                unitOfWork.DeletedPostRepository.Delete(post);
                unitOfWork.PostRepository.Add(RestoredPost);
                unitOfWork.Complete();
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {

                return RedirectToAction("Error", "Home");


            }

        }
       
    }
}
