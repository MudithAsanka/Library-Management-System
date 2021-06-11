using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Library_Management_System
{
    public partial class LMS : Form
    {
        public LMS()
        {
            InitializeComponent();
        }

        private void LMS_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void LMS_Load(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Maximized;
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bookDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BookDetails bookDetails = new BookDetails();
            bookDetails.Show();
            this.Hide();
        }

        private void membersDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MemberDetails memberDetails = new MemberDetails();
            memberDetails.Show();
            this.Hide();
        }

        private void newBooksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewBooks newBooks = new NewBooks();
            newBooks.Show();
            this.Hide();
        }

        private void issueBooksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IssueBook issueBook = new IssueBook();
            issueBook.Show();
            this.Hide();
        }

        private void returnBookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReturnBook returnBook = new ReturnBook();
            returnBook.Show();
            this.Hide();
        }

        private void viewBooksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewBooks viewBooks = new ViewBooks();
            viewBooks.Show();
            this.Hide();
        }

        private void newMemberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewMembers newMembers = new NewMembers();
            newMembers.Show();
            this.Hide();
        }

        private void viewMembersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewMembers viewMembers = new ViewMembers();
            viewMembers.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IssueBook issueBook = new IssueBook();
            issueBook.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ReturnBook returnBook = new ReturnBook();
            returnBook.Show();
            this.Hide();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            NewBooks newBooks = new NewBooks();
            newBooks.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            NewMembers newMembers = new NewMembers();
            newMembers.Show();
            this.Hide();
        }

        

        private void button5_Click(object sender, EventArgs e)
        {
            ViewBooks viewBooks = new ViewBooks();
            viewBooks.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ViewMembers viewMembers = new ViewMembers();
            viewMembers.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            BookDetails bookDetails = new BookDetails();
            bookDetails.Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            MemberDetails memberDetails = new MemberDetails();
            memberDetails.Show();
            this.Hide();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.Show();
            this.Hide();
        }

        private void aboutTheLibraryManagementSystemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.Show();
            this.Hide();
        }

    }
}
