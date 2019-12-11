using Paint.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;

namespace Paint.Domain
{
    public class PaintList
    {
        public int JobId { get; set; }
        public Job Job { get; set; }
        [NotMapped]
        public List<PaintItem> paints { get; set; } = new List<PaintItem>();
        public int CeilingId { get; set; } = 2;
        public int TrimId { get; set; } = 5;
        public int WallId { get; set; } = 10;
        public int TaxRate { get; set; } = 7;

        public PaintItem CeilingPaint
        {
            get { return paints?.FirstOrDefault(p => p.Id == CeilingId); }
            set { CeilingId = value.Id; }
        }

        public PaintItem TrimPaint
        {
            get { return paints?.FirstOrDefault(p => p.Id == TrimId); }
            set { TrimId = value.Id; }
        }

        public PaintItem WallPaint
        {
            get { return paints?.FirstOrDefault(p => p.Id == WallId); }
            set { WallId = value.Id; }
        }

        public PaintList()
        {
            if (!File.Exists("paint.json"))
            {
                this.paints.Add(new PaintItem
                {
                    Id = 1,
                    Type = PaintType.Ceiling,
                    Grade = PaintGrade.Good,
                    Name = "SPEED-WALL Flat",
                    LowCoverage = 300,
                    HiCoverage = 400,
                    GallonPrice = 11.98M,
                    FiveGallonPrice = 44.98M,
                });
                this.paints.Add(new PaintItem
                {
                    Id = 2,
                    Type = PaintType.Ceiling,
                    Grade = PaintGrade.Better,
                    Name = "PPG ULTRA-HIDE Zero Flat",
                    LowCoverage = 350,
                    HiCoverage = 400,
                    GallonPrice = 19.98M,
                    FiveGallonPrice = 70.98M,
                });
                this.paints.Add(new PaintItem
                {
                    Id = 3,
                    Type = PaintType.Trim,
                    Grade = PaintGrade.Good,
                    Name = "GLIDDEN ESSENTIALS SG",
                    LowCoverage = 300,
                    HiCoverage = 400,
                    GallonPrice = 21.98M,
                    FiveGallonPrice = 0M,
                });
                this.paints.Add(new PaintItem
                {
                    Id = 4,
                    Type = PaintType.Trim,
                    Grade = PaintGrade.Better,
                    Name = "PPG Ultra-Hide Zero SG",
                    LowCoverage = 350,
                    HiCoverage = 400,
                    GallonPrice = 19.98M,
                    FiveGallonPrice = 89.98M,
                });
                this.paints.Add(new PaintItem
                {
                    Id = 5,
                    Type = PaintType.Trim,
                    Grade = PaintGrade.Best,
                    Name = "Glidden Premium SG",
                    LowCoverage = 400,
                    HiCoverage = 400,
                    GallonPrice = 22.98M,
                    FiveGallonPrice = 102M,
                });
                this.paints.Add(new PaintItem
                {
                    Id = 6,
                    Type = PaintType.Trim,
                    Grade = PaintGrade.Premium,
                    Name = "PPG DIAMOND Eggshell",
                    LowCoverage = 300,
                    HiCoverage = 400,
                    GallonPrice = 25.98M,
                    FiveGallonPrice = 121M,
                });
                this.paints.Add(new PaintItem
                {
                    Id = 7,
                    Type = PaintType.Trim,
                    Grade = PaintGrade.Premium,
                    Name = "PPG TIMELESS Eggshell",
                    LowCoverage = 400,
                    HiCoverage = 400,
                    GallonPrice = 36.98M,
                    FiveGallonPrice = 168M,
                });
                this.paints.Add(new PaintItem
                {
                    Id = 8,
                    Type = PaintType.Walls,
                    Grade = PaintGrade.Good,
                    Name = "GLIDDEN ESSENTIALS Eggshell",
                    LowCoverage = 400,
                    HiCoverage = 400,
                    GallonPrice = 14.98M,
                    FiveGallonPrice = 76.98M,
                });
                this.paints.Add(new PaintItem
                {
                    Id = 9,
                    Type = PaintType.Walls,
                    Grade = PaintGrade.Better,
                    Name = "PPG Ultra-Hide Zero Eggshell",
                    LowCoverage = 350,
                    HiCoverage = 400,
                    GallonPrice = 17.98M,
                    FiveGallonPrice = 79.98M,
                });
                this.paints.Add(new PaintItem
                {
                    Id = 10,
                    Type = PaintType.Walls,
                    Grade = PaintGrade.Best,
                    Name = "Glidden Premium Eggshell",
                    LowCoverage = 400,
                    HiCoverage = 400,
                    GallonPrice = 20.98M,
                    FiveGallonPrice = 97.98M,
                });
                this.paints.Add(new PaintItem
                {
                    Id = 11,
                    Type = PaintType.Walls,
                    Grade = PaintGrade.Premium,
                    Name = "PPG DIAMOND Eggshell",
                    LowCoverage = 350,
                    HiCoverage = 400,
                    GallonPrice = 25.98M,
                    FiveGallonPrice = 112M,
                });
                this.paints.Add(new PaintItem
                {
                    Id = 12,
                    Type = PaintType.Walls,
                    Grade = PaintGrade.Premium,
                    Name = "PPG TIMELESS Eggshell",
                    LowCoverage = 400,
                    HiCoverage = 400,
                    GallonPrice = 34.98M,
                    FiveGallonPrice = 159M,
                });
            }
        }

        public PaintCalc WallCalc
        {
            get
            {
                var retValue = new PaintCalc(WallPaint, this.TaxRate, this.Job.Rooms);
                return retValue;
            }
        }

        public PaintCalc CeilingCalc
        {
            get
            {
                var retValue = new PaintCalc(CeilingPaint, this.TaxRate, this.Job.Rooms);
                return retValue;
            }
        }

        public PaintCalc TrimPaind
        {
            get
            {
                var retValue = new PaintCalc(TrimPaint, this.TaxRate, this.Job.Rooms);
                return retValue;
            }
        }
    }

    public class PaintCalc
    {
        private readonly PaintItem Paint;
        private readonly decimal TaxRate;

        private readonly List<Room> Rooms;

        public PaintType Type { get { return this.Paint.Type; } }
        public int SF
        {
            get
            {
                switch (Paint.Type)
                {
                    case PaintType.Ceiling:
                        return Rooms.Sum(r => r.Quantity * (r.Width * r.Length));

                    case PaintType.Walls:
                        return Rooms.Sum(r => r.Quantity * (r.Width + r.Length) * 2 * r.Height);

                    case PaintType.Trim:
                    default:
                        return 0;
                }
            }
        }
        public int Coverage { get { return this.Paint.Coverage; } }
        public float Gallons { get { return ((float)this.SF) / this.Paint.Coverage; } }
        public decimal CostPerSF { get { return this.Paint.PricePerSF5Gallon; } }
        public decimal TotalCost { get { return this.Paint.PricePerSF5Gallon * this.SF; } }
        public decimal TotalCostWithTax { get { return this.Paint.PricePerSF5Gallon * this.SF * this.TaxRate; } }

        public PaintCalc(PaintItem paint, int taxRate, List<Room> rooms)
        {
            this.Paint = paint;
            this.TaxRate = (100M + taxRate) / 100M;
            this.Rooms = rooms;
        }
    }
}
