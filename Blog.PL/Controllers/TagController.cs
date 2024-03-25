using AutoMapper;
using Blog.BLL.Interfaces;
using Blog.BLL.Repositories;
using Blog.DAL.Entities;
using Blog.PL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.PL.Controllers
{
    public class TagController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public TagController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public IActionResult Index()
        {

            var tags = unitOfWork.TagRepository.GetAll();
            var tagsView = mapper.Map<IEnumerable<TagViewModel>>(tags);
            return View(tagsView);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View(new TagViewModel());
        }
        [HttpPost]
        public IActionResult Create(TagViewModel tagView)
        {
            if (ModelState.IsValid)
            {
                try
                {
                var tag = mapper.Map<Tag>(tagView);
                unitOfWork.TagRepository.Add(tag);
                unitOfWork.Complete();
                
                return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "This Name Already Taken");
                }

            }
            return View(tagView);
        }
        [HttpGet]
        public IActionResult Update(int? id)
        {
            try
            {
                if (id is null)
                {
                    return BadRequest();
                }
                var Tag = unitOfWork.TagRepository.GetById(id);
                var tagsView = mapper.Map<TagViewModel>(Tag);

                if (Tag == null)
                {
                    return NotFound();
                }
                return View(tagsView);

            }
            catch (Exception ex)
            {
               
                return RedirectToAction("Error", "Home");


            }
        }
        [HttpPost]
        public IActionResult Update(int? id, TagViewModel tagView)
        {
            if (ModelState.IsValid)
            {
                try
                {
                var tag = mapper.Map<Tag>(tagView);
                unitOfWork.TagRepository.Update(tag);
                unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "This Name Already Taken");
                }

            }
            return View(tagView);
        }

        public IActionResult Delete(int? id)
        {
            try
            {
                if (id is null)
                {
                    return BadRequest();
                }
                var tag = unitOfWork.TagRepository.GetById(id);
                //var departmentViewModel = mapper.Map<DepartmentViewModel>(department);
                if (tag == null)
                {
                    return NotFound();
                }
                unitOfWork.TagRepository.Delete(tag);
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
