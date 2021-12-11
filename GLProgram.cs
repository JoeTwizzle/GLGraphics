using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace GLGraphics
{
    public class GLProgram : GraphicsResource
    {

        public GLProgram() : base(GLObjectType.Program)
        {

        }

        public void AddShader(GLShader shader)
        {
            GL.AttachShader(Handle, shader.Handle);
        }

        public void RemoveShader(GLShader shader)
        {
            GL.DetachShader(Handle, shader.Handle);
        }

        public void LinkProgram()
        {
            GL.LinkProgram(Handle);
            GL.GetProgram(Handle, GetProgramParameterName.LinkStatus, out int code);
            if (code != (int)All.True)
            {
                throw new Exception("Error occurred whilst linking Program(" + Handle + ") \n\r" + GL.GetProgramInfoLog(Handle));
            }
        }

        public void Bind()
        {
            GL.UseProgram(Handle);
        }

        public void Unbind()
        {
            GL.UseProgram(0);
        }

        #region Uniforms

        #region Instance
        public void AutoSetUniform<T>(int location, T value)
        {
            int program = Handle;
            if (value is bool b)
            {
                SetUniform1(program, location, b ? 1 : 0);
            }
            else if (value is int i)
            {
                SetUniform1(program, location, i);
            }
            else if (value is uint ui)
            {
                SetUniform1(program, location, ui);
            }
            else if (value is float f1)
            {
                SetUniform1(program, location, f1);
            }
            else if (value is Vector2 v2)
            {
                SetUniform2(program, location, v2);
            }
            else if (value is Vector3 v3)
            {
                SetUniform3(program, location, v3);
            }
            else if (value is Vector4 v4)
            {
                SetUniform4(program, location, v4);
            }
            else if (value is Color4 col)
            {
                SetUniform4(program, location, col);
            }
            else if (value is Matrix2 mat2)
            {
                SetUniformMat2(program, location, mat2);
            }
            else if (value is Matrix3 mat3)
            {
                SetUniformMat3(program, location, mat3);
            }
            else if (value is Matrix4 mat4)
            {
                SetUniformMat4(program, location, mat4);
            }
            else
            {
                Debug.WriteLine("The type " + value?.GetType() + " is not a valid type for uniforms.");
            }
        }

        public void SetUniformMat4(int location, Matrix4 mat4, bool rowMajor = true)
        {
            GL.ProgramUniformMatrix4(Handle, location, rowMajor, ref mat4);
        }

        public void SetUniformMat3(int location, Matrix3 mat3, bool rowMajor = true)
        {
            GL.ProgramUniformMatrix3(Handle, location, rowMajor, ref mat3);
        }

        public void SetUniformMat2(int location, Matrix2 mat2, bool rowMajor = true)
        {
            GL.ProgramUniformMatrix2(Handle, location, rowMajor, ref mat2);
        }

        public void SetUniform4(int location, Color4 val)
        {
            GL.ProgramUniform4(Handle, location, val);
        }

        public void SetUniform4(int location, Vector4 val)
        {
            GL.ProgramUniform4(Handle, location, val);
        }

        public void SetUniform3(int location, Vector3 val)
        {
            GL.ProgramUniform3(Handle, location, val);
        }

        public void SetUniform2(int location, Vector2 val)
        {
            GL.ProgramUniform2(Handle, location, val);
        }

        public void SetUniform1(int location, double val)
        {
            GL.ProgramUniform1(Handle, location, val);
        }

        public void SetUniform1(int location, float val)
        {
            GL.ProgramUniform1(Handle, location, val);
        }

        public void SetUniform1(int location, int val)
        {
            GL.ProgramUniform1(Handle, location, val);
        }

        public void SetUniform1(int location, uint val)
        {
            GL.ProgramUniform1(Handle, location, val);
        }
        #endregion

        #region Static
        public static void AutoSetUniform<T>(int program, int location, T value)
        {
            if (value is int i)
            {
                SetUniform1(program, location, i);
            }
            else if (value is uint ui)
            {
                SetUniform1(program, location, ui);
            }
            else if (value is float f1)
            {
                SetUniform1(program, location, f1);
            }
            else if (value is Vector2 v2)
            {
                SetUniform2(program, location, v2);
            }
            else if (value is Vector3 v3)
            {
                SetUniform3(program, location, v3);
            }
            else if (value is Vector4 v4)
            {
                SetUniform4(program, location, v4);
            }
            else if (value is Color4 col)
            {
                SetUniform4(program, location, col);
            }
            else if (value is Matrix2 mat2)
            {
                SetUniformMat2(program, location, mat2);
            }
            else if (value is Matrix3 mat3)
            {
                SetUniformMat3(program, location, mat3);
            }
            else if (value is Matrix4 mat4)
            {
                SetUniformMat4(program, location, mat4);
            }
            else
            {
                Debug.WriteLine("The type " + value?.GetType() + " is not a valid type for uniforms.");
            }
        }

        public static void SetUniformMat4(int program, int location, Matrix4 mat4)
        {
            GL.ProgramUniformMatrix4(program, location, false, ref mat4);
        }

        public static void SetUniformMat3(int program, int location, Matrix3 mat3)
        {
            GL.ProgramUniformMatrix3(program, location, false, ref mat3);
        }

        public static void SetUniformMat2(int program, int location, Matrix2 mat2)
        {
            GL.ProgramUniformMatrix2(program, location, false, ref mat2);
        }

        public static void SetUniform4(int program, int location, Color4 val)
        {
            GL.ProgramUniform4(program, location, val);
        }

        public static void SetUniform4(int program, int location, Vector4 val)
        {
            GL.ProgramUniform4(program, location, val);
        }

        public static void SetUniform3(int program, int location, Vector3 val)
        {
            GL.ProgramUniform3(program, location, val);
        }

        public static void SetUniform2(int program, int location, Vector2 val)
        {
            GL.ProgramUniform2(program, location, val);
        }

        public static void SetUniform1(int program, int location, double val)
        {
            GL.ProgramUniform1(program, location, val);
        }

        public static void SetUniform1(int program, int location, float val)
        {
            GL.ProgramUniform1(program, location, val);
        }

        public static void SetUniform1(int program, int location, int val)
        {
            GL.ProgramUniform1(program, location, val);
        }

        public static void SetUniform1(int program, int location, uint val)
        {
            GL.ProgramUniform1(program, location, val);
        }
        #endregion

        #endregion
    }
}
