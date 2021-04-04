using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.SimpleSystemsManagement.Model;

namespace aws_parameter_store_manager
{
    public interface IParameterStoreService
    {
        Task<GetParameterResponse> GetParameter(GetParameterRequest getParameterRequest);
        Task<List<Parameter>> GetAllParameters(string parameterPath = "/");
        Task<PutParameterResponse> CreateParameter(PutParameterRequest parameterToCreate);
        Task<PutParameterResponse> UpdateParameter(Parameter parameterToUpdate);
        Task<DeleteParameterResponse> DeleteParameter(DeleteParameterRequest parameterToDelete);

    }
}