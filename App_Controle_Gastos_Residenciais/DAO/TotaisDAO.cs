using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using App_Controle_Gastos_Residenciais.Model;

using MySql.Data.MySqlClient;

namespace App_Controle_Gastos_Residenciais.DAO
{

    public class TotaisDAO : Connection
    {

        public TotaisDAO() : base() {  }

        public Totais Calculate_Totals(int fk_pessoa)
        {

            try
            {

                Totais? valores_totais_pessoa = null;

                string sql = "SELECT (SELECT SUM(valor) FROM Transacao WHERE tipo = 'Despesa' AND fk_pessoa = @fk_pessoa) AS 'total_despesas', " +
                             "(SELECT SUM(valor) FROM Transacao WHERE tipo = 'Receita' AND fk_pessoa = @fk_pessoa) AS 'total_receitas' FROM Transacao transac " +
                             "WHERE fk_pessoa = @fk_pessoa ORDER BY fk_pessoa ASC;";

                MySqlCommand stmt = new MySqlCommand(sql, base.conexao);

                base.Open_Connection();

                stmt.Parameters.AddWithValue("@fk_pessoa", fk_pessoa);

                stmt.Prepare();

                MySqlDataReader dados_retornados = stmt.ExecuteReader();

                while (dados_retornados.Read())
                {

                    valores_totais_pessoa = new Totais()
                    {

                        total_despesas = (dados_retornados.IsDBNull(0)) ? 0.00 : dados_retornados.GetDouble(0),

                        total_receitas = (dados_retornados.IsDBNull(1)) ? 0.00 : dados_retornados.GetDouble(1)

                    };

                    valores_totais_pessoa.saldo_total = valores_totais_pessoa.total_receitas - valores_totais_pessoa.total_despesas;

                }

                if (valores_totais_pessoa == null)
                {

                    valores_totais_pessoa = new Totais()
                    {

                        total_despesas = 0.00,

                        total_receitas = 0.00,

                        saldo_total = 0.00

                    };

                }

                return valores_totais_pessoa;

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