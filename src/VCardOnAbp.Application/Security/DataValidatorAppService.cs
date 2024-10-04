using Microsoft.Extensions.Configuration;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace VCardOnAbp.Security
{
    public class DataValidatorAppService(IConfiguration configuration) : IDataValidatorAppService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly string KeyPath = configuration["App:KeyPath"]!;

        public async Task<string> DecryptData(string Input)
        {
            //string privateKeyPem = await File.ReadAllTextAsync(KeyPath);
            //RSACryptoServiceProvider rsa = GetRSAProviderFromPem(privateKeyPem);
            //byte[] encryptedBytes = Convert.FromBase64String(Input);
            //byte[] decryptedBytes = rsa.Decrypt(encryptedBytes, true);
            //return Encoding.UTF8.GetString(decryptedBytes);
            return "";
        }

        public string EncryptData(string Input)
        {
            throw new NotImplementedException();
        }

        public bool ValidateData(string OriginalData, string EncryptedData)
        {
            string publicKeyPem = File.ReadAllText(KeyPath);

            RSACryptoServiceProvider rsa = GetRSAProviderFromPem(publicKeyPem);

            byte[] originalDataBytes = Encoding.UTF8.GetBytes(OriginalData);
            byte[] signedDataBytes = Convert.FromBase64String(EncryptedData);

            bool verified = rsa.VerifyData(originalDataBytes, new SHA256CryptoServiceProvider(), signedDataBytes);
            return verified;
        }

        private RSACryptoServiceProvider GetRSAProviderFromPem(string pemString)
        {
            PemReader pemReader = new PemReader(new System.IO.StringReader(pemString));
            AsymmetricCipherKeyPair keyPair = pemReader.ReadObject() as AsymmetricCipherKeyPair;
            if (keyPair == null)
            {
                throw new Exception("Invalid PEM string");
            }

            RsaPrivateCrtKeyParameters rsaPrivateKey = (RsaPrivateCrtKeyParameters)keyPair.Private;
            RSAParameters rsaParameters = DotNetUtilities.ToRSAParameters(rsaPrivateKey);

            var rsa = new RSACryptoServiceProvider();
            rsa.ImportParameters(rsaParameters);
            return rsa;
        }
    }
}
