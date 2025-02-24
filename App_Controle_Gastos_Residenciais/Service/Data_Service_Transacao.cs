using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using App_Controle_Gastos_Residenciais.Model;

using Newtonsoft.Json;

namespace App_Controle_Gastos_Residenciais.Service
{

    public class Data_Service_Transacao : Api
    {

        // Método que cadastra uma transação.

        public static async Task<bool> Save(Transacao model)
        {

            // Convertendo o parâmetro passado por este método em um JSON.

            string post_json = JsonConvert.SerializeObject(model);

            // Obtendo o resultado retornado pela API.

            string returned_json = await Send_Data_Api("/transacao/salvar", post_json);

            // Convertendo o resultado retornado pela API para o mesmo tipo do retorno deste método e salvando em uma variável.

            bool result = JsonConvert.DeserializeObject<bool>(returned_json);

            // Retornando o resultado da API.

            return result;

        }

        // Método que obtém registros, da tabela Transacao, do banco dados, através da API PHP.

        public static async Task<List<Transacao>?> List()
        {

            // Obtendo o resultado retornado pela API.

            string returned_json = await Get_Data_Api("/transacao/listar");

            // Convertendo o resultado retornado pela API para o mesmo tipo do retorno deste método e salvando em uma variável.

            List<Transacao>? result = JsonConvert.DeserializeObject<List<Transacao>?>(returned_json);

            // Retornando o resultado da API.

            return result;

        }

    }

}