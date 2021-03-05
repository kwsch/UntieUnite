using System;
using System.Collections.Generic;

namespace UntieUnite.Core
{
    public static class HashUtil
    {
        public static uint GetHashCode(IEnumerable<char> value)
        {
            uint hash = 0;
            foreach (var c in value)
                hash = (hash * 29) + c;
            return hash;
        }

        public static uint GetHashCode(IEnumerable<byte> data)
        {
            uint hash = 17;
            foreach (byte b in data)
                hash = (hash * 31) + b;
            return hash;
        }

        public static uint GetHashCode(ReadOnlySpan<byte> data)
        {
            uint hash = 17;
            foreach (byte b in data)
                hash = (hash * 31) + b;
            return hash;
        }
    }
}
