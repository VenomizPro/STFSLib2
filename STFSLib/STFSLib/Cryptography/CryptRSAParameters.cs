using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace STFSLib.Cryptography
{
    public struct CryptRSAParameters
    {
        public CryptRSA Rsa;
        public byte[] M;
        public byte[] P;
        public byte[] Q;
        public byte[] DP;
        public byte[] DQ;
        public byte[] CR;
    }
}
