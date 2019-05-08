using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TotalCarForms
{
    public partial class Usuario : Form
    {
        public Usuario()
        {
            InitializeComponent();
        }


        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Usuario_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet1.Usuario' table. You can move, or remove it, as needed.

          
            this.usuarioTableAdapter.Fill(this.dataSet1.Usuario);     

        }

        SqlConnection conn = null;
        private string strCon = "Integrated Security=SSPI;Persist Security info = False;Initial Catalog=TotalCar;Data Source=FERNANDO-PC";
        private string strSql = string.Empty;


        private void btnIncluir_Click(object sender, EventArgs e)
        {
            strSql = "INSERT INTO Usuario(cpf,nome,sobrenome,nascimento,sexo,telefone,celular,endereco) VALUES (@cpf,@nome,@sobrenome,@nascimento,@sexo,@telefone,@celular,@endereco)";

            conn = new SqlConnection(strCon);

            SqlCommand comand = new SqlCommand(strSql, conn);

            comand.Parameters.Add("@cpf", SqlDbType.VarChar).Value = (txtCpf.Text).Replace("-","");
            comand.Parameters.Add("@nome", SqlDbType.VarChar).Value = txtNome.Text;
            comand.Parameters.Add("@sobrenome", SqlDbType.VarChar).Value = txtSobrenome.Text;
            comand.Parameters.Add("@nascimento", SqlDbType.DateTime).Value = DateTime.Now;
            comand.Parameters.Add("@sexo", SqlDbType.Bit).Value = rbtMasculino.Checked?1:0;
            comand.Parameters.Add("@telefone", SqlDbType.VarChar).Value = txtTelefone.Text;
            comand.Parameters.Add("@celular", SqlDbType.VarChar).Value = txtCelular.Text;
            comand.Parameters.Add("@endereco", SqlDbType.VarChar).Value = txtEndereco.Text;


            try
            {
                conn.Open();
                comand.ExecuteNonQuery();
                MessageBox.Show("Cadastro realizado comsucesso!");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {

                conn.Close();
            }

           // Limpar();
        }

        public void Limpar() {

            this.txtCpf.Clear();
            this.txtNome.Clear();
            this.txtSobrenome.Clear();
            this.txtNascimento.Clear();
            this.txtTelefone.Clear();
            this.txtCelular.Clear();
            this.txtEndereco.Clear();
            this.rbtMasculino.Enabled = false;
            this.rbtFeminino.Enabled = false;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           var cpf = this.dataGridView1.SelectedRows[0].Selected;

            strSql = "SELECT cpf,nome,sobrenome,nascimento,sexo,telefone,celular,endereco FROM Usuario WHERE cpf = @cpf";

            conn = new SqlConnection(strCon);

            SqlCommand comand = new SqlCommand(strSql, conn);

            conn.Open();

            comand.ExecuteReader();
        }
    }
}
