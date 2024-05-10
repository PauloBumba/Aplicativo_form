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
       private MySqlConnection conexão;
       private  string data_sorce = $"server=localhost; uid=root;database=db_agenda;password=;";

        private int? id_contato = null;
        public Form1()
        {
            InitializeComponent();
            list_Contato.View=View.Details;
            list_Contato.LabelEdit = true;
            list_Contato.AllowColumnReorder = true;
            list_Contato.FullRowSelect = true;
            list_Contato.GridLines = true;

            list_Contato.Columns.Add("Id", 30, HorizontalAlignment.Left);
            list_Contato.Columns.Add("Nome", 150, HorizontalAlignment.Left);
            list_Contato.Columns.Add("Email", 150, HorizontalAlignment.Left);
            list_Contato.Columns.Add("Telefone", 150, HorizontalAlignment.Left);

        }


        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                

                MySqlConnection conexao = new MySqlConnection(data_sorce);
                if (id_contato == null)
                {

                    // Montar a consulta SQL para inserção
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
                    try
                    {
                        
                        string consultaSql = "UPDATE contato SET nome = @nome, email = @email, telefone = @telefone WHERE id = @id;";
                        MySqlCommand comando = new MySqlCommand(consultaSql, conexao);
                        
                        string nome = textNome.Text.Trim();
                        string email = textEmail.Text.Trim();
                        string telefone = textTelefone.Text.Trim();
                        if(string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(email)  || string.IsNullOrEmpty(telefone))
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
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message);

            }
            finally 
            { 
                textNome.Clear();
                textEmail.Clear();
                textTelefone.Clear();
                
            }

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            try
            {
                MySqlConnection conexao = new MySqlConnection(data_sorce);
                conexao.Open();
                
                string consultaSql = "SELECT * FROM contato WHERE nome LIKE  @q OR email LIKE @q";
                

                MySqlCommand comando = new MySqlCommand(consultaSql, conexao);
               
                

                comando.Parameters.AddWithValue("@q","%" + textBuscar.Text + "%");

                MySqlDataReader reader = comando.ExecuteReader();
                list_Contato.Items.Clear();

                while (reader.Read())
                {
                    string[] row =
                    {
                                        reader.GetInt32(0).ToString(), // ID (assumindo que é um campo inteiro)
                                        reader.GetString(1), // Nome
                                        reader.GetString(2), // Email
                                        reader.GetString(3) // Outro campo (ajuste conforme necessário)
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
        private void textBuscar_TextChanged(object sender, EventArgs e)
        { 
        
                
            
        }

        private void list_Contato_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            ListView.SelectedListViewItemCollection itens_selecionados = list_Contato.SelectedItems;

            foreach(ListViewItem item in itens_selecionados)

            {
                id_contato =Convert.ToInt32(item.SubItems[0].Text);
                textNome.Text = item.SubItems[1].Text;
                textEmail.Text = item.SubItems[2].Text;
                textTelefone.Text = item.SubItems[3].Text;
            }
        }

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
                    id_contato = null;
                }
            }

        }
    }

