using System.Text;
using HackSystem.Cryptography.Options;

namespace HackSystem.Cryptography.RSACryptography;

public class RSACryptographyService : IRSACryptographyService
{
    private const string SignatureAlgorithm = nameof(SHA256);
    private readonly RSACryptoServiceProvider rsaCryptoServiceProvider;
    private readonly IOptions<RSACryptographyOptions> options;
    private bool disposedValue;

    public RSACryptographyService(
        RSACryptoServiceProvider rsaCryptoServiceProvider,
        IOptions<RSACryptographyOptions> options)
    {
        this.rsaCryptoServiceProvider = rsaCryptoServiceProvider;
        this.options = options;

        this.rsaCryptoServiceProvider.FromXmlString(this.options.Value.RSAKeyParameters);
    }

    public (string PublicKey, string PrivateKey) GenerateRSAKeys()
    {
        using RSACryptoServiceProvider provider = new();
        var publicKey = provider.ToXmlString(false);
        var privateKey = provider.ToXmlString(true);
        return (publicKey, privateKey);
    }

    public byte[] RSAEncrypt(byte[] source) => this.rsaCryptoServiceProvider.Encrypt(source, false);

    public byte[] RSADecrypt(byte[] source)
    {
        return this.rsaCryptoServiceProvider.PublicOnly
            ? throw new InvalidOperationException("Can not decrypt data when has only public key. Please just sign data at server side and verify sign at client side.")
            : this.rsaCryptoServiceProvider.Decrypt(source, false);
    }

    public string RSAEncrypt(string source)
    {
        var bytes = Encoding.UTF8.GetBytes(source);
        var encrypted = this.RSAEncrypt(bytes);
        var base64 = Convert.ToBase64String(encrypted);
        return base64;
    }

    public string RSADecrypt(string source)
    {
        var bytes = Convert.FromBase64String(source);
        var decrypted = this.RSADecrypt(bytes);
        var result = Encoding.UTF8.GetString(decrypted);
        return result;
    }

    public byte[] SignData(byte[] source) => this.rsaCryptoServiceProvider.SignData(source, SignatureAlgorithm);

    public string SignData(string source)
    {
        var bytes = Encoding.UTF8.GetBytes(source);
        var encrypted = this.SignData(bytes);
        var base64 = Convert.ToBase64String(encrypted);
        return base64;
    }

    public bool VerifyData(byte[] source, byte[] sign) => this.rsaCryptoServiceProvider.VerifyData(source, SignatureAlgorithm, sign);

    public bool VerifyData(string source, string sign)
    {
        var sourceBytes = Encoding.UTF8.GetBytes(source);
        var signBytes = Convert.FromBase64String(sign);
        return this.VerifyData(sourceBytes, signBytes);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposedValue)
        {
            if (disposing)
            {
                this.rsaCryptoServiceProvider.Dispose();
            }

            this.disposedValue = true;
        }
    }

    public void Dispose()
    {
        this.Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
