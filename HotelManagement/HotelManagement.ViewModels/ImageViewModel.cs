using System;
using System.ComponentModel.DataAnnotations;

namespace HotelManagement.ViewModels
{
    public class ImageViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime? CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public BusinessViewModel Business { get; set; }
    }
}