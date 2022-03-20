namespace Restluent;

public class CustomerVariables
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;
    
    public string? CompanyName { get; set; }

    public bool WithCompanies { get; set; }
}