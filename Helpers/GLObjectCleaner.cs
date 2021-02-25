using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GLGraphics.Helpers
{
    public static class GLObjectCleaner
    {
        public static TextWriter DebugWriter = Console.Out;
        public static List<(GLObjectType, int)> ObjectsToBeCleaned = new List<(GLObjectType, int)>();
        public static float SecondsPerClean = 1;
        static float timeSinceLastClean = 0;
        public static void Update(float dt)
        {
            timeSinceLastClean += dt;
            if (timeSinceLastClean >= SecondsPerClean)
            {
                timeSinceLastClean = 0;
                int c = ObjectsToBeCleaned.Count;
                for (int i = 0; i < c; i++)
                {
                    Clean(ObjectsToBeCleaned[i]);
                }
                if (c > 0)
                {
                    ObjectsToBeCleaned.Clear();
                }
            }
        }
        public static void Clean((GLObjectType, int) objectToBeCleaned)
        {
            int Handle = objectToBeCleaned.Item2;
            DebugWriter?.WriteLine("Cleaned GLObject of type: " + Enum.GetName(typeof(GLObjectType), objectToBeCleaned.Item1) + " with ID: " + objectToBeCleaned.Item2);
            switch (objectToBeCleaned.Item1)
            {
                case GLObjectType.VertexArray:
                    GL.DeleteVertexArray(Handle);
                    break;
                case GLObjectType.Program:
                    GL.DeleteProgram(Handle);
                    break;
                case GLObjectType.FrameBuffer:
                    GL.DeleteFramebuffer(Handle);
                    break;
                case GLObjectType.Sampler:
                    GL.DeleteSampler(Handle);
                    break;
                case GLObjectType.ProgramPipeline:
                    GL.DeleteProgramPipeline(Handle);
                    break;
                case GLObjectType.Texture:
                    GL.DeleteTexture(Handle);
                    break;
                case GLObjectType.Buffer:
                    GL.DeleteBuffer(Handle);
                    break;
                case GLObjectType.RenderBuffer:
                    GL.DeleteRenderbuffer(Handle);
                    break;
                default:
                    break;
            }
        }
    }
}
