using System;

namespace HackSystem.Cryptography.RSACryptography
{
    public interface IRSACryptographyService : IDisposable
    {
        (string PublicKey, string PrivateKey) GenerateRSAKeys();

        byte[] RSADecrypt(byte[] source);

        string RSADecrypt(string source);

        byte[] RSAEncrypt(byte[] source);

        string RSAEncrypt(string source);

        byte[] SignData(byte[] source);
        
        string SignData(string source);
        
        bool VerifyData(byte[] source, byte[] sign);
        
        bool VerifyData(string source, string sign);
    }
}
