using System;
using System.Security.Cryptography;
using System.Text;
using Volo.Abp.Domain.Services;

namespace VCardOnAbp.Security;
public class SecurityManager : DomainService
{
    // Encrypt using RSA
    public string RSAEncrypt(string DataToEncrypt, string publicKeyString, bool DoOAEPPadding)
    {
        try
        {
            byte[] encryptedData;

            using (RSA rsa = RSA.Create())
            {
                rsa.ImportParameters(ConvertFromBase64(publicKeyString)); // Convert string back to RSAParameters
                encryptedData = rsa.Encrypt(Convert.FromBase64String(DataToEncrypt), DoOAEPPadding ? RSAEncryptionPadding.OaepSHA1 : RSAEncryptionPadding.Pkcs1);
            }

            return Convert.ToBase64String(encryptedData);
        }
        catch (CryptographicException e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    // Decrypt using RSA
    public string RSADecrypt(string DataToDecrypt, string privateKeyString, bool DoOAEPPadding)
    {
        try
        {
            byte[] decryptedData;

            using (RSA rsa = RSA.Create())
            {
                rsa.ImportParameters(ConvertFromBase64(privateKeyString)); // Convert string back to RSAParameters
                decryptedData = rsa.Decrypt(Convert.FromBase64String(DataToDecrypt), DoOAEPPadding ? RSAEncryptionPadding.OaepSHA1 : RSAEncryptionPadding.Pkcs1);
            }

            return Convert.ToBase64String(decryptedData);
        }
        catch (CryptographicException e)
        {
            Console.WriteLine(e.ToString());
            return null;
        }
    }

    public (string publicKeyString, string privateKeyString) GenerateKeys(int keySize = 2048)
    {
        using RSA rsa = RSA.Create();
        rsa.KeySize = keySize;

        // Export RSA keys as XML strings (or Base64)
        string publicKeyString = ConvertToBase64(rsa.ExportParameters(false)); // public key
        string privateKeyString = ConvertToBase64(rsa.ExportParameters(true)); // private key

        return (publicKeyString, privateKeyString);
    }

    private string ConvertToBase64(RSAParameters rsaParams)
    {
        StringBuilder sb = new();
        sb.AppendLine(Convert.ToBase64String(rsaParams.Modulus));
        sb.AppendLine(Convert.ToBase64String(rsaParams.Exponent));

        if (rsaParams.D != null) sb.AppendLine(Convert.ToBase64String(rsaParams.D));
        if (rsaParams.P != null) sb.AppendLine(Convert.ToBase64String(rsaParams.P));
        if (rsaParams.Q != null) sb.AppendLine(Convert.ToBase64String(rsaParams.Q));
        if (rsaParams.DP != null) sb.AppendLine(Convert.ToBase64String(rsaParams.DP));
        if (rsaParams.DQ != null) sb.AppendLine(Convert.ToBase64String(rsaParams.DQ));
        if (rsaParams.InverseQ != null) sb.AppendLine(Convert.ToBase64String(rsaParams.InverseQ));

        return sb.ToString();
    }
    private RSAParameters ConvertFromBase64(string base64String)
    {
        string[] lines = base64String.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        RSAParameters rsaParams = new()
        {
            Modulus = Convert.FromBase64String(lines[0]),
            Exponent = Convert.FromBase64String(lines[1])
        };

        if (lines.Length > 2) rsaParams.D = Convert.FromBase64String(lines[2]);
        if (lines.Length > 3) rsaParams.P = Convert.FromBase64String(lines[3]);
        if (lines.Length > 4) rsaParams.Q = Convert.FromBase64String(lines[4]);
        if (lines.Length > 5) rsaParams.DP = Convert.FromBase64String(lines[5]);
        if (lines.Length > 6) rsaParams.DQ = Convert.FromBase64String(lines[6]);
        if (lines.Length > 7) rsaParams.InverseQ = Convert.FromBase64String(lines[7]);

        return rsaParams;
    }
}
