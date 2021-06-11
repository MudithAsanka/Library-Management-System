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
    public partial class Login : Form
    {
        MySqlConnection conn = new MySqlConnection(@"Data Source=localhost;Initial Catalog=library_management_system_db;User Id=root;password=''");

        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            label4.Visible = false;
            if (ConnectionState.Open == conn.State)
            {
                conn.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text != "") && (textBox2.Text != ""))
            {
                if (ConnectionState.Open == conn.State)
                {
                    conn.Close();
                }
                int count = 0;
                try
                {
                    conn.Open();

                    MySqlCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select * from login where username='" + textBox1.Text + "' and password='" + textBox2.Text + "'";
                    cmd.ExecuteNonQuery();
                    DataTable dt = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                    count = Convert.ToInt32(dt.Rows.Count.ToString());
                    if (count == 0)
                    {
                        conn.Close();
                        label4.Text = "Incorrect Username and Password. Please try again!";
                        label4.Visible = true;
                    }
                    else
                    {
                        conn.Close();
                        //login
                        this.Hide();    //This form(Login) should be disapear
                        LMS lms = new LMS();
                        lms.Show();     //lms form will be appeared
                    }


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
                label4.Text = "Please Enter Username Password";
                label4.Visible = true;
            }

            



            /*if((textBox1.Text=="admin")&&(textBox2.Text=="admin"))
            {
                //login
                this.Hide();    //This form(Login) should be disapear
                LMS lms = new LMS();
                lms.Show();     //lms form will be appeared
            }
            else if ((textBox1.Text == "") && (textBox2.Text == ""))
            {
                label4.Text = "Please enter Username and Password";
                label4.Visible = true;
               
            }
            else
            {
                label4.Text = "Incorrect Username and Password. Please try again!";
                label4.Visible = true;
            }*/
        }

        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

     
    }
}
