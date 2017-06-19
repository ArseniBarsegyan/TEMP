using System;
using System.Threading.Tasks;
using Task04.DAL.EF;
using Task04.DAL.Entities;

namespace Task04.DAL.Repositories
{
    public class PurchasesUnitOfWork
    {
        private AppDbContext _context;
        private bool _disposed;

        public PurchasesUnitOfWork(string connectionString)
        {
            _context = new AppDbContext(connectionString);
            ClientRepository = new GenericRepository<Client>(_context);
            ProductRepository = new GenericRepository<Product>(_context);
            ManagerRepository = new GenericRepository<Manager>(_context);
        }

        public GenericRepository<Client> ClientRepository { get; }
        public GenericRepository<Product> ProductRepository { get; }
        public GenericRepository<Manager> ManagerRepository { get; }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                ClientRepository.Dispose();
                ProductRepository.Dispose();
                ManagerRepository.Dispose();
            }
            _disposed = true;
        }
    }
}
