using App_Controle_Gastos_Residenciais.Model;

using System.Threading.Tasks;

namespace App_Controle_Gastos_Residenciais.View.Modules.Transacao;

public partial class Totais_Transacoes : ContentPage
{

    // Definindo uma lista dinâmica.

    List<Model.Pessoa> listagem_pessoas = new List<Model.Pessoa>();

    // Definindo um array que armazena os valores monetários gerais (De todas as pessoas somadas.).

    string[] dados_monetarios_gerais = new string[] { "$0,00", "$0,00", "$0,00" };

    public Totais_Transacoes()
	{

		InitializeComponent();

	}

    private async void Calculate_General_Monetary_Data(List<Model.Pessoa> pessoas)
    {

        try
        {

            // Variáveis de suporte.

            double total_geral_despesas = 0;

            double total_geral_receitas = 0;

            double saldo_geral = 0;

            // Iterando os registros da lista passada como parâmetro.

            foreach (Model.Pessoa pessoa in pessoas)
            {

                // Somando o valor total em despesas da pessoa atual do looping na variável de total geral de despesas.

                total_geral_despesas += pessoa.valores_totais.total_despesas;

                // Somando o valor total em receitas da pessoa atual do looping na variável de total geral de receitas.

                total_geral_receitas += pessoa.valores_totais.total_receitas;

                // Somando o valor do saldo da pessoa atual do looping na variável de saldo geral.

                saldo_geral += pessoa.valores_totais.saldo_total;

            }

            // Salvando os dados calculados, convertidos em strings, na variável global correspondente.

            this.dados_monetarios_gerais = new string[] {

                total_geral_despesas.ToString("C2"),
                total_geral_receitas.ToString("C2"),
                saldo_geral.ToString("C2")

            };

        }

        catch (Exception ex)
        {

            await DisplayAlert("Erro!", ex.Message, "OK");

        }

    }

    protected override async void OnAppearing()
    {

        try
        {

            // Obtendo os registros de todas as pessoas cadastradas no banco de dados.

            List<Model.Pessoa> lista_pessoas = Model.Pessoa.List();

            // Verificando o banco de dados retornou uma lista com ao menos um registro.

            if (lista_pessoas != null && lista_pessoas.Count > 0)
            {

                // Executando uma atividade assíncrona, pois, senão fizer isso, a página irá travar até que ela seja concluída.

                await Task.Run(() =>
                {

                    // Removendo todos os itens da lista.

                    this.listagem_pessoas.Clear();

                    // Inserindo todos os registros retornados pelo banco de dados na lista.

                    lista_pessoas.ForEach(pessoa => { this.listagem_pessoas.Add(pessoa); });

                });

                // Atualizando o ListView (Tabela.) da página.

                list_view_totais_pessoas.ItemsSource = this.listagem_pessoas.ToList<Model.Pessoa>();

                // Calculando o valor geral de despesas, receitas e saldos.

                Calculate_General_Monetary_Data(this.listagem_pessoas);

            }

            else
            {

                // Removendo todos os itens da lista.

                this.listagem_pessoas.Clear();

                // Atualizando o ListView (Tabela.) da página com uma lista vazia.

                list_view_totais_pessoas.ItemsSource = this.listagem_pessoas.ToList<Model.Pessoa>();

                // Calculando o valor geral de despesas, receitas e saldos.

                Calculate_General_Monetary_Data(this.listagem_pessoas);

            }

        }

        catch (Exception ex)
        {

            await DisplayAlert("Erro!", ex.Message, "OK");

        }

    }

    private async void btn_exibir_dados_monetarios_gerais_Clicked(object sender, EventArgs e)
    {

        try
        {

            string mensagem = $"\nQuantidade de pessoas cadastradas: {this.listagem_pessoas.Count}.\n\n" +
                              $"Valor total geral em despesas: {this.dados_monetarios_gerais[0]}\n\n" +
                              $"Valor total geral em receitas: {this.dados_monetarios_gerais[1]}\n\n" +
                              $"Saldo geral: {this.dados_monetarios_gerais[2]}";

            // Exibindo os dados monetários gerais na tela.

            await DisplayAlert("Valores Monetários Gerais", mensagem, "OK");

        }

        catch (Exception ex)
        {

            await DisplayAlert("Erro!", ex.Message, "OK");

        }

    }

}