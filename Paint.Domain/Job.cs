using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.Domain
{
    public class Job
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public Address Address { get; set; }
        public virtual PaintList PaintList { get; set; }

        public Client Client { get; set; }

        public List<Room> Rooms { get; set; } = new List<Room>();

    }
}
