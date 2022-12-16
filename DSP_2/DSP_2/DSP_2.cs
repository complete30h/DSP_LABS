using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace trDSP
{
	public partial class DSP_2 : Form
	{
		public DSP_2()
		{
			InitializeComponent();
			CreateGraph();
		}

		private void CreateGraph()
		{
			chart1.Series.Clear();
			var DataSer_1 = new Series
			{
				ChartType = SeriesChartType.Spline,
				Color = Color.Green
			};
			var DataSer_2 = new Series
			{
				ChartType = SeriesChartType.Spline,
				Color = Color.Red
			};
			int N = 512;
			int K = N * 1 / 8;
			float pi = (float)Math.PI;
			float phi = pi / 8f;
			float twoPI = (float)Math.PI * 2f;

			for (int M = K; M <= 2 * N; M++)
			{
				float value = 0;
				float valueSquared = 0;

				for (int n = 0; n <= M; n++)
				{
					float t = (float)Math.Sin((phi / 180f * pi) + (twoPI * n / N));
					value += t;
					valueSquared += t * t;
				}

				float temp = value / (M + 1);
				float squaredTemp = valueSquared / (M + 1);

				DataSer_1.Points.AddXY(M, 0.707f - (float)Math.Sqrt(squaredTemp));
				DataSer_2.Points.AddXY(M, 0.707f - (float)Math.Sqrt(squaredTemp - temp * temp));
			}

			chart1.ResetAutoValues();
			chart1.Series.Add(DataSer_1);
			chart1.Series.Add(DataSer_2);
		}

		private void TrackBar1_Scroll(object sender, EventArgs e)
		{
			CreateGraph();
		}

		private void TrackBar3_Scroll(object sender, EventArgs e)
		{
			CreateGraph();
		}

	}
}