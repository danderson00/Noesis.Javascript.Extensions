using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Noesis.Javascript.Dynamic
{
    public static class AssemblyExtensions
    {
        public static IEnumerable<string> LoadJavascript(this Assembly assembly)
        {
            return assembly.LoadEmbeddedResources("js");
        }

        public static IEnumerable<string> LoadEmbeddedResources(this Assembly assembly, string extension = "")
        {
            return assembly.GetManifestResourceNames()
                           .Where(x => extension == "" || x.EndsWith("." + extension))
                           .Select(x => LoadFromResourceStream(assembly, x));
        }

        private static string LoadFromResourceStream(Assembly assembly, string name)
        {
            var stream = assembly.GetManifestResourceStream(name);
            return new StreamReader(stream).ReadToEnd();
        }
    }
}
