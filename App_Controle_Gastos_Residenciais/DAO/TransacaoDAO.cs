using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using App_Controle_Gastos_Residenciais.Model;

using MySql.Data.MySqlClient;

namespace App_Controle_Gastos_Residenciais.DAO
{

    public class TransacaoDAO : Connection
    {

        public TransacaoDAO() : base() {  }

        public bool Insert(Transacao model)
        {

            try
            {

                string sql = "INSERT INTO Transacao(descricao, valor, tipo, fk_pessoa) VALUES(@descricao, @valor, @tipo, @fk_pessoa)";

                MySqlCommand stmt = new MySqlCommand(sql, base.conexao);

                base.Open_Connection();

                stmt.Parameters.AddWithValue("@descricao", model.descricao);

                stmt.Parameters.AddWithValue("@valor", model.valor);

                stmt.Parameters.AddWithValue("@tipo", model.tipo);

                stmt.Parameters.AddWithValue("@fk_pessoa", model.fk_pessoa);

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

        public List<Transacao> Select()
        {

            try
            {

                List<Transacao> lista_transacoes = new List<Transacao>();

                string sql = "SELECT * FROM Transacao ORDER BY id ASC";

                MySqlCommand stmt = new MySqlCommand(sql, base.conexao);

                base.Open_Connection();

                MySqlDataReader registros_retornados = stmt.ExecuteReader();

                while (registros_retornados.Read())
                {

                    Transacao transacao = new Transacao()
                    {

                        id = registros_retornados.GetInt32(0),

                        descricao = registros_retornados.GetString(1),

                        valor = registros_retornados.GetDouble(2),

                        tipo = registros_retornados.GetString(3),

                        fk_pessoa = registros_retornados.GetInt32(4)

                    };

                    transacao.pessoa = (new PessoaDAO()).Search(transacao.fk_pessoa);

                    lista_transacoes.Add(transacao);

                }

                return lista_transacoes;

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