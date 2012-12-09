using System.Linq;
using System.Reflection;

namespace Noesis.Javascript.Extensions
{
    public static class JavascriptContextExtensions
    {
        public static JavascriptContext LoadFromResources(this JavascriptContext context, Assembly assembly)
        {
            assembly.LoadJavascript().ToList().ForEach(x => context.Run(x));
            return context;
        }
    }
}
