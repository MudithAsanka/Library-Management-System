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
    public partial class NewMembers : Form
    {
        MySqlConnection conn = new MySqlConnection(@"Data Source=localhost;Initial Catalog=library_management_system_db;User Id=root;password=''");

        public NewMembers()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void NewMembers_Load(object sender, EventArgs e)
        {
            if(ConnectionState.Open==conn.State){
                conn.Close();
            }
            try
            {
                conn.Open();

                generateID();   //Generate Member ID
                //this.ActiveControl = textBox2;  //automatically cursor brings to textbox2

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

                /*
                cmd.CommandText = "select count(*) from member";
                int countofmembers = Convert.ToInt32(cmd.ExecuteScalar());
                int nextNo = countofmembers + 1;
                int charactersofnextno = Convert.ToInt32(nextNo.ToString().Length);
                //MessageBox.Show(charactersofnextno.ToString());
                string beforeprefix = "0000";
                //MessageBox.Show(beforeprefix);
                string editedprefix = beforeprefix.Remove((beforeprefix.Length-charactersofnextno),charactersofnextno);
                //MessageBox.Show(editedprefix);
                textBox1.Text = "M-" + editedprefix + nextNo.ToString();
                */
                
                // memberid generate from last id+1

                cmd.CommandText = "select count(*) from member";
                int countofmembers = Convert.ToInt32(cmd.ExecuteScalar());
                //MessageBox.Show(countofmembers.ToString());
                string lastdataid="";
                if (countofmembers > 0)
                {
                    MySqlCommand cmd1 = conn.CreateCommand();
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = "select memberid from member order by memberid desc limit 1";
                    MySqlDataReader dr = cmd1.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            lastdataid = dr["memberid"].ToString();
                        }
                    }
                    //string lastdataid = cmd1.ExecuteNonQuery().ToString();
                    //MessageBox.Show(lastdataid);
                    char[] removeprefix = { 'M', '-' };
                    int lastNo = Convert.ToInt32(lastdataid.TrimStart(removeprefix));
                    //MessageBox.Show(lastNo.ToString());
                    int nextNo = lastNo + 1;
                    int charactersofnextno = Convert.ToInt32(nextNo.ToString().Length);
                    //MessageBox.Show(charactersofnextno.ToString());
                    string beforeprefix = "0000";
                    //MessageBox.Show(beforeprefix);
                    string editedprefix = beforeprefix.Remove((beforeprefix.Length - charactersofnextno), charactersofnextno);
                    //MessageBox.Show(editedprefix);
                    textBox1.Text = "M-" + editedprefix + nextNo.ToString();
                }
                else
                {
                    textBox1.Text = "M-0000";
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
            if ((textBox1.Text != "") && (textBox2.Text != "") && (textBox3.Text != "") && (textBox4.Text != "") && (textBox5.Text != ""))
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
                    cmd.CommandText = "insert into member values('" + textBox1.Text + "','" + textBox2.Text + "','" + dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss") + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "')";
                    cmd.ExecuteNonQuery();

                    label8.Hide();
                    MessageBox.Show("New Member registered successfully!!!");
                    

                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";

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
                label8.Show();
            }
        }

        private void NewMembers_FormClosed(object sender, FormClosedEventArgs e)
        {
            LMS lms = new LMS();
            lms.Show();
        }
    }
}
