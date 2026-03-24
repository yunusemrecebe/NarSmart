using MediatR;
using NarSmart.Application.Common.Interfaces;
using NarSmart.Application.Common.Models;
using NarSmart.Domain.Common;
using NarSmart.Domain.Entities.Customer;
using NarSmart.Domain.Entities.Sale;
using NarSmart.Domain.ValueObjects;

namespace NarSmart.Application.Features.Sales.Commands.AddGuestToSale;

public class AddGuestToSaleCommandHandler : IRequestHandler<AddGuestToSaleCommand, Result<Guid>>
{
    private readonly ISaleRepository _saleRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentTenantService _tenantService;

    public AddGuestToSaleCommandHandler(
        ISaleRepository saleRepository,
        ICustomerRepository customerRepository,
        IUnitOfWork unitOfWork,
        ICurrentTenantService tenantService)
    {
        _saleRepository = saleRepository;
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
        _tenantService = tenantService;
    }

    public async Task<Result<Guid>> Handle(AddGuestToSaleCommand request, CancellationToken cancellationToken)
    {
        var sale = await _saleRepository.GetByIdAsync(request.SaleId, cancellationToken);

        if (sale is null)
            return Result<Guid>.NotFound($"Sale with id '{request.SaleId}' was not found.");

        var name = new PersonName(request.FirstName, request.LastName);
        var reservationPeriod = sale.GetPeriod();

        var guest = CustomerFactory.CreateGuest(
            _tenantService.HotelId, name, request.BirthDate,
            reservationPeriod, request.NationalId, request.PassportNumber);

        await _customerRepository.AddAsync(guest, cancellationToken);

        sale.AddCustomer(guest.Id);
        _saleRepository.Update(sale);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Created(guest.Id);
    }
}
