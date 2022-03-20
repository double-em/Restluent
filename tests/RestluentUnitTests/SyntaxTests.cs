using System.Collections.Generic;
using System.Linq;
using Restluent;
using Restluent.ConcreteApi;
using Restluent.Extensions;
using Xunit;

namespace RestluentUnitTests;

public class SyntaxTests
{
    [Fact]
    public void Test1()
    {
        var customer = RestluentApi.Customers()
            .Where(c => c.Name == "tester")
            .WithCompanies();

        var newCustomer = RestluentApi.Customers()
            .Add(new CustomerVariables
            {
                Name = "Test"
            });

        var name = customer._value.Name;
        var collection = new List<string>();
        var newCollection = collection.Where(s => s.Contains('t'));
    }
}