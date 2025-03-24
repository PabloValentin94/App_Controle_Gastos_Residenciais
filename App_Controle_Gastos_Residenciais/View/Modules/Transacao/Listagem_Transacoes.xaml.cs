using App_Controle_Gastos_Residenciais.Model;

using System.Threading.Tasks;

namespace App_Controle_Gastos_Residenciais.View.Modules.Transacao;

public partial class Listagem_Transacoes : ContentPage
{

    // Definindo uma lista de itens.

    List<Model.Transacao> listagem_transacoes = new List<Model.Transacao>();

	public Listagem_Transacoes()
	{

		InitializeComponent();

		Load_Resources();

	}

	private async void Load_Resources()
	{

		try
        {

            // Especificando o caminho do ícone da barra de navegação superior.

            btn_novo_cadastro_transacao.IconImageSource = ImageSource.FromResource("App_Controle_Gastos_Residenciais.View.Assets.Images.Add.png");

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

            // Obtendo os registros de todas as transações cadastradas no banco de dados.

            List<Model.Transacao> lista_transacoes = Model.Transacao.List();

            // Verificando se o banco de dados retornou uma lista com ao menos um registro.

            if (lista_transacoes != null && lista_transacoes.Count > 0)
            {

                // Executando uma atividade assíncrona, pois, senão fizer isso, a página irá travar até que ela seja concluída.

                await Task.Run(() => {

                    // Removendo todos os itens da lista.

                    this.listagem_transacoes.Clear();

                    // Inserindo todos os registros retornados pelo banco de dados na lista.

                    lista_transacoes.ForEach(transacao => { this.listagem_transacoes.Add(transacao); });

                });

                // Atualizando o ListView (Tabela.) da página.

                list_view_transacoes.ItemsSource = this.listagem_transacoes.ToList<Model.Transacao>();

            }

            else
            {

                // Removendo todos os itens da lista.

                this.listagem_transacoes.Clear();

                // Atualizando o ListView (Tabela.) da página com uma lista vazia.

                list_view_transacoes.ItemsSource = this.listagem_transacoes.ToList<Model.Transacao>();

            }

        }

		catch (Exception ex)
		{

            await DisplayAlert("Erro!", ex.Message, "OK");

        }

    }

    private async void btn_novo_cadastro_transacao_Clicked(object sender, EventArgs e)
    {

        try
		{

            // Obtendo os registros de todas as pessoas cadastradas no banco de dados.

            List<Model.Pessoa> lista_pessoas = Model.Pessoa.List();

            // Verificando se o banco de dados retornou uma lista com ao menos um registro.

            if (lista_pessoas != null && lista_pessoas.Count > 0)
            {

                // Exibindo um aviso, com opções de escolha, para o usuário.

                if (await DisplayAlert("Aviso!", "A página atual será fechada! Deseja prosseguir?", "Sim", "Não"))
                {

                    // Excluindo todos os itens da lista, pois ela será preenchida novamente quando esta página for aberta outra vez.

                    this.listagem_transacoes.Clear();

                    // Abrindo outra página (Adicionando na pilha de páginas da aplicação.).

                    await Navigation.PushAsync(new Cadastro_Transacao());

                }

            }

            else
            {

                // Exibindo um aviso.

                await DisplayAlert("Atenção!", "É impossível criar uma transação sem nenhuma pessoa cadastrada.", "OK");

            }

        }

		catch (Exception ex)
		{

            await DisplayAlert("Erro!", ex.Message, "OK");

        }

    }

}