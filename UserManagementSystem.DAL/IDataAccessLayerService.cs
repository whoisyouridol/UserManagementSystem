using UserManagementSystem.DAL.Models.RequestModels;
using UserManagementSystem.DAL.Models.ResponseModels;

namespace UserManagementSystem.DAL
{
    public interface IDataAccessLayerService
    {
        Task<int> Add(AddUserRequestModel dto);
        Task<GetUserResponseModel> Get(int id);
        Task Update(UpdateUserRequestModel dto);
        Task Delete(int id);
    }
}
