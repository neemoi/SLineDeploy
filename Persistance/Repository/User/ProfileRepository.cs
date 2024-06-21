using Application.DtoModels.Models.User.Profile;
using Application.Services.Interfaces.IRepository.User;
using Application.Services.Interfaces.IServices.Admin;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace Persistance.Repository.User
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly IMapper _mapper;
        private readonly UserManager<Users> _userManager;
        private readonly IEmailService _emailService;

        public ProfileRepository(IMapper mapper, UserManager<Users> userManager, IEmailService emailService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _emailService = emailService;
        }

        public async Task<Users> EditProfileAsync(EditProfileDto model, string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);

                if (user != null)
                {
                    if (!string.IsNullOrEmpty(model.CurrentPassword) && !string.IsNullOrEmpty(model.NewPassword))
                    {
                        var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

                        if (!changePasswordResult.Succeeded)
                        {
                            throw new InvalidOperationException($"Error changing password: {string.Join(", ", changePasswordResult.Errors.Select(e => e.Description))}");
                        }

                        if (!string.IsNullOrEmpty(user.Email))
                        {
                            await _emailService.SendEmailAsync(user.Email, "Уведомление о смене пароля в системе SLine", "Ваш пароль был успешно изменен");
                        }
                        else
                        {
                            throw new InvalidOperationException("User email is null or empty or invalid.");
                        }
                    }

                    if (!string.IsNullOrEmpty(model.Email) && model.Email != user.Email)
                    {
                        user.Email = model.Email;
                    }

                    if (!string.IsNullOrEmpty(model.UserName) && model.UserName != user.UserName)
                    {
                        user.UserName = model.UserName;
                    }

                    if (!string.IsNullOrEmpty(model.PhoneNumber) && model.PhoneNumber != user.PhoneNumber)
                    {
                        user.PhoneNumber = model.PhoneNumber;
                    }

                    if (!string.IsNullOrEmpty(model.Address) && model.Address != user.Address)
                    {
                        user.Address = model.Address;
                    }

                    if (model.Birthday.HasValue)
                    {
                        user.Birthday = DateTime.SpecifyKind(model.Birthday.Value, DateTimeKind.Unspecified);
                    }

                    var updateResult = await _userManager.UpdateAsync(user);

                    if (updateResult.Succeeded)
                    {
                        return user;
                    }
                    else
                    {
                        throw new InvalidOperationException($"Error updating user profile: {string.Join(", ", updateResult.Errors.Select(e => e.Description))}");
                    }
                }
                else
                {
                    throw new ArgumentNullException($"User with id {userId} not found.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in ProfileRepository -> EditProfileAsync: {ex.Message}");
            }
        }

        public async Task<Users> SetAddres(string userId, string address)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);

                if (user != null)
                {
                    _mapper.Map(address, user);

                    var updateResult = await _userManager.UpdateAsync(user);

                    if (updateResult.Succeeded)
                    {
                        return user;
                    }
                    else
                    {
                        throw new InvalidOperationException($"Error updating user address: {string.Join(", ", updateResult.Errors.Select(e => e.Description))}");
                    }
                }
                else
                {
                    throw new ArgumentNullException($"User with id {userId} not found.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in ProfileRepository -> SetAddres: {ex.Message}");
            }
        }

        public async Task<Users> GetAllInfoAsync(string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);

                if (user != null)
                {
                    return user;
                }
                else
                {
                    throw new ArgumentNullException($"User with id {userId} not found.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in ProfileRepository -> GetAllInfoAsync: {ex.Message}");
            }
        }
    }
}
