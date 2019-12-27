using System.Collections.Generic;

namespace Paint.Domain
{
    public class WorkItem
	{
		public int ID { get; set; }
		public string Description { get; set; }
		public List<WorkItem> SubItems { get; set; } = new List<WorkItem>();

		public int CategoryId { get; set; }
		public string Name { get; set; }
		public string MaterialSupplier { get; set; }
		public string ModelNumber { get; set; }
		public UOM UOM { get; set; }
		public decimal MaterialUnitCost { get; set; }
		public decimal Tax { get; set; }
		public decimal MaterialTotalCost { get; set; }
		public decimal LaborRate { get; set; }
		public decimal LaborHours { get; set; }
		public decimal LaborTotal { get; set; }
	}
}
