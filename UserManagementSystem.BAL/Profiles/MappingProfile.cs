using AutoMapper;
using UserManagementSystem.BAL.DTOs.Request;
using UserManagementSystem.BAL.DTOs.Response;
using UserManagementSystem.DAL.DB.Models;
using UserManagementSystem.DAL.Models.RequestModels;
using UserManagementSystem.DAL.Models.ResponseModels;

namespace UserManagementSystem.BAL.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AddUserRequestDTO, AddUserRequestModel>();
            CreateMap<User, GetUserResponseModel>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Profile.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Profile.LastName))
                .ForMember(dest => dest.PersonalNumber, opt => opt.MapFrom(src => src.Profile.PersonalNumber));
            CreateMap<GetUserResponseModel, GetUserResponseDTO>();

            CreateMap<UpdateUserRequestDTO, UpdateUserRequestModel>();
            CreateMap<UpdateUserRequestModel, User>();
            CreateMap<UpdateUserRequestModel, UserProfile>();
        }
    }
}