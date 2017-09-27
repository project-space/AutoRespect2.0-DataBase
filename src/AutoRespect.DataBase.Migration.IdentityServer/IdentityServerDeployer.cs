using AutoRespect.DataBase.Migration.Common;

namespace AutoRespect.DataBase.Migration.IdentityServer
{
    public class IdentityServerDeployer : Deployer
    {
        public IdentityServerDeployer() : 
            base("Data Source=(localdb)\\mssqllocaldb; Initial Catalog=AutoRespect.IdentityServer; Integrated Security=SSPI;", typeof(IdentityServerDeployer).Assembly)
        {
        }
    }
}
