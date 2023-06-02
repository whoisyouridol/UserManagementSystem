using UserManagementSystem.BAL.DTOs.Request;
using UserManagementSystem.BAL.DTOs.Response;

namespace UserManagementSystem.BAL
{
    public interface IBusinessAccessLayerService
    {
        Task<int> Add(AddUserRequestDTO dto);
        Task<GetUserResponseDTO> Get(int id);
        Task Update(UpdateUserRequestDTO dto);
        Task Delete(int id);
    }
}