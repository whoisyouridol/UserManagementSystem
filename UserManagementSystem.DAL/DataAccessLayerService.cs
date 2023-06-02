using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using UserManagementSystem.DAL.DB;
using UserManagementSystem.DAL.DB.Models;
using UserManagementSystem.DAL.Models.RequestModels;
using UserManagementSystem.DAL.Models.ResponseModels;

namespace UserManagementSystem.DAL
{
    public class DataAccessLayerService : IDataAccessLayerService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DataAccessLayerService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

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
            user.Profile.LastName= request.LastName;
            user.Profile.PersonalNumber= request.PersonalNumber;
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
            return  (await _context.Users.Include(x => x.Profile).FirstOrDefaultAsync(x => x.Id == id)) ?? throw new KeyNotFoundException();
        }
    }
}