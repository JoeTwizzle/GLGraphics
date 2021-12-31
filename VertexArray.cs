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
        int stride;

        public void SetIndexBuffer(GLBuffer buffer)
        {
            if (buffer.Buffertype != BufferType.ElementArrayBuffer)
            {
                throw new Exception("Buffertype \"" + buffer.Buffertype + "\" is not of type ElementArrayBuffer.");
            }
            GL.VertexArrayElementBuffer(Handle, buffer.Handle);
        }

        public void SetVertexBuffer(GLBuffer buffer, int bindingIndex = 0, int offset = 0)
        {
            if (buffer.Buffertype != BufferType.ArrayBuffer)
            {
                throw new Exception("Buffertype \"" + buffer.Buffertype + "\" is not of type ArrayBuffer.");
            }

            GL.VertexArrayVertexBuffer(Handle, bindingIndex, buffer.Handle, (IntPtr)offset, stride);
        }

        public void AutoSetLayout(Type type)
        {

            AttributeBindingData[] data = AttributeHelper.GetAttributeBindingData(type);

            for (int i = 0; i < data.Length; i++)
            {
                GL.EnableVertexArrayAttrib(Handle, data[i].Index);
                GL.VertexArrayAttribFormat(Handle, data[i].Index, data[i].Elements, data[i].Type, false, (int)data[i].Offset);
                GL.VertexArrayAttribBinding(Handle, data[i].Index, 0);
                GL.VertexArrayBindingDivisor(Handle, 0, 0);
            }
            stride = Marshal.SizeOf(type);
        }

        public void SetIndex(int index, int elements, VertexAttribType vertexAttribType, int offset, int bindingIndex = 0, int divisor = 0, bool normalized = false)
        {
            GL.EnableVertexArrayAttrib(Handle, index);
            GL.VertexArrayAttribFormat(Handle, index, elements, vertexAttribType, normalized, (int)offset);
            GL.VertexArrayAttribBinding(Handle, index, bindingIndex);
            GL.VertexArrayBindingDivisor(Handle, bindingIndex, divisor);
        }

        public void SetStride(int stride)
        {
            this.stride = stride;
        }

        public void AutoSetLayout<T>() where T : struct
        {
            AutoSetLayout(typeof(T));
            stride = Unsafe.SizeOf<T>();
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
