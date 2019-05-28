using HotelManagement.DataModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Services.Contracts
{
    public interface IBusinessService
    {
        Task<Business> GetBusinessByNameAsync(string name);

        Task<ICollection<Business>> GetBusinesses(string key, string location = null);

    }
}
