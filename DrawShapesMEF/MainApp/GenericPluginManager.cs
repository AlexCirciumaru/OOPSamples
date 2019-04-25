using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using Lesson2.Abstractions;
using System.Composition.Hosting;
using System.Composition;

namespace Lesson2.MainApp
{
    public class ShapePluginManager<T>
    {
        [ImportMany]
        public IEnumerable<T> Plugins { get; protected set; }

        public void LoadPlugins()
        {
                        
            var executableLocation = Assembly.GetEntryAssembly().Location;
            var path = Path.Combine(Path.GetDirectoryName(executableLocation), "Plugins");
            var assemblies = Directory
                        .GetFiles(path, "*.dll", SearchOption.TopDirectoryOnly)
                        .Select(AssemblyLoadContext.Default.LoadFromAssemblyPath)
                        .ToList();
            var configuration = new ContainerConfiguration()
                .WithAssemblies(assemblies);
            using (var container = configuration.CreateContainer())
            {
                Plugins = container.GetExports<T>();
            }            
        }

        
    }

}