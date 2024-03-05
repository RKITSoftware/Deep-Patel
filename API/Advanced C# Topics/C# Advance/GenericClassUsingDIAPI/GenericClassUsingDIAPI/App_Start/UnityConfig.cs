using GenericClassUsingDIAPI.Business_Logic;
using GenericClassUsingDIAPI.Interface;
using GenericClassUsingDIAPI.Models;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace GenericClassUsingDIAPI
{
    /// <summary>
    /// UnityConfig class responsible for configuring Unity container
    /// </summary>
    public static class UnityConfig
    {
        /// <summary>
        /// Method to register components with Unity container
        /// </summary>
        public static void RegisterComponents()
        {
            // Create a new Unity container
            var container = new UnityContainer();

            // Registering a service interface and its corresponding implementation
            // IService<STU01> is the service interface, and BLStudent is the implementation
            container.RegisterType<IService<STU01>, BLStudent>();

            // Set the Unity container as the dependency resolver for Web API
            GlobalConfiguration.Configuration.DependencyResolver = new
                UnityDependencyResolver(container);
        }
    }
}
