using Ionic.Zlib;

namespace UntieUnite.Core
{
    /// <summary>
    /// Zlib logic to decompress. Should probably be merged back into <see cref="ResDecoder"/>, but we'll just leave this accessible.
    /// </summary>
    public static class FileUtil
    {
        public static byte[] DecompressZlib(byte[] data)
        {
            return DeflateStream.UncompressBuffer(data);
        }
    }
}
