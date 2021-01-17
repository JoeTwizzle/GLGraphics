using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Text;

namespace GLGraphics
{
    public class GLFence : IDisposable
    {
        public IntPtr SyncHandle;

        public void CreateSync()
        {
            if (SyncHandle != IntPtr.Zero)
            {
                Dispose();
            }
            SyncHandle = GL.FenceSync(SyncCondition.SyncGpuCommandsComplete, WaitSyncFlags.None);
        }

        public void Wait(ClientWaitSyncFlags clientWaitSyncFlags = ClientWaitSyncFlags.SyncFlushCommandsBit)
        {
            if (SyncHandle == IntPtr.Zero)
            {
                return;
            }
            while (true)
            {
                WaitSyncStatus s = GL.ClientWaitSync(SyncHandle, clientWaitSyncFlags, 1);
                if (s == WaitSyncStatus.ConditionSatisfied || s == WaitSyncStatus.ConditionSatisfied)
                {
                    return;
                }
            }
        }

        public void Dispose()
        {
            GL.DeleteSync(SyncHandle);
        }
    }
}
