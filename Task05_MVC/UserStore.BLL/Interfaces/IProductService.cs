using System.Collections.Generic;
using UserStore.BLL.DTO;
using UserStore.BLL.Infrastructure;

namespace UserStore.BLL.Interfaces
{
    public interface IProductService
    {
        IEnumerable<ProductDto> GetAllProductsList();
        ProductDto GetProductById(int id);
        OperationDetails Create(ProductDto productDto);
        OperationDetails Edit(ProductDto productDto);
        OperationDetails Delete(int id);
    }
}