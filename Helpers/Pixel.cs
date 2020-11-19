using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace GLGraphics.Helpers
{
    /// <summary>
    /// A BGRA Pixel
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Pixel
    {
        public byte B;
        public byte G;
        public byte R;
        public byte A;

        public Pixel(float color)
        {
            B = (byte)(color * 255f);
            G = (byte)(color * 255f);
            R = (byte)(color * 255f);
            A = (byte)(color * 255f);
        }

        public Pixel(float b, float g, float r, float a)
        {
            B = (byte)(b * 255f);
            G = (byte)(g * 255f);
            R = (byte)(r * 255f);
            A = (byte)(a * 255f);
        }

        public Pixel(byte b, byte g, byte r, byte a)
        {
            B = b;
            G = g;
            R = r;
            A = a;
        }
    }
}
