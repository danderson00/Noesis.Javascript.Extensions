using System.Linq;
using System.Reflection;
using Noesis.Javascript.Extensions;

namespace Noesis.Javascript.Dynamic
{
    public static class JContextExtensions
    {
        public static JContext LoadFromResources(this JContext context, Assembly assembly)
        {
            context.Context.LoadFromResources(assembly);
            return context;
        }
    }
}
