using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Entidades
{
    public class ConnectionContext
    {
        static public string Connection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["GerenciadorOrdemServico"].ConnectionString;
            return connectionString;
        }
    }
}