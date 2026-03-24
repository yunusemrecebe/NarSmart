using MediatR;
using NarSmart.Application.Common.Models;
using NarSmart.Application.Features.Auth.DTOs;

namespace NarSmart.Application.Features.Auth.Commands.Login;

public record LoginCommand(string Email, string Password, Guid HotelId) : IRequest<Result<LoginResponseDto>>;
