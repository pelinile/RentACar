﻿using Microsoft.AspNetCore.Mvc;
using RentACar.Business.Services;
using RentACar.WebUI.Models;

namespace RentACar.WebUI.ViewComponents
{
    public class ProductsViewComponent : ViewComponent
    {
        private readonly IProductService _productService;

        public ProductsViewComponent(IProductService productService)
        {
            _productService = productService;
        }

        public IViewComponentResult Invoke(int? categoryId = null)
        {
            var productDtos = _productService.GetProductsByCategoryId(categoryId);

            var viewModel = productDtos.Select(x => new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                UnitInStock = x.UnitInStock,
                UnitPrice = x.UnitPrice,
                CategoryName = x.CategoryName,
                ImagePath = x.ImagePath,

            }).ToList();

            return View(viewModel);
        }
    }
}
