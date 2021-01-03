using OpenTK.Graphics.OpenGL4;
using GLGraphics.Helpers;
using System;
using System.IO;

namespace GLGraphics
{
    public class Texture2D : TextureBase
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public Texture2D() : base(TextureTarget.Texture2D)
        {
        }

        public void Init(int w, int h, TextureFormat textureFormat = TextureFormat.Rgba8, int levels = 1)
        {
            if (IsInitialized)
            {
                throw new Exception("The texture has already been initialized.");
            }
            TextureFormat = textureFormat;
            Levels = levels;
            IsInitialized = true;
            Width = w;
            Height = h;
            PixelCount = w * h; 
            GL.TextureStorage2D(Handle, levels, (SizedInternalFormat)textureFormat, w, h);
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
