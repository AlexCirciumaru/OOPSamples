using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using CustomerAppPaymentP.Abstractions;


namespace CustomerAppPaymentP.MainApp
{
    public class GenericPluginManager<T>
    {        

        public GenericPluginManager()
        {
            Plugins = new List<T>();
        }
        public List<T> Plugins { get; protected set; }

        public void LoadPlugins()
        {
                        
            var executableLocation = Assembly.GetEntryAssembly().Location;
            var path = Path.Combine(Path.GetDirectoryName(executableLocation), "Plugins");
            var assemblies = Directory
                        .GetFiles(path, "*.dll", SearchOption.TopDirectoryOnly)
                        .Select(AssemblyLoadContext.Default.LoadFromAssemblyPath)
                        .ToList();
            
           foreach(var assembly in assemblies)
           {
               var pluginInstances = LoadPluginsFromAssembly(assembly);
               Plugins.AddRange(pluginInstances);               
           }            
        }

        private IEnumerable<T> LoadPluginsFromAssembly(Assembly assemblyToScan)
        {
            var currentList = new List<T>();
            var exportedTypes = assemblyToScan.ExportedTypes;            
            var interfaceType = typeof(T);

            foreach(var type in exportedTypes)
            {
                if (interfaceType.IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                {
                   var pluginInstance = (T)Activator.CreateInstance(type);
                   currentList.Add(pluginInstance);
                }
            }

            return currentList;
        }

        
    }

}