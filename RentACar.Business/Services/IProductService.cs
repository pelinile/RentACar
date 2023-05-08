using RentACar.Business.Dtos;
using RentACar.Business.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Business.Services
{
    public interface IProductService
    {
        ServiceMessage AddProduct(AddProductDto addProductDto);

        List<ProductDto> GetProducts();

        EditProductDto GetProductById(int id);

        void UpdateProduct(EditProductDto editProductDto);

        void DeleteProduct(int id);

        List<ProductDto> GetProductsByCategoryId(int? categoryId = null);

        DetailProductDto GetProductDetail(int id);

    }
}
