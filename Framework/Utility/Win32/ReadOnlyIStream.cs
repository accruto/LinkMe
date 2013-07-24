using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using STATSTG = System.Runtime.InteropServices.ComTypes.STATSTG;

namespace LinkMe.Framework.Utility.Win32
{
    public class ReadOnlyIStream
        : IStream, IDisposable
    {
        private Stream _stream;

        public ReadOnlyIStream(Stream stream)
        {
            _stream = stream;
        }

        #region Implementation of IStream

        void IStream.Read(byte[] pv, int cb, IntPtr pcbRead)
        {
            var cbRead = _stream.Read(pv, 0, cb);
            if (pcbRead != IntPtr.Zero)
                Marshal.WriteInt32(pcbRead, cbRead);
        }

        void IStream.Write(byte[] pv, int cb, IntPtr pcbWritten)
        {
            throw new NotImplementedException();
        }

        void IStream.Seek(long lMove, int dwOrigin, IntPtr plNewPosition)
        {
            var newPosition = _stream.Seek(lMove, (SeekOrigin)dwOrigin);
            if (plNewPosition != IntPtr.Zero)
                Marshal.WriteInt64(plNewPosition, newPosition);
        }

        void IStream.SetSize(long libNewSize)
        {
            throw new NotImplementedException();
        }

        void IStream.CopyTo(IStream pstm, long cb, IntPtr pcbRead, IntPtr pcbWritten)
        {
            throw new NotImplementedException();
        }

        void IStream.Commit(int grfCommitFlags)
        {
            throw new NotImplementedException();
        }

        void IStream.Revert()
        {
            throw new NotImplementedException();
        }

        void IStream.LockRegion(long libOffset, long cb, int dwLockType)
        {
            throw new NotImplementedException();
        }

        void IStream.UnlockRegion(long libOffset, long cb, int dwLockType)
        {
            throw new NotImplementedException();
        }

        void IStream.Stat(out STATSTG pstatstg, int grfStatFlag)
        {
            throw new NotImplementedException();
        }

        void IStream.Clone(out IStream ppstm)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Implementation of IDisposable

        void IDisposable.Dispose()
        {
            if (_stream != null)
            {
                _stream.Dispose();
                _stream = null;
            }

            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
