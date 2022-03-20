using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using Restluent.Models;

namespace Restluent.ConcreteApi;

public static class RestluentApi
{ 
    public static RestluentApi<CustomerVariables> Customers()
    {
        var request = new RestluentRequest();
        request.SetEndpoint("Customers");

        return new RestluentApi<CustomerVariables>(request, new CustomerVariables());
    }
}


public class RestluentApi<T>
{
    public RestluentRequest Request;

    public T _value;

    public RestluentApi(RestluentRequest request, T value)
    {
        Request = request;
        _value = value;
    }

    public T Add(T value)
    {
        Post(value);
        return value;
    }

    public RestluentApi<T> Where(Expression<Func<T, bool>> selection)
    {
        var body = selection.Body.ToString();

        var match = Regex.Match(body, @"\(\w+\.(\w+).*== ?(.*)\)");
        var propertyName = match.Groups[1].ToString();
        var value = match.Groups[2].ToString().Trim('"');
        
        _value?.GetType().GetProperty(propertyName)?.SetValue(_value, value);
        
        return this;
    }

    public RestluentApi<T> WithCompanies()
    {
        _value?.GetType().GetProperty(nameof(WithCompanies))?.SetValue(_value, true);
        return this;
    }

    private string Post(T value)
    {
        return "Posted";
    }
}