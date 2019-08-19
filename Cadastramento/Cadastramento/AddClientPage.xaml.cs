using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Cadastramento.models;
using System.IO;

namespace Cadastramento {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddClientPage : ContentPage {
        public AddClientPage() {
            InitializeComponent();
        }

        /* -------- INTERAÇÕES -------- */
        /* Usuário deseja incluir cliente no Bd */
        async void OnSaveButtonClicked(object sender, EventArgs e) {
            var resposta = await DisplayAlert("Salvar", "Deseja cadastrar os dados digitados?", "Sim", "Não");
            try {
                if (resposta == false) {
                    return;
                }
                else {
                    // Salvar o Cliente em BD
                    InsertClient();
                    await Navigation.PopAsync(); // Volta para tela inicial
                }
            }
            catch (Exception ex) {
                await DisplayAlert("", ex.InnerException.Message, "OK");
            }
        }

        /* Interação para cancelar o que foi feito */
        async void OnCancelButtonClicked(object sender, EventArgs e) {
            var resposta = await DisplayAlert("Cancelar", "Deseja cancelar o cadastramento?", "Sim", "Não");
            if (resposta == true) {
                await Navigation.PopAsync(); // Volta para tela inicial
            }
            else {
                return;
            }
        }

        /* -------- BD -------- */

        /* Inserção no Banco de Dados */
        protected void InsertClient() {
            var dbPath = new DbConfig().GetDbPath();
            using (var db = new AppDbContext(dbPath)) {
                var nclient = (Client)BindingContext; // Cliente novo;
                var empty = db.Clients.Any(); // Verifico se a tabela está vázia

                if (!empty) {
                    nclient.Id = 1;
                    db.Add(new Client() { Name = nclient.Name, Id = nclient.Id, Age = nclient.Age, Phone = nclient.Phone });                    
                }
                else {
                    var lclient = db.Clients.Last();
                    nclient.Id = (lclient.Id + 1);
                    db.Add(new Client() { Name = nclient.Name, Id = nclient.Id, Age = nclient.Age, Phone = nclient.Phone });
                }
                db.SaveChanges();
            }
        }
    }
}

/* Inserção no Banco de Dados */
// protected async void InsertClient() {
//     var dbPath = new DbConfig().getDbPath();
//     using (var db = new AppDbContext(dbPath)) {
//         var nclient = (Client)BindingContext;
// 
// 
//         /* Tratamento de Id repetido */
//         var repetido = db.Clients.Any(c => c.Id == nclient.Id); // Vejo se Id se repete
// 
//         if (repetido) {
//             var maxId = db.Clients.Max(c => c.Id); // Pego maior Id
//             while (repetido) {
//                 nclient.Id = maxId + 1;
//                 repetido = db.Clients.Any(c => c.Id == nclient.Id);
//             }
//             await DisplayAlert("ID Repetido", "Este ID já foi cadastrado, iremos ajusta-lo para você", "continuar");
//         }