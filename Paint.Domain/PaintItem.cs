using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace Paint.Domain
{
    public class PaintItem
    {
        public int Id { get; set; }
        public PaintType Type { get; set; }
        public PaintGrade Grade { get; set; }
        public string Name { get; set; }
        public int LowCoverage { get; set; }
        public int HiCoverage { get; set; }
        public decimal GallonPrice { get; set; }
        public decimal FiveGallonPrice { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public int Coverage { get { return (HiCoverage + LowCoverage) / 2; } }

        [XmlIgnore]
        [JsonIgnore]
        public decimal PricePerSFGallon { get { return (GallonPrice / Coverage); } }

        [XmlIgnore]
        [JsonIgnore]
        public decimal PricePerSF5Gallon { get { return (FiveGallonPrice / (Coverage * 5)); } }

        public override string ToString()
        {
            return this.Name ;
        }
    }


}
