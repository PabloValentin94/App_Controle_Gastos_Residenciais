using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;

namespace App_Controle_Gastos_Residenciais.Service
{

    public abstract class Api
    {

        // Host específico para conexão do emulador Android com a API PHP.

        private static readonly string host = "http://10.0.2.2:8000";

        // Método que exibe a saída retornada pela API no console de depuração. Usado para fins de teste.

        private static void Show_Return(string message)
        {

            Debug.WriteLine("----------------------------------------------------------");

            Debug.WriteLine(message);

            Debug.WriteLine("----------------------------------------------------------");

        }

        // Método que obtém dados através da API (Requisição GET.).

        protected static async Task<string> Get_Data_Api(string route)
        {

            // Variável onde o JSON retornado pela API será armazenado.

            string? json = null;

            // Definindo a URL completa à requisição.

            string url = host + route;

            /*
             
                De forma simplificada, na linha abaixo estamos usando uma classe do .NET que "abre" uma 
                janela de navegador e executa a URL especificada na barra de URL do navegador. Esta janela 
                não é acessível para o usuário, apenas à máquina. Como o PHP funciona com base em rotas, é 
                o caminho ideal a ser utilizado nesta situação.

            */

            using (HttpClient browser = new HttpClient())
            {

                // Obtendo a resposta retornada pela API

                HttpResponseMessage api_response = await browser.GetAsync(url);

                // Exibindo a resposta, convertida para uma string, no console de depuração.

                Show_Return(api_response.Content.ReadAsStringAsync().Result);

                /*
             
                    Se uma resposta válida foi retornada pelo navegador, então ela será armazenada 
                    na variável especificada no início deste método, senão, será gerada uma exceção (Erro.).

                */

                if (api_response.IsSuccessStatusCode)
                {

                    json = api_response.Content.ReadAsStringAsync().Result; // Convertendo a resposta em uma string.

                }

                else
                {

                    throw new Exception("Erro ao obter dados da API!"); // Gerando um erro.

                }

            }

            // Retornando a resposta da API.

            return json;

        }

        // Método que envia dados à API (Requisição POST.).

        protected static async Task<string> Send_Data_Api(string route, string json_object)
        {

            // Variável onde o JSON retornado pela API será armazenado.

            string? json = null;

            // Definindo a URL completa à requisição.

            string url = host + route;

            /*
             
                De forma simplificada, na linha abaixo estamos usando uma classe do .NET que "abre" uma 
                janela de navegador e executa a URL especificada na barra de URL do navegador. Esta janela 
                não é acessível para o usuário, apenas à máquina. Como o PHP funciona com base em rotas, é 
                o caminho ideal a ser utilizado nesta situação.

            */

            using (HttpClient browser = new HttpClient())
            {

                // Obtendo a resposta retornada pela API

                HttpResponseMessage api_response = await browser.PostAsync(url, new StringContent(json_object, Encoding.UTF8, "application/json"));

                // Exibindo a resposta, convertida para uma string, no console de depuração.

                Show_Return(api_response.Content.ReadAsStringAsync().Result);

                /*
             
                    Se uma resposta válida foi retornada pelo navegador, então ela será armazenada 
                    na variável especificada no início deste método, senão, será gerada uma exceção (Erro.).

                */

                if (api_response.IsSuccessStatusCode)
                {

                    json = api_response.Content.ReadAsStringAsync().Result; // Convertendo a resposta em uma string.

                }

                else
                {

                    throw new Exception("Erro ao enviar dados à API!"); // Gerando um erro.

                }

            }

            // Retornando a resposta da API.

            return json;

        }

    }

}