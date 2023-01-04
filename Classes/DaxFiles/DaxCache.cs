using System.Collections.Generic;

namespace Classes.DaxFiles
{
    public static class DaxCache
    {
        private static readonly Dictionary<string, DaxFile> FileCache = new Dictionary<string, DaxFile>();

        public static byte[] LoadDax(string fileName, int blockId)
        {
            fileName = fileName.ToLower();

            if (!FileCache.TryGetValue(fileName, out var daxFileCache))
            {
                daxFileCache = new DaxFile(fileName);
                FileCache.Add(fileName, daxFileCache);
            }

            return daxFileCache.GetData(blockId);
        }
    }
}
