using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace GLGraphics
{
    public class FrameBuffer : GraphicsResource
    {
        public FrameBuffer() : base(GLObjectType.FrameBuffer)
        {
        }

        public void AttachTexture(TextureBase texture, FramebufferAttachment framebufferAttachment)
        {
            GL.NamedFramebufferTexture(Handle, framebufferAttachment, texture.Handle, 0);
        }

        public void AttachRenderBuffer(RenderBuffer renderBuffer, FramebufferAttachment framebufferAttachment)
        {
            GL.NamedFramebufferRenderbuffer(Handle, framebufferAttachment, RenderbufferTarget.Renderbuffer, renderBuffer.Handle);
        }

        public void DetachTexture(FramebufferAttachment framebufferAttachment)
        {
            GL.NamedFramebufferTexture(Handle, framebufferAttachment, 0, 0);
        }

        public void DetachRenderBuffer(FramebufferAttachment framebufferAttachment)
        {
            GL.NamedFramebufferRenderbuffer(Handle, framebufferAttachment, RenderbufferTarget.Renderbuffer, 0);
        }

        public void Bind(FramebufferTarget framebufferTarget = FramebufferTarget.Framebuffer)
        {
            GL.BindFramebuffer(framebufferTarget, Handle);
        }

        public void Unbind(FramebufferTarget framebufferTarget = FramebufferTarget.Framebuffer)
        {
            GL.BindFramebuffer(framebufferTarget, 0);
        }

        public static void BindDefault(FramebufferTarget framebufferTarget = FramebufferTarget.Framebuffer)
        {
            GL.BindFramebuffer(framebufferTarget, 0);
        }
        /// <summary>
        /// Copies data from one source framebuffer to a destination framebuffer
        /// </summary>
        /// <param name="dest"></param>
        public void Blit(FrameBuffer dest, Box2i scrRect, Box2i destRect, ClearBufferMask clearBufferMask = ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit)
        {
            FrameBuffer src = this;
            GL.BlitNamedFramebuffer(src.Handle, dest.Handle, scrRect.Min.X, scrRect.Min.Y, scrRect.Max.X, scrRect.Max.Y,
                destRect.Min.X, destRect.Min.Y, destRect.Max.X, destRect.Max.Y, clearBufferMask, BlitFramebufferFilter.Nearest);
        }
        /// <summary>
        /// Copies data from one source framebuffer to a destination framebuffer
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dest"></param>
        public static void Blit(FrameBuffer src, FrameBuffer dest, Box2i scrRect, Box2i destRect, ClearBufferMask clearBufferMask = ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit)
        {
            GL.BlitNamedFramebuffer(src.Handle, dest.Handle, scrRect.Min.X, scrRect.Min.Y, scrRect.Max.X, scrRect.Max.Y,
                destRect.Min.X, destRect.Min.Y, destRect.Max.X, destRect.Max.Y, clearBufferMask, BlitFramebufferFilter.Nearest);
        }

        /// <summary>
        /// Copies data from one source framebuffer to a destination framebuffer
        /// </summary>
        /// <param name="destHandle"></param>
        public void Blit(int destHandle, Box2i scrRect, Box2i destRect, ClearBufferMask clearBufferMask = ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit)
        {
            GL.BlitNamedFramebuffer(Handle, destHandle, scrRect.Min.X, scrRect.Min.Y, scrRect.Max.X, scrRect.Max.Y,
                destRect.Min.X, destRect.Min.Y, destRect.Max.X, destRect.Max.Y, clearBufferMask, BlitFramebufferFilter.Nearest);
        }
        /// <summary>
        /// Copies data from one source framebuffer to a destination framebuffer
        /// </summary>
        /// <param name="srcHandle"></param>
        /// <param name="destHandle"></param>
        public static void Blit(int srcHandle, int destHandle, Box2i scrRect, Box2i destRect, ClearBufferMask clearBufferMask = ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit)
        {
            GL.BlitNamedFramebuffer(srcHandle, destHandle, scrRect.Min.X, scrRect.Min.Y, scrRect.Max.X, scrRect.Max.Y,
                destRect.Min.X, destRect.Min.Y, destRect.Max.X, destRect.Max.Y, clearBufferMask, BlitFramebufferFilter.Nearest);
        }
    }
}
