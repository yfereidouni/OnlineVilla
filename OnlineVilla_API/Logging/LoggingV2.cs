namespace OnlineVilla_API.Logging;

public class LoggingV2 : ILogging
{
    public void Log(string message, string type)
    {
        if (type=="error")
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"ERROR - {message}");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"INFORMATION - {message}");
        }

        Console.ForegroundColor = ConsoleColor.Gray;
    }
}
