using System;
using System.Collections.Generic;
using System.Text;

namespace GLGraphics
{
    //
    // Summary:
    //     Used in GL.BindBuffer, GL.BufferData and 12 other functions
    public enum BufferType
    {
        //
        // Summary:
        //     Original was GL_ARRAY_BUFFER = 0x8892
        ArrayBuffer = 34962,
        //
        // Summary:
        //     Original was GL_ELEMENT_ARRAY_BUFFER = 0x8893
        ElementArrayBuffer = 34963,
        //
        // Summary:
        //     Original was GL_PIXEL_PACK_BUFFER = 0x88EB
        PixelPackBuffer = 35051,
        //
        // Summary:
        //     Original was GL_PIXEL_UNPACK_BUFFER = 0x88EC
        PixelUnpackBuffer = 35052,
        //
        // Summary:
        //     Original was GL_UNIFORM_BUFFER = 0x8A11
        UniformBuffer = 35345,
        //
        // Summary:
        //     Original was GL_TEXTURE_BUFFER = 0x8C2A
        TextureBuffer = 35882,
        //
        // Summary:
        //     Original was GL_TRANSFORM_FEEDBACK_BUFFER = 0x8C8E
        TransformFeedbackBuffer = 35982,
        //
        // Summary:
        //     Original was GL_COPY_READ_BUFFER = 0x8F36
        CopyReadBuffer = 36662,
        //
        // Summary:
        //     Original was GL_COPY_WRITE_BUFFER = 0x8F37
        CopyWriteBuffer = 36663,
        //
        // Summary:
        //     Original was GL_DRAW_INDIRECT_BUFFER = 0x8F3F
        DrawIndirectBuffer = 36671,
        //
        // Summary:
        //     Original was GL_SHADER_STORAGE_BUFFER = 0x90D2
        ShaderStorageBuffer = 37074,
        //
        // Summary:
        //     Original was GL_DISPATCH_INDIRECT_BUFFER = 0x90EE
        DispatchIndirectBuffer = 37102,
        //
        // Summary:
        //     Original was GL_QUERY_BUFFER = 0x9192
        QueryBuffer = 37266,
        //
        // Summary:
        //     Original was GL_ATOMIC_COUNTER_BUFFER = 0x92C0
        AtomicCounterBuffer = 37568
    }
}
