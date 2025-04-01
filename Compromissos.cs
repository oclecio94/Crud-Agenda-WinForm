using System.Linq;
using System.Windows.Forms;
using System.Data.Entity;
using Crud_Agenda_WinForm.Data;
using Crud_Agenda_WinForm.Models;
using System;

namespace Crud_Agenda_WinForm
{
    public partial class Compromissos : Form
    {
        public Compromissos()
        {
            InitializeComponent();
            VerificarAcesso();
            CarregarCompromissos();
        }

        private void VerificarAcesso()
        {
            if (!UserSession.IsLoggedIn)
            {
                MessageBox.Show("Acesso negado! Faça login primeiro.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
            }
        }

        private void brnSair_Click(object sender, System.EventArgs e)
        {
            UserSession.Logout();
            MessageBox.Show("Você saiu da conta!", "Logout", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Hide();
            Form1 telaLogin = new Form1();
            telaLogin.Show();
        }

        private void dgCompromissos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void CarregarCompromissos()
        {
            using (var db = new AgendaContext())
            {
                var compromissos = db.Compromissos
                    .Where(c => c.UsuarioId == UserSession.UserId)
                    .Include(c => c.Usuario)
                    .Select(c => new
                    {
                        Id = c.Id,
                        Titulo = c.Titulo,
                        Descricao = c.Descricao,
                        DataHora = c.DataHora
                    })
                    .ToList();
                dgCompromissos.DataSource = compromissos;
            }
        }

        private void btnAdicionar_Click(object sender, System.EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitulo.Text) ||
                string.IsNullOrWhiteSpace(txtDescricao.Text) ||
                dtpDataHora.Value == DateTime.MinValue)
            {
                MessageBox.Show("Todos os campos devem ser preenchidos!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var db = new AgendaContext())
            {
               var novoCompromisso = new Compromisso
               {
               UsuarioId = UserSession.UserId,
               Titulo = txtTitulo.Text.Trim(),
               Descricao = txtDescricao.Text.Trim(),
               DataHora = dtpDataHora.Value
               };

               db.Compromissos.Add(novoCompromisso);
               db.SaveChanges();

               MessageBox.Show("Compromisso adicionado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                CarregarCompromissos();

                LimparCampos();
            }
        }

        private void LimparCampos()
        {
            txtId.Clear();
            txtTitulo.Clear();
            txtDescricao.Clear();
            dtpDataHora.Value = DateTime.Now;
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtId.Text))
            {
                MessageBox.Show("Compromisso não selecionado", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int compromissoId;
            if (!int.TryParse(txtId.Text, out compromissoId))
            {
                MessageBox.Show("Compromisso não selecionado", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var db = new AgendaContext())
            {
                var compromissoSelecionado = db.Compromissos.Find(compromissoId);

                if (compromissoSelecionado == null)
                {
                    MessageBox.Show("Compromisso não encontrado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var confirmacao = MessageBox.Show("Tem certeza de que deseja excluir este compromisso?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirmacao == DialogResult.Yes)
                {
                    db.Compromissos.Remove(compromissoSelecionado);
                    db.SaveChanges();

                    MessageBox.Show("Compromisso excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    CarregarCompromissos(); 
                    LimparCampos();  
                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtId.Text))
            {
                MessageBox.Show("Compromisso não selecionado!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int compromissoId;
            if (!int.TryParse(txtId.Text, out compromissoId))
            {
                MessageBox.Show("Compromisso não selecionado!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var db = new AgendaContext())
            {
                var compromissoSelecionado = db.Compromissos.Find(compromissoId);

                if (compromissoSelecionado == null)
                {
                    MessageBox.Show("Compromisso não encontrado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtTitulo.Text) ||
                    string.IsNullOrWhiteSpace(txtDescricao.Text) ||
                    dtpDataHora.Value == DateTime.MinValue)
                {
                    MessageBox.Show("Todos os campos devem ser preenchidos!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                compromissoSelecionado.Titulo = txtTitulo.Text.Trim();
                compromissoSelecionado.Descricao = txtDescricao.Text.Trim();
                compromissoSelecionado.DataHora = dtpDataHora.Value;

                db.Entry(compromissoSelecionado).State = EntityState.Modified;
                db.SaveChanges();
            }

            MessageBox.Show("Compromisso atualizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

            CarregarCompromissos();  
            LimparCampos();  
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtId.Clear();
            txtTitulo.Clear();
            txtDescricao.Clear();
            dtpDataHora.Value = DateTime.Now;
        }

        private void dgCompromissos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (dgv == null) return;

            txtId.Text = dgv.CurrentRow.Cells["Id"]?.Value.ToString();
            txtTitulo.Text = dgv.CurrentRow.Cells["Titulo"]?.Value.ToString();
            txtDescricao.Text = dgv.CurrentRow.Cells["Descricao"]?.Value.ToString();
            dtpDataHora.Value = (DateTime)(dgv.CurrentRow.Cells["DataHora"]?.Value);
        }
    }
}
