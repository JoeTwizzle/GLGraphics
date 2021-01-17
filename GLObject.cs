using GLGraphics.Helpers;
using System;
using OpenTK.Graphics.OpenGL4;
using System.Text;
using System.Diagnostics;

namespace GLGraphics
{
    public abstract class GLObject : IDisposable
    {


        protected GLObject(GLObjectType ObjectType)
        {
            GLObjectType = ObjectType;
        }

        public GLObjectType GLObjectType { get; protected set; }
        public int Handle { get; protected set; }

        private bool disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    GLObjectCleaner.Clean((GLObjectType, Handle));
                }
                else
                {
                    GLObjectCleaner.ObjectsToBeCleaned.Add((GLObjectType, Handle));
                }
                disposed = true;
            }
        }

        public void SetLabel(string label)
        {
            switch (GLObjectType)
            {
                case GLObjectType.VertexArray:
                    GL.ObjectLabel(ObjectLabelIdentifier.VertexArray, Handle, label.Length, label);
                    break;
                case GLObjectType.Program:
                    GL.ObjectLabel(ObjectLabelIdentifier.Program, Handle, label.Length, label);
                    break;
                case GLObjectType.FrameBuffer:
                    GL.ObjectLabel(ObjectLabelIdentifier.Framebuffer, Handle, label.Length, label);
                    break;
                case GLObjectType.Sampler:
                    GL.ObjectLabel(ObjectLabelIdentifier.Sampler, Handle, label.Length, label);
                    break;
                case GLObjectType.ProgramPipeline:
                    GL.ObjectLabel(ObjectLabelIdentifier.ProgramPipeline, Handle, label.Length, label);
                    break;
                case GLObjectType.Buffer:
                    GL.ObjectLabel(ObjectLabelIdentifier.Buffer, Handle, label.Length, label);
                    break;
                case GLObjectType.Shader:
                    GL.ObjectLabel(ObjectLabelIdentifier.Shader, Handle, label.Length, label);
                    break;
                case GLObjectType.Texture:
                    GL.ObjectLabel(ObjectLabelIdentifier.Texture, Handle, label.Length, label);
                    break;
                case GLObjectType.RenderBuffer:
                    GL.ObjectLabel(ObjectLabelIdentifier.Renderbuffer, Handle, label.Length, label);
                    break;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public override bool Equals(object? obj)
        {
            return obj is GLObject @object &&
                   GLObjectType == @object.GLObjectType &&
                   Handle == @object.Handle;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(GLObjectType, Handle);
        }

        ~GLObject()
        {
            Dispose(false);
        }
    }
}
