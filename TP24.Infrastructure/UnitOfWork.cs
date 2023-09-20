using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TP24.Domain.Entities;
using TP24.Domain.Interfaces;
using TP24.Infrastructure.Repositories;

namespace TP24.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EFContext _dbContext;

        public UnitOfWork(EFContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IAsyncRepository<T> AsyncRepository<T>() where T : BaseEntity
        {
            return new RepositoryBase<T>(_dbContext);
        }

        public Task<int> SaveChangesAsync()
        {
            return _dbContext.SaveChangesAsync();
        }
    }
}
