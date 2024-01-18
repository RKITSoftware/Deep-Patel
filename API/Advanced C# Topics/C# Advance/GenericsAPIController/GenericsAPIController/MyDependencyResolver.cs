using GenericsAPIController.Controllers;
using GenericsAPIController.Interface;
using GenericsAPIController.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;

namespace GenericsAPIController
{
    public class MyDependencyResolver : IDependencyResolver
    {
        private readonly IGenericService<EMP01> _employeeService;

        public MyDependencyResolver(IGenericService<EMP01> employeeService)
        {
            _employeeService = employeeService;
        }

        public IDependencyScope BeginScope()
        {
            return this;
        }

        public void Dispose() { }

        public object GetService(Type serviceType)
        {
            if(serviceType == typeof(CLEmployeeController))
            {
                return new CLEmployeeController(_employeeService);
            }

            return null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return Enumerable.Empty<object>();
        }
    }
}