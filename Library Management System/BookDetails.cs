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
    public partial class BookDetails : Form
    {
        MySqlConnection conn = new MySqlConnection(@"Data Source=localhost;Initial Catalog=library_management_system_db;User Id=root;password=''");

        public BookDetails()
        {
            InitializeComponent();
        }

        private void BookDetails_Load(object sender, EventArgs e)
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
                cmd.CommandText = "select * from book where bookid like('%" + comboBox2.Text + "%') or bookname like('%" + comboBox2.Text + "%')";
                MySqlDataReader dr;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        //string sName = dr.GetString(1);
                        //comboBox1.Items.Add(sName);
                        comboBox2.Items.Add(dr[0].ToString());
                        comboBox2.Items.Add(dr[2].ToString());
                    }
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

        private void button1_Click(object sender, EventArgs e)
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
                cmd.CommandText = "select * from book where bookid like('%" + comboBox2.Text + "%') or bookname like('%" + comboBox2.Text + "%')";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                
                foreach (DataRow dr in dt.Rows)
                {
                    textBox2.Text=dr["bookid"].ToString();
                    comboBox1.SelectedText= dr["class"].ToString();
                    textBox3.Text = dr["bookname"].ToString();
                    textBox4.Text = dr["author"].ToString();
                    textBox5.Text = dr["publisher"].ToString();
                    numericUpDown1.Value = Convert.ToInt32(dr["edition"]);
                    numericUpDown2.Value= Convert.ToInt32(dr["quantity"]);
                    textBox6.Text = dr["availablequantity"].ToString();
                    textBox7.Text = (numericUpDown2.Value-Convert.ToInt32(dr["availablequantity"])).ToString();
                    textBox8.Text = dr["price"].ToString();
                    textBox9.Text = dr["returndays"].ToString();
                    textBox10.Text = dr["fine"].ToString();
                }

                MySqlCommand cmd2 = conn.CreateCommand();
                cmd2.CommandType = CommandType.Text;
                cmd2.CommandText = "select memberid,issuedate,returndate,returneddate from issue where bookid='"+textBox2.Text+"'";
                cmd2.ExecuteNonQuery();
                DataTable dt2 = new DataTable();
                MySqlDataAdapter da2 = new MySqlDataAdapter(cmd2);
                da2.Fill(dt2);
                dataGridView1.DataSource = dt2;

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

        private void button2_Click(object sender, EventArgs e)
        {
            comboBox1.Enabled = true;
            textBox3.ReadOnly = false;
            textBox4.ReadOnly = false;
            textBox5.ReadOnly = false;
            numericUpDown1.ReadOnly = false;
            numericUpDown2.ReadOnly = false;
            //textBox6.ReadOnly = false;
            //textBox7.ReadOnly = false;
            textBox8.ReadOnly = false;
            textBox9.ReadOnly = false;
            textBox10.ReadOnly = false;
            button3.Enabled = true;
            button2.Enabled = false;
            button4.Enabled = true;
            

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if ((comboBox1.SelectedItem!=null) && (textBox3.Text != "") && (textBox4.Text != "") && (textBox5.Text != "") && (textBox8.Text != "") && (textBox9.Text != "") && (textBox10.Text != ""))
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

                    cmd.CommandText = "update book set class='" + comboBox1.SelectedItem.ToString() + "',bookname='" + textBox3.Text + "',author='" + textBox4.Text + "',publisher='" + textBox5.Text + "',edition='" + numericUpDown1.Value + "',quantity='"+ numericUpDown2.Value +"',price='"+ Convert.ToDouble(textBox8.Text)+"',returndays='"+Convert.ToInt32(textBox9.Text)+"',fine='"+Convert.ToDouble(textBox10.Text)+"' where bookid='" + textBox2.Text + "'";
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record updated successfully!!!");

                    label18.Visible = false;
                    comboBox1.Enabled = false;
                    textBox3.ReadOnly = true;
                    textBox4.ReadOnly = true;
                    textBox5.ReadOnly = true;
                    numericUpDown1.ReadOnly = true;
                    numericUpDown2.ReadOnly = true;
                    textBox6.ReadOnly = true;
                    textBox7.ReadOnly = true;
                    textBox8.ReadOnly = true;
                    textBox9.ReadOnly = true;
                    textBox10.ReadOnly = true;
                    button2.Enabled = true;
                    button3.Enabled = false;
                    button4.Enabled = false;

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
            else
            {
                label18.Visible = true;
            }
        }

        private void BookDetails_FormClosed(object sender, FormClosedEventArgs e)
        {
            LMS lms = new LMS();
            lms.Show();
        }

        private void button4_Click(object sender, EventArgs e)
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

                cmd.CommandText = "delete from book where bookid='"+textBox2.Text+"'";
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record deleted successfully!!!");

                textBox2.Text = "";
                comboBox1.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                numericUpDown1.Value = 0;
                numericUpDown2.Value = 1;
                textBox6.Text = "";
                textBox7.Text = "";
                textBox8.Text = "";
                textBox9.Text = "";
                textBox10.Text = "";
                

                label18.Visible = false;
                comboBox1.Enabled = false;
                textBox3.ReadOnly = true;
                textBox4.ReadOnly = true;
                textBox5.ReadOnly = true;
                numericUpDown1.ReadOnly = true;
                numericUpDown2.ReadOnly = true;
                textBox6.ReadOnly = true;
                textBox7.ReadOnly = true;
                textBox8.ReadOnly = true;
                textBox9.ReadOnly = true;
                textBox10.ReadOnly = true;
                button2.Enabled = true;
                button3.Enabled = false;
                comboBox2.Items.Clear();
                
                MySqlCommand cmd2 = conn.CreateCommand();
                cmd2.CommandType = CommandType.Text;
                cmd2.CommandText = "select * from book";
                MySqlDataReader dr2;
                dr2 = cmd2.ExecuteReader();
                if (dr2.HasRows)
                {
                    while (dr2.Read())
                    {
                        //string sName = dr.GetString(1);
                        //comboBox1.Items.Add(sName);
                        comboBox2.Items.Add(dr2[0].ToString());
                        comboBox2.Items.Add(dr2[2].ToString());
                    }
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
    }
}
