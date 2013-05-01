using STFSLib.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace STFSLib.STFS
{
    public class KeyVault
    {
        public byte[] ConsoleCertificate;
        public EndianIO IO;
        public RSAParameters SigningParams;
        public CryptRSAParameters cryptRsaPrv;

        public KeyVault(EndianIO IO)
        {
            this.IO = IO;
            this.IO.Open();
        }

        public void LoadSigningParameters()
        {
            int num = 0;
            switch (IO.Stream.Length)
            {
                case 0x3ff0:
                    num = 8;
                    break;

                case 0x4000:
                    num = 0x18;
                    break;

                default:
                    throw new Exception("Invalid keyvault loaded.");
            }
        }
    }
}
