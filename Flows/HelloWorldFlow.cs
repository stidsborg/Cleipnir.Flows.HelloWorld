using Cleipnir.Flows;

namespace Test.Flows;

public class HelloWorldFlow : Flow<string>
{
    public override async Task Run(string param)
    {
        Console.WriteLine("Executing flow!");
        await Task.CompletedTask;
    }
}