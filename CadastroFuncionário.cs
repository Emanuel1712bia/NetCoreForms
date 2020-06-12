using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NetCoreForms.Modelo;
using System.ComponentModel.DataAnnotations;
using NetCoreForms.Banco;


namespace NetCoreForms
{
    public partial class CadastroFuncionário : Form
    {
        private TelaPrincipal TelaPrincipal;
        private Funcionario func;

        public CadastroFuncionário(TelaPrincipal tela)
        {
            TelaPrincipal = tela;
            InitializeComponent();
            
            
        }
        
        
        public CadastroFuncionário(TelaPrincipal tela, int Id)
        {
            TelaPrincipal = tela;
            
            InitializeComponent();

            func = FuncionarioDataAccess.CarregarFuncionarios(Id);
            CarregarFuncionarioTela(func);
        }

        private void CarregarFuncionarioTela(Funcionario funcionario)
        {

            txtNome.Text = funcionario.Nome;
            txtEmail.Text = funcionario.Email;
            txtCargo.Text = funcionario.Cargo;
            txtDepartamento.Text = funcionario.Departamento;
            txtSalario.Text = funcionario.Salario.ToString();
            if(funcionario.Sexo == "M") { rbMasculino.Checked = true; } else { rbFeminino.Checked = true; };
            if(funcionario.Ativo == "S") { rbAtivo.Checked = true; } else { rbInativo.Checked = true; };
            if(funcionario.TipoContrato == "CLT") { rbCLT.Checked = true; } else if (funcionario.TipoContrato == "PJ") { rbPJ.Checked = true; } else { rbAutonomo.Checked = true; };

        }
        private void SalvarAction(object sender, EventArgs e)
        {
            Funcionario funcionario;
            if (func != null)
            {
                //Atualização
                funcionario = func;
                funcionario.DataAtualizacao = DateTime.Now;
            }
            else
            {
                //Novo Cadastro
                funcionario = new Funcionario();
                funcionario.DataCadastro = DateTime.Now;

            }

            //Mover dados para a Classe Funcionario
            funcionario.Nome = txtNome.Text.Trim();
            funcionario.Email = txtEmail.Text.Trim();
            funcionario.Cargo = txtCargo.Text.Trim();
            funcionario.Departamento = txtDepartamento.Text.Trim();
            funcionario.Salario = decimal.Parse (txtSalario.Text);
            funcionario.Sexo = (rbMasculino.Checked) ? "M" : "F" ;
            funcionario.Ativo = (rbAtivo.Checked) ? "S" : "N" ;
            funcionario.TipoContrato = (rbCLT.Checked) ? "CLT" : (rbPJ.Checked) ? "PJ" : "Aut" ;
            

            //Validar os Dados
            List<ValidationResult> listErros = new List<ValidationResult>();
            ValidationContext contexto = new ValidationContext(funcionario);
            bool Validar = Validator.TryValidateObject(funcionario, contexto, listErros, true);

            if (Validar)
            {
                //Salvar os Dados
                //Fechar e Atualizar a TelaPrincipal
                bool result;
                if (func != null)
                {
                    //Atualizar
                    result = FuncionarioDataAccess.AtualizarFuncionario(funcionario);
                }
                else
                {
                    result = FuncionarioDataAccess.SalvarFuncionario(funcionario);
                }

                if (result)
                {
                    //Sucesso
                    TelaPrincipal.AtualizarTabela();
                    this.Close();
                }
                else
                {
                    //Erro
                    lblErros.Text = "Não foi possível gravar os dados. Verifique!";
                }
            }
            else
            {
                //Validar os dados.
                StringBuilder sb = new StringBuilder();
                foreach (ValidationResult erro in listErros)
                {
                    sb.Append(erro.ErrorMessage + "\n");
                }
                lblErros.Text = sb.ToString();

            }
            
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }
    }
}
