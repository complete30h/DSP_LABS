using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DSP_4
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
			chart1.SetBounds(10, 10, Size.Width - 200, (Size.Height - 70) / 3);
			chart2.SetBounds(10, (Size.Height - 70) / 3 + 20, Size.Width - 200, (Size.Height - 70) / 3);
			chart3.SetBounds(10, 2 * (Size.Height - 70) / 3 + 30, Size.Width - 200, (Size.Height - 70) / 3);
			Calculate(1, NoiseSignal.FilteringType.Parabolic);
		}

		private void MainForm_Resize(object sender, EventArgs e)
		{
			chart1.SetBounds(10, 10, Size.Width - 200, (Size.Height - 70) / 3);
			chart2.SetBounds(10, (Size.Height - 70) / 3 + 20, Size.Width - 200, (Size.Height - 70) / 3);
			chart3.SetBounds(10, 2 * (Size.Height - 70) / 3 + 30, Size.Width - 200, (Size.Height - 70) / 3);
		}

		private void Calculate(int frequency, NoiseSignal.FilteringType ft)
		{
			Series DataSer_1, DataSer_2, DataSer_3, DataSer_4, DataSer_5, DataSer_6;
			NoiseSignal noisySignal = new NoiseSignal(10, frequency, 0, 360);
			float[] fs;

			switch (ft)
			{
				case NoiseSignal.FilteringType.Parabolic:
					fs = noisySignal.ps;
					break;
				case NoiseSignal.FilteringType.Median:
					fs = noisySignal.ms;
					break;
				case NoiseSignal.FilteringType.Sliding:
					fs = noisySignal.ss;
					break;
				default:
					return;
			}
			chart1.Series.Clear();
			chart1.Legends.Clear();
			Legend l1 = new Legend();
			chart1.Legends.Add(l1);
			l1.Title = "Сигналы";
			DataSer_1 = new Series
			{
				ChartType = SeriesChartType.Spline,
				Color = Color.Red,
				Name = "Исходный сигнал"
			};
			DataSer_2 = new Series
			{
				ChartType = SeriesChartType.Spline,
				Color = Color.Blue,
				Name = "Cглаженный сигнал"
			};
			for (int i = 0; i <= 359; i++)
			{
				DataSer_1.Points.AddXY(2 * Math.PI * i / 360, noisySignal.SignVal[i]);
				DataSer_2.Points.AddXY(2 * Math.PI * i / 360, fs[i]);
			}
			chart1.ResetAutoValues();
			chart1.Series.Add(DataSer_1);
			chart1.Series.Add(DataSer_2);
			noisySignal.Operate(ft);
			chart2.Series.Clear();
			chart3.Series.Clear();
			chart2.Legends.Clear();
			chart3.Legends.Clear();
			Legend l2 = new Legend();
			chart2.Legends.Add(l2);
			l2.Title = "Фазовый спектр";
			Legend l3 = new Legend();
			chart3.Legends.Add(l3);
			l3.Title = "Амплитудный спектр";
			DataSer_3 = new Series
			{
				ChartType = SeriesChartType.Candlestick,
				Color = Color.Red,
				Name = "Исходный сигнал"
			};
			DataSer_4 = new Series
			{
				ChartType = SeriesChartType.Candlestick,
				Color = Color.Green,
				Name = "Исходный сигнал"
			};
			DataSer_5 = new Series
			{
				ChartType = SeriesChartType.Candlestick,
				Color = Color.BlueViolet,
				Name = "Сглаженный сигнал"
			};
			DataSer_6 = new Series
			{
				ChartType = SeriesChartType.Candlestick,
				Color = Color.Cyan,
				Name = "Сглаженный сигнал"
			};
			for (int i = 0; i <= 49; i++)
			{
				DataSer_3.Points.AddXY(i, noisySignal.phaseSp[i]);
				DataSer_4.Points.AddXY(i, noisySignal.amplSp[i]);
				DataSer_5.Points.AddXY(i, noisySignal.psp[i]);
				DataSer_6.Points.AddXY(i, noisySignal.asp[i]);
			}
			chart2.ResetAutoValues();
			chart2.Series.Add(DataSer_3);
			chart2.Series.Add(DataSer_5);
			chart3.ResetAutoValues();
			chart3.Series.Add(DataSer_4);
			chart3.Series.Add(DataSer_6);
		}

		private void RadioButton1_Checked(object sender, EventArgs e)
		{
			Calculate(1, NoiseSignal.FilteringType.Parabolic);
		}

		private void RadioButton2_Checked(object sender, EventArgs e)
		{
			Calculate(1, NoiseSignal.FilteringType.Median);
		}

		private void RadioButton3_Checked(object sender, EventArgs e)
		{
			Calculate(1, NoiseSignal.FilteringType.Sliding);
		}
	}
}