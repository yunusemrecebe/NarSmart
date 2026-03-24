using MediatR;
using NarSmart.Application.Common.Models;
using NarSmart.Application.Features.Auth.DTOs;

namespace NarSmart.Application.Features.Auth.Queries.GetUserHotels;

public record GetUserHotelsQuery(string Email) : IRequest<Result<List<UserHotelDto>>>;
