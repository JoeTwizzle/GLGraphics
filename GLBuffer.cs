using OpenTK.Graphics.OpenGL4;
using System;
using System.Runtime.CompilerServices;

namespace GLGraphics
{
    public class GLBuffer : GraphicsResource
    {
        /// <summary>
        /// The BufferType of this buffer
        /// </summary>
        public BufferType Buffertype { get; private set; }
        /// <summary>
        /// Number of elements stored in this buffer.
        /// </summary>
        public int ElementCount { get; private set; }
        /// <summary>
        /// Size in bytes of an instance of the data stored in this buffer.
        /// </summary>
        public int DataSize { get; private set; }
        /// <summary>
        /// Total size in bytes of this buffer.
        /// </summary>
        public int Size { get; private set; }

        public bool Initialized { get; private set; }

        /// <summary>
        /// The C# type of the generic given in the Init() method.
        /// </summary>
        public Type? BufferElementType { get; private set; }


        public GLBuffer() : base(GLObjectType.Buffer)
        {
        }

        public void Init<T>(BufferType buffertype, T[] data, BufferStorageFlags bufferStorageFlags = BufferStorageFlags.DynamicStorageBit) where T : unmanaged
        {
            if (Initialized)
            {
                throw new InvalidOperationException("Buffer has already been initialized");
            }
            Initialized = true;
            BufferElementType = typeof(T);
            Buffertype = buffertype;
            DataSize = Unsafe.SizeOf<T>();
            ElementCount = data.Length;
            Size = DataSize * data.Length;
            GL.NamedBufferStorage(Handle, Size, data, bufferStorageFlags);
        }

        public void Init<T>(BufferType buffertype, int elementCount, T[] data, BufferStorageFlags bufferStorageFlags = BufferStorageFlags.DynamicStorageBit) where T : unmanaged
        {
            if (Initialized)
            {
                throw new InvalidOperationException("Buffer has already been initialized");
            }

            Initialized = true;
            BufferElementType = typeof(T);
            Buffertype = buffertype;
            DataSize = Unsafe.SizeOf<T>();
            ElementCount = data.Length;
            Size = DataSize * data.Length;
            if (elementCount < data.Length)
            {
                throw new Exception("elementCount can't be less than the amount of elements contained in data");
            }
            GL.NamedBufferStorage(Handle, DataSize * elementCount, data, bufferStorageFlags);
        }

        public void Init<T>(BufferType buffertype, int elementCount, BufferStorageFlags bufferStorageFlags = BufferStorageFlags.DynamicStorageBit) where T : unmanaged
        {
            if (Initialized)
            {
                throw new InvalidOperationException("Buffer has already been initialized");
            }
            Initialized = true;
            BufferElementType = typeof(T);
            Buffertype = buffertype;
            DataSize = Unsafe.SizeOf<T>();
            ElementCount = elementCount;
            Size = DataSize * elementCount;
            GL.NamedBufferStorage(Handle, Size, IntPtr.Zero, bufferStorageFlags);
        }

        public void Init(BufferType buffertype,int dataSize, int elementCount, BufferStorageFlags bufferStorageFlags = BufferStorageFlags.DynamicStorageBit)
        {
            if (Initialized)
            {
                throw new InvalidOperationException("Buffer has already been initialized");
            }
            Initialized = true;
            BufferElementType = typeof(byte[]);
            Buffertype = buffertype;
            DataSize = dataSize;
            ElementCount = elementCount;
            Size = DataSize * elementCount;
            GL.NamedBufferStorage(Handle, Size, IntPtr.Zero, bufferStorageFlags);
        }

        public void Init(BufferType buffertype, int dataSize, byte[] data, BufferStorageFlags bufferStorageFlags = BufferStorageFlags.DynamicStorageBit)
        {
            if (Initialized)
            {
                throw new InvalidOperationException("Buffer has already been initialized");
            }
            Initialized = true;
            BufferElementType = typeof(byte[]);
            Buffertype = buffertype;
            DataSize = dataSize;
            ElementCount = data.Length / dataSize;
            Size = DataSize * ElementCount;
            GL.NamedBufferStorage(Handle, Size, IntPtr.Zero, bufferStorageFlags);
        }

        public void Init(BufferType buffertype, int dataSize, int elementCount, byte[] data, BufferStorageFlags bufferStorageFlags = BufferStorageFlags.DynamicStorageBit)
        {
            if (Initialized)
            {
                throw new InvalidOperationException("Buffer has already been initialized");
            }

            Initialized = true;
            BufferElementType = typeof(byte[]);
            Buffertype = buffertype;
            DataSize = dataSize;
            ElementCount = elementCount;
            Size = DataSize * elementCount;
            if (elementCount < data.Length/ dataSize)
            {
                throw new Exception("elementCount can't be less than the amount of elements contained in data");
            }
            GL.NamedBufferStorage(Handle, DataSize * elementCount, data, bufferStorageFlags);
        }

        /*public void InitDynamic<T>(BufferType buffertype, T[] data, BufferUsageHint bufferUsageHint = BufferUsageHint.StaticDraw) where T : unmanaged
        {
            if (Initialized)
            {
                throw new InvalidOperationException("Buffer has already been initialized");
            }
            Initialized = true;
            BufferElementType = typeof(T);
            Buffertype = buffertype;
            DataSize = Marshal.SizeOf(typeof(T));
            ElementCount = data.Length;
            Size = DataSize * data.Length;
            GL.NamedBufferData(Handle, Size, data, bufferUsageHint);
        }

        public void InitDynamic<T>(BufferType buffertype, int elementCount, BufferUsageHint bufferUsageHint = BufferUsageHint.StaticDraw) where T : unmanaged
        {
            if (Initialized)
            {
                throw new InvalidOperationException("Buffer has already been initialized");
            }
            Initialized = true;
            BufferElementType = typeof(T);
            Buffertype = buffertype;
            DataSize = Marshal.SizeOf(typeof(T));
            ElementCount = elementCount;
            Size = DataSize * elementCount;
            GL.NamedBufferData(Handle, Size, IntPtr.Zero, bufferUsageHint);
        }
        */

        public IntPtr MapBuffer(BufferAccess bufferAccess)
        {
            return GL.MapNamedBuffer(Handle, bufferAccess);
        }

        public IntPtr MapBufferRange(int offset, int length, BufferAccessMask bufferAccess)
        {
            return GL.MapNamedBufferRange(Handle, (IntPtr)(offset * DataSize), length * DataSize, bufferAccess);
        }

        public bool UnmapBuffer()
        {
            return GL.UnmapNamedBuffer(Handle);
        }

        public void UpdateData<T>(T data, int offset = 0) where T : unmanaged
        {
            GL.NamedBufferSubData(Handle, (IntPtr)offset, DataSize, ref data);
        }

        public void UpdateData<T>(T[] data, int offset = 0) where T : unmanaged
        {
            GL.NamedBufferSubData(Handle, (IntPtr)offset, data.Length * DataSize, data);
        }

        public void UpdateData<T>(int length, T[] data, int offset = 0) where T : unmanaged
        {
            GL.NamedBufferSubData(Handle, (IntPtr)offset, length * DataSize, data);
        }

        public void Bind()
        {
            GL.BindBuffer((BufferTarget)Buffertype, Handle);
        }

        public void BindStorage(int index = 0)
        {
            GL.BindBufferBase((BufferRangeTarget)Buffertype, index, Handle);
        }
    }
}
