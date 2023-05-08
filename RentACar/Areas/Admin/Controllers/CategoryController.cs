using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentACar.Business.Dtos;
using RentACar.Business.Services;
using RentACar.WebUI.Areas.Admin.Models;
using System.Data;


namespace RentACar.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult List()
        {
            var categoryDtos = _categoryService.GetCategories();

            var viewModel = categoryDtos.Select(x => new CategoryViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description

            }).ToList();

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult New()
        {
            return View("Form", new CategoryFormViewModel());
        }

        [HttpPost]
        public IActionResult Save(CategoryFormViewModel formData)
        {
            if (!ModelState.IsValid)
            {
                return View("Form", formData);
            }

            if (formData.Id == 0)
            {
                if (formData.Description is not null)
                    formData.Description = formData.Description.Trim();


                var addCategoryDto = new AddCategoryDto()
                {
                    Name = formData.Name.Trim(),
                    Description = formData.Description
                };

                var response = _categoryService.AddCategory(addCategoryDto);

                if (response.IsSucceed)
                {
                    return RedirectToAction("List");
                }
                else
                {
                    ViewBag.ErrorMessage = response.Message;
                    return View("Form", formData);

                }
            }
            else //güncelleme için
            {
                var editCategoryDto = new EditCategoryDto()
                {
                    Id = formData.Id,
                    Name = formData.Name,
                    Description = formData.Description
                };
                _categoryService.UpdateCategory(editCategoryDto);

            }

            return RedirectToAction("List");

        }

        [HttpGet]

        public IActionResult Edit(int id)
        {
            var editCategoryDto = _categoryService.GetCategory(id);

            var viewModel = new CategoryFormViewModel()
            {
                Id = editCategoryDto.Id,
                Name = editCategoryDto.Name,
                Description = editCategoryDto.Description
            };

            return View("Form", viewModel);
        }

        public IActionResult Delete(int id)
        {
            _categoryService.DeleteCategory(id);
            return RedirectToAction("List");
        }
    }
}
