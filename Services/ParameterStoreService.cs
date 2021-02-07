using System.Threading.Tasks;
using Amazon.SimpleSystemsManagement;
using Amazon.SimpleSystemsManagement.Model;

namespace aws_parameter_store_manager
{
    public class ParameterStoreService : IParameterStoreService
    {
        public async Task<PutParameterResponse> CreateParameter(PutParameterRequest parameterToCreate)
        {
            using (var client = new AmazonSimpleSystemsManagementClient())
            {
                parameterToCreate.Type = "String";
                return await client.PutParameterAsync(parameterToCreate);
            }
        }

        public async Task<DeleteParameterResponse> DeleteParameter(DeleteParameterRequest parameterToDelete)
        {
            throw new System.NotImplementedException();
        }

        public async Task<GetParameterResponse> GetParameter(GetParameterRequest getParameterRequest)
        {
            throw new System.NotImplementedException();
        }

        public async Task<PutParameterResponse> UpdateParameter(PutParameterRequest parameterToUpdate)
        {
            throw new System.NotImplementedException();
        }

        async Task<GetParametersByPathResponse> IParameterStoreService.GetAllParameters(string parameterPath)
        {
            using (var client = new AmazonSimpleSystemsManagementClient())
            {
                return await client.GetParametersByPathAsync(new GetParametersByPathRequest()
                {
                    Path = parameterPath,
                    WithDecryption = true,
                    Recursive = true
                });
            }
        }

    }
}