using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public int SquareFootage { get; set; }
        public DateTime  Date { get; set; }
        public int Year { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal RenovationBudget { get; set; }
        public virtual PaintList PaintList { get; set; }
        public int PropertyId { get; set; }

        public Client Client { get; set; }
        public BidSheet BidSheet { get; set; }

        public List<Room> Rooms { get; set; } = new List<Room>();

    }
}
