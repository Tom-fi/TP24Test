using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TP24.Domain.Entities;

namespace TP24.Infrastructure.Repositories
{
    public class InvoiceRepository : RepositoryBase<Invoice>
    {
        public InvoiceRepository(EFContext dbContext) : base(dbContext)
        {

        }
    }
}
