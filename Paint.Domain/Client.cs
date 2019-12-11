using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.Domain
{
    public partial class Client
    {
        public Client()
        {
            this.Jobs = new List<Job>();
            this.PhoneNumbers = new List<PhoneNumber>();
            this.Clients = new List<Client>();
        }

        [Key]
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public int ClientTypeId { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(50)]
        public string CompanyName { get; set; }

        [StringLength(50)]
        public Address BillingAddress { get; set; }
        public List<PhoneNumber> PhoneNumbers { get; set; }

        public List<Job> Jobs { get; set; }

        [Column(TypeName = "ntext")]
        public string Notes { get; set; }
        [Required]
        public bool? Active { get; set; }

        public virtual ClientType ClientType { get; set; }
        public virtual Client Parent { get; set; }

        public List<Client> Clients { get; set; }
    }

}
