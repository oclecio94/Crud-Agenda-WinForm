using System;
using System.Linq;
using System.Windows.Forms;
using Crud_Agenda_WinForm.Data;
using Crud_Agenda_WinForm.Utils;

namespace Crud_Agenda_WinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string senha = txtSenha.Text.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha))
            {
                MessageBox.Show("Preencha todos os campos!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var db = new AgendaContext())
            {
                var usuario = db.Usuarios.FirstOrDefault(u => u.Email == email);

                if (usuario == null || !Criptografia.VerificarSenha(senha, usuario.SenhaHash))
                {
                    MessageBox.Show("Email ou senha incorretos!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                UserSession.UserId = usuario.Id;
                UserSession.Nome = usuario.Nome;
                UserSession.Email = usuario.Email;

                MessageBox.Show($"Bem-vindo, {usuario.Nome}!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Compromissos compromissos = new Compromissos();
                compromissos.Show();
                this.Hide();
            }
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            TelaRegistro telaRegistro = new TelaRegistro();
            telaRegistro.Show();

            this.Hide();
        }
    }
}
