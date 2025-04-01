CRUD de Agenda com Controle de Acesso

Este projeto é um sistema de agenda desenvolvido em C# .NET Windows Forms com SQL Server, permitindo a criação, edição e exclusão de compromissos de usuários autenticados.

🚀 Como Rodar o Projeto

1. Clonar o Repositório

git clone https://github.com/oclecio94/Crud-Agenda-WinForm.git
cd Crud-Agenda-WinForm

2. Abrir no Visual Studio

Carregue a solução (Crud-Agenda-WinForm.sln).

3. Configurar o Banco de Dados e instalar pacotes

Executar o comando

Update-Package –reinstall

Antes de iniciar a aplicação, é necessário configurar o banco de dados SQL Server e aplicar as migrations.

Verifique se o SQL Server está rodando.

Atualize a string de conexão no arquivo App.config caso necessário.

Crie o banco de dados AgendaDB, abra o Package Manager Console no Visual Studio e execute o comando:

Update-Database

Isso criará automaticamente as tabelas no banco de dados.

4. Compilar e Executar o Projeto

Abra o Visual Studio.

Compile a solução (Ctrl + Shift + B).

Execute a aplicação (F5).

📌 Funcionalidades Principais

Autenticação de Usuários (Login e Cadastro com senha criptografada).

CRUD de Compromissos (Criar, Ler, Atualizar e Deletar).

Acesso Restrito (Cada usuário só vê seus próprios compromissos).

🎨 Interface Gráfica

Tela de Login: Permite o login ou cadastro de um novo usuário.

Tela de Cadastro: Criar uma conta com senha segura.

Tela de Compromissos: Exibe e gerencia os compromissos do usuário.

⚙️ Tecnologias Utilizadas

C# .NET Framework 4.7.2

Windows Forms para interface gráfica

Entity Framework para ORM

SQL Server para armazenamento de dados

📢 Notas

As telas de login e cadastro alternam entre si sem abrir múltiplas janelas.

O sistema sempre abre no centro da tela para melhor usabilidade.

Caso tenha dúvidas ou precise de ajuda, entre em contato! 😊