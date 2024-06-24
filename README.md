WindowsFormsApp1 - Gerenciador de Contatos
Este projeto é um aplicativo de desktop desenvolvido em C# com Windows Forms, que permite gerenciar contatos, incluindo operações de criação, leitura, atualização e exclusão (CRUD) em um banco de dados MySQL.

Funcionalidades
Adicionar Contato: Insere um novo contato no banco de dados.
Visualizar Contatos: Exibe todos os contatos armazenados no banco de dados em um ListView.
Buscar Contatos: Permite buscar contatos pelo nome ou email.
Atualizar Contato: Atualiza os dados de um contato selecionado.
Excluir Contato: Remove um contato selecionado do banco de dados.
Tecnologias Utilizadas
C#: Linguagem de programação principal.
Windows Forms: Framework para construção da interface gráfica.
MySQL: Banco de dados utilizado para armazenar os contatos.
MySql.Data: Biblioteca para conexão e manipulação do banco de dados MySQL.
Estrutura do Projeto
O projeto contém os seguintes componentes principais:

Form1.cs: Contém a lógica principal do aplicativo, incluindo a conexão com o banco de dados e as operações CRUD.
Program.cs: Ponto de entrada do aplicativo.
App.config: Arquivo de configuração contendo informações da conexão com o banco de dados.
Configuração do Ambiente
Para configurar o ambiente e executar o projeto localmente, siga os passos abaixo:

Pré-requisitos:

Visual Studio instalado
MySQL Server instalado e em execução
Biblioteca MySql.Data instalada via NuGet
Configuração do Banco de Dados:

Crie um banco de dados chamado db_agenda no MySQL.
Crie uma tabela contato com a seguinte estrutura:
sql
CREATE TABLE contato (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(255) NOT NULL,
    email VARCHAR(255) NOT NULL,
    telefone VARCHAR(20) NOT NULL
);
Configuração da String de Conexão:

Verifique e atualize a string de conexão no arquivo Form1.cs se necessário:
csharp
Copiar código
private string data_sorce = $"server=localhost; uid=root;database=db_agenda;password=root;";
Executando o Projeto
Abra o projeto no Visual Studio.
Compile o projeto para garantir que todas as dependências estão corretamente configuradas.
Execute o projeto pressionando F5 ou clicando no botão de "Iniciar Depuração".
Uso do Aplicativo
Adicionar Contato:

Preencha os campos "Nome", "Email" e "Telefone".
Clique no botão "Adicionar".
Buscar Contato:

Digite o nome ou email do contato no campo de busca.
Clique no botão "Buscar".
Atualizar Contato:

Selecione um contato na lista.
Edite os campos "Nome", "Email" ou "Telefone".
Clique no botão "Salvar".
Excluir Contato:

Selecione um contato na lista.
Clique no botão "Excluir".
