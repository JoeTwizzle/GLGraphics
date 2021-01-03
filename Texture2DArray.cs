using OpenTK.Graphics.OpenGL4;
using GLGraphics.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace GLGraphics
{
    public class Texture2DArray : TextureBase
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int Depth { get; private set; }

        public Texture2DArray() : base(TextureTarget.Texture2DArray)
        {
        }

        public void Init(int width, int height, int depth, TextureFormat internalFormat = TextureFormat.Rgba8, int levels = 0)
        {
            if (IsInitialized)
            {
                throw new Exception("The texture has already been initialized or is immutable.");
            }
            IsInitialized = true;
            IsLayered = true;
            TextureFormat = internalFormat;
            Width = width;
            Height = height;
            Depth = depth;
            PixelCount = Width * Height * Depth;
            GL.TexStorage3D(TextureTarget3d.Texture2DArray, levels, (SizedInternalFormat)internalFormat, width, height, depth);
        }

        public void SetImage<T>(T[] data, int xOffset, int yOffset, int width, int height, PixelFormat pixelFormat, PixelType pixelType, int depth, int level = 0) where T : unmanaged
        {
            if (!IsInitialized)
            {
                throw new Exception("The texture has not been initialized.");
            }

            if (data is null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            GL.TexSubImage3D(TextureTarget, level, 0, xOffset, yOffset, width, height, depth, pixelFormat, pixelType, data);
        }

        public void SetImage(IntPtr data, int xOffset, int yOffset, int width, int height, PixelFormat pixelFormat, PixelType pixelType, int depth, int level = 0)
        {
            if (!IsInitialized)
            {
                throw new Exception("The texture has not been initialized.");
            }

            GL.TexSubImage3D(TextureTarget, level, 0, xOffset, yOffset, width, height, depth, pixelFormat, pixelType, data);
        }
    }
}
