using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using App_Controle_Gastos_Residenciais.Model;

using MySql.Data.MySqlClient;

namespace App_Controle_Gastos_Residenciais.DAO
{

    public class PessoaDAO : Connection
    {

        public PessoaDAO() : base() {  }

        public bool Insert(Pessoa model)
        {

            try
            {

                string sql = "INSERT INTO Pessoa(nome, idade) VALUES(@nome, @idade)";

                MySqlCommand stmt = new MySqlCommand(sql, base.conexao);

                base.Open_Connection();

                stmt.Parameters.AddWithValue("@nome", model.nome);

                stmt.Parameters.AddWithValue("@idade", model.idade);

                stmt.Prepare();

                return (stmt.ExecuteNonQuery() > 0);

            }

            catch (Exception ex)
            {

                throw new Exception(ex.Message);

            }

            finally
            {

                base.Close_Connection();

            }

        }

        public bool Delete(int id)
        {

            try
            {

                string sql = "DELETE FROM Pessoa WHERE id = @id";

                MySqlCommand stmt = new MySqlCommand(sql, base.conexao);

                base.Open_Connection();

                stmt.Parameters.AddWithValue("@id", id);

                stmt.Prepare();

                return (stmt.ExecuteNonQuery() > 0);

            }

            catch (Exception ex)
            {

                throw new Exception(ex.Message);

            }

            finally
            {

                base.Close_Connection();

            }

        }

        public List<Pessoa> Select()
        {

            try
            {

                List<Pessoa> lista_pessoas = new List<Pessoa>();

                string sql = "SELECT * FROM Pessoa ORDER BY nome ASC";

                MySqlCommand stmt = new MySqlCommand(sql, base.conexao);

                base.Open_Connection();

                MySqlDataReader registros_retornados = stmt.ExecuteReader();

                while (registros_retornados.Read())
                {

                    Pessoa pessoa = new Pessoa()
                    {

                        id = registros_retornados.GetInt32(0),

                        nome = registros_retornados.GetString(1),

                        idade = registros_retornados.GetInt32(2)

                    };

                    pessoa.valores_totais = (new TotaisDAO()).Calculate_Totals(pessoa.id);

                    lista_pessoas.Add(pessoa);

                }

                return lista_pessoas;

            }

            catch (Exception ex)
            {

                throw new Exception(ex.Message);

            }

            finally
            {

                base.Close_Connection();

            }

        }

        public Pessoa? Search(int id)
        {

            try
            {

                Pessoa? pessoa = null;

                string sql = "SELECT * FROM Pessoa WHERE id = @id ORDER BY id ASC";

                MySqlCommand stmt = new MySqlCommand(sql, base.conexao);

                base.Open_Connection();

                stmt.Parameters.AddWithValue("@id", id);

                stmt.Prepare();

                MySqlDataReader registro_retornado = stmt.ExecuteReader();

                while (registro_retornado.Read())
                {

                    pessoa = new Pessoa()
                    {

                        id = registro_retornado.GetInt32(0),

                        nome = registro_retornado.GetString(1),

                        idade = registro_retornado.GetInt32(2)

                    };

                    pessoa.valores_totais = (new TotaisDAO()).Calculate_Totals(pessoa.id);

                }

                return pessoa;

            }

            catch (Exception ex)
            {

                throw new Exception(ex.Message);

            }

            finally
            {

                base.Close_Connection();

            }

        }

    }

}