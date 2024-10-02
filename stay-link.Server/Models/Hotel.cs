using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore.Metadata;

namespace stay_link.Server.Models
{
    public class Hotel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ImageUrl { get; set; }


        public List<int> RoomIds { get; set; } = new List<int>();

        public Hotel(string name, string location, List<int> rooms, string imgUrl)
        {
            Name = name;
            ImageUrl = imgUrl;
            Address = location;
            RoomIds = rooms;
        }

        public Hotel() { }


    }




}
