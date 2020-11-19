using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace GLGraphics.Commands
{
    [StructLayout(LayoutKind.Sequential)]
    public struct DrawElementsIndirectCommand
    {
        public uint Count;
        public uint InstanceCount;
        public uint FirstIndex;
        public uint BaseVertex;
        public uint BaseInstance;

        public DrawElementsIndirectCommand(uint count, uint instanceCount, uint firstIndex, uint baseVertex, uint baseInstance)
        {
            Count = count;
            InstanceCount = instanceCount;
            FirstIndex = firstIndex;
            BaseVertex = baseVertex;
            BaseInstance = baseInstance;
        }
    }
}
