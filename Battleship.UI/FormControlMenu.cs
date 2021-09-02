using System;
using System.Windows.Forms;

namespace Battleship.UI
{
    public partial class FormControlMenu : Form
    {
        static Battleship main;

        public FormControlMenu(Battleship form1)
        {
            InitializeComponent();
            btnResume.Text = Resources.Strings.Resume;
            btnRestart.Text = Resources.Strings.Restart;
            btnExit.Text = Resources.Strings.Exit;

            main = form1;
            this.KeyPreview = true;
            this.KeyDown += Esc;
        }

        private void btnResume_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            main.Dispose();
            Battleship form = new Battleship();
            form.Show();
            this.Dispose();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Esc(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}