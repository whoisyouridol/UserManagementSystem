using AutoMapper;
using UserManagementSystem.BAL.DTOs.Request;
using UserManagementSystem.BAL.DTOs.Response;
using UserManagementSystem.DAL;
using UserManagementSystem.DAL.Models.RequestModels;

namespace UserManagementSystem.BAL
{
    public class BusinessAccessLayerService : IBusinessAccessLayerService
    {
        private readonly IDataAccessLayerService _service;
        private readonly IMapper _mapper;
        public BusinessAccessLayerService(IDataAccessLayerService dataAccessLayerService, IMapper mapper)
        {
            _service = dataAccessLayerService;
            _mapper = mapper;
        }

        public async Task<int> Add(AddUserRequestDTO dto)
        {
            var result = await _service.Add(_mapper.Map<AddUserRequestModel>(dto));
            return result;
        }

        public async Task Delete(int id)
        {
            await _service.Delete(id);
        }

        public async Task<GetUserResponseDTO> Get(int id)
        {
            var result = await _service.Get(id);
            return _mapper.Map<GetUserResponseDTO>(result);
        }

        public async Task Update(UpdateUserRequestDTO dto)
        {
            await _service.Update(_mapper.Map<UpdateUserRequestModel>(dto));
        }

        public async Task<List<AlbumResponseDTO>> GetAlbums(int userId)
        {
            var result = await _service.GetAlbums(userId);
            return _mapper.Map<List<AlbumResponseDTO>>(result);
        }
        public async Task<List<TodoResponseDTO>> GetTodos(int userId)
        {
            var result = await _service.GetTodos(userId);
            return _mapper.Map<List<TodoResponseDTO>>(result);
        }
        public async Task<List<PostResponseDTO>> GetPosts(int userId)
        {
            var result = await _service.GetPosts(userId);
            return _mapper.Map<List<PostResponseDTO>>(result);
        }

        public async Task<int> ValidateUser(string email, string password)
        {
            return await _service.ValidateUser(email, password);
        }
    }
}
