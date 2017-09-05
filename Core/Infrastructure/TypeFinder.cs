using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.Logging;

namespace Core.Infrastructure
{
    public class TypeFinder : ITypeFinder
    {
        private const string assemblySkipLoadingPattern =
                "^System|^mscorlib|^Microsoft|^AjaxControlToolkit|^Antlr3|^Autofac|^AutoMapper|^Castle|^ComponentArt|^CppCodeProvider|^DotNetOpenAuth|^EntityFramework|^EPPlus|^FluentValidation|^ImageResizer|^itextsharp|^log4net|^MaxMind|^MbUnit|^MiniProfiler|^Mono.Math|^MvcContrib|^Newtonsoft|^NHibernate|^nunit|^Org.Mentalis|^PerlRegex|^QuickGraph|^Recaptcha|^Remotion|^RestSharp|^Rhino|^Telerik|^Iesi|^TestDriven|^TestFu|^UserAgentStringLibrary|^VJSharpCodeProvider|^WebActivator|^WebDev|^WebGrease"
            ;

        
        private readonly ILogger _logger;

        private readonly bool ignoreReflectionErrors = true;

        public TypeFinder(ILoggerFactory logFacoty)
        {
            _logger = logFacoty.CreateLogger<TypeFinder>();
        }


        /// <summary>
        ///     是否应该在应用程序域中迭代程序集
        ///     加载这些装配体时应用加载模式
        /// </summary>
        public bool LoadAppDomainAssemblies { get; set; } = true;

        /// <summary>
        ///     Gets the pattern for dlls that we know don't need to be investigated.
        ///     If you change this so that Nop assemblies arn't investigated (e.g. by not including something like "^Nop|..." you
        ///     may break core functionality.
        ///     Gets or sets the pattern for dll that will be investigated. For ease of use this defaults to match all but to
        ///     increase performance you might want to configure a pattern that includes assemblies and your own.
        /// </summary>
        public string AssemblySkipLoadingPattern { get; set; } = assemblySkipLoadingPattern;

        /// <summary>
        ///     If you change this so that Nop assemblies arn't investigated (e.g. by not including something like "^Nop|..." you
        ///     may break core functionality.
        ///     Gets or sets the pattern for dll that will be investigated. For ease of use this defaults to match all but to
        ///     increase performance you might want to configure a pattern that includes assemblies and your own.
        /// </summary>
        /// <remarks></remarks>
        public string AssemblyRestrictToLoadingPattern { get; set; } = ".*";

        /// <summary>
        ///     获取或设置加载启动的程序集以及AppDomain中加载的程序
        /// </summary>
        public IList<AssemblyName> AssemblyNames { get; set; }= new List<AssemblyName>();

        

        public IList<Assembly> GetAssemblies()
        {
            var addedAssemblyNames = new List<AssemblyName>();
            var assemblies = new List<Assembly>();

            if (LoadAppDomainAssemblies)
                AddAssembliesInAppDomain(ref addedAssemblyNames, ref assemblies);
            AddConfiguredAssemblies(ref addedAssemblyNames, ref assemblies);

            return assemblies;
        }

        public IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, bool onlyConcreteClasses = true)
        {
            return FindClassesOfType(assignTypeFrom, GetAssemblies(), onlyConcreteClasses);
        }

