using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace OtelSistemi

{
    public class DataBase
    {
        public SqlConnection baglanti = new SqlConnection(@"Data Source=.;Initial Catalog=OtelRezervasyon;Integrated Security=True;MultipleActiveResultSets=True");
    }
}
