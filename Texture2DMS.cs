using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Text;

namespace GLGraphics
{
    public class Texture2DMS : TextureBase
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int Samples { get; private set; }

        public Texture2DMS() : base(TextureTarget.Texture2DMultisample)
        {
        }

        public void Init(int w, int h, bool fixedSampleLocations = false, TextureFormat textureFormat = TextureFormat.Rgba8, int samples = 1)
        {
            if (IsInitialized)
            {
                throw new Exception("The texture has already been initialized.");
            }
            TextureFormat = textureFormat;
            Samples = samples;
            IsInitialized = true;
            Width = w;
            Height = h;
            PixelCount = w * h;
            GL.TextureStorage2DMultisample(Handle, samples, (SizedInternalFormat)textureFormat, w, h, fixedSampleLocations);
        }

        public void SetImage<T>(T[] data, int xOffset, int yOffset, int w, int h, PixelFormat pixelFormat, PixelType pixelType, int level = 0) where T : unmanaged
        {
            if (!IsInitialized)
            {
                throw new Exception("The texture has not been initialized.");
            }

            if (data is null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            GL.TextureSubImage2D(Handle, level, xOffset, yOffset, w, h, pixelFormat, pixelType, data);
        }

        public void SetImage(IntPtr data, int xOffset, int yOffset, int w, int h, PixelFormat pixelFormat, PixelType pixelType, int level = 0)
        {
            if (!IsInitialized)
            {
                throw new Exception("The texture has not been initialized.");
            }

            GL.TextureSubImage2D(Handle, level, xOffset, yOffset, w, h, pixelFormat, pixelType, data);
        }
    }
}
