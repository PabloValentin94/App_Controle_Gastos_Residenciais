using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;

namespace App_Controle_Gastos_Residenciais.DAO
{

    /*
     
        Como esta classe é abstrata, não pode ser instânciada, mas pode ser utilizada 
        por suas classes filhas, através de uma relação de herança.

     */

    public abstract class Connection
    {

        // Variável de acesso a conexão com o MySQL.

        protected MySqlConnection? conexao = null;

        // Método construtor desta classe.

        public Connection()
        {

            // Parâmetros de conexão com o MySQL.

            string host = "10.0.2.2"; // Host específico para o emulador (Esse host se refere ao localhost da máquina real.).

            string port = "3306"; // Porta de acesso padrão do MySQL.

            string user = "root"; // Usuário padrão do MySQL.

            string password = ""; // Senha do usuário especificado.

            string db_name = "db_controle_gastos_residenciais"; // Nome do banco de dados utilizado.

            string dsn = $"datasource={host};port={port};username={user};password={password};database={db_name};"; // String de conexão.

            // Estabelecendo uma nova conexão com o MySQL.

            this.conexao = new MySqlConnection(dsn);

        }

        // Método que abre a conexão do objeto atual desta classe.

        public void Open_Connection()
        {

            // Verificando se uma conexão com o MySQL já foi estabelecida.

            if (this.conexao != null)
            {

                // Abrindo a conexão do objeto atual desta classe.

                this.conexao.Open();

            }

        }

        // Método que fecha a conexão do objeto atual desta classe.

        public void Close_Connection()
        {

            // Verificando se uma conexão com o MySQL já foi estabelecida.

            if (this.conexao != null)
            {

                // Fechando a conexão do objeto atual desta classe.

                this.conexao.Close();

                // Removendo o valor da variável de acesso a conexão com o MySQL, por razões de segurança.

                this.conexao = null;

            }

        }

    }

}