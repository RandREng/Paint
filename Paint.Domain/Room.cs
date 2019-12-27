using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Paint.Domain
{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Width { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Length { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Height { get; set; }
        public int DoorsSF { get; set; }
        public int WindowSF { get; set; }
        public int BaseBoardHeight { get; set; }
        public int ChairRailHeight { get; set; }
        public int CrownMoldingHeight { get; set; }
    }


}
