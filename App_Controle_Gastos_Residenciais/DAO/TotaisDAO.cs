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

    public class TotaisDAO : Connection // Extendendo da classe Connection (Herança.).
    {

        // Executando o construtor da classe pai à partir do construtor da classe filha (Herança.).

        public TotaisDAO() : base() {  }

        // Método que calcula os dados monetários da pessoa especificada, com base na coluna "ID".

        public Totais Calculate_Totals(int fk_pessoa)
        {

            // Bloco de execução padrão.

            try
            {

                // Criando um objeto Model que irá armazenar os dados retornados pelo MySQL.

                Totais? valores_totais_pessoa = null;

                // Definindo uma consulta do MySQL.

                string sql = "SELECT (SELECT SUM(valor) FROM Transacao WHERE tipo = 'Despesa' AND fk_pessoa = @fk_pessoa) AS 'total_despesas', " +
                             "(SELECT SUM(valor) FROM Transacao WHERE tipo = 'Receita' AND fk_pessoa = @fk_pessoa) AS 'total_receitas' FROM Transacao transac " +
                             "WHERE fk_pessoa = @fk_pessoa ORDER BY fk_pessoa ASC;";

                // Criando uma declaração de execução para o MySQL.

                MySqlCommand stmt = new MySqlCommand(sql, base.conexao);

                // Abrindo a conexão MySQL que foi estabelecida na classe herdada (Herança.).

                base.Open_Connection();

                // Especificando os parâmetros da declaração MySQL criada anteriomente.

                stmt.Parameters.AddWithValue("@fk_pessoa", fk_pessoa);

                // Preparando a declaração MySQL criada anteriormente (Associando seus parâmetros com seus respectivos valores.).

                stmt.Prepare();

                /*
                 
                    O método "ExecuteReader()" retorna uma lista, do tipo MySQLDataReader, de registros 
                    da tabela referenciada na declaração de execução para o MySQL.

                 */

                MySqlDataReader dados_retornados = stmt.ExecuteReader();

                /*
                 
                    O método "Read()" faz com que o MySQLDataReader utilizado avance para a próxima 
                    "linha" na lista de registros, ou seja, avança para o registro a seguir dentro 
                    dessa lista. Caso não haja um próximo registro, o método retornará "False". Analisando 
                    esse funcionamento, se o utilizarmos dentro de um looping, podemos percorrer todos 
                    os registros presentes na lista retornada pelo banco de dados MySQL. É exatamente esse 
                    processo que ocorre na linha abaixo.

                 */

                while (dados_retornados.Read())
                {

                    // Instanciando um novo objeto Model para armazenar os dados da "linha" atual do looping.

                    valores_totais_pessoa = new Totais()
                    {

                        /*
                         
                            Para saber a ordem dos índices das colunas, basta executar a consulta SQL 
                            especificada anteriormente dentro do MySQL e observar a ordem, da esquerda 
                            para a direita, em que as colunas retornadas estão organizadas. A primeira 
                            coluna será o índice 0, a segunda, o 1 e assim por diante.
                            Nas linhas abaixo, também foi implementada uma verificação que analisa se 
                            o valor de um campo é null ou não existe, e, se for o caso, define um valor 
                            zerado ao atributo.

                         */

                        total_despesas = (dados_retornados.IsDBNull(0)) ? 0.00 : dados_retornados.GetDouble(0), // Obtendo o valor do campo do registro atual cujo índice da coluna é igual a zero.

                        total_receitas = (dados_retornados.IsDBNull(1)) ? 0.00 : dados_retornados.GetDouble(1) // Obtendo o valor do campo do registro atual cujo índice da coluna é igual a um.

                    };

                    // Calculando o valor do atributo "saldo_total" com base nos dois atributos já definidos acima.

                    valores_totais_pessoa.saldo_total = valores_totais_pessoa.total_receitas - valores_totais_pessoa.total_despesas;

                }

                /*
                 
                    Se a pessoa informada não possuir nenhuma transação cadastrada no banco de dados, 
                    o MySQL não retornará dados, porém, é preciso ter algo para mostrar ao usuário tela 
                    de exibição, nem que sejam valores monetários zerados, pois se exibirmos campos em branco 
                    na tela, ele poderá pensar que o aplicativo deu um erro inesperado. Essa validação é feita 
                    a seguir.

                 */

                if (valores_totais_pessoa == null)
                {

                    // Instanciando um novo objeto Model com os valores de seus atributos zerados.

                    valores_totais_pessoa = new Totais()
                    {

                        total_despesas = 0.00,

                        total_receitas = 0.00,

                        saldo_total = 0.00

                    };

                }

                // Retornando o objeto Model criado anteriormente, já preenchido com os dados retornados pelo MySQL.

                return valores_totais_pessoa;

            }

            // Bloco de exceções (É executado quando ocorrem erros.).

            catch (Exception ex)
            {

                // Gerando uma nova exceção.

                throw new Exception(ex.Message);

            }

            // Bloco de execução obrigatória (Independente de ocorrer ou não algum erro, ele é executado.).

            finally
            {

                // Fechando a conexão MySQL que foi estabelecida na classe herdada (Herança.).

                base.Close_Connection();

            }

        }

    }

}