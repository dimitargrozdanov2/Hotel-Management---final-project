using HotelManagement.ViewModels;
using HotelManagement.ViewModels.PublicArea;
using System.Threading.Tasks;

namespace HotelManagement.Services.Contracts
{
    public interface IFeedbackService
    {
        Task<FeedbackViewModel> AddComment(AddFeedbackViewModel model);

        Task<FeedbackViewModel> AddReply(AddFeedbackViewModel model);

        Task<string> DeleteCommentAsync(string id);
    }
}