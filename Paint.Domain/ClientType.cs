using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Paint.Domain
{
    public partial class ClientType
    {
        public ClientType()
        {
            this.Clients = new HashSet<Client>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(50)]
        public string ImageName { get; set; }
        [Column(TypeName = "image")]
        public byte[] Logo { get; set; }

        public virtual ICollection<Client> Clients { get; set; }
    }

}
