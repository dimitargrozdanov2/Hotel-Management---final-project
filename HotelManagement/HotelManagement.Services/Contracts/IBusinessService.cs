﻿using HotelManagement.DataModels;
using HotelManagement.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Services.Contracts
{
    public interface IBusinessService
    {
        Task<BusinessViewModel> GetBusinessByNameAsync(string name);

        Task<ICollection<BusinessViewModel>> GetBusinesses(string key, string location = null);

        Task<BusinessViewModel> CreateBusinessAsync(string name, string location, string description);

        Task<BusinessViewModel> AddImageToBusiness(string name, string imageUrl, IFormFile Image);

    }
}
