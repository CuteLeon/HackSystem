using System.Text;
using Xunit;

namespace HackSystem.Cryptography.RSACryptography.Tests;

public class RSACryptographyServiceTests
{
    [Fact()]
    public void GenerateRSAKeysTest()
    {
        var keysProvider = new RSACryptoServiceProvider();
        var publicKey = keysProvider.ToXmlString(false);
        var privateKey = keysProvider.ToXmlString(true);
        var clientCryptography = new RSACryptoServiceProvider();
        var serverCryptography = new RSACryptoServiceProvider();
        clientCryptography.FromXmlString(publicKey);
        serverCryptography.FromXmlString(privateKey);

        const string request = "Hi, I am Leon, requesting to access the Hack System.";
        const string response = "Hi, Leon, great welcome to visit.";

        var encryptedRequest = clientCryptography.Encrypt(Encoding.UTF8.GetBytes(request), false);
        var decryptedRequest = Encoding.UTF8.GetString(serverCryptography.Decrypt(encryptedRequest, false));
        Assert.Equal(request, decryptedRequest);

        var responseBytes = Encoding.UTF8.GetBytes(response);
        var sign = serverCryptography.SignData(responseBytes, nameof(SHA256));
        var verified = clientCryptography.VerifyData(responseBytes, nameof(SHA256), sign);
        Assert.True(verified, "Can not verify sign of data.");
    }

    [Fact()]
    public void RSAEncryptDecryptTest()
    {
        IServiceCollection clientServiceDescriptors = new ServiceCollection();
        clientServiceDescriptors.AddRSACryptography(options =>
            options.RSAKeyParameters = "<RSAKeyValue><Modulus>t0O9fG5M0yzRvgqE1hrW5wOq+Uj71VP5O5fDoL2NC/9wavWgdi8+tuyhxNyHG0/FM5l8m1HrveyLMxA4wCvZ1s8e4AiRFIS+3x0avCeMPtiP9OV4v79fE4/a1DaOIDu0+lGSINLRO1+yRS1QKgc4RVioK8i/I5utkRO8u8GNk30=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>");
        var clientServiceProvider = clientServiceDescriptors.BuildServiceProvider();
        var clientRSACryptographyService = clientServiceProvider.GetService<IRSACryptographyService>();

        IServiceCollection serverServiceDescriptors = new ServiceCollection();
        serverServiceDescriptors.AddRSACryptography(options =>
            options.RSAKeyParameters = "<RSAKeyValue><Modulus>t0O9fG5M0yzRvgqE1hrW5wOq+Uj71VP5O5fDoL2NC/9wavWgdi8+tuyhxNyHG0/FM5l8m1HrveyLMxA4wCvZ1s8e4AiRFIS+3x0avCeMPtiP9OV4v79fE4/a1DaOIDu0+lGSINLRO1+yRS1QKgc4RVioK8i/I5utkRO8u8GNk30=</Modulus><Exponent>AQAB</Exponent><P>1uxgavaqw24CPLyArbSWroz2xbzpFEs+K7yqwZYFUYUyn+r6ntxtPtD3J4/CD/3NYC8xPNQrEA1vSbjtx9MKew==</P><Q>2kpi7QRFVD2USF4dLYj+CVN7RvWIV5hksRtPk/mnSXDvSKt2nP3z1b2g5v9TCtBzIFoUC598F3ZRoG1iiFxUZw==</Q><DP>U7XhoAfPXysb5/grznyGLBpvi3kW93aPEo37nEcIb0YH/82QLAwC6PKPMXOGzJ+4PHxlGyIwW6I/9GD5DFmgvQ==</DP><DQ>Wa2tronOoakavhBMFGTvWI1/W8uLU9E7rLb3nmc2HqnS5BvtAtohznG1JLFIQG0anvPiwFOo+0qUhj/p9vNXiQ==</DQ><InverseQ>EQQfGNU7na4FgPnX9M+mnDb7r+Hpg0JnP5mVyhZ1tOWw0VV8B7U0FKsMQuTHYfCu+90nUHUaVaigGGijQopeRA==</InverseQ><D>RH8mII7dWgSjdENcOOYZMokVa00TCz3ypopnzPlr8XMM4n1h9ypbZ2V0ZS8DtHqRO2L/xtHMqdTyhihm/bSmG1itO6Poyj6WeYiOasSclkvXLbG2FOpw9cOCqkrE8BTBoANFJvDm3No+wGMBSb+/NIZOrlp/XEnSowIYPZpG2qU=</D></RSAKeyValue>");
        var serverServiceProvider = serverServiceDescriptors.BuildServiceProvider();
        var serverRSACryptographyService = serverServiceProvider.GetService<IRSACryptographyService>();

        foreach (var (Request, Response) in new (string Request, string Response)[]
        {
            ("Hi, I am Leon, requesting to access the Hack System.", "Hi, Leon, great welcome to visit."),
            ("I want to see my assets.", "Sure, Here is your asset amount: $10,000,000"),
            ("Is there anything I need to pay attention to?", "Nope. Have a good day."),
            ("All right, log out now.", "Logged out, hope to see you soon.")
        })
        {
            var encryptedRequest = clientRSACryptographyService.RSAEncrypt(Request);
            var decryptedRequest = serverRSACryptographyService.RSADecrypt(encryptedRequest);
            Assert.Equal(Request, decryptedRequest);

            var sign = serverRSACryptographyService.SignData(Response);
            var verified = clientRSACryptographyService.VerifyData(Response, sign);
            Assert.True(verified, "Can not verify sign of data.");
        }

        clientRSACryptographyService.Dispose();
        serverRSACryptographyService.Dispose();
    }
}
