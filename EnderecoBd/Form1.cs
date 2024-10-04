using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace EnderecoBd
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            try
            {
                // Conexão com o banco de dados MySQL
                MySqlConnection conn = new MySqlConnection("server=127.0.0.1;userid=root;password=root;database=correios");
                conn.Open();
                MySqlCommand comando = new MySqlCommand("INSERT INTO endereco (cep, rua, bairro, cidade, numero, tipoLogadouro) VALUES (@cep, @rua, @bairro, @cidade, @numero, @tipoLogadouro);", conn);
                comando.Parameters.AddWithValue("@cep", txbCep.Text);
                comando.Parameters.AddWithValue("@rua", txbRua.Text);
                comando.Parameters.AddWithValue("@bairro", txbBairro.Text);
                comando.Parameters.AddWithValue("@cidade", txbCidade.Text);
                comando.Parameters.AddWithValue("@numero", txbNumero.Text);
                comando.Parameters.AddWithValue("@tipoLogadouro", cbxLogadouro.Text);

                comando.ExecuteNonQuery();
                conn.Close();


                // Salvando em um arquivo de texto
                string caminhoArquivo = @"C:\Users\lucas_c_leandro\Documents\Enderecos.txt"; // Substitua com o caminho correto do seu arquivo

                using (StreamWriter sw = new StreamWriter(caminhoArquivo, true))
                {
                    sw.WriteLine("CEP: " + txbCep.Text);
                    sw.WriteLine("Rua: " + txbRua.Text);
                    sw.WriteLine("Bairro: " + txbBairro.Text);
                    sw.WriteLine("Cidade: " + txbCidade.Text);
                    sw.WriteLine("Número: " + txbNumero.Text);
                    sw.WriteLine("Tipo de Logradouro: " + cbxLogadouro.Text);
                    sw.WriteLine("----------------------------------------");
                }

                MessageBox.Show("Endereço cadastrado!", "Sucesso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                label1.Text = ex.Message;
            }
            finally
            {
                txbCep.Text = "";
                txbRua.Text = "";
                txbBairro.Text = "";
                txbCidade.Text = "";
                txbNumero.Text = "";
                cbxLogadouro.Text = "";
            }


        }

        //Visualizar o arquivo existente
        private void btnVisualizar_Click(object sender, EventArgs e)
        {
            try
            {
                string caminhoArquivo = @"C:\Users\lucas_c_leandro\Documents\Enderecos.txt"; 

                // Verifica se o arquivo existe
                if (File.Exists(caminhoArquivo))
                {
                    System.Diagnostics.Process.Start("notepad.exe", caminhoArquivo);
                }
                else
                {
                    MessageBox.Show("O arquivo não foi encontrado!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao abrir o arquivo: " + ex.Message);
            }
        }
    }
}

