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
    public partial class ReturnBook : Form
    {
        MySqlConnection conn = new MySqlConnection(@"Data Source=localhost;Initial Catalog=library_management_system_db;User Id=root;password=''");

        public ReturnBook()
        {
            InitializeComponent();
        }

        private void ReturnBook_Load(object sender, EventArgs e)
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
                cmd.CommandText = "select memberid from issue where returneddate is null";
                MySqlDataReader dr;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        //string sName = dr.GetString(1);
                        //comboBox1.Items.Add(sName);
                        comboBox2.Items.Add(dr["memberid"].ToString());
                    }
                }
                dr.Close();
                // ***********************************
                /*MySqlCommand cmd2 = conn.CreateCommand();
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
                dr2.Close();
                 */
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
                    //cmd.CommandText = "select * from issue left join book on issue.bookid=book.bookid and book.bookid='" + comboBox1.SelectedItem.ToString() + "'";
                    cmd.CommandText = "select * from book where bookid='" + comboBox1.SelectedItem.ToString() + "'";
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

                    label8.Visible = true;
                    label9.Visible = true;
                    label10.Visible = true;
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

                //*******************************************************

                DateTime issuedate = DateTime.Today.Date;
                DateTime returndate = DateTime.Today.Date;
                DateTime returneddate = DateTime.Today.Date;
                Double fine = 0.00;
                Double payment = 0.00;

                if ((comboBox1.SelectedItem != null) && (comboBox2.SelectedItem != null))
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
                        cmd.CommandText = "select issue.bookid,issue.memberid,issue.issuedate,issue.returndate,book.fine from issue inner join book on issue.bookid=book.bookid where issue.bookid='" + comboBox1.SelectedItem.ToString() + "' and issue.memberid='" + comboBox2.SelectedItem.ToString() + "'";
                        cmd.ExecuteNonQuery();
                        DataTable dt = new DataTable();
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        da.Fill(dt);

                        foreach (DataRow dr in dt.Rows)
                        {
                            label26.Text = Convert.ToDateTime(dr["returndate"]).ToString("yyyy-MM-dd");
                            label28.Text = Convert.ToDateTime(dr["issuedate"]).ToString("yyyy-MM-dd");
                            issuedate = Convert.ToDateTime(dr["issuedate"]).Date;
                            returndate = Convert.ToDateTime(dr["returndate"]).Date;
                            fine = Convert.ToDouble(dr["fine"]);
                        }
                        
                        /*TimeSpan daydifferencetoreturneddate = returneddate - issuedate;
                        int daysdifferencetoreturneddate = daydifferencetoreturneddate.Days;
                        TimeSpan daydifferencetoreturndate = returndate - issuedate;
                        int daysdifferencetoreturndate = daydifferencetoreturndate.Days;
                        
                        if (daysdifferencetoreturneddate <= daysdifferencetoreturndate)
                        {
                            label30.Text = "0";
                            label32.Text = "0.00";
                        }
                        else
                        {
                            TimeSpan delaydaysTS = returneddate - returndate;
                            int delaydays = delaydaysTS.Days;
                            label30.Text = delaydays.ToString();
                            payment = fine * delaydays;
                            label32.Text = payment.ToString();
                        }*/
                        
                        TimeSpan daydifferenttoreturneddate = returneddate - returndate;
                        int daysdifferenttoreturneddate = daydifferenttoreturneddate.Days;
                        if (daysdifferenttoreturneddate > 0)
                        {
                            label30.Text = daysdifferenttoreturneddate.ToString();
                            payment = fine * daysdifferenttoreturneddate;
                            label32.Text = payment.ToString();
                        }
                        else
                        {
                            label30.Text = "0";
                            label32.Text = "0.00";
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

                    button3.Enabled = true;
                    panel4.Visible = true;
                }

                //*********************************************

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
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
                    label20.Visible = true;
                    label21.Visible = true;
                    label22.Visible = true;
                    label23.Visible = true;

                    MySqlCommand cmd2 = conn.CreateCommand();
                    cmd2.CommandType = CommandType.Text;
                    cmd2.CommandText = "select bookid from issue where memberid='" + comboBox2.SelectedItem.ToString() + "' and returneddate is null";
                    MySqlDataReader dr2;
                    dr2 = cmd2.ExecuteReader();
                    if (dr2.HasRows)
                    {
                        while (dr2.Read())
                        {
                            //string sName = dr.GetString(1);
                            //comboBox1.Items.Add(sName);
                            comboBox1.Items.Add(dr2["bookid"].ToString());
                        }
                    }
                    dr2.Close();

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

                button1.Enabled = true;
                comboBox1.Enabled = true;
                
                
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            

            DateTime returneddate = DateTime.Today.Date;

            if (ConnectionState.Open == conn.State)
            {
                conn.Close();
            }
            try
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update issue set returneddate='" + returneddate.ToString("yyyy-MM-dd") + "' where memberid='" + comboBox2.SelectedItem.ToString() + "' and bookid='" + comboBox1.SelectedItem.ToString() + "'";
                cmd.ExecuteNonQuery();

                
                MySqlCommand cmd1 = conn.CreateCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = "update book set availablequantity=availablequantity+1 where bookid='" + comboBox1.SelectedItem.ToString() + "'";
                cmd1.ExecuteNonQuery();

                MessageBox.Show("Book has returned successfully");

                /////////////////////////////////////////////////////////
                comboBox1.Enabled = false;
                comboBox2.Items.Clear();
                MySqlCommand cmd2 = conn.CreateCommand();
                cmd2.CommandType = CommandType.Text;
                cmd2.CommandText = "select memberid from issue where returneddate is null";
                MySqlDataReader dr;
                dr = cmd2.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        //string sName = dr.GetString(1);
                        //comboBox1.Items.Add(sName);
                        comboBox2.Items.Add(dr["memberid"].ToString());
                    }
                }
                dr.Close();
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

            

            panel4.Visible = false;

            label19.Visible = false;
            label20.Visible = false;
            label21.Visible = false;
            label22.Visible = false;
            label23.Visible = false;

            label8.Visible = false;
            label9.Visible = false;
            label10.Visible = false;
            label11.Visible = false;
            label12.Visible = false;

            comboBox1.Text = "";
            comboBox2.Text = "";
        }

        private void ReturnBook_FormClosed(object sender, FormClosedEventArgs e)
        {
            LMS lms = new LMS();
            lms.Show();
        }
    }
}
