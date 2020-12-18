using GLGraphics.Helpers;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;

namespace GLGraphics
{
    public abstract class TextureBase : GLObject
    {
        public bool IsBindless { get; private set; } = false;
        public long BindlessHandle;

        public TextureBase(TextureTarget TextureTarget) : base(GLObjectType.Texture)
        {
            this.TextureTarget = TextureTarget;
            GL.CreateTextures(TextureTarget, 1, out int h);
            Handle = h;
        }

        public bool IsInitialized { get; protected set; }

        public int PixelCount { get; protected set; }
        public int Levels { get; protected set; }
        public bool IsLayered { get; protected set; }
        public TextureFormat TextureFormat { get; protected set; }
        public TextureTarget TextureTarget { get; protected set; }

        #region Properties
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
                SetWrapMode();
            }
        }

        private TextureWrapMode wrapModeV = TextureWrapMode.Repeat;
        public TextureWrapMode WrapModeV
        {
            get => wrapModeV; set
            {
                wrapModeV = value;
                SetWrapMode();
            }
        }

        private TextureWrapMode wrapModeW = TextureWrapMode.Repeat;
        public TextureWrapMode WrapModeW
        {
            get => wrapModeW; set
            {
                wrapModeW = value;
                SetWrapMode();
            }
        }
        #endregion

        public void Bind(int unit = 0)
        {
            GL.BindTextureUnit(unit, Handle);
        }

        public void SetTextureParams()
        {
            SetTextureFilter();
            SetAnisotropy();
            SetWrapMode();
            GenMipmaps();
        }

        public void MakeBindless()
        {
            if (IsBindless)
            {
                throw new Exception("This texture is already bindless.");
            }
            IsBindless = true;
            BindlessHandle = GL.Arb.GetTextureHandle(Handle);
        }

        public void MakeNonBindless()
        {
            if (!IsBindless)
            {
                throw new Exception("This texture is not bindless.");
            }
            MakeNonResident();
            BindlessHandle = 0;
            IsBindless = false;
        }

        public void MakeResident()
        {
            if (!IsBindless)
            {
                throw new Exception("This texture is not bindless.");
            }
            GL.Arb.MakeTextureHandleResident(BindlessHandle);
        }

        public void MakeNonResident()
        {
            if (!IsBindless)
            {
                throw new Exception("This texture is not bindless.");
            }
            GL.Arb.MakeTextureHandleNonResident(BindlessHandle);
        }

        public void CopyImageSubData(int srcLvl, Box3i scrBounds, TextureBase dest, int destLvl, Vector3i destOffset)
        {
            CopyImageSubData(this, srcLvl, scrBounds, dest, destLvl, destOffset);
        }

        public static void CopyImageSubData(TextureBase scr, int srcLvl, Box3i scrBounds, TextureBase dest, int destLvl, Vector3i destOffset)
        {
            GL.CopyImageSubData(scr.Handle, (ImageTarget)scr.TextureTarget, srcLvl, scrBounds.Min.X, scrBounds.Min.Y, scrBounds.Min.Z,
                                dest.Handle, (ImageTarget)dest.TextureTarget, destLvl, destOffset.X, destOffset.Y, destOffset.Z,
                                scrBounds.Max.X, scrBounds.Max.Y, scrBounds.Max.Z);
        }

        public void GenMipmaps()
        {
            GL.GenerateTextureMipmap(Handle);
        }

        public void Clear(Pixel clearColor, int level = 0)
        {
            GL.ClearTexImage(Handle, level, PixelFormat.Bgra, PixelType.UnsignedByte, ref clearColor);
        }

        public void BindImage(TextureAccess access, int unit = 0, int level = 0, int layer = 0)
        {
            GL.BindImageTexture(unit, Handle, level, IsLayered, layer, access, (SizedInternalFormat)TextureFormat);
        }

        void SetTextureFilter()
        {
            GL.TextureParameter(Handle, TextureParameterName.TextureMinFilter, (int)GetMinFilter());
            GL.TextureParameter(Handle, TextureParameterName.TextureMagFilter, (int)GetMagFilter());
        }

        public void SetAnisotropy()
        {
            GL.TextureParameter(Handle, (TextureParameterName)All.TextureMaxAnisotropy, AnisotropyLevel);
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

        public void SetWrapMode()
        {
            GL.TextureParameter(Handle, TextureParameterName.TextureWrapS, (int)wrapModeU);
            GL.TextureParameter(Handle, TextureParameterName.TextureWrapT, (int)wrapModeV);
            GL.TextureParameter(Handle, TextureParameterName.TextureWrapR, (int)wrapModeW);
        }

        public void SetParameter(TextureParameterName param, int value)
        {
            GL.TextureParameter(Handle, param, value);
        }
    }
}
