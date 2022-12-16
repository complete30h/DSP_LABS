using System;
using System.Collections.Generic;
using System.Linq;

namespace DSP_4
{
	class NoiseSignal : Signal
	{
		public enum FilteringType
		{
			Sliding, Median, Parabolic
		}

		private readonly float A;
		private readonly float f;
		private readonly float phi;
		public float[] ps, ms, ss, asp, psp;

		public NoiseSignal(float amplitude, float freq, float phase, int discrPoints)
		{
			A = amplitude;
			n = discrPoints;
			f = freq;
			phi = phase;
			signal = CreateSignal();
			ps = ParabolicFilter();
			ms = MedianFilter(5);
			ss = SlidingFilter(3);
			sineSp = GetSinSpectr(signal);
			cosineSp = GetCosSpectr(signal);
			amplSp = GetAmplSpectr(sineSp, cosineSp);
			phaseSp = GetPhaseSpectr(sineSp, cosineSp);
			restSignal = RecoverSignal();
			nfSignal = RecoverNFSignal();
		}

		public void Operate(FilteringType ft)
		{
			float[] fs = null;
			switch (ft)
			{
				case FilteringType.Parabolic:
					fs = ps;
					break;
				case FilteringType.Median:
					fs = ms;
					break;
				case FilteringType.Sliding:
					fs = ss;
					break;
				default:
					break;
			}
			float[] sinSp = GetSinSpectr(fs);
			float[] cosinSp = GetCosSpectr(fs);
			asp = GetAmplSpectr(sinSp, cosinSp);
			psp = GetPhaseSpectr(sinSp, cosinSp);
		}

		internal override float[] CreateSignal()
		{
			float[] sign = new float[n];
			Random rnd = new Random();
			float B = A / 70;

			for (int i = 0; i <= n - 1; i++)
			{
				sign[i] = (float)(A * Math.Sin(2 * Math.PI * f * i / n + phi));
				float noise = 0;
				for (int j = 50; j <= 70; j++)
				{
					noise += (float)((rnd.Next(100000) % 2 == 0) ? (B * Math.Sin(2 * Math.PI * f * i * j / n + phi)) : (-B * Math.Sin(2 * Math.PI * f * i * j / n + phi)));
				}
				sign[i] += noise;
			}

			return sign;
		}

		public float[] ParabolicFilter()
		{
			float[] rest = new float[n];

			for (int i = 7; i <= rest.Length - 8; i++)
			{
				rest[i] = (-3 * signal[i - 7] - 6 * signal[i - 6] - 5 * signal[i - 5] + 3 * signal[i - 4] + 21 * signal[i - 3] + 46 * signal[i - 2] + 67 * signal[i - 1] + 74 * signal[i] - 3 * signal[i + 7] - 6 * signal[i + 6] - 5 * signal[i + 5] + 3 * signal[i + 4] + 21 * signal[i + 3] + 46 * signal[i + 2] + 67 * signal[i + 1]) / 320;
			}

			return rest;
		}

		public float[] SlidingFilter(int windowSize)
		{
			float[] rest = (float[])signal.Clone();
			List<float> window = new List<float>();

			for (int i = 0; i <= rest.Length - 1 - windowSize; i++)
			{
				window.Clear();
				for (int j = i; j <= i + windowSize - 1; j++)
				{
					window.Add(signal[j]);
				}
				float avg = window.Sum() / windowSize;
				rest[i + windowSize / 2] = avg;
			}

			return rest;
		}

		public float[] MedianFilter(int windowSize)
		{
			float[] rest = (float[])signal.Clone();
			List<float> window = new List<float>();

			for (int i = 0; i <= rest.Length - 1 - windowSize; i++)
			{
				window.Clear();
				for (int j = i; j <= i + windowSize - 1; j++)
				{
					window.Add(signal[j]);
				}
				window.Sort();
				rest[i + windowSize / 2] = window[windowSize / 2 + 1];
			}

			return rest;
		}

	}
}