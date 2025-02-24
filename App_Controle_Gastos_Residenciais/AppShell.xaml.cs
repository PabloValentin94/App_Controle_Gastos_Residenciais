using App_Controle_Gastos_Residenciais.View.Modules.Pessoa;
using App_Controle_Gastos_Residenciais.View.Modules.Transacao;

namespace App_Controle_Gastos_Residenciais
{

    public partial class AppShell : Shell
    {

        public AppShell()
        {

            InitializeComponent();

            Load_Resources();

        }

        private async void Load_Resources()
        {

            try
            {

                // Especificando o caminho dos ícones das abas da barra de navegação inferior.

                tab_pessoa.Icon = ImageSource.FromResource("App_Controle_Gastos_Residenciais.View.Assets.Images.Person.png");

                tab_transacao.Icon = ImageSource.FromResource("App_Controle_Gastos_Residenciais.View.Assets.Images.Transaction.png");

                tab_totais.Icon = ImageSource.FromResource("App_Controle_Gastos_Residenciais.View.Assets.Images.Query.png");

                // Especificando as páginas que devem ser abertas para cada aba da barra de navegação inferior.

                tab_pessoa.Content = new Listagem_Pessoas();

                tab_transacao.Content = new Listagem_Transacoes();

                tab_totais.Content = new Totais_Transacoes();

            }

            catch (Exception ex)
            {

                await DisplayAlert("Erro!", ex.Message, "OK");

            }

        }

    }

}