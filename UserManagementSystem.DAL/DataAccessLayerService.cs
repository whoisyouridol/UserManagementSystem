using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using UserManagementSystem.DAL.DB;
using UserManagementSystem.DAL.DB.Models;
using UserManagementSystem.DAL.Models.RequestModels;
using UserManagementSystem.DAL.Models.ResponseModels;

namespace UserManagementSystem.DAL
{
    [Authorize]
    public class DataAccessLayerService : IDataAccessLayerService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _httpClientFactory;
        private const string _baseBath = "https://jsonplaceholder.typicode.com/";
        public DataAccessLayerService(ApplicationDbContext context, IMapper mapper, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
        }
        #region users
        public async Task<int> Add(AddUserRequestModel request)
        {
            var user = new User
            {
                Email = request.Email,
                Username = request.Username,
                IsActive = true,
                PasswordHash = HashPassword(request.Password),
                Profile = new UserProfile
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    PersonalNumber = request.PersonalNumber,
                }
            };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user.Id;
        }

        public async Task Delete(int id)
        {
            var user = await FindUser(id);

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<GetUserResponseModel> Get(int id)
        {
            var user = await FindUser(id);

            return _mapper.Map<GetUserResponseModel>(user);
        }

        public async Task Update(UpdateUserRequestModel request)
        {
            var user = await FindUser(request.Id);
            user.Username = request.Username;
            user.Email = request.Email;
            user.PasswordHash = HashPassword(request.Password);
            user.IsActive = request.IsActive;
            user.Profile.FirstName = request.FirstName;
            user.Profile.LastName = request.LastName;
            user.Profile.PersonalNumber = request.PersonalNumber;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
        private static string HashPassword(string password)
        {
            using var sha256Hash = SHA256.Create();

            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

            StringBuilder builder = new();
            for (int i = 0; i < bytes.Length; i++)
                builder.Append(bytes[i].ToString("x2"));

            return builder.ToString();
        }
        private async Task<User> FindUser(int id)
        {
            return (await _context.Users.Include(x => x.Profile).FirstOrDefaultAsync(x => x.Id == id)) ?? throw new KeyNotFoundException();
        }
        private async Task<string> GetData(string endpoint)
        {
            var client = _httpClientFactory.CreateClient();
            var result = await client.GetAsync($"{_baseBath}{endpoint}");
            var jsonResult = await result.Content.ReadAsStringAsync() ?? throw new KeyNotFoundException();
            return jsonResult;
        }
        #endregion
        #region external
        public async Task<List<AlbumResponseModel>> GetAlbums(int userId)
        {
            var json = await GetData("albums");

            var converted = JsonConvert.DeserializeObject<List<AlbumResponseModel>>(json);
            var response = converted.Where(x => x.UserId == userId).ToList();

            if (response.Count == 0 || response == null)
            {
                throw new KeyNotFoundException();
            }
            return response;
        }


        public async Task<List<PostResponseModel>> GetPosts(int userId)
        {
            var postsJson = await GetData("posts");
            var posts = JsonConvert.DeserializeObject<List<PostResponseModel>>(postsJson) ?? throw new KeyNotFoundException();
            posts = posts.Where(x => x.UserId == userId).ToList();
            var commentsJson = await GetData("comments");
            var comments = JsonConvert.DeserializeObject<List<CommentResponseModel>>(commentsJson) ?? throw new KeyNotFoundException();
            foreach (var post in posts)
            {
                var postComments = comments.Where(x => x.PostId == post.Id);
                if (postComments.Any())
                {
                    post.Comments = new List<CommentResponseModel>();
                    post.Comments.AddRange(postComments);
                }
            }
            return posts;
        }

        public async Task<List<TodoResponseModel>> GetTodos(int userId)
        {
            var json = await GetData("todos");

            var converted = JsonConvert.DeserializeObject<List<TodoResponseModel>>(json);
            var response = converted.Where(x => x.UserId == userId).ToList();

            if (response.Count == 0 || response == null)
            {
                throw new KeyNotFoundException();
            }
            return response;
        }

        public async Task<int> ValidateUser(string email, string password)
        {
            var hash = HashPassword(password);
            var user = _context.Users.FirstOrDefaultAsync(x => x.Email == email && hash == x.PasswordHash);
            if (user != null)
                return user.Id;
            else return -1;
        }
        #endregion
    }
}