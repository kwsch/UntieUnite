using Ionic.Zlib;

namespace UntieUnite.Core
{
    public static class FileUtil
    {
        public static byte[] DecompressZlib(byte[] data)
        {
            return DeflateStream.UncompressBuffer(data);
        }
    }
}
