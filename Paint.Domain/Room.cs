using System.ComponentModel.DataAnnotations;

namespace Paint.Domain
{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int Width { get; set; }
        public int Length { get; set; }
        public int Height { get; set; }
        public int DoorsSF { get; set; }
        public int WindowSF { get; set; }
        public int BaseBoardHeight { get; set; }
        public int ChairRailHeight { get; set; }
        public int CrownMoldingHeight { get; set; }
    }


}
