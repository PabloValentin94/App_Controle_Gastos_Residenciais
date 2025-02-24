using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using App_Controle_Gastos_Residenciais.Service;

namespace App_Controle_Gastos_Residenciais.Model
{

    public class Transacao
    {

        // Atributos (Os campos são gerados automaticamente pelo compilador.).

        public int id { get; set; }

        public string? descricao { get; set; }

        public double valor { get; set; }

        public string? tipo { get; set; }

        public int fk_pessoa { get; set; }

        public Pessoa? pessoa { get; set; }

        // Métodos.

        public async Task<bool> Save()
        {

            // Retornando o resultado do método "Save" da camada Service.

            return await Data_Service_Transacao.Save(this);

        }

        public static async Task<List<Transacao>?> List()
        {

            // Retornando o resultado do método "List" da camada Service.

            return await Data_Service_Transacao.List();

        }

    }

}
