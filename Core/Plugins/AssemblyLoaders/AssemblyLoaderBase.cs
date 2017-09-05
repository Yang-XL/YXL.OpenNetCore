using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Core.Plugins.AssemblyLoaders
{
    /// <summary>
    /// Base class of assembly loader
    /// </summary>
    public abstract class AssemblyLoaderBase : IAssemblyLoader
    {
        /// <summary>
        /// Replacement assemblies
        /// </summary>
        protected IDictionary<string, string> ReplacementAssemblies { get; set; }
        /// <summary>
        /// Loaded assemblies
        /// </summary>
        protected virtual ISet<Assembly> LoadedAssemblies { get; set; } = new HashSet<Assembly>();

        /// <summary>
        /// Initialize
        /// </summary>
        public AssemblyLoaderBase()
        {
            ReplacementAssemblies = new Dictionary<string, string>();
        }

        /// <summary>
        /// Handle loaded assembly
        /// Preload it's dependencies
        /// </summary>
        /// <param name="assembly">Assembly</param>
        /// <returns></returns>
        protected virtual Assembly HandleLoadedAssembly(Assembly assembly)
        {
            if (LoadedAssemblies.Contains(assembly))
            {
                return assembly;
            }
            LoadedAssemblies.Add(assembly);
            // preload it's dependencies
            foreach (var dependentAssemblyname in assembly.GetReferencedAssemblies())
            {
                Assembly dependentAssembly;
                try
                {
                    dependentAssembly = Load(dependentAssemblyname);
                }
                catch
                {
                    // may fail, ignore it
                    continue;
                }
                HandleLoadedAssembly(dependentAssembly);
            }
            return assembly;
        }

        /// <summary>
        /// Get loaded assemblies
        /// Except wrapper assemblies and dynamic assemblies
        /// </summary>
        public virtual IList<Assembly> GetLoadedAssemblies()
        {
            return LoadedAssemblies.Where(a => !a.IsDynamic).ToList();
        }
        
        /// <summary>
        /// Load assembly by name object
        /// </summary>
        public abstract Assembly Load(AssemblyName assemblyName);
        /// <summary>
        /// Load assembly from it's binary contents
        /// </summary>
        public abstract Assembly Load(byte[] rawAssembly);
        /// <summary>
        /// Load assembly from file path
        /// </summary>
        public abstract Assembly LoadFile(string path);
    }
}
