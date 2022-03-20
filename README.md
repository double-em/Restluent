# Restluent (Work in progress)
Restluent is supposed to be a Fluent Api Client generator.  
The only thing needed is an OpenAPI Standard Schema eg. from Swagger.

## Todo (Proof of Concept):
- [x] Example API
- [x] Open API Component parsing from a Json file
- [x] SourceGenerated classes from parsed components
- [x] Fluent Api builder
- [ ] Couple the Api Client with the generated classes
- [ ] HttpClient request handling
- [ ] Unit Tests

## Concept

### Schema Example
If given a schema with the following components:
```json
"components": {
    "schemas": {
        "WeatherForecast": {
            "type": "object",
            "properties": {
                "date": {
                    "type": "string",
                    "format": "date-time"
                },
                "temperatureC": {
                    "type": "integer",
                    "format": "int32"
                },
                "temperatureF": {
                    "type": "integer",
                    "format": "int32",
                    "readOnly": true
                },
                "summary": {
                    "type": "string",
                    "nullable": true
                }
            },
            "additionalProperties": false
        }
    }
}
```
  
### Generated Example
There should be generated a class as:
```csharp
// Auto-generated code
using System;

namespace Restluent
{
    public class WeatherForecast
    {
        public string date { get; set; }
        public int temperatureC { get; set; }
        public int temperatureF { get; set; }
        public string summary { get; set; }
    }
}

```

### Usage Example
With a generated class like:
```csharp
public class Customer
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;
    
    public bool WithCompanies { get; set; }
}
```

Then the usage should be:
```csharp
var customer = RestluentApi.Customers()
    .Where(c => c.Name == "tester")
    .WithCompanies();

var newCustomer = RestluentApi.Customers()
    .Add(new CustomerVariables
    {
        Name = "Test"
    });
```
