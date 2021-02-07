using System.Threading.Tasks;
using Amazon.SimpleSystemsManagement.Model;

namespace aws_parameter_store_manager
{
    public interface IParameterStoreService
    {
        Task<GetParameterResponse> GetParameter(GetParameterRequest getParameterRequest);
        Task<GetParametersByPathResponse> GetAllParameters(string parameterPath = "/");
        Task<PutParameterResponse> CreateParameter(PutParameterRequest parameterToCreate);
        Task<PutParameterResponse> UpdateParameter(PutParameterRequest parameterToUpdate);
        Task<DeleteParameterResponse> DeleteParameter(DeleteParameterRequest parameterToDelete);

    }
}