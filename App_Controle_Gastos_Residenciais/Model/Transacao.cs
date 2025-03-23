using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using App_Controle_Gastos_Residenciais.DAO;

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

        public bool Save()
        {

            // Retornando o resultado do método "Insert" da camada DAO.

            return (new TransacaoDAO()).Insert(this);

        }

        public static List<Transacao> List()
        {

            // Retornando o resultado do método "Select" da camada DAO.

            return (new TransacaoDAO()).Select();

        }

    }

}