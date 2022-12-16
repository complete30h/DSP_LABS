using System;

namespace DSP_3
{
	class PolyHSignal : Signal
	{
		private readonly double[] A;
		private readonly double[] phi;
		private readonly double f;
		public PolyHSignal(double[] amplitudes, double freq, double[] phases, int discrPoints)
		{
			A = amplitudes;
			n = discrPoints;
			f = freq;
			phi = phases;
			signal = CreateSignal();
			sineSp = GetSineSpectr();
			cosineSp = GetCosineSpectr();
			amplSp = GetAmplSpectr();
			phaseSp = GetPhaseSpectr();
			restSignal = RecoverSignal();
			nfSignal = RecoverNFSignal();
		}

		internal override double[] CreateSignal()
		{
			double[] signal = new double[n];
			Random rnd = new Random();
			for (int i = 0; i <= n - 1; i++)
			{
				double tm = 0;
				for (int j = 0; j <= 29; j++)
				{
					tm += A[rnd.Next(7)] * Math.Cos(2 * Math.PI * f * i / n + phi[rnd.Next(6)]);
				}
				signal[i] = tm;
			}
			return signal;
		}
	}
}