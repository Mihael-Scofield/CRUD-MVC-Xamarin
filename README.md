# Aplicativo de Cadastramento de Clientes (MVC)
Um aplicativo para android implementado em C# e Xamarin.Forms capaz de cadastrar, ler, atualizar, deletar, buscar e ordenar clientes utilizando SQLite para isso.

Implementado por Mihael Scofield de Azevedo.

## Instalação
Abra a solução "cadastramento.sln" no Visual Studio, então conecte seu celular com o computador em modo de depuração e faça o Deploy pelo Visual Studio.

## Execução
O programa é capaz de fazer inserção (cadastramento), busca, exclusão, ordenação da listagem de clientes dentro de um banco de dados SQLite.
Com um layout de menus intuitivos e simples, foi pensado para usuários do dia-a-dia, "conversando" com quem o utiliza.

## Estruturação
O modelo MVC foi adotado para esse projeto, conversando com a View diretamente pelo Code-Behind, a lógica do programa foi pensada.

### Divisão de Páginas
O programa foi implementado em 3 páginas diferentes

  - Main 
  
  Faz a listagem dos clientes, busca os mesmos e ordena. Encaminha para a pagina de adição e para a de edição
  
  - Cadastramento
  
  Página contendo uma tabela de cadastro e botões para cancelar ou salvar as alterações
   
  - Edição
  
  Semelhante ao cadastramento, mas agora puxa os dados que uma vez já foram digitados.
