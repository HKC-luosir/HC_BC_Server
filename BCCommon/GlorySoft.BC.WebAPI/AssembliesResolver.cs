using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Http.Dispatcher;

namespace GlorySoft.BC.WebAPI
{
    public class AssembliesResolver : DefaultAssembliesResolver
    {
        public override ICollection<Assembly> GetAssemblies()
        {
            ICollection<Assembly> baseAssemblies = base.GetAssemblies();
            List<Assembly> assemblies = new List<Assembly>(baseAssemblies);
            var assembly = AppDomain.CurrentDomain.BaseDirectory;
            string path = assembly + "\\" + "GlorySoft.BC.WebAPI.dll";
            var controllersAssembly = Assembly.LoadFrom(path);
            baseAssemblies.Add(controllersAssembly);
            return assemblies;
        }
    }
}
