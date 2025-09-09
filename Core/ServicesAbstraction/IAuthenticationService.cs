using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DataTransferObjects.IdentityModuleDtos;

namespace ServicesAbstraction
{
    public interface IAuthenticationService
    {
        Task<UserDto> LoginAsync(LoginDto loginDto);
        Task<UserDto> RegisterAsync(RegisterDto registerDto);
        Task<bool> CheckEmailAsync(string email);
        Task<UserDto> GetCurrentUserAsync(string email);
        Task<AddressDto> GetCurrentUserAddressAsync(string email);
        Task<AddressDto> UpdateCurrentAddressAsync(string email ,AddressDto addressDto);
    }
}
