﻿using HotelManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Services.Contracts
{
    public interface ICategoryService
    {
        Task<ICollection<string>> GetAllCategoryNamesAsync();
    }
}
