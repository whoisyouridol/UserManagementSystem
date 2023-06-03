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
        #region external
        Task<List<AlbumResponseModel>> GetAlbums(int userId);
        Task<List<PostResponseModel>> GetPosts(int userId);
        Task<List<TodoResponseModel>> GetTodos(int userId);
        #endregion
        Task<int> ValidateUser(string email, string password);
    }
}
