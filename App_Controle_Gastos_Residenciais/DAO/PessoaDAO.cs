using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using App_Controle_Gastos_Residenciais.Model;

using MySql.Data.MySqlClient;

namespace App_Controle_Gastos_Residenciais.DAO
{

    public class PessoaDAO : Connection // Extendendo da classe Connection (Herança.).
    {

        // Executando o construtor da classe pai à partir do construtor da classe filha (Herança.).

        public PessoaDAO() : base() {  }

        // Método que insere um registro na tabela.

        public bool Insert(Pessoa model)
        {

            // Bloco de execução padrão.

            try
            {

                // Definindo uma consulta do MySQL.

                string sql = "INSERT INTO Pessoa(nome, idade) VALUES(@nome, @idade)";

                // Criando uma declaração de execução para o MySQL.

                MySqlCommand stmt = new MySqlCommand(sql, base.conexao);

                // Abrindo a conexão MySQL que foi estabelecida na classe herdada (Herança.).

                base.Open_Connection();

                // Especificando os parâmetros da declaração MySQL criada anteriomente.

                stmt.Parameters.AddWithValue("@nome", model.nome);

                stmt.Parameters.AddWithValue("@idade", model.idade);

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

        // Método que apaga um registro da tabela.

        public bool Delete(int id)
        {

            // Bloco de execução padrão.

            try
            {

                // Definindo uma consulta do MySQL.

                string sql = "DELETE FROM Pessoa WHERE id = @id";

                // Criando uma declaração de execução para o MySQL.

                MySqlCommand stmt = new MySqlCommand(sql, base.conexao);

                // Abrindo a conexão MySQL que foi estabelecida na classe herdada (Herança.).

                base.Open_Connection();

                // Especificando os parâmetros da declaração MySQL criada anteriomente.

                stmt.Parameters.AddWithValue("@id", id);

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

        public List<Pessoa> Select()
        {

            // Bloco de execução padrão.

            try
            {

                // Criando uma lista que irá armazenar todos os registros retornados pelo MySQL.

                List<Pessoa> lista_pessoas = new List<Pessoa>();

                // Definindo uma consulta do MySQL.

                string sql = "SELECT * FROM Pessoa ORDER BY nome ASC";

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

                    Pessoa pessoa = new Pessoa()
                    {

                        /*
                         
                            Para saber a ordem dos índices das colunas, basta executar a consulta SQL 
                            especificada anteriormente dentro do MySQL e observar a ordem, da esquerda 
                            para a direita, em que as colunas retornadas estão organizadas. A primeira 
                            coluna será o índice 0, a segunda, o 1 e assim por diante.

                         */

                        id = registros_retornados.GetInt32(0), // Obtendo o valor do campo do registro atual cujo índice da coluna é igual a zero.

                        nome = registros_retornados.GetString(1), // Obtendo o valor do campo do registro atual cujo índice da coluna é igual a um.

                        idade = registros_retornados.GetInt32(2) // Obtendo o valor do campo do registro atual cujo índice da coluna é igual a dois.

                    };

                    // Obtendo os dados monetários referentes a este registro da tabela Pessoa.

                    pessoa.valores_totais = (new TotaisDAO()).Calculate_Totals(pessoa.id);

                    // Adicionando o registro atual na lista que será retornada por este método.

                    lista_pessoas.Add(pessoa);

                }

                // Retornando a lista criada anteriormente, já preenchida com todos os registros retornados pelo MySQL.

                return lista_pessoas;

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

        // Método que retorna um registro em específico da tabela.

        public Pessoa? Search(int id)
        {

            // Bloco de execução padrão.

            try
            {

                // Criando um objeto Model que irá armazenar o registro retornado pelo MySQL.

                Pessoa? pessoa = null;

                // Definindo uma consulta do MySQL.

                string sql = "SELECT * FROM Pessoa WHERE id = @id ORDER BY id ASC";

                // Criando uma declaração de execução para o MySQL.

                MySqlCommand stmt = new MySqlCommand(sql, base.conexao);

                // Abrindo a conexão MySQL que foi estabelecida na classe herdada (Herança.).

                base.Open_Connection();

                // Especificando os parâmetros da declaração MySQL criada anteriomente.

                stmt.Parameters.AddWithValue("@id", id);

                // Preparando a declaração MySQL criada anteriormente (Associando seus parâmetros com seus respectivos valores.).

                stmt.Prepare();

                /*
                 
                    O método "ExecuteReader()" retorna uma lista, do tipo MySQLDataReader, de registros 
                    da tabela referenciada na declaração de execução para o MySQL.

                 */

                MySqlDataReader registro_retornado = stmt.ExecuteReader();

                /*
                 
                    O método "Read()" faz com que o MySQLDataReader utilizado avance para a próxima 
                    "linha" na lista de registros, ou seja, avança para o registro a seguir dentro 
                    dessa lista. Caso não haja um próximo registro, o método retornará "False". Analisando 
                    esse funcionamento, se o utilizarmos dentro de um looping, podemos percorrer todos 
                    os registros presentes na lista retornada pelo banco de dados MySQL. É exatamente esse 
                    processo que ocorre na linha abaixo.
                    Como passamos o parâmetro "id", que é único, como um filtro, apenas um registro será 
                    retornado na lista.

                 */

                while (registro_retornado.Read())
                {

                    // Instanciando um novo objeto Model para armazenar o registro atual do looping.

                    pessoa = new Pessoa()
                    {

                        /*
                         
                            Para saber a ordem dos índices das colunas, basta executar a consulta SQL 
                            especificada anteriormente dentro do MySQL e observar a ordem, da esquerda 
                            para a direita, em que as colunas retornadas estão organizadas. A primeira 
                            coluna será o índice 0, a segunda, o 1 e assim por diante.

                         */

                        id = registro_retornado.GetInt32(0), // Obtendo o valor do campo do registro atual cujo índice da coluna é igual a zero.

                        nome = registro_retornado.GetString(1), // Obtendo o valor do campo do registro atual cujo índice da coluna é igual a um.

                        idade = registro_retornado.GetInt32(2) // Obtendo o valor do campo do registro atual cujo índice da coluna é igual a dois.

                    };

                    // Obtendo os dados monetários referentes a este registro da tabela Pessoa.

                    pessoa.valores_totais = (new TotaisDAO()).Calculate_Totals(pessoa.id);

                }

                // Retornando o objeto Model criado anteriormente, já preenchido com o registro retornado pelo MySQL.

                return pessoa;

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