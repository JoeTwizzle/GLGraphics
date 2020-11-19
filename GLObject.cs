using GLGraphics.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace GLGraphics
{
    public abstract class GLObject : IDisposable
    {

        protected GLObject(GLObjectType ObjectType)
        {
            GLObjectType = ObjectType;
        }

        public GLObjectType GLObjectType { get; protected set; }
        public int Handle { get; protected set; }

        private bool disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    GLObjectCleaner.Clean((GLObjectType, Handle));
                }
                else
                {
                    GLObjectCleaner.ObjectsToBeCleaned.Add((GLObjectType, Handle));
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~GLObject()
        {
            Dispose(false);
        }
    }
}
