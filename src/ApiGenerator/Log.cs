// using System.Collections.Generic;
// using System.Text;
// using Microsoft.CodeAnalysis;
// using Microsoft.CodeAnalysis.Text;
//
// namespace ApiGenerator
// {
// #if DEBUG
//     internal class Log : ISourceGenerator
//     {
//         public static List<string> Logs { get; } = new List<string>();
//         public static void Print(string msg) => Logs.Add("//\t" + msg);
//
//         public static void FlushLogs(GeneratorExecutionContext context)
//         {
//             context.AddSource($"logs.g.cs", SourceText.From(string.Join("\n", Logs), Encoding.UTF8));
//         }
//
//         public void Initialize(GeneratorInitializationContext context)
//         {
//         }
//
//         public void Execute(GeneratorExecutionContext context)
//         {
//             Print("Tester");
//             FlushLogs(context);
//         }
//     }
// #endif
// }