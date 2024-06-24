using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        // Declaração de uma variável de conexão MySQL
        private MySqlConnection conexão;
        // String de conexão com o banco de dados MySQL

        private string data_sorce = $"server=localhost; uid=root;database=db_agenda;password=root;";

        // Variável para armazenar o ID do contato selecionado
        private int? id_contato = null;

        public Form1()
        {
            InitializeComponent();
            CarregarContatos(); // Carregar contatos do banco de dados ao iniciar
            list_Contato.View = View.Details;
            list_Contato.LabelEdit = true;
            list_Contato.AllowColumnReorder = true;
            list_Contato.FullRowSelect = true;
            list_Contato.GridLines = true;

            // Adicionar colunas ao ListView
            list_Contato.Columns.Add("Id", 30, HorizontalAlignment.Left);
            list_Contato.Columns.Add("Nome", 150, HorizontalAlignment.Left);
            list_Contato.Columns.Add("Email", 150, HorizontalAlignment.Left);
            list_Contato.Columns.Add("Telefone", 150, HorizontalAlignment.Left);
        }

        // Método para carregar contatos do banco de dados e exibir no ListView
        private void CarregarContatos()
        {
            try
            {
                MySqlConnection conexao = new MySqlConnection(data_sorce);
                conexao.Open();

                // Consulta SQL para buscar todos os contatos
                string consultaSql = "SELECT * FROM contato";
                MySqlCommand comando = new MySqlCommand(consultaSql, conexao);

                MySqlDataReader reader = comando.ExecuteReader();
                list_Contato.Items.Clear();

                // Ler dados do banco de dados e adicionar ao ListView
                while (reader.Read())
                {
                    string[] row =
                    {
                        reader.GetInt32(0).ToString(), // ID
                        reader.GetString(1), // Nome
                        reader.GetString(2), // Email
                        reader.GetString(3) // Telefone
                    };

                    var linha_listview = new ListViewItem(row);
                    list_Contato.Items.Add(linha_listview);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}");
            }
        }

        private void label3_Click(object sender, EventArgs e) { }

        private void textBox3_TextChanged(object sender, EventArgs e) { }

        // Método para inserir ou atualizar um contato no banco de dados
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection conexao = new MySqlConnection(data_sorce);

                if (id_contato == null)
                {
                    // Inserir novo contato
                    string consultaSql = "INSERT INTO contato (nome, email, telefone) VALUES (@nome, @email, @telefone)";
                    MySqlCommand comando = new MySqlCommand(consultaSql, conexao);

                    string nome = textNome.Text.Trim();
                    string email = textEmail.Text.Trim();
                    string telefone = textTelefone.Text.Trim();

                    comando.Parameters.AddWithValue("@nome", nome);
                    comando.Parameters.AddWithValue("@email", email);
                    comando.Parameters.AddWithValue("@telefone", telefone);

                    conexao.Open();
                    int linhasAfetadas = comando.ExecuteNonQuery();
                    if (linhasAfetadas > 0)
                    {
                        MessageBox.Show("Valores inseridos com sucesso!");
                    }
                    else
                    {
                        MessageBox.Show("Nenhum valor foi inserido. Verifique os dados e tente novamente.");
                    }
                }
                else
                {
                    // Atualizar contato existente
                    try
                    {
                        string consultaSql = "UPDATE contato SET nome = @nome, email = @email, telefone = @telefone WHERE id = @id;";
                        MySqlCommand comando = new MySqlCommand(consultaSql, conexao);

                        string nome = textNome.Text.Trim();
                        string email = textEmail.Text.Trim();
                        string telefone = textTelefone.Text.Trim();
                        if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(telefone))
                        {
                            MessageBox.Show("Uns dos Campos esta vazio");
                        }
                        else
                        {
                            comando.Parameters.AddWithValue("@nome", nome);
                            comando.Parameters.AddWithValue("@email", email);
                            comando.Parameters.AddWithValue("@telefone", telefone);
                            comando.Parameters.AddWithValue("@id", id_contato);
                        }
                        conexao.Open();
                        int linhasAfetadas = comando.ExecuteNonQuery();
                        if (linhasAfetadas > 0)
                        {
                            MessageBox.Show("Valores atualizados com sucesso!");
                            CarregarContatos(); // Recarregar contatos após atualização
                        }
                        else
                        {
                            MessageBox.Show("Nenhum valor foi atualizado. Verifique os dados e tente novamente.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                textNome.Clear();
                textEmail.Clear();
                textTelefone.Clear();
                CarregarContatos(); // Recarregar contatos após inserção ou atualização
            }
        }

        private void label2_Click(object sender, EventArgs e) { }

        private void label1_Click(object sender, EventArgs e) { }

        // Método para buscar contatos no banco de dados pelo nome ou email
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection conexao = new MySqlConnection(data_sorce);
                conexao.Open();

                string consultaSql = "SELECT * FROM contato WHERE nome LIKE  @q OR email LIKE @q";
                MySqlCommand comando = new MySqlCommand(consultaSql, conexao);
                comando.Parameters.AddWithValue("@q", "%" + textBuscar.Text + "%");

                MySqlDataReader reader = comando.ExecuteReader();
                list_Contato.Items.Clear();

                // Ler dados do banco de dados e adicionar ao ListView
                while (reader.Read())
                {
                    string[] row =
                    {
                        reader.GetInt32(0).ToString(), // ID
                        reader.GetString(1), // Nome
                        reader.GetString(2), // Email
                        reader.GetString(3) // Telefone
                    };

                    var linha_listview = new ListViewItem(row);
                    list_Contato.Items.Add(linha_listview);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}");
            }
        }

        private void textBuscar_TextChanged(object sender, EventArgs e) { }

        // Método para carregar dados do contato selecionado no ListView para os campos de texto
        private void list_Contato_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            ListView.SelectedListViewItemCollection itens_selecionados = list_Contato.SelectedItems;

            foreach (ListViewItem item in itens_selecionados)
            {
                id_contato = Convert.ToInt32(item.SubItems[0].Text);
                textNome.Text = item.SubItems[1].Text;
                textEmail.Text = item.SubItems[2].Text;
                textTelefone.Text = item.SubItems[3].Text;
            }
        }

        // Método para excluir um contato do banco de dados
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (id_contato != null)
                {
                    MySqlConnection conexao = new MySqlConnection(data_sorce);
                    string consultaSql = "DELETE FROM contato WHERE id = @id;";
                    MySqlCommand comando = new MySqlCommand(consultaSql, conexao);
                    comando.Parameters.AddWithValue("@id", id_contato);
                    conexao.Open();
                    int linhasAfetadas = comando.ExecuteNonQuery();
                    if (linhasAfetadas > 0)
                    {
                        MessageBox.Show("Contato excluído com sucesso!");
                    }
                    else
                    {
                        MessageBox.Show("Nenhum contato foi excluído. Verifique os dados e tente novamente.");
                    }
                }
                else
                {
                    MessageBox.Show("Por favor, selecione um contato para excluir.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}");
            }
            finally
            {
                textNome.Clear();
                textEmail.Clear();
                textTelefone.Clear();
                CarregarContatos(); // Recarregar contatos após exclusão
                id_contato = null;
            }
        }

        private void textEmail_TextChanged(object sender, EventArgs e) { }
    }
}
