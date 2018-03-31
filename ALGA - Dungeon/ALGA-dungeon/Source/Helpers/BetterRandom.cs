using System;
using System.Security.Cryptography;

namespace ALGAdungeon.Source.Helpers
{
    public class BetterRandom : RandomNumberGenerator
    {
        private static RandomNumberGenerator _rng;

        public BetterRandom()
        {
            _rng = Create();
        }

        public override void GetBytes(byte[] data)
        {
            _rng.GetBytes(data);
        }

        public double NextDouble()
        {
            var b = new byte[4];
            _rng.GetBytes(b);

            return (double) BitConverter.ToUInt32(b, 0) / uint.MaxValue;
        }

        public int Next(int minValue, int maxValue)
        {
            return (int) Math.Round(NextDouble() * (maxValue - minValue - 1)) + minValue;
        }

        public int Next()
        {
            return Next(0, int.MaxValue);
        }

        public int Next(int maxValue)
        {
            return Next(0, maxValue);
        }
    }
}