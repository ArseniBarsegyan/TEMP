using System.Collections.Generic;
using UserStore.BLL.DTO;
using UserStore.BLL.Infrastructure;
using UserStore.BLL.Interfaces;
using UserStore.DAL.Repositories;

namespace UserStore.BLL.Services
{
    public class ManagerService : IManagerService
    {
        private IdentityUnitOfWork UnitOfWork { get; }

        public ManagerService(IdentityUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public IEnumerable<ManagerDto> GetAllManagersList()
        {
            var managerList = new List<ManagerDto>();

            foreach (var manager in UnitOfWork.ManagerRepository.GetAll())
            {
                var managerDto = new ManagerDto
                {
                    Id = manager.Id,
                    LastName = manager.LastName
                };
                managerList.Add(managerDto);
            }
            return managerList;
        }

        public ManagerDto GetManagerById(int id)
        {
            var manager = UnitOfWork.ManagerRepository.GetById(id);
            var managerDto = new ManagerDto
            {
                Id = manager.Id,
                LastName = manager.LastName
            };
            return managerDto;
        }

        public OperationDetails Create(ManagerDto managerDto)
        {
            throw new System.NotImplementedException();
        }

        public OperationDetails Edit(ManagerDto managerDto)
        {
            throw new System.NotImplementedException();
        }

        public OperationDetails Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}