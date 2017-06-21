using System.Collections.Generic;
using UserStore.BLL.DTO;
using UserStore.BLL.Infrastructure;

namespace UserStore.BLL.Interfaces
{
    public interface IManagerService
    {
        IEnumerable<ManagerDto> GetAllManagersList();
        ManagerDto GetManagerById(int id);
        OperationDetails Create(ManagerDto managerDto);
        OperationDetails Edit(ManagerDto managerDto);
        OperationDetails Delete(int id);
    }
}