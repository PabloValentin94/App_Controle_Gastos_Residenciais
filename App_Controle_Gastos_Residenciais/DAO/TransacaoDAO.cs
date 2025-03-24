using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using App_Controle_Gastos_Residenciais.Model;

using MySql.Data.MySqlClient;

namespace App_Controle_Gastos_Residenciais.DAO
{

    public class TransacaoDAO : Connection // Extendendo da classe Connection (Herança.).
    {

        // Executando o construtor da classe pai à partir do construtor da classe filha (Herança.).

        public TransacaoDAO() : base() {  }

        // Método que insere um registro na tabela.

        public bool Insert(Transacao model)
        {

            // Bloco de execução padrão.

            try
            {

                // Definindo uma consulta do MySQL.

                string sql = "INSERT INTO Transacao(descricao, valor, tipo, fk_pessoa) VALUES(@descricao, @valor, @tipo, @fk_pessoa)";

                // Criando uma declaração de execução para o MySQL.

                MySqlCommand stmt = new MySqlCommand(sql, base.conexao);

                // Abrindo a conexão MySQL que foi estabelecida na classe herdada (Herança.).

                base.Open_Connection();

                // Especificando os parâmetros da declaração MySQL criada anteriomente.

                stmt.Parameters.AddWithValue("@descricao", model.descricao);

                stmt.Parameters.AddWithValue("@valor", model.valor);

                stmt.Parameters.AddWithValue("@tipo", model.tipo);

                stmt.Parameters.AddWithValue("@fk_pessoa", model.fk_pessoa);

                // Preparando a declaração MySQL criada anteriormente (Associando seus parâmetros com seus respectivos valores.).

                stmt.Prepare();

                /*
                 
                    O método "ExecuteNonQuery()" executa a declaração MySQL que foi definida e retorna a 
                    quantidade de linhas da tabela referenciada que foram afetadas. Se um novo registro 
                    foi inserido, então uma linha foi afetada, senão, nenhuma linha foi afetada. A partir 
                    deste retorno podemos definir se uma consulta foi executada com sucesso, utilizando um 
                    valor booleano ("True", se funcionou, e "False", caso não tenha funcionado). É exatamente 
                    isso que a linha abaixo faz.

                 */

                return (stmt.ExecuteNonQuery() > 0); // Retornando se a execução do MySQL funcionou.

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

        // Método que obtém todos os registros da tabela.

        public List<Transacao> Select()
        {

            // Bloco de execução padrão.

            try
            {

                // Criando uma lista que irá armazenar todos os registros retornados pelo MySQL.

                List<Transacao> lista_transacoes = new List<Transacao>();

                // Definindo uma consulta do MySQL.

                string sql = "SELECT * FROM Transacao ORDER BY id ASC";

                // Criando uma declaração de execução para o MySQL.

                MySqlCommand stmt = new MySqlCommand(sql, base.conexao);

                // Abrindo a conexão MySQL que foi estabelecida na classe herdada (Herança.).

                base.Open_Connection();

                /*
                 
                    O método "ExecuteReader()" retorna uma lista, do tipo MySQLDataReader, de registros 
                    da tabela referenciada na declaração de execução para o MySQL.

                 */

                MySqlDataReader registros_retornados = stmt.ExecuteReader();

                /*
                 
                    O método "Read()" faz com que o MySQLDataReader utilizado avance para a próxima 
                    "linha" na lista de registros, ou seja, avança para o registro a seguir dentro 
                    dessa lista. Caso não haja um próximo registro, o método retornará "False". Analisando 
                    esse funcionamento, se o utilizarmos dentro de um looping, podemos percorrer todos 
                    os registros presentes na lista retornada pelo banco de dados MySQL. É exatamente esse 
                    processo que ocorre na linha abaixo.

                 */

                while (registros_retornados.Read())
                {

                    // Instanciando um novo objeto Model para armazenar o registro atual do looping.

                    Transacao transacao = new Transacao()
                    {

                        /*
                         
                            Para saber a ordem dos índices das colunas, basta executar a consulta SQL 
                            especificada anteriormente dentro do MySQL e observar a ordem, da esquerda 
                            para a direita, em que as colunas retornadas estão organizadas. A primeira 
                            coluna será o índice 0, a segunda, o 1 e assim por diante.

                         */

                        id = registros_retornados.GetInt32(0), // Obtendo o valor do campo do registro atual cujo índice da coluna é igual a zero.

                        descricao = registros_retornados.GetString(1), // Obtendo o valor do campo do registro atual cujo índice da coluna é igual a um.

                        valor = registros_retornados.GetDouble(2), // Obtendo o valor do campo do registro atual cujo índice da coluna é igual a dois.

                        tipo = registros_retornados.GetString(3), // Obtendo o valor do campo do registro atual cujo índice da coluna é igual a três.

                        fk_pessoa = registros_retornados.GetInt32(4) // Obtendo o valor do campo do registro atual cujo índice da coluna é igual a quatro.

                    };

                    // Obtendo o objeto Model referente à pessoa que criou a transação atual do looping.

                    transacao.pessoa = (new PessoaDAO()).Search(transacao.fk_pessoa);

                    // Adicionando o registro atual na lista que será retornada por este método.

                    lista_transacoes.Add(transacao);

                }

                // Retornando a lista criada anteriormente, já preenchida com todos os registros retornados pelo MySQL.

                return lista_transacoes;

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