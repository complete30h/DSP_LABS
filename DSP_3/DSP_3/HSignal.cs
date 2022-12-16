using System;

namespace DSP_3
{
    class HSignal:Signal
    {
		private readonly double A;
		private readonly double f;
		private readonly double phi;

		public HSignal(double amplitude, double freq, double phase, int discrPoints)
        {
            A = amplitude;
            n = discrPoints;
            f = freq;
            phi = phase;
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
            double[] sign = new double[n];
            for (int i = 0; i <= n - 1; i++)
            {
                sign[i] = A * Math.Cos(2 * Math.PI * f * i / n + phi);
            }
            return sign;
        }
        
    }
}