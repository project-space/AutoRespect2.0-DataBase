using AutoRespect.DataBase.Migration.Common;

namespace AutoRespect.DataBase.Migration.ResourceServer
{
    public class ResourceServerDeployer : Deployer
    {
        public ResourceServerDeployer() :
            base("Data Source=(localdb)\\mssqllocaldb; Initial Catalog=AutoRespect.ResourceServer; Integrated Security=SSPI;", typeof(ResourceServerDeployer).Assembly)
        {
        }
    }
}