using Application.DtoModels.Models.User.Profile;
using Application.DtoModels.Response.User.Profile;
using Application.Services.Interfaces.IServices.User;
using Application.UnitOfWork;
using AutoMapper;

namespace Application.Services.Implementations.User
{
    public class ProfileService : IProfileService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProfileService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProfileResposneDto> EditProfileAsync(EditProfileDto model, string userId)
        {
            try
            {
                var result = await _unitOfWork.ProfileRepository.EditProfileAsync(model, userId);
               
                return _mapper.Map<ProfileResposneDto>(result);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in ProfileService -> EditProfileAsync: {ex.Message}");
            }
        }

        public async Task<ProfileResposneDto> SetAddres(string userId, string address)
        {
            try
            {
                var result = await _unitOfWork.ProfileRepository.SetAddres(userId, address);

                return _mapper.Map<ProfileResposneDto>(result);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in ProfileService -> SetAddres: {ex.Message}");
            }
        }

        public async Task<ProfileResposneDto> GetAllInfoAsync(string userId)
        {
            try
            {
                var result = await _unitOfWork.ProfileRepository.GetAllInfoAsync(userId);

                return _mapper.Map<ProfileResposneDto>(result);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in ProfileService -> GetAllInfoAsync: {ex.Message}");
            }
        }

    }
}
