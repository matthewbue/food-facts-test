using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnessFoods.Domain.Entities;

namespace FitnessFoods.Application.Interfaces
{
    public interface IImportHistoryRepository 
    { 
        Task SaveAsync(ImportHistory history); 
        Task<ImportHistory?> GetLastAsync(); 
    }
}
