using System.Collections.Generic;
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
                return await client.PutParameterAsync(parameterToCreate);
            }
        }

        public async Task<DeleteParameterResponse> DeleteParameter(DeleteParameterRequest parameterToDelete)
        {
            using (var client = new AmazonSimpleSystemsManagementClient())
            {
                return await client.DeleteParameterAsync(parameterToDelete);
            }
        }

        public async Task<GetParameterResponse> GetParameter(GetParameterRequest getParameterRequest)
        {
            using (var client = new AmazonSimpleSystemsManagementClient())
            {
                return await client.GetParameterAsync(getParameterRequest);
            }
        }

        public async Task<PutParameterResponse> UpdateParameter(Parameter parameterToUpdate)
        {
            using (var client = new AmazonSimpleSystemsManagementClient())
            {
                return await client.PutParameterAsync(new PutParameterRequest(){
                    Name = parameterToUpdate.Name,
                    DataType = parameterToUpdate.DataType,
                    Type = parameterToUpdate.Type,
                    Value = parameterToUpdate.Value,
                    Overwrite = true
                });
            }
        }

        async Task<List<Parameter>> IParameterStoreService.GetAllParameters(string parameterPath)
        {
            List<Parameter> allParameters = new List<Parameter>();
            string nextToken = null;
            using (var client = new AmazonSimpleSystemsManagementClient())
            {
                do
                {
                    var request = new GetParametersByPathRequest()
                    {
                        Path = parameterPath,
                        WithDecryption = true,
                        Recursive = true,
                        NextToken = nextToken ?? null
                    };
                    var result = await client.GetParametersByPathAsync(request);
                    allParameters.AddRange(result.Parameters);
                    nextToken = result.NextToken;
                } while (!string.IsNullOrEmpty(nextToken));
            }
            return allParameters;
        }

    }
}