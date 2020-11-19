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

        public Texture2DMS() : base(TextureTarget.Texture2DMultisample)
        {
        }
       

        public void InitMultiSample(int w, int h, TextureFormat internalFormat = TextureFormat.Rgba8, int samples = 1)
        {
            if (IsInitialized)
            {
                throw new Exception("The texture has already been initialized.");
            }
            IsInitialized = true; 
            TextureFormat = internalFormat;
            Width = w;
            Height = h;
            PixelCount = w * h;
            GL.TextureStorage2DMultisample(Handle, samples, (SizedInternalFormat)internalFormat, w, h, false);
        }
    }
}
