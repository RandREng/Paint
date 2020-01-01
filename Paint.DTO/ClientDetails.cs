using Paint.Domain;
using RandREng.Paging;
using System.Collections.Generic;
using System.Text;

namespace Paint.DTO
{
	public class ClientDetails : ClientItem
    {
        public ClientDetails()
        {
            this.PhoneNumbers = new List<PhoneNumber>();
            this.Clients = new List<ClientItem>();
        }

        public string BillingAddress { get; set; }
        public string Notes { get; set; }
        public bool? Active { get; set; }

        public List<PhoneNumber> PhoneNumbers { get; set; }

        public List<ClientItem> Clients { get; set; }
    }

}
