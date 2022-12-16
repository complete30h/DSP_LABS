using System;

namespace DSP_3
{
	abstract class Signal
	{
		internal int n;
		internal double[] signal, restSignal, nfSignal;
		internal double[] sineSp, cosineSp;
		internal double[] amplSp, phaseSp;
		internal int numHarm = 100;

		public Signal()
		{
			signal = CreateSignal();
			sineSp = GetSineSpectr();
			cosineSp = GetCosineSpectr();
			amplSp = GetAmplSpectr();
			phaseSp = GetPhaseSpectr();
			restSignal = RecoverSignal();
			nfSignal = RecoverNFSignal();
		}
		public double[] SignalValues
		{
			get
			{
				return signal;
			}
		}
		public double[] RecoveredSignal
		{
			get
			{
				return restSignal;
			}
		}
		public double[] RecoverednonphasedSignal
		{
			get
			{
				return nfSignal;
			}
		}

		internal virtual double[] CreateSignal()
		{
			return null;
		}

		internal double[] GetSineSpectr()
		{
			double[] values = new double[numHarm];
			for (int j = 0; j <= numHarm - 1; j++)
			{
				double val = 0;
				for (int i = 0; i <= n - 1; i++)
				{
					val += signal[i] * Math.Sin(2 * Math.PI * i * j / n);
				}
				values[j] = 2 * val / n;
			}
			return values;
		}

		internal double[] GetCosineSpectr()
		{
			double[] values = new double[numHarm];
			for (int j = 0; j <= numHarm - 1; j++)
			{
				double val = 0;
				for (int i = 0; i <= n - 1; i++)
				{
					val += signal[i] * Math.Cos(2 * Math.PI * i * j / n);
				}
				values[j] = 2 * val / n;
			}
			return values;
		}

		internal double[] GetAmplSpectr()
		{
			double[] values = new double[numHarm];
			for (int j = 0; j <= numHarm - 1; j++)
			{
				values[j] = Math.Sqrt(Math.Pow(sineSp[j], 2) + Math.Pow(cosineSp[j], 2));
			}
			return values;
		}

		internal double[] GetPhaseSpectr()
		{
			double[] values = new double[numHarm];
			for (int j = 0; j <= numHarm - 1; j++)
			{
				values[j] = Math.Atan(sineSp[j] / cosineSp[j]);
			}
			return values;
		}

		internal double[] RecoverSignal()
		{
			double[] values = new double[n];
			for (int i = 0; i <= n - 1; i++)
			{
				double val = 0;
				for (int j = 0; j <= numHarm - 1; j++)
				{
					val += amplSp[j] * Math.Cos(2 * Math.PI * i * j / n - phaseSp[j]);
				}
				values[i] = val;
			}
			return values;
		}

		internal double[] RecoverNFSignal()
		{
			double[] values = new double[n];
			for (int i = 0; i <= n - 1; i++)
			{
				double val = 0;
				for (int j = 0; j <= numHarm - 1; j++)
				{
					val += amplSp[j] * Math.Cos(2 * Math.PI * i * j / n);
				}
				values[i] = val;
			}
			return values;
		}
	}
}