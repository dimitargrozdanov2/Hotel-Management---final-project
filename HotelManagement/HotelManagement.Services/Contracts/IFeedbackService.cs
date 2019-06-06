using HotelManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Services.Contracts
{
    public interface IFeedbackService
    {
        Task<FeedbackViewModel> AddComment(string businessId, string authorName, string email, string comment/*, string rating*/);
    }
}
