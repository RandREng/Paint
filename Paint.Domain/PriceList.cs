using System.Collections.Generic;
using System.Linq;

namespace Paint.Domain
{
    public class PriceList : List<PriceListLineItem>
	{
		public new void Add(PriceListLineItem item)
		{
			PriceListLineItem i2 = this.FirstOrDefault(i => string.Compare(i.Name.Trim(), item.Name.Trim(), true) == 0);
			if (i2 == null)
			{
				base.Add(item);
			}
		}

	}
}
