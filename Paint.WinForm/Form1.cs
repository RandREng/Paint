using Paint.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint.WinForm
{
    public partial class Form1 : Form
    {
        public Job Job { get; set; } = new Job();

        public Form1()
        {
            InitializeComponent();


            var bs = new BindingSource();
            this.Job.PaintList = new PaintList();

            bs.DataSource = this.Job.PaintList.paints.Where(p => p.Type == PaintType.Trim);
            this.comboBoxTrim.DataSource = bs;
            this.comboBoxTrim.DisplayMember = "Name";
            this.comboBoxTrim.DataBindings.Add("SelectedItem", this.Job.PaintList, "TrimPaint");

            bs = new BindingSource();
            bs.DataSource = this.Job.PaintList.paints.Where(p => p.Type == PaintType.Ceiling);
            this.comboBoxCeiling.DataSource = bs;
            this.comboBoxCeiling.DisplayMember = "Name";
            this.comboBoxCeiling.DataBindings.Add("SelectedItem", this.Job.PaintList, "CeilingPaint");

            bs = new BindingSource();
            bs.DataSource = this.Job.PaintList.paints.Where(p => p.Type == PaintType.Walls);
            this.comboBoxWalls.DataSource = bs;
            this.comboBoxWalls.DisplayMember = "Name";
            this.comboBoxWalls.DataBindings.Add("SelectedItem", this.Job.PaintList, "WallPaint");

            bs = new BindingSource();
            bs.DataSource = this.Job.Rooms;
            bs.AllowNew = true;
            this.dataGridView1.DataSource = bs;
            bs.CurrentItemChanged += Bs_CurrentItemChanged;

        }

        private void Bs_CurrentItemChanged(object sender, EventArgs e)
        {
            //this.labelWallSF.Text = this.PaintList.WallCalc.SF.ToString();
            //this.labelCeilingSF.Text = this.PaintList.CeilingCalc.SF.ToString();
            this.dataGridView2.DataSource = new List<PaintCalc>()
            {
                this.Job.PaintList.WallCalc,
                this.Job.PaintList.CeilingCalc
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }
    }
}
