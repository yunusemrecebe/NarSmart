namespace NarSmart.Application.Features.Sales.DTOs;

public class SaleDto
{
    public Guid Id { get; set; }
    public Guid RoomId { get; set; }
    public Guid SalesPackageId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; }
    public List<SaleCustomerDto> Customers { get; set; } = new();
}

public class SaleCustomerDto
{
    public Guid CustomerId { get; set; }
}
