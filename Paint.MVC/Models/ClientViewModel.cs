using Paint.Domain;
using RandREng.Paging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Paint.MVC.Models
{
    public class ClientViewModel
    {
        public ClientViewModel()
        {
            this.PhoneNumbers = new List<PhoneNumber>();
            this.Clients = new List<Client>();
        }

        public int Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        public string BillingAddress { get; set; }

        [Display(Name = "Phone Numbers")]
        public List<PhoneNumber> PhoneNumbers { get; set; }

        public string Notes { get; set; }

        [Required]
        public bool? Active { get; set; }

        [Display(Name = "Type")]
        public string ClientType { get; set; }

        public List<Client> Clients { get; set; }
        public PagedResult<Job> Jobs { get; set; }

    }
}
