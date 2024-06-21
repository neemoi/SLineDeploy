using Application.DtoModels.Models.User.Profile;
using Application.DtoModels.Response.User.Profile;

namespace Application.Services.Interfaces.IServices.User
{
    public interface IProfileService
    {
        Task<ProfileResposneDto> EditProfileAsync(EditProfileDto model, string userId);

        Task<ProfileResposneDto> SetAddres(string userId, string address);

        Task<ProfileResposneDto> GetAllInfoAsync(string userId);
    }
}
