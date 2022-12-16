using System;
using System.Drawing;
using System.Windows.Forms;

namespace DSP
{
	public partial class DSP_1_Form : Form
	{

		private const int offset = 20;
		private const int N = 512;
		private const float scale = 7.0f;

		private readonly Graphics g;
		private readonly Pen p;
		private int table = 0;

		public DSP_1_Form()
		{
			InitializeComponent();

			g = Graphics.FromHwnd(PictureBox.Handle);
			p = new Pen(Color.Red);
		}

		private void Draw()
		{
			g.Clear(Color.White);
			g.DrawLine(p, offset, PictureBox.Height - offset, offset, offset);
			g.DrawLine(p, offset, PictureBox.Height - offset, PictureBox.Width - offset, PictureBox.Height - offset);

			switch (table)
			{
				case 0:
					DrawPlot(7.0f, 5.0f, (float)(Math.PI * 1.0f));
					DrawPlot(7.0f, 5.0f, (float)(Math.PI * 0.0f));
					DrawPlot(7.0f, 5.0f, (float)(Math.PI / 3.0f));
					DrawPlot(7.0f, 5.0f, (float)(Math.PI / 6.0f));
					DrawPlot(7.0f, 5.0f, (float)(Math.PI / 2.0f));
					break;
				case 1:
					DrawPlot(5.0f, 1.0f, (float)(Math.PI * 0.75f));
					DrawPlot(5.0f, 5.0f, (float)(Math.PI * 0.75f));
					DrawPlot(5.0f, 11.0f, (float)(Math.PI * 0.75f));
					DrawPlot(5.0f, 6.0f, (float)(Math.PI * 0.75f));
					DrawPlot(5.0f, 3.0f, (float)(Math.PI * 0.75f));
					break;
				case 2:
					DrawPlot(1.0f, 3.0f, (float)(Math.PI * 0.75f));
					DrawPlot(2.0f, 3.0f, (float)(Math.PI * 0.75f));
					DrawPlot(11.0f, 3.0f, (float)(Math.PI * 0.75f));
					DrawPlot(4.0f, 3.0f, (float)(Math.PI * 0.75f));
					DrawPlot(2.0f, 3.0f, (float)(Math.PI * 0.75f));
					break;
				case 3:
					DrawPolyPlot
					(
						new float[]
						{
							9.0f,
							9.0f,
							9.0f,
							9.0f,
							9.0f
						},
						new float[]
						{
							1.0f,
							2.0f,
							3.0f,
							4.0f,
							5.0f
						},
						new float[]
						{
							(float)(Math.PI / 2.0f),
							(float)(0.0f),
							(float)(Math.PI / 4.0f),
							(float)(Math.PI / 3.0f),
							(float)(Math.PI / 6.0f)
						},
						5
					);
					break;
				case 4:
					DrawPolyPlot
					(
						new float[]
						{
							1.0f,
							2.0f,
							3.0f,
							4.0f,
							5.0f
						},
						new float[]
						{
							1.0f,
							2.0f,
							3.0f,
							4.0f,
							5.0f
						},
						new float[]
						{
							(float)(Math.PI / 2.0f),
							(float)(0.0f),
							(float)(Math.PI / 4.0f),
							(float)(Math.PI / 3.0f),
							(float)(Math.PI / 6.0f)
						},
						5
					);
					break;

			}
			table++;
			table %= 5;
		}

		private void DrawPlot(float amplitude, float frequency, float phase)
		{
			float length = PictureBox.Width - offset * 2;
			float step = length / N;
			float center = PictureBox.Height / 2;
			float[] array = new float[N];
			for (int i = 0; i < N; i++)
			{
				array[i] = Func(amplitude, frequency, phase, i, N) * scale;
			}
			for (int i = 0; i < N - 1; i++)
			{
				float x = offset + i * step;
				g.DrawLine(p, x, center + array[i], x + step, center + array[i + 1]);
			}
		}

		private void DrawPolyPlot(float[] amplitudes, float[] frequencies, float[] phases, int k)
		{
			float length = PictureBox.Width - offset * 2;
			float step = length / N;
			float center = PictureBox.Height / 2;
			float[] array = new float[N];
			for (int i = 0; i < N; i++)
			{
				for (int j = 0; j < k; j++)
				{
					array[i] += Func(amplitudes[j], frequencies[j], phases[j], i, N) * scale;
				}
			}
			for (int i = 0; i < N - 1; i++)
			{
				float x = offset + i * step;
				g.DrawLine(p, x, center + array[i], x + step, center + array[i + 1]);
			}
		}

		private static float Func(float amplitude, float frequency, float phase, int n, int N)
		{
			return amplitude * (float)Math.Sin(phase + (2 * Math.PI * frequency * n / N));
		}

		private void PictureBox_Click(object sender, EventArgs e)
		{
			Draw();
		}

	}
}