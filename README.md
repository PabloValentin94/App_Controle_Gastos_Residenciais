# APP (Sem API) - Sistema de Controle de Gastos Residenciais

Desenvolvimento de um aplicativo que permite que pessoas possam gerenciar suas transações monetárias.

## Layout / Funcionalidades (Aplicação C#)

O aplicativo consiste em três abas inferiores, sendo elas:

- Pessoas (Cadastro, deleção e listagem de pessoas.);

- Transações (Cadastro e listagem de transações.);

- Totais (Consulta do total em despesas, receitas e saldo de cada pessoa, e, também, dos valores gerais.).

## Consulta de Totais de Gastos

Na terceira aba do aplicativo será exibida uma listagem de todas as pessoas e seus gastos. Gastos calculados:

- Total de despesas de cada pessoa;

- Total de receitas de cada pessoa;

- Saldo total de cada pessoa (Receitas - despesas.).

## Como Executar o Aplicativo

- Abra a solução do projeto através do Visual Studio (Não é o Code.);

- Nas opções de dispositivos de depuração, selecione alguma opção de dispositivo android;

- Inicie a depuração do aplicativo (Caso ocorra algum erro, tente recompilar o aplicativo e a solução do projeto e depure novamente.).

Tenha em mente que para o funcionamento correto, é preciso ter um emulador android configurado no Visual Studio.

## Estrutura - Banco de Dados

- Tabela Pessoa: id, nome e idade;

- Tabela Transacao: id, descricao, valor, tipo e id da pessoa (Criador da transação.).

## Como Rodar o Banco de Dados

- Abra algum SGBD MySQL (MySQL Workbench, SQLyog, etc.) e execute o arquivo **"DDL.sql"**, localizado na pasta **"Database"**;

- Se necessário, altere os parâmetros de conexão com o banco de dados localizados no arquivo **"Connection.cs"**, dentro da pasta **"DAO"**.

## Como Utilizar o pacote nuget MySQL.Data (Fonte: [Tiago A. Silva](https://www.youtube.com/@prof.tiagotas))

- Playlist: [Clique aqui](https://www.youtube.com/playlist?list=PLHVpcBDJr5dlCd-l3GwnoqMETMdbxNDCl).