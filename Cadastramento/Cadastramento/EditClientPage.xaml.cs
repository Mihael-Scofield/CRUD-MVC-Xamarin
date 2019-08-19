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
    public partial class EditClientPage : ContentPage {
        public EditClientPage() {
            InitializeComponent();
        }

        /* -------- INTERAÇÕES -------- */
        /* Usuário deseja incluir cliente no Bd */
        async void OnEditButtonClicked(object sender, EventArgs e) {
            var resposta = await DisplayAlert("Salvar", "Deseja salvar os dados digitados?", "Sim", "Não");
            try {
                if (resposta == false) {
                    return;
                }
                else {
                    // Salvar o Cliente em BD
                    EditClient();
                    await Navigation.PopAsync(); // Volta para tela inicial
                }
            }
            catch (Exception ex) {
                await DisplayAlert("", ex.InnerException.Message, "OK");
            }
        }

        /* Usuário deseja deletar cliente atual */
        async void OnDeleteButtonClicked(object sender, EventArgs e) {
            bool resposta = await DisplayAlert("Deletar", "Deseja remover o cadastro desse cliente?", "Sim", "Não");
            if (resposta == true) {
                var dbPath = new DbConfig().GetDbPath();
                using (var db = new AppDbContext(dbPath)) {
                    var dclient = (Client)BindingContext;
                    db.Clients.Remove(dclient);
                    db.SaveChanges();
                }
                await DisplayAlert("Deletar", "Usuário removido com sucesso!", "Continuar");
                await Navigation.PopAsync(); // Volta para tela inicial
            }
            else {
                return;
            }
        }

        /* -------- BD -------- */

        /* Inserção no Banco de Dados */
        protected void EditClient() {
            var dbPath = new DbConfig().GetDbPath();
            using (var db = new AppDbContext(dbPath)) {
                /* contextualização */
                var ctx_client = (Client)BindingContext; // Recebo o cliente do contexto externo (Main)
                var edit_client = db.Clients.Find(ctx_client.Id); // Faço a vinculação com o cliente interno (Tabela)

                /* Atualização */
                edit_client.Name = ctx_client.Name;
                edit_client.Age = ctx_client.Age;
                edit_client.Phone = ctx_client.Phone;

                db.SaveChanges();
            }
        }
    }
}