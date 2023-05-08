using RentACar.Business.Dtos;
using RentACar.Business.Services;
using RentACar.Business.Types;
using RentACar.Data.Entities;
using RentACar.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Business.Manager
{
    public class ProductManager : IProductService
    {
        private readonly IRepository<ProductEntity> _productRepository;
        private readonly ICategoryService _categoryService;
        public ProductManager(IRepository<ProductEntity> productRepository, ICategoryService categoryService)
        {
            _productRepository = productRepository;
            _categoryService = categoryService;
        }

        public ServiceMessage AddProduct(AddProductDto addProductDto)
        {
            var hasProduct = _productRepository.GetAll(x => x.Name.ToLower() == addProductDto.Name.ToLower()).ToList();

            if (hasProduct.Any())
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Bu isimde bir ürün zaten mevcut."
                };
            }

            var productEntity = new ProductEntity()
            {
                Name = addProductDto.Name,
                Description = addProductDto.Description,
                UnitInStock = addProductDto.UnitInStock,
                UnitPrice = addProductDto.UnitPrice,
                CategoryId = addProductDto.CategoryId,
                ImagePath = addProductDto.ImagePath,

            };

            _productRepository.Add(productEntity);

            return new ServiceMessage
            {
                IsSucceed = true,
                Message = "Yeni ürün eklendi"
            };
        }

        public void DeleteProduct(int id)
        {
            _productRepository.Delete(id);
        }

        public EditProductDto GetProductById(int id)
        {
            var productEntity = _productRepository.GetById(id);

            var editProductDto = new EditProductDto()
            {
                Id = productEntity.Id,
                Name = productEntity.Name,
                Description = productEntity.Description,
                UnitInStock = productEntity.UnitInStock,
                UnitPrice = productEntity.UnitPrice,
                ImagePath = productEntity.ImagePath,
                CategoryId = productEntity.CategoryId
            };

            return editProductDto;
        }

        public DetailProductDto GetProductDetail(int id)
        {
            var productEntity = _productRepository.GetById(id);

            var productDto = new DetailProductDto()
            {
                Id = productEntity.Id,
                Name = productEntity.Name,
                Description = productEntity.Description,
                UnitPrice = productEntity.UnitPrice,
                ImagePath = productEntity.ImagePath,
                CategoryId = productEntity.CategoryId,
                UnitInStock = productEntity.UnitInStock,
            };

            productDto.CategoryName = _categoryService.GetCategoryName(productDto.CategoryId);

            return productDto;
        }

        public List<ProductDto> GetProducts()
        {
            var productEntities = _productRepository.GetAll().OrderBy(x => x.Category.Name).ThenBy(x => x.Name); // Önce kategori adına, sonra ürün adına göre sıralıyorum.



            var productDtoList = productEntities.Select(x => new ProductDto()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                UnitInStock = x.UnitInStock,
                UnitPrice = x.UnitPrice,
                CategoryId = x.CategoryId,
                CategoryName = x.Category.Name,
                ImagePath = x.ImagePath
            }).ToList();

            return productDtoList;
        }

        public List<ProductDto> GetProductsByCategoryId(int? categoryId = null)
        {
            if (categoryId.HasValue) 
            {
                var productEntities = _productRepository.GetAll(x => x.CategoryId == categoryId).OrderBy(x => x.Name);
               


                var productDto = productEntities.Select(x => new ProductDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    UnitInStock = x.UnitInStock,
                    UnitPrice = x.UnitPrice,
                    CategoryId = x.CategoryId,
                    CategoryName = x.Category.Name,
                    ImagePath = x.ImagePath
                }).ToList();

                return productDto;

            }
            else
            {
                return GetProducts();
            }
        }

        public void UpdateProduct(EditProductDto editProductDto)
        {
            var productEntity = _productRepository.GetById(editProductDto.Id);

            productEntity.Name = editProductDto.Name;
            productEntity.Description = editProductDto.Description;
            productEntity.UnitInStock = editProductDto.UnitInStock;
            productEntity.UnitPrice = editProductDto.UnitPrice;
            productEntity.CategoryId = editProductDto.CategoryId;

            if (editProductDto.ImagePath != null)
            {
                productEntity.ImagePath = editProductDto.ImagePath;
            }


            _productRepository.Update(productEntity);
        }
    }
}
