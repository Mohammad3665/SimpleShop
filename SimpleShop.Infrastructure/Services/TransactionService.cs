using Microsoft.EntityFrameworkCore.Storage;
using SimpleShop.Application.Common.Interfaces;
using SimpleShop.Infrastructure.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShop.Infrastructure.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly SimpleShopDbContext _dbContext;
        public TransactionService(SimpleShopDbContext dbContext) => _dbContext = dbContext;
        public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            return _dbContext.Database.BeginTransactionAsync(cancellationToken);
        }
    }
}
