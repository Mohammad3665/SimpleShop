using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace SimpleShop.Application.Common.Interfaces
{
    public interface ITransactionService
    {
        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken =  default);
    }
}
