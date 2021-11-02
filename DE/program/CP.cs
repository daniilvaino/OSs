using ComputationalPractice.Properties;
using ComputationalPractice.src;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Definitions.Series;
using LiveCharts.Wpf;
using LiveCharts.Geared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace ComputationalPractice
{
    public class CP : Form
    {
        private IContainer components = (IContainer)null;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label5;
        private Label label3;
        private Label label1;
        private Button btn_plot;
        private Label label7;
        private NumericUpDown tb_N;
        private LiveCharts.WinForms.CartesianChart d_SD;
        private TextBox tb_X;
        private TextBox tx_y0;
        private TextBox tb_x0;
        private Label label8;
        private Label label4;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private LiveCharts.WinForms.CartesianChart d_LTE;
        private TabPage tabPage3;
        private LiveCharts.WinForms.CartesianChart d_GTE;
        private CheckBox cb_RungeKutta;
        private CheckBox cb_ImprovedEuler;
        private CheckBox cb_Euler;
        public PictureBox pictureBox1;

        public CP() => this.InitializeComponent();

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void label5_Click(object sender, EventArgs e)
        {
        }

        

        private void cartesianChart1_ChildChanged(object sender, ChildChangedEventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double x0;
            double y0;
            double X;
            int N;
            double num1;
            try
            {
                x0 = double.Parse(this.tb_x0.Text);
                y0 = double.Parse(this.tx_y0.Text);
                X = double.Parse(this.tb_X.Text);
                N = int.Parse(this.tb_N.Value.ToString());
                num1 = (X - x0) / (double)N;
            }
            catch (Exception ex)
            {
                int num2 = (int)MessageBox.Show("Initial Conditions are not valid! ");
                return;
            }
            try
            {
                GearedValues<ObservablePoint> chartValues1 = new GearedValues<ObservablePoint>();
                for (int index = 0; index <= N; ++index)
                    chartValues1.Add(new ObservablePoint(x0 + (double)index * num1, Problem.y(x0 + (double)index * num1, x0, y0)));
                this.d_SD.Series = new SeriesCollection();
                this.d_LTE.Series = new SeriesCollection();
                this.d_GTE.Series = new SeriesCollection();
                this.d_SD.Zoom = this.d_LTE.Zoom = this.d_GTE.Zoom = ZoomingOptions.X;
                this.d_SD.Pan = this.d_LTE.Pan = this.d_GTE.Pan = PanningOptions.X;
                //this.d_SD.AnimationsSpeed = new TimeSpan(1L);
                this.d_SD.DisableAnimations = true;
                this.d_LTE.DisableAnimations = true;
                this.d_GTE.DisableAnimations = true;
                this.d_SD.Hoverable = false;
                this.d_SD.LegendLocation = this.d_LTE.LegendLocation = this.d_GTE.LegendLocation = LegendLocation.Bottom;
                SeriesCollection series1 = this.d_SD.Series;
                LineSeries lineSeries1 = new LineSeries();
                lineSeries1.Title = "Exact Solution";
                lineSeries1.Values = chartValues1;
                lineSeries1.PointGeometry = DefaultGeometries.Diamond;
                lineSeries1.PointGeometrySize = 10.0;
                series1.Add(lineSeries1);

                if (this.cb_Euler.Checked)
                    AddNumericalMethod(new EulerNM(x0, y0, X, N));
                if (this.cb_ImprovedEuler.Checked)
                    AddNumericalMethod(new ImprovedEulerNM(x0, y0, X, N));
                if (this.cb_RungeKutta.Checked)
                    AddNumericalMethod(new RungeKuttaNM(x0, y0, X, N));
            }
            catch (Exception ex)
            {
                int num3 = (int)MessageBox.Show("Error! \n " + ex.Message);
                //this.Close();
            }
        }
        void AddNumericalMethod(NumericalMethod method)
        {
            GearedValues<ObservablePoint> chartValues2 = new GearedValues<ObservablePoint>();
            foreach (KeyValuePair<double, double> keyValuePair in method.KeyValuePairs)
                if (Math.Abs(keyValuePair.Value) < Math.Pow(10, 10)) chartValues2.Add(new ObservablePoint(keyValuePair.Key, keyValuePair.Value));
            GearedValues<ObservablePoint> chartValues3 = new GearedValues<ObservablePoint>();
            foreach (KeyValuePair<double, double> keyValuePair in method.Error)
                if (Math.Abs(keyValuePair.Value) < Math.Pow(10, 10)) chartValues3.Add(new ObservablePoint(keyValuePair.Key, keyValuePair.Value));
            GearedValues<ObservablePoint> chartValues4 = new GearedValues<ObservablePoint>();
            foreach (KeyValuePair<double, double> keyValuePair in method.GTE)
                if (Math.Abs(keyValuePair.Value) < Math.Pow(10, 10)) chartValues4.Add(new ObservablePoint(keyValuePair.Key, keyValuePair.Value));
            SeriesCollection series2 = this.d_SD.Series;
            LineSeries lineSeries2 = new LineSeries();
            lineSeries2.Title = method.Name;
            lineSeries2.Values = chartValues2;
            lineSeries2.PointGeometry = DefaultGeometries.Square;
            lineSeries2.PointGeometrySize = 2.0;
            series2.Add(lineSeries2);
            SeriesCollection series3 = this.d_LTE.Series;
            LineSeries lineSeries3 = new LineSeries();
            lineSeries3.Title = $"{method.Name} LTE";
            lineSeries3.Values = chartValues3;
            lineSeries3.PointGeometry = DefaultGeometries.Square;
            lineSeries3.PointGeometrySize = 2.0;
            series3.Add(lineSeries3);
            SeriesCollection series4 = this.d_GTE.Series;
            LineSeries lineSeries4 = new LineSeries();
            lineSeries4.Title = $"{method.Name} GTE";
            lineSeries4.Values = chartValues4;
            lineSeries4.PointGeometry = DefaultGeometries.Square;
            lineSeries4.PointGeometrySize = 2.0;
            series4.Add(lineSeries4);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CP));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cb_RungeKutta = new System.Windows.Forms.CheckBox();
            this.cb_ImprovedEuler = new System.Windows.Forms.CheckBox();
            this.cb_Euler = new System.Windows.Forms.CheckBox();
            this.tb_X = new System.Windows.Forms.TextBox();
            this.tx_y0 = new System.Windows.Forms.TextBox();
            this.tb_x0 = new System.Windows.Forms.TextBox();
            this.btn_plot = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_N = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.d_SD = new LiveCharts.WinForms.CartesianChart();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.d_LTE = new LiveCharts.WinForms.CartesianChart();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.d_GTE = new LiveCharts.WinForms.CartesianChart();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tb_N)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cb_RungeKutta);
            this.groupBox1.Controls.Add(this.cb_ImprovedEuler);
            this.groupBox1.Controls.Add(this.cb_Euler);
            this.groupBox1.Controls.Add(this.tb_X);
            this.groupBox1.Controls.Add(this.tx_y0);
            this.groupBox1.Controls.Add(this.tb_x0);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.btn_plot);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tb_N);
            this.groupBox1.Location = new System.Drawing.Point(2, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(223, 368);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Parameters";
            // 
            // cb_RungeKutta
            // 
            this.cb_RungeKutta.AutoSize = true;
            this.cb_RungeKutta.Location = new System.Drawing.Point(14, 342);
            this.cb_RungeKutta.Name = "cb_RungeKutta";
            this.cb_RungeKutta.Size = new System.Drawing.Size(86, 17);
            this.cb_RungeKutta.TabIndex = 20;
            this.cb_RungeKutta.Text = "Runge Kutta";
            this.cb_RungeKutta.UseVisualStyleBackColor = true;
            // 
            // cb_ImprovedEuler
            // 
            this.cb_ImprovedEuler.AutoSize = true;
            this.cb_ImprovedEuler.Location = new System.Drawing.Point(14, 311);
            this.cb_ImprovedEuler.Name = "cb_ImprovedEuler";
            this.cb_ImprovedEuler.Size = new System.Drawing.Size(97, 17);
            this.cb_ImprovedEuler.TabIndex = 19;
            this.cb_ImprovedEuler.Text = "Improved Euler";
            this.cb_ImprovedEuler.UseVisualStyleBackColor = true;
            // 
            // cb_Euler
            // 
            this.cb_Euler.AutoSize = true;
            this.cb_Euler.Location = new System.Drawing.Point(14, 280);
            this.cb_Euler.Name = "cb_Euler";
            this.cb_Euler.Size = new System.Drawing.Size(50, 17);
            this.cb_Euler.TabIndex = 18;
            this.cb_Euler.Text = "Euler";
            this.cb_Euler.UseVisualStyleBackColor = true;
            // 
            // tb_X
            // 
            this.tb_X.Location = new System.Drawing.Point(41, 220);
            this.tb_X.Name = "tb_X";
            this.tb_X.Size = new System.Drawing.Size(167, 20);
            this.tb_X.TabIndex = 15;
            this.tb_X.Text = "15";
            // 
            // tx_y0
            // 
            this.tx_y0.Location = new System.Drawing.Point(41, 194);
            this.tx_y0.Name = "tx_y0";
            this.tx_y0.Size = new System.Drawing.Size(167, 20);
            this.tx_y0.TabIndex = 14;
            this.tx_y0.Text = "0";
            // 
            // tb_x0
            // 
            this.tb_x0.Location = new System.Drawing.Point(41, 168);
            this.tb_x0.Name = "tb_x0";
            this.tb_x0.Size = new System.Drawing.Size(167, 20);
            this.tb_x0.TabIndex = 1;
            this.tb_x0.Text = "0";
            // 
            // btn_plot
            // 
            this.btn_plot.Location = new System.Drawing.Point(143, 311);
            this.btn_plot.Name = "btn_plot";
            this.btn_plot.Size = new System.Drawing.Size(65, 48);
            this.btn_plot.TabIndex = 10;
            this.btn_plot.Text = "plot >";
            this.btn_plot.UseVisualStyleBackColor = true;
            this.btn_plot.Click += new System.EventHandler(this.button1_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(29, 206);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(10, 12);
            this.label8.TabIndex = 8;
            this.label8.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(29, 176);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(10, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "0";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(11, 246);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(20, 20);
            this.label7.TabIndex = 6;
            this.label7.Text = "N";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(11, 192);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(16, 20);
            this.label4.TabIndex = 4;
            this.label4.Text = "y";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(10, 220);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "X";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 165);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "x";
            // 
            // tb_N
            // 
            this.tb_N.Location = new System.Drawing.Point(41, 246);
            this.tb_N.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.tb_N.Name = "tb_N";
            this.tb_N.Size = new System.Drawing.Size(167, 20);
            this.tb_N.TabIndex = 1;
            this.tb_N.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tabControl1);
            this.groupBox2.Location = new System.Drawing.Point(231, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(642, 368);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Diagram";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(6, 19);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(629, 340);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.d_SD);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(621, 314);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Solution Diagram";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // d_SD
            // 
            this.d_SD.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.d_SD.Location = new System.Drawing.Point(6, 6);
            this.d_SD.Name = "d_SD";
            this.d_SD.Size = new System.Drawing.Size(609, 302);
            this.d_SD.TabIndex = 0;
            this.d_SD.Text = "cartesianChart1";
            this.d_SD.ChildChanged += new System.EventHandler<System.Windows.Forms.Integration.ChildChangedEventArgs>(this.cartesianChart1_ChildChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.d_LTE);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(621, 314);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "LTE";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // d_LTE
            // 
            this.d_LTE.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.d_LTE.Location = new System.Drawing.Point(6, 6);
            this.d_LTE.Name = "d_LTE";
            this.d_LTE.Size = new System.Drawing.Size(609, 302);
            this.d_LTE.TabIndex = 1;
            this.d_LTE.Text = "cartesianChart2";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.d_GTE);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(621, 314);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "GTE";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // d_GTE
            // 
            this.d_GTE.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.d_GTE.Location = new System.Drawing.Point(6, 6);
            this.d_GTE.Name = "d_GTE";
            this.d_GTE.Size = new System.Drawing.Size(609, 302);
            this.d_GTE.TabIndex = 1;
            this.d_GTE.Text = "cartesianChart3";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.Image = global::ComputationalPractice.Properties.Resources.Screenshot_2020_11_06_102417;
            this.pictureBox1.Location = new System.Drawing.Point(20, 19);
            this.pictureBox1.MaximumSize = new System.Drawing.Size(189, 143);
            this.pictureBox1.MinimumSize = new System.Drawing.Size(189, 143);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(189, 143);
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // CP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(878, 375);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(894, 414);
            this.MinimumSize = new System.Drawing.Size(894, 414);
            this.Name = "CP";
            this.Text = "Computational Practicum";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tb_N)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
