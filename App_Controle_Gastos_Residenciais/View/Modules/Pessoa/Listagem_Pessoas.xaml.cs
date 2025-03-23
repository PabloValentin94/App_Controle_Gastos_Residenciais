using App_Controle_Gastos_Residenciais.Model;

using System.Threading.Tasks;

namespace App_Controle_Gastos_Residenciais.View.Modules.Pessoa;

public partial class Listagem_Pessoas : ContentPage
{

    // Definindo uma lista dinâmica de itens.

    List<Model.Pessoa> listagem_pessoas = new List<Model.Pessoa>();

	public Listagem_Pessoas()
	{

		InitializeComponent();

        Load_Resources();

	}

	private async void Load_Resources()
	{

		try
        {

            // Especificando o caminho do ícone da barra de navegação superior.

            btn_novo_cadastro_pessoa.IconImageSource = ImageSource.FromResource("App_Controle_Gastos_Residenciais.View.Assets.Images.Add.png");

        }

		catch (Exception ex)
		{

            await DisplayAlert("Erro!", ex.Message, "OK");

        }

    }

    // Método que preenche o ListView (Tabela.) da página.

    private async void Load_List_View()
    {

        try
        {

            // Obtendo os registros de todas as pessoas cadastradas no banco de dados.

            List<Model.Pessoa> lista_pessoas = Model.Pessoa.List();

            // Verificando o banco de dados retornou uma lista com ao menos um registro.

            if (lista_pessoas != null && lista_pessoas.Count > 0)
            {

                // Executando uma atividade assíncrona, pois, senão fizer isso, a página irá travar até que ela seja concluída.

                await Task.Run(() => {

                    // Removendo todos os itens da lista.

                    this.listagem_pessoas.Clear();

                    // Inserindo todos os registros retornados pelo banco de dados na lista.

                    lista_pessoas.ForEach(pessoa => { this.listagem_pessoas.Add(pessoa); });

                });

                // Atualizando o ListView (Tabela.) da página.

                list_view_pessoas.ItemsSource = this.listagem_pessoas.ToList<Model.Pessoa>();

            }

            else
            {

                // Removendo todos os itens da lista.

                this.listagem_pessoas.Clear();

                // Atualizando o ListView (Tabela.) da página com uma lista vazia.

                list_view_pessoas.ItemsSource = this.listagem_pessoas.ToList<Model.Pessoa>();

            }

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

            Load_List_View();

        }

		catch (Exception ex)
		{

            await DisplayAlert("Erro!", ex.Message, "OK");

        }

    }

    private async void btn_novo_cadastro_pessoa_Clicked(object sender, EventArgs e)
    {

		try
		{

            // Exibindo um aviso, com opções de escolha, para o usuário.

            if (await DisplayAlert("Aviso!", "A página atual será fechada! Deseja prosseguir?", "Sim", "Não"))
            {

                // Excluindo todos os itens da lista, pois ela será preenchida novamente quando esta página for aberta outra vez.

                this.listagem_pessoas.Clear();

                // Abrindo outra página (Adicionando na pilha de páginas da aplicação.).

                await Navigation.PushAsync(new Cadastro_Pessoa());

            }

        }

		catch (Exception ex)
		{

            await DisplayAlert("Erro!", ex.Message, "OK");

        }

    }

    private async void btn_deletar_pessoa_Clicked(object sender, EventArgs e)
    {

        try
        {

            // Obtendo o botão do menu de contexto referente a linha que disparou este evento.

            MenuItem disparador = (MenuItem) sender;

            // Obtendo os dados da pessoa selecionada no ListView.

            Model.Pessoa pessoa_selecionada = (Model.Pessoa) disparador.BindingContext;

            // Exibindo um aviso, com opções de escolha, para o usuário.

            if (await DisplayAlert("Atenção!", "Realmente deseja apagar a pessoa selecionada?", "Sim", "Não"))
            {

                // Se os dados forem apagados corretamente, será exibida uma mensagem de êxito, senão, uma de erro.

                if (Model.Pessoa.Erase(pessoa_selecionada.id))
                {

                    // Excluindo o item selecionado no ListView da lista.

                    this.listagem_pessoas.Remove(pessoa_selecionada);

                    // Recarregando o ListView da página.

                    Load_List_View();

                    await DisplayAlert("Sucesso!", "Pessoa deletada com êxito.", "OK");

                }

                else
                {

                    throw new Exception("Erro ao tentar deletar! Tente novamente.");

                }

            }

        }

        catch (Exception ex)
        {

            await DisplayAlert("Erro!", ex.Message, "OK");

        }

    }

}