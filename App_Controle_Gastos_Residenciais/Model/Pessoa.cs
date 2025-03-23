using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using App_Controle_Gastos_Residenciais.DAO;

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

        public bool Save()
        {

            // Retornando o resultado do método "Insert" da camada DAO.

            return (new PessoaDAO()).Insert(this);

        }

        public static bool Erase(int id)
        {

            // Retornando o resultado do método "Delete" da camada DAO.

            return (new PessoaDAO()).Delete(id);

        }

        public static List<Pessoa> List()
        {

            // Retornando o resultado do método "Select" da camada DAO.

            return (new PessoaDAO()).Select();

        }

        public static Pessoa? Find(int id)
        {

            // Retornando o resultado do método "Search" da camada DAO.

            return (new PessoaDAO()).Search(id);

        }

    }

}