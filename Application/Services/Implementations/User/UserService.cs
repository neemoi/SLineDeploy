using Application.DtoModels.Response.User.Profile;
using Application.Services.Interfaces.IServices.Admin;
using Application.UnitOfWork;
using AutoMapper;

namespace Application.Services.Implementations.User
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<ProfileResposneDto>> GetAllInfoAboutUsersAsync()
        {
            try
            {
                var result = await _unitOfWork.UserRepository.GetAllInfoAboutUsersAsync();

                return _mapper.Map<List<ProfileResposneDto>>(result);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in UserService -> GetAllInfoAboutUsersAsync: {ex.Message}");
            }
        }
    }
}
