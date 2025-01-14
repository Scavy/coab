﻿using Logging;
using System;
using System.IO;

namespace Classes
{
    class ReadOnlyFileStream : IDisposable
    {
        public Stream BaseStream { get; private set; }
        public bool FileExists { get; }

        private readonly string _filePath;

        public ReadOnlyFileStream(string fileName)
        {
            _filePath = System.IO.File.Exists(fileName) ? fileName : "Data\\" + fileName;

            FileExists = System.IO.File.Exists(_filePath);
            Open();
        }

        public void Open()
        {
            if (!FileExists)
            {
                Logger.Log("File not found:{0}", Path.GetFullPath(_filePath));
                return;
            }
            BaseStream = System.IO.File.Open(_filePath, FileMode.Open, FileAccess.Read);
            Logger.Debug("Reading File:{0}", _filePath);
        }

        public void Dispose()
        {
            Logger.Debug("Closing File:{0}", _filePath);
            BaseStream?.Dispose();
        }

        internal void Seek(long offset, SeekOrigin seekOrigin)
        {
            BaseStream?.Seek(offset, seekOrigin);
        }

        internal void Read(byte[] array, int offset, int count)
        {
            BaseStream?.Read(array, offset, count);
        }
    }
}