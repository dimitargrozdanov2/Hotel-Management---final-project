using System.ComponentModel.DataAnnotations;

namespace HotelManagement.ViewModels.PublicArea
{
    public class AddFeedbackViewModel
    {
        public string BusinessId { get; set; }

        public string FeedbackParentId { get; set; }

        [Required]
        public string AuthorName { get; set; }

        public string Comment { get; set; }

        public string Email { get; set; }

        public double Rating { get; set; }
    }
}