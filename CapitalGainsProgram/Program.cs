namespace CapitalGainsProgram;

public class Program
{
    public static void Main(string[] args)
    {
        if (args.Length > 0) 
        {
            var inputLines = Functions.ReadFile(args[0]);

            foreach (var line in inputLines)
            {
                var result = Processor.ProcessOperation(line);

                Console.WriteLine(result);
            }

            return;
        }

        Console.WriteLine("Argument is null");
    }
}