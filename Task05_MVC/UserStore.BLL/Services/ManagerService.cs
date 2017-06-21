using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UserStore.BLL.DTO;
using UserStore.BLL.Infrastructure;
using UserStore.BLL.Interfaces;
using UserStore.DAL.Entities;
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
            UnitOfWork.ManagerRepository.Create(new Manager{LastName = managerDto.LastName});
            UnitOfWork.Save();

            return new OperationDetails(true, "manager create successful", "");
        }

        public OperationDetails Edit(ManagerDto managerDto)
        {
            var manager = UnitOfWork.ManagerRepository.GetById(managerDto.Id);
            manager.LastName = managerDto.LastName;
            UnitOfWork.ManagerRepository.Update(manager);
            UnitOfWork.Save();

            return new OperationDetails(true, "manager update successful", "");
        }

        public OperationDetails Delete(int id)
        {
            var product = UnitOfWork.ProductRepository.GetAll()
                .Include(x => x.Manager).Include(x => x.Client)
                .FirstOrDefault(x => x.Manager.Id == id);
            
            product.Manager = null;

            UnitOfWork.ManagerRepository.Delete(id);
            UnitOfWork.Save();

            return new OperationDetails(true, "manager delete successful", "");
        }
    }
}