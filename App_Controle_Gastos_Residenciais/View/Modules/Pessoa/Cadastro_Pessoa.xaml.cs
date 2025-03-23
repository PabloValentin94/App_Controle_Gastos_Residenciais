using App_Controle_Gastos_Residenciais.Model;

using System.Collections.ObjectModel;

namespace App_Controle_Gastos_Residenciais.View.Modules.Pessoa;

public partial class Cadastro_Pessoa : ContentPage
{

    public Cadastro_Pessoa()
	{

		InitializeComponent();

	}

    private async void btn_salvar_pessoa_Clicked(object sender, EventArgs e)
    {

		try
		{

            // Verificando se todos os campos estão preenchidos corretamente.

            if (String.IsNullOrWhiteSpace(txt_nome_pessoa.Text) || String.IsNullOrWhiteSpace(txt_idade_pessoa.Text))
            {

                throw new Exception("Preencha todos os campos corretamente antes de prosseguir."); // Gerando um erro.

            }

            else
            {

                // Instanciando um novo objeto Model.

                Model.Pessoa model = new Model.Pessoa()
                {

                    nome = txt_nome_pessoa.Text,

                    idade = int.Parse(txt_idade_pessoa.Text)

                };

                // Obtendo o resultado retornado pelo método "Save" da camada Model.

                bool success = model.Save();

                // Se os dados forem cadastrados corretamente, será exibida uma mensagem de êxito, senão, uma de erro.

                if (success)
                {

                    await DisplayAlert("Sucesso!", "Pessoa cadastrada com êxito.", "OK");

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