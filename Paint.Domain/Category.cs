using System.Collections.Generic;

namespace Paint.Domain
{
    public class Category
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public List<Category> SubCategories { get; set; } = new List<Category>();
	}
}
