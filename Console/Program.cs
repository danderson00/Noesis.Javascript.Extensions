namespace Noesis.Javascript.Console
{
    class Program
    {
        static void Main()
        {
            SystemConsole.Attach(new JavascriptContext());
        }
    }
}
