using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetCoreForms
{
    public partial class TelaPrincipal : Form
    {
        public TelaPrincipal()
        {
            InitializeComponent();

            AtualizarTabela();

        }
        

        public void AtualizarTabela()
        {
            dgvTabelaFuncionario.DataSource = Banco.FuncionarioDataAccess.CarregarFuncionarios();
        }

        private void NovoAction(object sender, EventArgs e)
        {
            new CadastroFuncionário(this).Show();
        }

        private void EditarAction(object sender, EventArgs e)
        {
            int id = (int)dgvTabelaFuncionario.SelectedRows[0].Cells[0].Value;
            new CadastroFuncionário(this, id).Show();
        }

        private void ExcluirAction(object sender, EventArgs e)
        {
            int id = (int)dgvTabelaFuncionario.SelectedRows[0].Cells[0].Value;
            NetCoreForms.Banco.FuncionarioDataAccess.ExcluirFuncionario(id);
            AtualizarTabela();
        }
    }
}
