using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.CodeAnalysis.Text;

namespace ApiGenerator
{
    [Generator]
    public class ClassGenerator : ISourceGenerator
    {
        private enum PropertyType
        {
            String,
            Integer,
            DateTime
        }
        
        private static string EnumPropertyTypeToString(PropertyType? propertyType)
        {
            switch (propertyType)
            {
                case PropertyType.String:
                    return "string";

                case PropertyType.Integer:
                    return "int";

                case PropertyType.DateTime:
                    return "DateTime";

                default:
                    throw new ArgumentOutOfRangeException(nameof(propertyType), propertyType, null);
            }
        }

        private static class PropertyTypes
        {
            public const string String = "string";
            public const string Integer = "integer";
            public const string Int32 = "int32";
            public const string DateTime = "date-time";
        }

        private static PropertyType? GetPropertyType(string propertyType)
        {
            switch (propertyType)
            {
                case PropertyTypes.String:
                    return PropertyType.String;
                
                case PropertyTypes.Integer:
                    return PropertyType.Integer;
            }

            return null;
        }

        public void GenerateSource(AdditionalText file, GeneratorExecutionContext context)
        {
            var content = file.GetText(context.CancellationToken);
            var jsonDocument = JsonSerializer.Deserialize<JsonObject>(content.ToString());
            var schemas = jsonDocument["components"]["schemas"].AsObject();

            foreach (var obj in schemas)
            {
                var objectName = obj.Key;
                var objectType = obj.Value["type"];

                var generatedProperties = new List<string>();

                var properties = obj.Value["properties"].AsObject();
                foreach (var property in properties)
                {
                    var propertyName = property.Key;
                    var propertyType = GetPropertyType(property.Value["type"].ToString());

                    var propertySource = $@"public {EnumPropertyTypeToString(propertyType)} {propertyName} {{ get; set; }}";

                    generatedProperties.Add(propertySource);
                }

                var generatedPropertiesSource = string.Join("\n", generatedProperties);

                context.AddSource($"{objectName}.g.cs", SourceText.From($@"// Auto-generated code
using System;

namespace Restluent
{{
    public class {objectName}
    {{
        {generatedPropertiesSource}
    }}
}}
", Encoding.UTF8));
            }
        }

        public void Execute(GeneratorExecutionContext context)
        {
            var syntaxTrees = context.Compilation.SyntaxTrees;
            var restluentFiles = context.AdditionalFiles.Where(at => at.Path.EndsWith(".json"));

            foreach (AdditionalText file in context.AdditionalFiles)
            {
                if (Path.GetExtension(file.Path).Equals(".json", StringComparison.OrdinalIgnoreCase))
                {
                    GenerateSource(file, context);
                }
            }
        }
        
        public void Initialize(GeneratorInitializationContext context)
        {
// #if DEBUG
//             if (!Debugger.IsAttached)
//             {
//                 Debugger.Launch();
//             }
// #endif
        }
    }
}