using System.Collections.Generic;
using UserStore.BLL.DTO;
using UserStore.BLL.Infrastructure;

namespace UserStore.BLL.Interfaces
{
    public interface IOrderService
    {
        IEnumerable<OrderDto> GetAllOrderList();
        OrderDto GetOrderDtoById(int id);
        OperationDetails Create(OrderDto orderDto);
        OperationDetails Edit(OrderDto orderDto);
        OperationDetails Delete(int id);
    }
}