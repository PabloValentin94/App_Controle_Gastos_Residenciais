using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using App_Controle_Gastos_Residenciais.Model;

using Newtonsoft.Json;

namespace App_Controle_Gastos_Residenciais.Service
{

    public class Data_Service_Pessoa : Api
    {

        // Método que cadastra uma pessoa.

        public static async Task<bool> Save(Pessoa model)
        {

            // Convertendo o parâmetro passado por este método em um JSON.

            string post_object = JsonConvert.SerializeObject(model);

            // Obtendo o resultado retornado pela API.

            string returned_json = await Send_Data_Api("/pessoa/salvar", post_object);

            // Convertendo o resultado retornado pela API para o mesmo tipo do retorno deste método e salvando em uma variável.

            bool result = JsonConvert.DeserializeObject<bool>(returned_json);

            // Retornando o resultado da API.

            return result;

        }

        // Método que deleta uma pessoa.

        public static async Task<bool> Erase(int id)
        {

            // Convertendo o parâmetro passado por este método em um JSON.

            string post_object = JsonConvert.SerializeObject(id);

            // Obtendo o resultado retornado pela API.

            string returned_json = await Send_Data_Api("/pessoa/deletar", post_object);

            // Convertendo o resultado retornado pela API para o mesmo tipo do retorno deste método e salvando em uma variável.

            bool result = JsonConvert.DeserializeObject<bool>(returned_json);

            // Retornando o resultado da API.

            return result;

        }

        // Método que obtém registros, da tabela Pessoa, do banco dados, através da API PHP.

        public static async Task<List<Pessoa>?> List()
        {

            // Obtendo o resultado retornado pela API.

            string returned_json = await Get_Data_Api("/pessoa/listar");

            // Convertendo o resultado retornado pela API para o mesmo tipo do retorno deste método e salvando em uma variável.

            List<Pessoa>? result = JsonConvert.DeserializeObject<List<Pessoa>?>(returned_json);

            // Retornando o resultado da API.

            return result;

        }

    }

}