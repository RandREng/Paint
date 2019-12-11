using Microsoft.EntityFrameworkCore;
using RandREng.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.Domain
{
	[Owned]
	public partial class Address
	{
		[StringLength(255)]
		public string Line1 { get; set; }
		[StringLength(255)]
		public string Line2 { get; set; }
		[StringLength(50)]
		public string City { get; set; }
		[StringLength(2)]
		public string State { get; set; }
		[StringLength(10)]
		public string ZipCode { get; set; }
		[StringLength(13)]
		public Nullable<double> Latitude { get; set; }
		public Nullable<double> Longitude { get; set; }

		public string GetFormattedSiteAddress()
		{
			StringBuilder formattedSiteAddress = new StringBuilder();

			formattedSiteAddress.AppendLine(this.Line1.SafeTrim());
			if (this.Line2.IsValid())
			{
				formattedSiteAddress.AppendLine(this.Line2.SafeTrim());
			}
			formattedSiteAddress.AppendFormat("{0}, {1}  {2}\r\n", this.City.SafeTrim(),
												this.State.SafeTrim(), this.ZipCode.SafeTrim());

			return formattedSiteAddress.ToString();
		}


	}
}
