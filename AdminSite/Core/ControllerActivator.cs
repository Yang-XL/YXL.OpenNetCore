using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Internal;

namespace AdminSite.Core
{
    public class ControllerActivator : DefaultControllerActivator
    {
        private readonly ITypeActivatorCache _typeActivatorCache;

        public ControllerActivator(ITypeActivatorCache typeActivatorCache) : base(typeActivatorCache)
        {
            _typeActivatorCache = typeActivatorCache;
        }

        public override object Create(ControllerContext controllerContext)
        {
            if (controllerContext == null)
            {
                throw new ArgumentNullException(nameof(controllerContext));
            }

            if (controllerContext.ActionDescriptor == null)
            {
                throw new ArgumentException("");
            }

            var controllerTypeInfo = controllerContext.ActionDescriptor.ControllerTypeInfo;

            if (controllerTypeInfo == null)
            {
                throw new ArgumentException("");
            }

            var serviceProvider = controllerContext.HttpContext.RequestServices;
            return _typeActivatorCache.CreateInstance<object>(serviceProvider, controllerTypeInfo.AsType());
        }
    }
}
