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
    public partial class IssueBook : Form
    {
        MySqlConnection conn = new MySqlConnection(@"Data Source=localhost;Initial Catalog=library_management_system_db;User Id=root;password=''");

        public IssueBook()
        {
            InitializeComponent();
        }

        private void IssueBook_Load(object sender, EventArgs e)
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
                cmd.CommandText = "select * from book where bookid like('%" + comboBox1.Text + "%')";
                MySqlDataReader dr;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        //string sName = dr.GetString(1);
                        //comboBox1.Items.Add(sName);
                        comboBox1.Items.Add(dr[0].ToString());
                    }
                }
                dr.Close();
                // ***********************************
                MySqlCommand cmd2 = conn.CreateCommand();
                cmd2.CommandType = CommandType.Text;
                cmd2.CommandText = "select * from member where memberid like('%" + comboBox2.Text + "%')";
                MySqlDataReader dr2;
                dr2 = cmd2.ExecuteReader();
                if (dr2.HasRows)
                {
                    while (dr2.Read())
                    {
                        //string sName = dr.GetString(1);
                        //comboBox1.Items.Add(sName);
                        comboBox2.Items.Add(dr2[0].ToString());
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
            if (comboBox1.SelectedItem != null)
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
                    cmd.CommandText = "select * from book where bookid='"+comboBox1.SelectedItem.ToString()+"'";
                    cmd.ExecuteNonQuery();
                    DataTable dt = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);

                    foreach (DataRow dr in dt.Rows)
                    {
                        label8.Text = dr["class"].ToString();
                        label9.Text = dr["bookname"].ToString();
                        label10.Text = dr["author"].ToString();
                        label11.Text = dr["publisher"].ToString();
                        label12.Text = dr["edition"].ToString();
                        
                    }

                    label8.Visible=true;
                    label9.Visible = true;
                    label10.Visible=true;
                    label11.Visible = true;
                    label12.Visible = true;

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

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem != null)
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
                    cmd.CommandText = "select * from member where memberid='" + comboBox2.SelectedItem.ToString() + "'";
                    cmd.ExecuteNonQuery();
                    DataTable dt = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);

                    foreach (DataRow dr in dt.Rows)
                    {
                        label19.Text = dr["membername"].ToString();
                        label20.Text = Convert.ToDateTime(dr["dateofbirth"]).ToString("yyyy-MM-dd");
                        label21.Text = dr["address"].ToString();
                        label22.Text = dr["contactno"].ToString();
                        label23.Text = dr["email"].ToString();

                    }

                    label19.Visible = true;
                    label20.Visible=true;
                    label21.Visible = true;
                    label22.Visible = true;
                    label23.Visible = true;

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

        private void button3_Click(object sender, EventArgs e)
        {
            DateTime issuedate = DateTime.Now.Date;

            if ((comboBox1.SelectedItem != null) && (comboBox2.SelectedItem != null))
            {
                if (ConnectionState.Open == conn.State)
                {
                    conn.Close();
                }
                try
                {
                    conn.Open();

                    int booksquantity = 0;

                    MySqlCommand cmd4 = conn.CreateCommand();
                    cmd4.CommandType = CommandType.Text;
                    cmd4.CommandText = "select * from book where bookid='" + comboBox1.SelectedItem.ToString() + "'";
                    cmd4.ExecuteNonQuery();
                    DataTable dt2 = new DataTable();
                    MySqlDataAdapter da2 = new MySqlDataAdapter(cmd4);
                    da2.Fill(dt2);

                    foreach (DataRow dr2 in dt2.Rows)
                    {
                        booksquantity = Convert.ToInt32(dr2["availablequantity"]);
                    }

                    if (booksquantity > 0)
                    {
                        MySqlCommand cmd = conn.CreateCommand();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "select * from book where bookid='" + comboBox1.SelectedItem.ToString() + "'";
                        cmd.ExecuteNonQuery();
                        DataTable dt = new DataTable();
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        da.Fill(dt);
                        int returndays = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            returndays = Convert.ToInt32(dr["returndays"]);
                        }

                        DateTime returnday = issuedate.AddDays(returndays);

                        MySqlCommand cmd2 = conn.CreateCommand();
                        cmd2.CommandType = CommandType.Text;
                        cmd2.CommandText = "insert into issue(bookid, memberid, issuedate, returndate) values('" + comboBox1.SelectedItem.ToString() + "','" + comboBox2.SelectedItem.ToString() + "','" + issuedate.ToString("yyyy-MM-dd HH:mm:ss") + "','" + returnday.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        cmd2.ExecuteNonQuery();
                        MessageBox.Show("Book Should be returned on " + returnday.ToString("yyyy-MM-dd"));
                        MessageBox.Show("Book Issued Successfully");

                        MySqlCommand cmd3 = conn.CreateCommand();
                        cmd3.CommandType = CommandType.Text;
                        cmd3.CommandText = "update book set availablequantity=availablequantity-1 where bookid='" + comboBox1.SelectedItem.ToString() + "'";
                        cmd3.ExecuteNonQuery();

                        // Everything Should Be Clear

                        comboBox1.SelectedItem = null;
                        comboBox2.SelectedItem = null;
                        label8.Visible = false;
                        label9.Visible = false;
                        label10.Visible = false;
                        label11.Visible = false;
                        label12.Visible = false;
                        label19.Visible = false;
                        label20.Visible = false;
                        label21.Visible = false;
                        label22.Visible = false;
                        label23.Visible = false;

                        conn.Close();
                    }
                    else
                    {
                        MessageBox.Show("There are no available books in stock");
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
                MessageBox.Show("Please select Book ID or Member ID");
            }
            
        }

        private void IssueBook_FormClosed(object sender, FormClosedEventArgs e)
        {
            LMS lms = new LMS();
            lms.Show();
        }
    }
}
