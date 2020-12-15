using PG.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace PG.Models
{
    public class StoichkovRoom : Entity
    {
        public int RoomNumber { get; set; }

        public string type { get; set; }

        public int HotelId { get; set; }

        public StoichkovHotel StoichkovHotel { get; set; }
    }
}
