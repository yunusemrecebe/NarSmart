using MediatR;
using NarSmart.Application.Common.Interfaces;
using NarSmart.Application.Common.Models;
using NarSmart.Domain.Common;
using NarSmart.Domain.Entities.Sale;
using NarSmart.Domain.ValueObjects;

namespace NarSmart.Application.Features.Sales.Commands.CreateSale;

public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, Result<Guid>>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentTenantService _tenantService;

    public CreateSaleCommandHandler(
        ISaleRepository saleRepository,
        IUnitOfWork unitOfWork,
        ICurrentTenantService tenantService)
    {
        _saleRepository = saleRepository;
        _unitOfWork = unitOfWork;
        _tenantService = tenantService;
    }

    public async Task<Result<Guid>> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        var hasOverlap = await _saleRepository.HasOverlappingReservationAsync(
            _tenantService.HotelId, request.RoomId,
            request.StartDate, request.EndDate, cancellationToken);

        if (hasOverlap)
            return Result<Guid>.Failure("Room already has a reservation for this date range.");

        var period = new DateRange(request.StartDate, request.EndDate);
        var sale = SaleFactory.Create(
            _tenantService.HotelId, request.RoomId, request.SalesPackageId,
            period, request.CustomerIds);

        await _saleRepository.AddAsync(sale, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Created(sale.Id);
    }
}
