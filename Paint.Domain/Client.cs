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

        [Display(Name = "Parent")]
        public int? ParentId { get; set; }
        [Display(Name = "Type")]
        public int ClientTypeId { get; set; }

        [StringLength(50)]
        [Display(Name = "Firrst Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(50)]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [StringLength(50)]
        public Address BillingAddress { get; set; }
        [Display(Name = "Phone Numbers")]
        public List<PhoneNumber> PhoneNumbers { get; set; }

        public List<Job> Jobs { get; set; }

        [Column(TypeName = "ntext")]
        public string Notes { get; set; }
        [Required]
        public bool? Active { get; set; }

        [Display(Name = "Type")]
        public virtual ClientType ClientType { get; set; }
        public virtual Client Parent { get; set; }

        public List<Client> Clients { get; set; }

        [NotMapped]
        public string Name
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                if (this.Parent != null)
                {
                    sb.Append(Parent.Name);
                    sb.Append(" - ");
                }

                if (!string.IsNullOrWhiteSpace(this.CompanyName))
                {
                    sb.Append(this.CompanyName.Trim());
                    if (!string.IsNullOrWhiteSpace(this.FirstName) || !string.IsNullOrWhiteSpace(this.LastName))
                    {
                        sb.Append(" - ");
                    }
                }

                if (!string.IsNullOrWhiteSpace(this.FirstName))
                {
                    sb.Append(this.FirstName.Trim());
                    if (!string.IsNullOrWhiteSpace(this.LastName))
                    {
                        sb.Append(" ");
                    }
                }
                if (!string.IsNullOrWhiteSpace(this.LastName))
                {
                    sb.Append(this.LastName.Trim());
                }
                return sb.ToString();
            }
        }
    }

}
