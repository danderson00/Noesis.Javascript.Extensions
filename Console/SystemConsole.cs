using System;
using Newtonsoft.Json;
using Noesis.Javascript.Dynamic;

namespace Noesis.Javascript.Console
{
    public class SystemConsole
    {
        private JContext Context { get; set; }

        public static void Attach(JavascriptContext context)
        {
            new SystemConsole(context);
        }

        public SystemConsole(JavascriptContext context)
        {
            Context = new JContext(context);

            string input;
            while((input = GetInput()) != "")
                WriteResult(input);
        }

        private string GetInput()
        {
            System.Console.ForegroundColor = ConsoleColor.White;
            System.Console.Write("> ");
            return System.Console.ReadLine();
        }

        private void WriteResult(string input)
        {
            System.Console.WriteLine(GetResult(input));
        }

        private string GetResult(string input)
        {
            try
            {
                System.Console.ForegroundColor = ConsoleColor.Gray;
                return JsonConvert.SerializeObject(Context.Evaluate(input), Formatting.Indented);
            }
            catch (JavascriptException ex)
            {
                System.Console.ForegroundColor = ConsoleColor.Red;
                return string.Format("ERROR: {0}", ex.Message);
            }
        }
    }
}
