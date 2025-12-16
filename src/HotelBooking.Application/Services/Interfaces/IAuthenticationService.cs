using HotelBooking.Application.DTOs.UserDTOs;
using HotelBooking.Application.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Application.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<Result<TokenResponseDTO>> RegisterAsync(RegisterDTO registerDTO);
        Task<Result<TokenResponseDTO>> LoginAsync(LoginDTO loginDTO);
        Task<Result<TokenResponseDTO>> RefreshTokenAsync(string userId, string refreshToken);
        Task<Result> LogoutAsync(string refreshToken);
        Task<bool> EmailExistsAsync(string email);
    }
}
