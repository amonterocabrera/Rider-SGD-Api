using SGDPEDIDOS.Infrastructure.Data;

namespace SGDPEDIDOS.UnitTest_Main.Configurations
{
    public class DbConexionStringConfig
    {

        public static PrincipalContext DbConexion()
        {
            string CnString = "Data Source=.\\SQL2019;Initial Catalog=ServiciosMICM;Integrated Security=True";
            var context = new PrincipalContext(CnString);
            return context;
        }



    }
}
