using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace InventoryTracking.Persistence
{
    /// <summary>
    /// Implementation of the Unit of work pattern based on Entity Framework
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        private bool _disposed;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="context"></param>
        public UnitOfWork(DbContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// Asynchronously commits db context changes
        /// </summary>
        /// <returns></returns>
        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Disposes unit of work
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Perform dispose of unit of work depending on disposing flag.
        /// </summary>
        /// <param name="disposing"></param>
        public virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }
            _context.Dispose();
            _disposed = true;
        }
    }
}
