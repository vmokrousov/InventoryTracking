using System;
using System.Threading.Tasks;

namespace InventoryTracking.Persistence
{
    /// <summary>
    /// Interface for unit of work component that manages the scope of changes.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Commits all made changes asynchronically.
        /// </summary>
        Task<int> CommitAsync();
    }
}
