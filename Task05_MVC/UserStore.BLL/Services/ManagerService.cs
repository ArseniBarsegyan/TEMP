using System.Collections.Generic;
using UserStore.BLL.DTO;
using UserStore.BLL.Infrastructure;
using UserStore.BLL.Interfaces;
using UserStore.DAL.Entities;
using UserStore.DAL.Repositories;

namespace UserStore.BLL.Services
{
    public class ManagerService : IManagerService
    {
        private UnitOfWork UnitOfWork { get; }

        public ManagerService(UnitOfWork unitOfWork)
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
            UnitOfWork.ManagerRepository.Create(new Manager{LastName = managerDto.LastName});
            UnitOfWork.Save();

            return new OperationDetails(true, "manager create successful", "");
        }

        public OperationDetails Edit(ManagerDto managerDto)
        {
            return new OperationDetails(true, "manager update successful", "");
        }

        public OperationDetails Delete(int id)
        {
            return new OperationDetails(true, "manager delete successful", "");
        }
    }
}