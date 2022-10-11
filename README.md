# GLGraphics
![Logo](https://i.imgur.com/krtVm22.png)

A small library designed to be an object oriented wrapper around the OpenGL API.

GLGraphics is available as a [nuget package](https://www.nuget.org/packages/GLGraphics).

# Dependencies
[OpenTK](https://github.com/opentk/opentk)

# Additional information

This library is stateless and uses 100% DSA(Direct State Access) OpenGL, it also exposes some optional extentions such as bindless textures, make sure your GPU can support DSA.

# Commented Sample Code

```cs
using GLGraphics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;

internal class SampleApp : GameWindow
{
    public SampleApp() : base(GameWindowSettings.Default, NativeWindowSettings.Default)
    {
    }

    //The source code for our vertex shader
    const string vertexShaderSource =
        @"#version 330 core
        layout (location = 0) in vec3 pos;
        
        void main()
        {
            gl_Position = vec4(pos, 1.0);
        }";

    //The source code for our fragment shader
    const string fragmentShaderSource =
        @"#version 330 core
        out vec4 color;
        
        void main()
        {
            color = vec4(146/255.0, 8/255.0, 199/255.0, 1.0);
        }";

    readonly float[] vertices = //Vertices for our Square
    {
             0.5f,  0.5f, 0.0f,
             0.5f, -0.5f, 0.0f,
            -0.5f, -0.5f, 0.0f,
            -0.5f,  0.5f, 0.0f
    };

    readonly uint[] indices = { 0, 1, 3, 1, 2, 3 }; //Indices for our Square

    //The VertexArrayObject holding information about
    //how our vertices are layed out in memory and
    //from which buffers to pull data
    VertexArray VAO;

    //The GPU side buffer that holds our vertex data
    GLBuffer VertexBuffer;
    //The GPU side buffer that holds our index data
    GLBuffer IndexBuffer;

    //The Shaderprogram that decides how things are rendered
    GLProgram ShaderProgram;

    protected override void OnLoad()
    {
        base.OnLoad();

        //The OpenGL vertex input index 0 consists of 3 float values. 
        //The data is read from a byte offset of zero.
        VAO = new VertexArray();
        VAO.SetIndex(0, 3, VertexAttribType.Float, 0);
        //Define how many bytes we need to advance, in order to find the next vertex
        VAO.SetStride(3 * sizeof(float));

        //Initialize the buffer as an ArrayBuffer (Vertex buffer) and
        //fill it with the data of out vertices
        VertexBuffer = new GLBuffer();
        VertexBuffer.Init(BufferType.ArrayBuffer, vertices);


        //Initialize the buffer as an ElementArrayBuffer (Index buffer) and
        //fill it with the data of out indices
        IndexBuffer = new GLBuffer();
        IndexBuffer.Init(BufferType.ElementArrayBuffer, indices);

        //Assign our vertex and index buffers as the current targets of
        VAO.SetVertexBuffer(VertexBuffer);
        VAO.SetIndexBuffer(IndexBuffer);

        //The VertexShader part of our shader Program
        GLShader vertexShader = new GLShader(ShaderType.VertexShader);
        vertexShader.SetSource(vertexShaderSource);

        //The FragmentShader part of our shader Program
        GLShader fragmentShader = new GLShader(ShaderType.FragmentShader);
        fragmentShader.SetSource(fragmentShaderSource);


        //Create the Compiled Shader Program
        ShaderProgram = new GLProgram();
        //Add our shader parts to the final Shader Program
        ShaderProgram.AddShader(vertexShader);
        ShaderProgram.AddShader(fragmentShader);
        //Compile the program
        ShaderProgram.LinkProgram();

        //We can remove the fragment and vertex shader parts after compiling, to free memory
        ShaderProgram.RemoveShader(vertexShader);
        ShaderProgram.RemoveShader(fragmentShader);
        vertexShader.Dispose();
        fragmentShader.Dispose();
    }

    protected override void OnResize(ResizeEventArgs e)
    {
        base.OnResize(e);

        GL.Viewport(0, 0, ClientSize.X, ClientSize.Y);
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        base.OnRenderFrame(args);
        GL.ClearColor(58 / 255f, 17 / 255f, 214 / 255f, 1f);
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        //Set our VAO as the active VAO
        VAO.Bind();
        //Set our shader program as the active Shader Program
        ShaderProgram.Bind();
        //Render our Square
        GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
        SwapBuffers();
    }

    protected override void OnClosed()
    {
        base.OnClosed();
        //Delete our OpenGL objects
        VAO.Dispose();
        VertexBuffer.Dispose();
        IndexBuffer.Dispose();
        ShaderProgram.Dispose();
    }
}
```
