using System.Collections.Generic;

namespace Paint.DTO
{
    public class BidAreaDto
	{
		public int Id { get; set; }
		public int BidSheetId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public List<BidItemDto> Items { get; set; }

		public BidAreaDto()
		{
			Items = new List<BidItemDto>();
		}

	}
}
