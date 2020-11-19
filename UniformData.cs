using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Text;

namespace GLGraphics
{
    public struct UniformData
    {
        public int Location;
        public int Size;
        public string Name;
        public ActiveUniformType Type;

        public UniformData(int location, int size, string name, ActiveUniformType type)
        {
            Location = location;
            Size = size;
            Name = name;
            Type = type;
        }
    }
}
