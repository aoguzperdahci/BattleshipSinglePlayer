using System;
using System.Globalization;
using System.Windows.Forms;

namespace Battleship.UI
{
    public partial class Launcher : Form
    {
        public Launcher()
        {
            InitializeComponent();

            if (Properties.Settings.Default.Language == "unassigned ")
            {
                if (CultureInfo.InstalledUICulture.Name == "tr-TR")
                {
                    Properties.Settings.Default.Language = "tr-TR";
                }
                else
                {
                    Properties.Settings.Default.Language = "en-US";
                }
                Properties.Settings.Default.Save();
            }

            CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo(Properties.Settings.Default.Language);

            CultureInfo[] laguages = { CultureInfo.GetCultureInfo("en-US"), CultureInfo.GetCultureInfo("tr-TR"), CultureInfo.GetCultureInfo("de-DE") };
            cmbLanguages.DataSource = laguages;
            cmbLanguages.DisplayMember = "NativeName";
            cmbLanguages.ValueMember = "Name";

            cmbLanguages.SelectedValue = Properties.Settings.Default.Language;


        }

        private void CmbSelectedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Language = (string)cmbLanguages.SelectedValue;
            Properties.Settings.Default.Save();
            CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo(Properties.Settings.Default.Language);

            lblLanguage.Text = Resources.Strings.Language;
            btnStart.Text = Resources.Strings.Start;
            btnExit.Text = Resources.Strings.Exit;

        }

        private void StartCilck(object sender, EventArgs e)
        {
            Battleship form = new Battleship();
            form.Show();
            ShowInTaskbar = false;
            WindowState = FormWindowState.Minimized;
            Hide();
        }

        private void ExitClick(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
