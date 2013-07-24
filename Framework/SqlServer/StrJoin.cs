using System;
using System.Data.SqlTypes;
using System.IO;
using System.Text;
using Microsoft.SqlServer.Server;

namespace LinkMe.Framework.SqlServer
{
    [Serializable]
    [SqlUserDefinedAggregate(
        Format.UserDefined,
        IsInvariantToNulls = true,
        IsInvariantToDuplicates = false,
        IsInvariantToOrder = false,
        MaxByteSize = 8000)]
    public class StrJoin : IBinarySerialize
    {
        private const string _separator = " ";
        private StringBuilder _buffer;

        #region Aggregation Methods

        public void Init()
        {
            _buffer = new StringBuilder();
        }

        public void Accumulate(SqlString value)
        {
            if (value.IsNull)
                return;

            _buffer.Append(value.Value).Append(_separator);
        }

        public void Merge(StrJoin other)
        {
            _buffer.Append(other._buffer);
        }

        public SqlString Terminate()
        {
            string result = string.Empty;

            // Delete the trailing separator, if any
            if (_buffer.Length > 0)
                result = _buffer.ToString(0, _buffer.Length - _separator.Length);

            return new SqlString(result);
        }

        #endregion

        #region IBinarySerialize Methods

        public void Read(BinaryReader reader)
        {
            _buffer = new StringBuilder(reader.ReadString());
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(_buffer.ToString());
        }

        #endregion
    }
}