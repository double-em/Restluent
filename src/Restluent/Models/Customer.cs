namespace Restluent.Models;

public class Customer
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? CompanyName { get; set; }
}