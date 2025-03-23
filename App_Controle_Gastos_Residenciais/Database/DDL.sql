CREATE DATABASE db_controle_gastos_residenciais;

USE db_controle_gastos_residenciais;

CREATE TABLE Pessoa (

    id INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(255) NOT NULL,
    idade INT NOT NULL

);

CREATE TABLE Transacao (

    id INT AUTO_INCREMENT PRIMARY KEY,
    descricao VARCHAR(255) NOT NULL DEFAULT "Nenhuma descrição.",
    valor DECIMAL(10, 2) NOT NULL DEFAULT 0.00,
    tipo ENUM("Despesa", "Receita") NOT NULL,

    fk_pessoa INT NOT NULL,
    CONSTRAINT fk_pessoa_transacao FOREIGN KEY (fk_pessoa) REFERENCES Pessoa(id) ON DELETE CASCADE

    /*
    
        ON DELETE CASCADE: se o valor de um campo de um registro for excluído, todos os registros de outras tabelas, 
        que fizerem referência a ele, também serão.

    */

);

SHOW TABLES;