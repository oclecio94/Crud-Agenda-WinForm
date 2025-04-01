using System;
using System.Linq;
using System.Windows.Forms;
using Crud_Agenda_WinForm.Data;
using Crud_Agenda_WinForm.Models;
using Crud_Agenda_WinForm.Utils;

namespace Crud_Agenda_WinForm
{
    public partial class TelaRegistro : Form
    {
        public TelaRegistro()
        {
            InitializeComponent();
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            Form1 telaLogin = new Form1();
            telaLogin.Show();

            this.Hide();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            string nome = txtNome.Text.Trim();
            string email = txtEmail.Text.Trim();
            string senha = txtSenha.Text.Trim();

            if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha))
            {
                MessageBox.Show("Preencha todos os campos!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var db = new AgendaContext())
            {
                if (db.Usuarios.Any(u => u.Email == email))
                {
                    MessageBox.Show("Este email já está cadastrado!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Usuario novoUsuario = new Usuario
                {
                    Nome = nome,
                    Email = email,
                    SenhaHash = Criptografia.HashSenha(senha)
                };

                db.Usuarios.Add(novoUsuario);
                db.SaveChanges();

                MessageBox.Show("Cadastro realizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
               
                Form1 telaLogin = new Form1();
                telaLogin.Show();

                this.Hide();
            }
        }
    }
}
