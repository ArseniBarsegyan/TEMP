using System.Collections.Generic;
using UserStore.BLL.DTO;
using UserStore.BLL.Infrastructure;

namespace UserStore.BLL.Interfaces
{
    public interface ISalesService
    {
        IEnumerable<PurchaseDto> GetAllSalesList();
        OperationDetails Create(PurchaseDto saleDto);
        OperationDetails Edit(PurchaseDto saleDto);
        OperationDetails Delete(PurchaseDto saleDto);
    }
}