        public IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, IEnumerable<Assembly> assemblies,
            bool onlyConcreteClasses = true)
        {
            var result = new List<Type>();
            try
            {
                foreach (var a in assemblies)
                {
                    Type[] types = null;
                    try
                    {
                        types = a.GetTypes();
                    }
                    catch
                    {
                        //Entity Framework 6 doesn't allow getting types (throws an exception)
                        if (!ignoreReflectionErrors)
                            throw;
                    }

                    if (types != null)
                        foreach (var t in types)
                            if (assignTypeFrom.IsAssignableFrom(t) ||
                                assignTypeFrom.GetTypeInfo().IsGenericTypeDefinition &&
                                DoesTypeImplementOpenGeneric(t, assignTypeFrom))
                                if (!t.GetTypeInfo().IsInterface)
                                    if (onlyConcreteClasses)
                                    {
                                        if (t.GetTypeInfo().IsClass && !t.GetTypeInfo().IsAbstract)
                                            result.Add(t);
                                    }
                                    else
                                    {
                                        result.Add(t);
                                    }
                }
            }
            catch (ReflectionTypeLoadException ex)
            {
                var msg = string.Empty;
                foreach (var e in ex.LoaderExceptions)
                    msg += e.Message + Environment.NewLine;

                var fail = new Exception(msg, ex);
                _logger.LogDebug(fail.Message, fail);
                throw fail;
            }
            return result;
        }

        public IEnumerable<Type> FindClassesOfType<T>(bool onlyConcreteClasses = true)
        {
            return FindClassesOfType(typeof(T), onlyConcreteClasses);
        }

        public IEnumerable<Type> FindClassesOfType<T>(IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true)
        {
            return FindClassesOfType(typeof(T), assemblies, onlyConcreteClasses);
        }


        /// <summary>
        ///     Check if a dll is one of the shipped dlls that we know don't need to be investigated.
        /// </summary>
        /// <param name="assemblyFullName">
        ///     The name of the assembly to check.
        /// </param>
        /// <returns>
        ///     True if the assembly should be loaded into Nop.
        /// </returns>
        public virtual bool Matches(AssemblyName assemblyFullName)
        {
            return !Matches(assemblyFullName.Name, AssemblySkipLoadingPattern) &&
                   Matches(assemblyFullName.Name, AssemblyRestrictToLoadingPattern);
        }

        /// <summary>
        ///     Check if a dll is one of the shipped dlls that we know don't need to be investigated.
        /// </summary>
        /// <param name="assemblyFullName">
        ///     The assembly name to match.
        /// </param>
        /// <param name="pattern">
        ///     The regular expression pattern to match against the assembly name.
        /// </param>
        /// <returns>
        ///     True if the pattern matches the assembly name.
        /// </returns>
        protected virtual bool Matches(string assemblyFullName, string pattern)
        {
            return Regex.IsMatch(assemblyFullName, pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        /// <summary>
        ///     Adds specifically configured assemblies.
        /// </summary>
        /// <param name="addedAssemblyNames"></param>
        /// <param name="assemblies"></param>
        protected virtual void AddConfiguredAssemblies(ref List<AssemblyName> addedAssemblyNames, ref List<Assembly> assemblies)
        {
            foreach (var assemblyName in AssemblyNames)
            {
                var assembly = Assembly.Load(assemblyName);
                if (!addedAssemblyNames.Contains(assembly.GetName()))
                {
                    assemblies.Add(assembly);
                    addedAssemblyNames.Add(assembly.GetName());
                }
            }
        }

        /// <summary>
        ///     Iterates all assemblies in the AppDomain and if it's name matches the configured patterns add it to our list.
        /// </summary>
        /// <param name="addedAssemblyNames"></param>
        /// <param name="assemblies"></param>
        private void AddAssembliesInAppDomain( ref List<AssemblyName> addedAssemblyNames, ref List<Assembly> assemblies)
        {
            _logger.LogDebug("RuntimeLibraries.Count:{0}", DependencyContext.Default.RuntimeLibraries.Count);
            foreach (var library in DependencyContext.Default.RuntimeLibraries)
            {
                foreach (var assemblyName in library.GetDefaultAssemblyNames(DependencyContext.Default))
                {
                    _logger.LogDebug("Assembly.assemblyName:{0}，FullNum:{1}", assemblyName.Name, assemblyName.FullName);
                    if (Matches(assemblyName))
                        if (!addedAssemblyNames.Contains(assemblyName))
                        {
                            assemblies.Add(Assembly.Load(assemblyName));
                            addedAssemblyNames.Add(assemblyName);
                        }

                }
            }
        }


        /// <summary>
        ///     Does type implement generic?
        /// </summary>
        /// <param name="type"></param>
        /// <param name="openGeneric"></param>
        /// <returns></returns>
        protected virtual bool DoesTypeImplementOpenGeneric(Type type, Type openGeneric)
        {
            try
            {
                var genericTypeDefinition = openGeneric.GetGenericTypeDefinition();
                foreach (var implementedInterface in type.GetTypeInfo()
                    .FindInterfaces((objType, objCriteria) => true, null))
                {
                    if (!implementedInterface.GetTypeInfo().IsGenericType)
                        continue;

                    var isMatch =
                        genericTypeDefinition.IsAssignableFrom(implementedInterface.GetGenericTypeDefinition());
                    return isMatch;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}