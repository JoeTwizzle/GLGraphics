using OpenTK.Graphics.OpenGL4;
using System;
using System.Drawing;

namespace GLGraphics
{

    public enum CubemapFace
    {
        Right = 0,
        Left = 1,
        Top = 2,
        Bottom = 3,
        Front = 4,
        Back = 5
    }

    public class TextureCube : TextureBase
    {
        public int FaceWidth { get; private set; }
        public int FaceHeight { get; private set; }
        public int FaceCount => 6;

        public TextureCube() : base(TextureTarget.TextureCubeMap)
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
            IsLayered = true;
            IsInitialized = true;
            FaceWidth = w;
            FaceHeight = h;
            PixelCount = w * h;
            GL.TextureStorage2D(Handle, levels, (SizedInternalFormat)textureFormat, w, h);
        }

        public void SetFaces<T>(T[][] data, PixelFormat pixelFormat, PixelType pixelType) where T : unmanaged
        {
            if (data.Length != FaceCount)
            {
                throw new Exception("Bitmaps must be an array of length 6.");
            }
            for (int i = 0; i < data.Length; i++)
            {
                SetFace((CubemapFace)i, data[i], pixelFormat, pixelType);
            }
        }

        public void SetFace<T>(CubemapFace face, T[] data, PixelFormat pixelFormat, PixelType pixelType) where T : unmanaged
        {
            GL.TextureSubImage3D(Handle, 0, 0, 0, (int)face, FaceWidth, FaceHeight, 1, pixelFormat, pixelType, data);
        }
    }
}
