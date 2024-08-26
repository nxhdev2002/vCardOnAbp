using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace VCardOnAbp.Security
{
    public interface IDataValidatorAppService : ISingletonDependency
    {
        public string EncryptData(string Input);
        public Task<string> DecryptData(string Input);
        public bool ValidateData(string OriginalData, string EncryptedData);
    }
}
