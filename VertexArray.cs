using OpenTK.Graphics.OpenGL4;
using GLGraphics.Helpers;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace GLGraphics
{
    public class VertexArray : GraphicsResource
    {
        public VertexArray() : base(GLObjectType.VertexArray)
        {
        }

        //public void BindAttribute(int index, GLBuffer buffer, int elements, VertexAttribPointerType type, int size, int offset, bool normalized = false, int divisor = 0)
        //{
        //    //invalid index
        //    if (index < 0)
        //    {
        //        throw new IndexOutOfRangeException("Invalid index given index was: " + index);
        //    }
        //    buffer.Bind();
        //    // make sure the vertex attribute is enabled
        //    GL.EnableVertexAttribArray(index);
        //    // set the vertex attribute pointer to the current buffer
        //    GL.VertexAttribPointer(index, elements, type, normalized, size, offset);
        //    //set the divisor
        //    GL.VertexAttribDivisor(index, divisor);
        //}
        AttributeBindingData[]? data;

        public void SetIndexBuffer(GLBuffer buffer)
        {
            if (buffer.Buffertype != BufferType.ElementArrayBuffer)
            {
                throw new Exception("Buffertype \"" + buffer.Buffertype + "\" is not of type ElementArrayBuffer.");
            }
            GL.VertexArrayElementBuffer(Handle, buffer.Handle);
        }

        public void SetVertexBuffer(GLBuffer buffer)
        {
            if (buffer.Buffertype != BufferType.ArrayBuffer)
            {
                throw new Exception("Buffertype \"" + buffer.Buffertype + "\" is not of type ArrayBuffer.");
            }
            if (data == null)
            {
                throw new Exception("Layout must be set before setting the vertex buffer.");
            } 
            GL.VertexArrayVertexBuffer(Handle, 0, buffer.Handle, IntPtr.Zero, data[0].Size);
        }

        public void SetLayout(Type type, bool normalized = false, int divisor = 0)
        {
            data = AttributeHelper.GetAttributeBindingData(type);

            for (int i = 0; i < data.Length; i++)
            {
                GL.EnableVertexArrayAttrib(Handle, data[i].Index);
                GL.VertexArrayAttribFormat(Handle, data[i].Index, data[i].Elements, data[i].Type, normalized, (int)data[i].Offset);
                GL.VertexArrayAttribBinding(Handle, data[i].Index, 0);
                GL.VertexArrayBindingDivisor(Handle, 0, divisor);
            }
        }

        public void SetLayout<T>(bool normalized = false, int divisor = 0) where T : struct
        {
            data = AttributeHelper.GetAttributeBindingData<T>();

            for (int i = 0; i < data.Length; i++)
            {
                GL.EnableVertexArrayAttrib(Handle, data[i].Index);
                GL.VertexArrayAttribFormat(Handle, data[i].Index, data[i].Elements, data[i].Type, normalized, (int)data[i].Offset);
                GL.VertexArrayAttribBinding(Handle, data[i].Index, 0);
                GL.VertexArrayBindingDivisor(Handle, 0, divisor);
            }
        }

        public void Bind()
        {
            GL.BindVertexArray(Handle);
        }

        public void Unbind()
        {
            GL.BindVertexArray(0);
        }
    }
}
