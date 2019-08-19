using Cadastramento.models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Cadastramento {
    public partial class MainPage : ContentPage {
        public MainPage() {
            InitializeComponent();
        }

        /* ------------- INTERAÇÕES ------------- */

        /* Cria o vínculo entre a listView e o Bd assim que o App é iniciado */
        protected override void OnAppearing() {
            base.OnAppearing();
            var dbPath = new DbConfig().GetDbPath(); // Pega as configurações de onde está o bd

            using (var db = new AppDbContext(dbPath)) { // Passa o contexto para "db", onde está meu Banco de Dados
                db.Database.EnsureCreated(); // Verifica se o banco de fato foi criado.                
                var clientlist = db.Clients.OrderBy(c => c.Id);
                listView.ItemsSource = clientlist;
                // var clientlist = db.Clients.OrderBy(c => c.Name);
            }
        }

        /* Usuário deseja realizar um cadastro */
        async void OnClientAddClicked(object sender, EventArgs e) {
            await Navigation.PushAsync(new AddClientPage { // Adiciona uma pagina a pilha de paginas
                BindingContext = new Client() // Passa como contexto um objeto novo
            });
        }

        /* Usuário deseja editar */
        async void OnListViewClientSelected(object sender, SelectedItemChangedEventArgs e) {
            if (e.SelectedItem != null) {
                await Navigation.PushAsync(new EditClientPage {
                    BindingContext = e.SelectedItem as Client // Redireciona
                });
            }
        }

        /* ------------- INTERAÇÕES MENU SECONDÁRIO ------------- */
        
        /* Ordenação através do nome */
        private void OnNameFilterClicked(object sender, EventArgs e) {
            var dbPath = new DbConfig().GetDbPath();
            using (var db = new AppDbContext(dbPath)) {
                var clientlist = db.Clients.OrderBy(c => c.Name); // Ordenação propriamente dita
                listView.BeginRefresh(); 
                listView.ItemsSource = clientlist; // Acima abre edição, aqui a edita e abaixo a fecha.
                listView.EndRefresh();
            }
        } 

        private void OnIdFilterClicked(object sender, EventArgs e) {
            var dbPath = new DbConfig().GetDbPath();
            using (var db = new AppDbContext(dbPath)) {
                var clientlist = db.Clients.OrderBy(c => c.Id);
                listView.BeginRefresh();
                listView.ItemsSource = clientlist;
                listView.EndRefresh();
            }
        }

        private void OnAgeFilterClicked(object sender, EventArgs e) {
            var dbPath = new DbConfig().GetDbPath();
            using (var db = new AppDbContext(dbPath)) {
                var clientlist = db.Clients.OrderBy(c => c.Age);
                listView.BeginRefresh();
                listView.ItemsSource = clientlist;
                listView.EndRefresh();
            }
        }

        /* ------------- FIM: INTERAÇÕES ------------- */

        /* ------------- TRATAMENTOS ------------- */

        /* Trata o texto passado pela barra de pesquisa */
        private void Handle_TextChanged(object sender, TextChangedEventArgs e) {
            var dbPath = new DbConfig().GetDbPath();
            using (var db = new AppDbContext(dbPath)) {
                var clientlist = db.Clients.OrderBy(c => c.Id);

                /* Tratamento propriamente dito do texto */
                listView.BeginRefresh(); // Abre lista para edição.

                if (string.IsNullOrWhiteSpace(e.NewTextValue)) { // Nenhuma entrada até então
                    listView.ItemsSource = clientlist;
                }
                /* Existe uma entrada, basta descobrir qual é */
                else { // Entrada é um int
                    if (
                        e.NewTextValue[0].ToString() == "0"
                        || e.NewTextValue[0].ToString() == "1"
                        || e.NewTextValue[0].ToString() == "2"
                        || e.NewTextValue[0].ToString() == "3"
                        || e.NewTextValue[0].ToString() == "4"
                        || e.NewTextValue[0].ToString() == "5"
                        || e.NewTextValue[0].ToString() == "6"
                        || e.NewTextValue[0].ToString() == "7"
                        || e.NewTextValue[0].ToString() == "8"
                        || e.NewTextValue[0].ToString() == "9") {

                        int idClient = Convert.ToInt32(e.NewTextValue); // Pega o Id
                        listView.ItemsSource = clientlist.Where(i => i.Id.Equals(idClient)); // Imprime somente os iguais a pesquisa
                    }
                    else { // Entrada é uma string
                        listView.ItemsSource = clientlist.Where(i => i.Name.Contains(e.NewTextValue));
                    }
                }
                listView.EndRefresh(); // Finaliza edição da lista
            }
        }
    }
}
/* ------------- FIM: TRATAMENTOS ------------- */

/* ------------- CEMTIÉRIO DE IDEIAS ------------- */

/* Remoção do Banco de Dados */
// protected void DeleteClient() {
//     var nclient = (Client)BindingContext;
// 
//     var dbPath = new DbConfig().getDbPath();
//     using (var db = new AppDbContext(dbPath)) {
//         var dclient = (Client)BindingContext;
//         db.Clients.Remove(dclient);
//         db.SaveChanges();
//     }
// }

/* Tratamento manual de informações */
// protected override async void OnAppearing() {
//     listView.ItemsSource = new List<Client>() {
//         new Client() {Id = 876, Age = 18, Name = "Max Caufield", Phone = "4002-8922" },
//         new Client() {Id = 459, Age = 17, Name = "Rachel Amber", Phone = "1321-1341" }
//     };
// }
// }

/* Usuário deseja remover cadastro */
// async void OnDeleteClicked(object sender, EventArgs e) {
//     bool resposta = await DisplayAlert("Deletar", "Deseja remover o cadastro desse cliente?", "Sim", "Não");
//     if (resposta == true) {
//         // Faz a remoção do cliente no BD
//         await DisplayAlert("Deletar", "Usuário removido com sucesso!", "Continuar");
//     }
//     else {
//         return;
//     }
// }

//async void OnFilterClicked(object sender, EventArgs e) {
//    bool resposta = await DisplayAlert("Filtrar", "Como deseja exibir sua lista de clientes?", "Simples", "Ordenada");
//}