using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;

using MySql.Data.MySqlClient;

namespace App_Controle_Gastos_Residenciais.DAO
{

    public abstract class Connection
    {

        protected MySqlConnection? conexao = null;

        public Connection()
        {

            string host = "10.0.2.2";

            string port = "3306";

            string user = "root";

            string password = "etecjau";

            string db_name = "db_controle_gastos_residenciais";

            string dsn = $"datasource={host};port={port};username={user};password={password};database={db_name};";

            this.conexao = new MySqlConnection(dsn);

        }

        public void Open_Connection()
        {

            if (this.conexao != null)
            {

                this.conexao.Open();

            }

        }

        public void Close_Connection()
        {

            if (this.conexao != null)
            {

                this.conexao.Close();

                this.conexao = null;

            }

        }

    }

}