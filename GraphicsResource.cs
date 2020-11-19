using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace GLGraphics
{
    public abstract class GraphicsResource : GLObject
    {
        public GraphicsResource(GLObjectType GLObjectType) : base(GLObjectType)
        {
            int handle = 0;
            switch (GLObjectType)
            {
                case GLObjectType.VertexArray:
                    GL.CreateVertexArrays(1, out handle);
                    break;
                case GLObjectType.Buffer:
                    GL.CreateBuffers(1, out handle);
                    break;
                case GLObjectType.NamedFrameBuffer:
                    GL.CreateFramebuffers(1, out handle);
                    break;
                case GLObjectType.Program:
                    handle = GL.CreateProgram();
                    break;
                case GLObjectType.Sampler:
                    GL.CreateSamplers(1, out handle);
                    break;
                case GLObjectType.ProgramPipeline:
                    GL.CreateProgramPipelines(1, out handle);
                    break;
                case GLObjectType.RenderBuffer:
                    GL.CreateRenderbuffers(1, out handle);
                    break;
                default:
                    break;
            }
            Handle = handle;
        }
    }
}
