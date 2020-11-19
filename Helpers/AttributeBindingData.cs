using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Text;

namespace GLGraphics.Helpers
{
    public struct AttributeBindingData
    {
        public int Index;
        public int Elements;
        public VertexAttribType Type;
        public int Size;
        public int ElementSize;
        public IntPtr Offset;
    }
}
