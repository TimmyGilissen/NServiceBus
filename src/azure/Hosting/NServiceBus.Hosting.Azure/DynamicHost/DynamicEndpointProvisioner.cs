using System.Collections.Generic;
using System.IO;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace NServiceBus.Hosting
{
    internal class DynamicEndpointProvisioner
    {
        public string LocalResource { get; set; }

        public void Provision(IEnumerable<EndpointToHost> endpoints)
        {
            var localResource = RoleEnvironment.GetLocalResource(LocalResource);

            foreach (var endpoint in endpoints)
            {
                endpoint.ExtractTo(localResource.RootPath);

                endpoint.EntryPoint = Path.Combine(localResource.RootPath, endpoint.EndpointName, "NServiceBus.Hosting.Azure.HostProcess.exe");
            }
            
        }

        public void Remove(IEnumerable<EndpointToHost> endpoints)
        {
            var localResource = RoleEnvironment.GetLocalResource(LocalResource);

            foreach (var endpoint in endpoints)
            {
                var path = Path.Combine(localResource.RootPath, endpoint.EndpointName);
                Directory.Delete(path, true);
            }
        }
    }
}