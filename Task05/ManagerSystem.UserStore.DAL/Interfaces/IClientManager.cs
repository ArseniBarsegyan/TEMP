using System;
using ManagerSystem.UserStore.DAL.Entities;

namespace ManagerSystem.UserStore.DAL.Interfaces
{
    public interface IClientManager : IDisposable
    {
        void Create(ClientProfile item);
    }
}