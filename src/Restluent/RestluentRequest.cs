using System.Text;

namespace Restluent;

public class RestluentRequest
{
    private string _endpoint = "";
    private StringBuilder RequestUrl { get; set; }

    public RestluentRequest()
    {
        RequestUrl = new StringBuilder();
    }

    public RestluentRequest SetEndpoint(string endpointName)
    {
        _endpoint = endpointName;
        return this;
    }
    
    public RestluentRequest AddParameter(string name, string value)
    {
        RequestUrl.Append($"{name}={value}");
        return this;
    }

    public string RequestString()
    {
        return _endpoint + "?" + RequestUrl;
    }
}