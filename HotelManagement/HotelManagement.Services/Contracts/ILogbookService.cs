using HotelManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Services.Contracts
{
    public interface ILogbookService
    {
        Task<IEnumerable<LogbookViewModel>> GetLogBooksForBusiness(string name);

        Task<LogbookViewModel> CreateLogbookAsync(string businessname, string name, string description);

        Task<LogbookViewModel> ManageManagerAsync(string logbookName, string managerEmail);
    }
}
