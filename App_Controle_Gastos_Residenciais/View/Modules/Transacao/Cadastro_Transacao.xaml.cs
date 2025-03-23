using App_Controle_Gastos_Residenciais.Model;

namespace App_Controle_Gastos_Residenciais.View.Modules.Transacao;

public partial class Cadastro_Transacao : ContentPage
{

	public Cadastro_Transacao()
	{

		InitializeComponent();

        Load_Inputs();

	}

    private async void Load_Inputs()
	{

		try
		{

            // Preenchendo a caixa de seleção do proprietário da transação.

            pck_proprietario_transacao.ItemsSource = Model.Pessoa.List();

            // Definindo que o primeiro item da caixa de seleção do proprietário da transação é que deve ser exibido.

            pck_proprietario_transacao.SelectedIndex = 0;

            // Preenchendo a caixa de seleção do tipo da transação.

            pck_tipo_transacao.ItemsSource = new List<string>() { "Despesa", "Receita" };

            // Definindo que o primeiro item da caixa de seleção do tipo da transação é que deve ser exibido.

            pck_tipo_transacao.SelectedIndex = 0;

        }

		catch (Exception ex)
		{

            await DisplayAlert("Erro!", ex.Message, "OK");

        }

    }

    private async void Verify_Transaction_Owner_Age(int idade_proprietario_transacao = 18)
    {

        try
        {

            /*
             
                Se o tipo de transação selecionado for receita, e o proprietário da transação for um menor de idade, o tipo será 
                redefinido para a opção despesa e será exibido um aviso na tela.
            
            */

            if (pck_tipo_transacao.SelectedIndex == 1 && idade_proprietario_transacao < 18)
            {

                pck_tipo_transacao.SelectedIndex = 0;

                await DisplayAlert("Aviso!", "Somente pessoas maiores de idade podem cadastrar receitas!", "OK");

            }

        }

        catch (Exception ex)
        {

            await DisplayAlert("Erro!", ex.Message, "OK");

        }

    }

    private async void pck_proprietario_transacao_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {

            // Redefinindo o valor da caixa de seleção do tipo da transação toda vez que o item da caixa de seleção do proprietário for alterado.

            pck_tipo_transacao.SelectedIndex = 0;

        }

        catch (Exception ex)
        {

            await DisplayAlert("Erro!", ex.Message, "OK");

        }

    }

    private async void pck_tipo_transacao_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {

            // Verificando a idade do proprietário da transação toda vez que o item da caixa de seleção do tipo for alterado.

            Verify_Transaction_Owner_Age(((Model.Pessoa) pck_proprietario_transacao.SelectedItem).idade);

        }

        catch (Exception ex)
        {

            await DisplayAlert("Erro!", ex.Message, "OK");

        }

    }

    private async void btn_salvar_transacao_Clicked(object sender, EventArgs e)
    {

		try
		{

            // Verificando se todos os campos estão preenchidos corretamente.

            if (String.IsNullOrWhiteSpace(txt_descricao_transacao.Text) || String.IsNullOrWhiteSpace(txt_valor_transacao.Text)
                || pck_tipo_transacao.SelectedIndex < 0 || pck_proprietario_transacao.SelectedIndex < 0)
            {

                throw new Exception("Preencha todos os campos corretamente antes de prosseguir."); // Gerando um erro.

            }

            else
            {

                // Instanciando um novo objeto Model.

                Model.Transacao model = new Model.Transacao()
                {

                    id = 0,

                    descricao = txt_descricao_transacao.Text,

                    valor = double.Parse(txt_valor_transacao.Text),

                    tipo = pck_tipo_transacao.SelectedItem.ToString(),

                    fk_pessoa = ((Model.Pessoa) pck_proprietario_transacao.SelectedItem).id,

                    pessoa = null

                };

                // Obtendo o resultado retornado pelo método "Save" da camada Model.

                bool success = model.Save();

                // Se os dados forem cadastrados corretamente, será exibida uma mensagem de êxito, senão, uma de erro.

                if (success)
                {

                    await DisplayAlert("Sucesso!", "Transação cadastrada com êxito.", "OK");

                    await Navigation.PopAsync();

                }

                else
                {

                    throw new Exception("Erro ao tentar cadastrar! Tente novamente.");

                }

            }

        }

		catch (Exception ex)
		{

			await DisplayAlert("Erro!", ex.Message, "OK");

		}

    }

}