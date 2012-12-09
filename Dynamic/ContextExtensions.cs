using System.Linq;
using System.Reflection;

namespace Noesis.Javascript.Dynamic
{
    public static class ContextExtensions
    {
        public static JavascriptContext LoadFromResources(this JavascriptContext context, Assembly assembly)
        {
            assembly.LoadJavascript().ToList().ForEach(x => context.Run(x));
            return context;
        }

        public static JContext LoadFromResources(this JContext context, Assembly assembly)
        {
            context.Context.LoadFromResources(assembly);
            return context;
        }
    }
}
