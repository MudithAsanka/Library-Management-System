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
    public partial class MemberDetails : Form
    {
        MySqlConnection conn = new MySqlConnection(@"Data Source=localhost;Initial Catalog=library_management_system_db;User Id=root;password=''");

        public MemberDetails()
        {
            InitializeComponent();
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void MemberDetails_Load(object sender, EventArgs e)
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
                cmd.CommandText = "select * from member where memberid like('%" + comboBox1.Text + "%') or membername like('%" + comboBox1.Text + "%')";
                MySqlDataReader dr;
                dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        //string sName = dr.GetString(1);
                        //comboBox1.Items.Add(sName);
                        comboBox1.Items.Add(dr[0].ToString());
                        comboBox1.Items.Add(dr[1].ToString());
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
                cmd.CommandText = "select * from member where memberid like('%" + comboBox1.Text + "%') or membername like('%" + comboBox1.Text + "%')";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    textBox2.Text=dr["memberid"].ToString();
                    textBox3.Text = dr["membername"].ToString();
                    dateTimePicker1.Value = Convert.ToDateTime(dr["dateofbirth"]);
                    textBox4.Text = dr["address"].ToString();
                    textBox5.Text = dr["contactno"].ToString();
                    textBox6.Text = dr["email"].ToString();

                }

                MySqlCommand cmd2 = conn.CreateCommand();
                cmd2.CommandType = CommandType.Text;
                cmd2.CommandText = "select bookid,issuedate,returndate,returneddate from issue where memberid='" + textBox2.Text + "'";
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
            
            textBox3.ReadOnly = false;
            dateTimePicker1.Enabled=true;
            textBox4.ReadOnly = false;
            textBox5.ReadOnly = false;
            textBox6.ReadOnly = false;
            button3.Enabled = true;
            button2.Enabled = false;
            button4.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if ((textBox2.Text != "") && (textBox3.Text != "") && (textBox4.Text != "") && (textBox5.Text != "") && (textBox6.Text != ""))
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
                    cmd.CommandText = "update member set membername='" + textBox3.Text + "',dateofbirth='" + dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss") + "',address='" + textBox4.Text + "',contactno='" + textBox5.Text + "',email='" + textBox6.Text + "' where memberid='" + textBox2.Text + "'";
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record updated successfully!!!");

                    textBox3.ReadOnly = true;
                    dateTimePicker1.Enabled = false;
                    textBox4.ReadOnly = true;
                    textBox5.ReadOnly = true;
                    textBox6.ReadOnly = true;
                    button3.Enabled = false;
                    button2.Enabled = true;
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
                label9.Visible = true;
            }

            
        }

        private void MemberDetails_FormClosed(object sender, FormClosedEventArgs e)
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
                cmd.CommandText = "delete from member where memberid='"+textBox2.Text+"'";
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record deleted successfully!!!");

                comboBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                

                textBox3.ReadOnly = true;
                dateTimePicker1.Enabled = false;
                textBox4.ReadOnly = true;
                textBox5.ReadOnly = true;
                textBox6.ReadOnly = true;
                button3.Enabled = false;
                button2.Enabled = true;
                button4.Enabled = false;
                comboBox1.Items.Clear();

                MySqlCommand cmd2 = conn.CreateCommand();
                cmd2.CommandType = CommandType.Text;
                cmd2.CommandText = "select * from member";
                MySqlDataReader dr2;
                dr2 = cmd2.ExecuteReader();

                if (dr2.HasRows)
                {
                    while (dr2.Read())
                    {
                        //string sName = dr.GetString(1);
                        //comboBox1.Items.Add(sName);
                        comboBox1.Items.Add(dr2[0].ToString());
                        comboBox1.Items.Add(dr2[1].ToString());
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
