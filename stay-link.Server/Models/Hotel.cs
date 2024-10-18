using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore.Metadata;

namespace stay_link.Server.Models
{
    public class Hotel
    {
        public int ID { get; set; }
        
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, ErrorMessage = "Title can't be longer than 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(50, ErrorMessage = "Address can't be shorter than 10 characters.", MinimumLength = 10)]
        public string Address { get; set; }

        [Required(ErrorMessage = "Image location is required.")]
        public string ImageUrl { get; set; }

        public Hotel(string name, string location, string imgUrl)
        {
            Name = name;
            ImageUrl = imgUrl;
            Address = location;
        }

        public Hotel() { }


    }




}
