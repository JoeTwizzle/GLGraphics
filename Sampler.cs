using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Text;

namespace GLGraphics
{
    public class Sampler : GraphicsResource
    {
        public Sampler() : base(GLObjectType.Sampler)
        {

        }

        float maxAniso;
        private float anisotropyLevel = 16f;
        public float AnisotropyLevel
        {
            get
            {
                if (maxAniso == 0)
                {
                    maxAniso = GL.GetFloat((GetPName)All.MaxTextureMaxAnisotropy);
                }
                if (anisotropyLevel > maxAniso)
                {
                    anisotropyLevel = maxAniso;
                }
                return anisotropyLevel;
            }

            set
            {
                if (maxAniso == 0)
                {
                    maxAniso = GL.GetFloat((GetPName)All.MaxTextureMaxAnisotropy);
                }
                anisotropyLevel = MathF.Min(MathF.Max(1, value), maxAniso);
                SetAnisotropy();
            }
        }

        private TextureFilter filter = TextureFilter.Nearest;
        public TextureFilter Filter
        {
            get => filter; set
            {
                filter = value;
                SetTextureFilter();
            }
        }

        private TextureWrapMode wrapModeU = TextureWrapMode.Repeat;
        public TextureWrapMode WrapModeU
        {
            get => wrapModeU; set
            {
                wrapModeU = value;
                SetWrapModeS();
            }
        }

        private TextureWrapMode wrapModeV = TextureWrapMode.Repeat;
        public TextureWrapMode WrapModeV
        {
            get => wrapModeV; set
            {
                wrapModeV = value;
                SetWrapModeT();
            }
        }

        private TextureWrapMode wrapModeW = TextureWrapMode.Repeat;
        public TextureWrapMode WrapModeW
        {
            get => wrapModeW; set
            {
                wrapModeW = value;
                SetWrapModeR();
            }
        }

        TextureMinFilter GetMinFilter()
        {
            return filter switch
            {
                TextureFilter.Nearest => TextureMinFilter.NearestMipmapLinear,
                TextureFilter.Bilinear => TextureMinFilter.LinearMipmapNearest,
                TextureFilter.Trilinear => TextureMinFilter.LinearMipmapLinear,
                _ => throw new Exception("Invalid filter"),
            };
        }

        TextureMagFilter GetMagFilter()
        {
            return filter switch
            {
                TextureFilter.Nearest => TextureMagFilter.Nearest,
                TextureFilter.Bilinear => TextureMagFilter.Linear,
                TextureFilter.Trilinear => TextureMagFilter.Linear,
                _ => throw new Exception("Invalid filter"),
            };
        }

        void SetTextureFilter()
        {
            SetParameter(SamplerParameterName.TextureMinFilter, (int)GetMinFilter());
            SetParameter(SamplerParameterName.TextureMagFilter, (int)GetMagFilter());
        }

        void SetAnisotropy()
        {
            SetParameter((SamplerParameterName)All.TextureMaxAnisotropy, AnisotropyLevel);
        }

        void SetWrapModeS()
        {
            SetParameter(SamplerParameterName.TextureWrapS, (int)wrapModeU);
        }

        void SetWrapModeT()
        {
            SetParameter(SamplerParameterName.TextureWrapT, (int)wrapModeV);
        }

        void SetWrapModeR()
        {
            SetParameter(SamplerParameterName.TextureWrapR, (int)wrapModeW);
        }

        public void Bind(int index = 0)
        {
            GL.BindSampler(index, Handle);
        }

        public static void Unbind(int index = 0)
        {
            GL.BindSampler(index, 0);
        }

        public void SetParameter(SamplerParameterName param, int value)
        {
            GL.SamplerParameter(Handle, param, value);
        }

        public void SetParameter(SamplerParameterName param, float value)
        {
            GL.SamplerParameter(Handle, param, value);
        }

        public long GetTextureSamplerHandle(TextureBase texture)
        {
            return GL.Arb.GetTextureSamplerHandle(texture.Handle, Handle);
        }

        public long GetTextureSamplerHandle(int texture)
        {
            return GL.Arb.GetTextureSamplerHandle(texture, Handle);
        }
    }
}
