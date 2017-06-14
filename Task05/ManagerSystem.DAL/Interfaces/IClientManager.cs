using System;
using ManagerSystem.DAL.Entities;

namespace ManagerSystem.DAL.Interfaces
{
    public interface IClientManager : IDisposable
    {
        void Create(ClientProfile item);
    }
}