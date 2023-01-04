using System.Collections.Generic;
using System.IO;

namespace Classes.DaxFiles
{
    class DaxFile
    {
        private readonly Dictionary<int, byte[]> _entries;

        internal DaxFile(string filename)
        {
            _entries = new Dictionary<int, byte[]>();

            LoadFile(filename);
        }

        private void LoadFile(string filename)
        {
            using (var fileStream = new ReadOnlyFileStream(filename))
            using (var fileA = new BinaryReader(fileStream.BaseStream))
            {
                int dataOffset = fileA.ReadInt16() + 2;

                List<DaxHeaderEntry> headers = new List<DaxHeaderEntry>();

                const int headerEntrySize = 9;

                for (int i = 0; i < ((dataOffset - 2) / headerEntrySize); i++)
                {
                    DaxHeaderEntry header = new DaxHeaderEntry();
                    header.Id = fileA.ReadByte();
                    header.Offset = fileA.ReadInt32();
                    header.DataSize = fileA.ReadInt16();
                    header.CompressedSize = fileA.ReadUInt16();

                    headers.Add(header);
                }

                foreach (DaxHeaderEntry header in headers)
                {
                    byte[] compressed = new byte[header.CompressedSize];
                    byte[] data = new byte[header.DataSize];

                    fileA.BaseStream.Seek(dataOffset + header.Offset, SeekOrigin.Begin);

                    compressed = fileA.ReadBytes(header.CompressedSize);

                    data = Extract(header.DataSize, compressed);

                    _entries.Add(header.Id, data);
                }
            }
        }

        private byte[] Extract(int outputSize, byte[] compressed)
        {
            var output = new byte[outputSize];

            var inputIndex = 0;
            var outputIndex = 0;

            while (inputIndex < compressed.Length)
            {
                var runLength = (sbyte)compressed[inputIndex];

                if (runLength >= 0)
                {
                    for (int i = 0; i <= runLength; i++)
                    {
                        output[outputIndex + i] = compressed[inputIndex + i + 1];
                    }

                    inputIndex += runLength + 2;
                    outputIndex += runLength + 1;
                }
                else
                {
                    runLength = (sbyte)(-runLength);

                    for (int i = 0; i < runLength; i++)
                    {
                        output[outputIndex + i] = compressed[inputIndex + 1];
                    }

                    inputIndex += 2;
                    outputIndex += runLength;
                }
            }

            return output;
        }

        internal byte[] GetData(int blockId)
        {
            byte[] orig;
            if (_entries.TryGetValue(blockId, out orig) == false)
            {
                return null;
            }

            return (byte[])orig.Clone();
        }
    }

}
