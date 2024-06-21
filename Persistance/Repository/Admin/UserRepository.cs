using Application.Services.Interfaces.IRepository.Admin;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Persistance.Context;

namespace Persistance.Repository.Admin
{
    public class UserRepository : IUserRepository
    {
        private readonly StoreLineContext _context;
        private readonly IMapper _mapper;

        public UserRepository(StoreLineContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Users>> GetAllInfoAboutUsersAsync()
        {
            try
            {
                var users = await _context.Users.ToListAsync();

                return _mapper.Map<List<Users>>(users);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in UserRepository -> GetAllInfoAboutUserAsync: {ex.Message}");
            }
        }
    }
}
