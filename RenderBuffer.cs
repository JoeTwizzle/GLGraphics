using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Text;

namespace GLGraphics
{
    public class RenderBuffer : GraphicsResource
    {
        public RenderBuffer() : base(GLObjectType.RenderBuffer)
        {
        }

        public int Width { get; private set; }
        public int Height { get; private set; }
        public RenderbufferStorage RenderbufferStorage { get; private set; }

        public void Init(int width, int height, RenderbufferStorage renderbufferStorage)
        {
            Width = width;
            Height = height;
            RenderbufferStorage = renderbufferStorage;
            GL.NamedRenderbufferStorage(Handle, renderbufferStorage, width, height);
        }

        public void InitMultiSample(int width, int height, RenderbufferStorage renderbufferStorage, int samples = 1)
        {
            Width = width;
            Height = height;
            RenderbufferStorage = renderbufferStorage; 
            GL.NamedRenderbufferStorageMultisample(Handle, samples, renderbufferStorage, width, height);
        }
    }
}
