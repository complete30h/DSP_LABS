using System;

namespace DSP_4
{
	abstract class Signal
	{
		internal int n;
		internal float[] signal, restSignal, nfSignal;
		internal float[] sineSp, cosineSp;
		internal float[] amplSp, phaseSp;
		internal int numHarm = 100;
		public float[] SignVal { get { return signal; } }

		internal virtual float[] CreateSignal()
		{
			return null;
		}

		internal float[] GetSinSpectr(float[] signal)
		{
			float[] values = new float[numHarm];

			for (int j = 0; j <= numHarm - 1; j++)
			{
				float val = 0;
				for (int i = 0; i <= n - 1; i++)
				{
					val += (float)(signal[i] * Math.Sin(2 * Math.PI * i * j / n));
				}
				values[j] = 2 * val / n;
			}

			return values;
		}

		internal float[] GetCosSpectr(float[] signal)
		{
			float[] values = new float[numHarm];

			for (int j = 0; j <= numHarm - 1; j++)
			{
				float val = 0;
				for (int i = 0; i <= n - 1; i++)
				{
					val += (float)(signal[i] * Math.Cos(2 * Math.PI * i * j / n));
				}
				values[j] = 2 * val / n;
			}

			return values;
		}

		internal float[] GetAmplSpectr(float[] sineSp, float[] cosineSp)
		{
			float[] values = new float[numHarm];

			for (int j = 0; j <= numHarm - 1; j++)
			{
				values[j] = (float)Math.Sqrt(Math.Pow(sineSp[j], 2) + Math.Pow(cosineSp[j], 2));
			}

			return values;
		}

		internal float[] GetPhaseSpectr(float[] sineSp, float[] cosineSp)
		{
			float[] values = new float[numHarm];

			for (int j = 0; j <= numHarm - 1; j++)
			{
				values[j] = (float)Math.Atan(sineSp[j] / cosineSp[j]);
			}

			return values;
		}

		internal float[] RecoverSignal()
		{
			float[] values = new float[n];

			for (int i = 0; i <= n - 1; i++)
			{
				float val = 0;
				for (int j = 0; j <= numHarm - 1; j++)
				{
					val += (float)(amplSp[j] * Math.Cos(2 * Math.PI * i * j / n - phaseSp[j]));
				}
				values[i] = val;
			}

			return values;
		}

		internal float[] RecoverNFSignal()
		{
			float[] values = new float[n];

			for (int i = 0; i <= n - 1; i++)
			{
				float val = 0;
				for (int j = 0; j <= numHarm - 1; j++)
				{
					val += (float)(amplSp[j] * Math.Cos(2 * Math.PI * i * j / n));
				}
				values[i] = val;
			}

			return values;
		}
	}
}