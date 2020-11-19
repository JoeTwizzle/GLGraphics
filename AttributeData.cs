using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Text;

namespace GLGraphics
{
    public struct AttributeData
    {
        public int Location;
        public int Size;
        public string Name;
        public ActiveAttribType Type;

        public AttributeData(int location, int size, string name, ActiveAttribType type)
        {
            Location = location;
            Size = size;
            Name = name;
            Type = type;
        }
    }
}
