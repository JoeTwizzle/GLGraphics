using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace GLGraphics.Helpers
{
    public static class AttributeHelper
    {
        public static AttributeBindingData[] GetAttributeBindingData<T>() where T : struct
        {
            var type = typeof(T);
            var fields = type.GetFields();
            AttributeBindingData[] bindingData = new AttributeBindingData[fields.Length];
            for (int i = 0; i < fields.Length; i++)
            {
                var attribData = new AttributeBindingData();

                attribData.Offset = Marshal.OffsetOf(type, fields[i].Name);
                attribData.Type = GetPointerType(fields[i].FieldType);
                attribData.Elements = GetElements(fields[i].FieldType);
                attribData.ElementSize = attribData.Elements * Marshal.SizeOf(fields[i].FieldType);
                attribData.Index = i;
                bindingData[i] = attribData;
            }
            var size = 0;
            for (int i = 0; i < fields.Length; i++)
            {
                size += bindingData[i].ElementSize;
            }
            for (int i = 0; i < fields.Length; i++)
            {
                bindingData[i].Size = size;
            }
            return bindingData;
        }

        public static AttributeBindingData[] GetAttributeBindingData(Type type)
        {

            var fields = type.GetFields();
            AttributeBindingData[] bindingData = new AttributeBindingData[fields.Length];
            for (int i = 0; i < fields.Length; i++)
            {
                var attribData = new AttributeBindingData();

                attribData.Offset = Marshal.OffsetOf(type, fields[i].Name);
                attribData.Type = GetPointerType(fields[i].FieldType);
                attribData.Elements = GetElements(fields[i].FieldType);
                attribData.ElementSize = Marshal.SizeOf(fields[i].FieldType);
                attribData.Index = i;
                bindingData[i] = attribData;
            }
            var size = Marshal.SizeOf(type);

            for (int i = 0; i < fields.Length; i++)
            {
                bindingData[i].Size = size;
            }
            return bindingData;
        }

        static int GetElements(Type fieldType)
        {
            if (fieldType == typeof(float))
            {
                return 1;
            }

            if (fieldType == typeof(Vector2))
            {
                return 2;
            }

            if (fieldType == typeof(Vector3))
            {
                return 3;
            }

            if (fieldType == typeof(Vector4))
            {
                return 4;
            }

            if (fieldType == typeof(Color4))
            {
                return 4;
            }
            if (fieldType == typeof(byte))
            {
                return 1;
            }
            if (fieldType == typeof(sbyte))
            {
                return 1;
            }
            if (fieldType == typeof(int))
            {
                return 1;
            }
            if (fieldType == typeof(uint))
            {
                return 1;
            }
            if (fieldType == typeof(OpenTK.Mathematics.Half))
            {
                return 1;
            }
            if (fieldType == typeof(double))
            {
                return 1;
            }
            throw new Exception("Invalid type " + fieldType);
        }

        static VertexAttribType GetPointerType(Type fieldType)
        {
            if (fieldType == typeof(float) || fieldType == typeof(Vector2) || fieldType == typeof(Vector3) || fieldType == typeof(Vector4) || fieldType == typeof(Color4))
            {
                return VertexAttribType.Float;
            }
            if (fieldType == typeof(byte))
            {
                return VertexAttribType.UnsignedByte;
            }
            if (fieldType == typeof(sbyte))
            {
                return VertexAttribType.Byte;
            }
            if (fieldType == typeof(int))
            {
                return VertexAttribType.Int;
            }
            if (fieldType == typeof(uint))
            {
                return VertexAttribType.UnsignedInt;
            }
            if (fieldType == typeof(OpenTK.Mathematics.Half))
            {
                return VertexAttribType.HalfFloat;
            }
            if (fieldType == typeof(double))
            {
                return VertexAttribType.Double;
            }
            throw new Exception("Can't parse VertexAttribType from " + fieldType);
        }
    }
}
