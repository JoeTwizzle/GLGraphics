using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace GLGraphics
{
    public class GLShader : GLObject
    {
        public GLShader(ShaderType shaderType) : base(GLObjectType.Shader)
        {
            Handle = GL.CreateShader(shaderType);
        }

        public void SetSource(string source)
        {
            GL.ShaderSource(Handle, source);
            Compile();
        }

        void Compile()
        {
            GL.CompileShader(Handle);
            GL.GetShader(Handle, ShaderParameter.CompileStatus, out int code);
            if (code != (int)All.True)
            {
                throw new Exception("Error occurred whilst compiling Shader with handle: " + Handle + " \n\r " + GL.GetShaderInfoLog(Handle));
            }
        }
    }
}
