using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Library_Management_System
{
    public partial class NewBooks : Form
    {
        MySqlConnection conn = new MySqlConnection(@"Data Source=localhost;Initial Catalog=library_management_system_db;User Id=root;password=''");

        public NewBooks()
        {
            InitializeComponent();
        }

        private void NewBooks_Load(object sender, EventArgs e)
        {
            if (ConnectionState.Open == conn.State)
            {
                conn.Close();
            }
            try
            {
                conn.Open();

                generateID();   //Generate Member ID
                comboBox1.SelectedIndex = 0;
                this.ActiveControl = comboBox1;  //automatically cursor brings to textbox2

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void generateID()
        {

            if (ConnectionState.Open == conn.State)
            {
                conn.Close();
            }
            try
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "select count(*) from book";
                int countofbooks = Convert.ToInt32(cmd.ExecuteScalar());
                string lastdataid="";
                if (countofbooks > 0)
                {
                    MySqlCommand cmd1 = conn.CreateCommand();
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = "select bookid from book order by bookid desc limit 1";
                    MySqlDataReader dr = cmd1.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            lastdataid = dr["bookid"].ToString();
                        }
                    }
                    //string lastdataid = cmd1.ExecuteNonQuery().ToString();
                    //MessageBox.Show(lastdataid);
                    char[] removeprefix = { 'B', '-' };
                    int lastNo = Convert.ToInt32(lastdataid.TrimStart(removeprefix));
                    int nextNo = lastNo + 1;
                    int charactersofnextno = Convert.ToInt32(nextNo.ToString().Length);
                    //MessageBox.Show(charactersofnextno.ToString());
                    string beforeprefix = "00000";
                    //MessageBox.Show(beforeprefix);
                    string editedprefix = beforeprefix.Remove((beforeprefix.Length - charactersofnextno), charactersofnextno);
                    //MessageBox.Show(editedprefix);


                    textBox1.Text = "B-" + editedprefix + nextNo.ToString();
                }
                else
                {
                    textBox1.Text = "B-00000";
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void comboBox1_Leave(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if ((textBox1.Text != "") && (comboBox1.SelectedItem != null) && (textBox2.Text != "") && (textBox3.Text != "") && (textBox4.Text != "") && (textBox5.Text != "") && (textBox6.Text != "") && (textBox7.Text != ""))
            {
                if (ConnectionState.Open == conn.State)
                {
                    conn.Close();
                }

                try
                {
                    conn.Open();
                    MySqlCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "insert into book(bookid, class, bookname, author, publisher, edition, quantity, price, returndays, fine, availablequantity) values('" + textBox1.Text + "','" + comboBox1.SelectedItem.ToString() + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + numericUpDown1.Value + "','" + numericUpDown2.Value + "','" + Convert.ToDouble(textBox5.Text) + "','" + Convert.ToInt32(textBox6.Text) + "','" + Convert.ToDouble(textBox7.Text) + "','"+numericUpDown2.Value+"')";
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("New Book added successfully!!!");


                    textBox1.Text = "";
                    comboBox1.SelectedIndex = 0;
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    numericUpDown1.Value = 0;
                    numericUpDown2.Value = 1;
                    textBox5.Text = "";
                    textBox6.Text = "";
                    textBox7.Text = "";

                    conn.Close();

                    generateID();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
            else
            {
                label15.Text = "Please fill the fields!!!";
                label15.Visible = true;
            }
        }

        private void NewBooks_FormClosed(object sender, FormClosedEventArgs e)
        {
            LMS lms = new LMS();
            lms.Show();
        }
    }
}
