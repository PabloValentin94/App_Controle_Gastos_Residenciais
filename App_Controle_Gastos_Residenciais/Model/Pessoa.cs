using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using App_Controle_Gastos_Residenciais.Service;

namespace App_Controle_Gastos_Residenciais.Model
{

    public class Pessoa
    {

        // Atributos (Os campos são gerados automaticamente pelo compilador.).

        public int id { get; set; }

        public string? nome { get; set; }

        public int idade { get; set; }

        public Totais? valores_totais { get; set; }

        // Métodos.

        public async Task<bool> Save()
        {

            // Retornando o resultado do método "Save" da camada Service.

            return await Data_Service_Pessoa.Save(this);

        }

        public static async Task<bool> Erase(int id)
        {

            // Retornando o resultado do método "Erase" da camada Service.

            return await Data_Service_Pessoa.Erase(id);

        }

        public static async Task<List<Pessoa>?> List()
        {

            // Retornando o resultado do método "List" da camada Service.

            return await Data_Service_Pessoa.List();

        }

    }

}
