using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP24.Domain.Entities;

namespace TP24.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();

        IAsyncRepository<T> AsyncRepository<T>() where T : BaseEntity;
    }
}
