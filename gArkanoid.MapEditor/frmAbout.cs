using System;
using System.Reflection;
using System.Windows.Forms;

namespace MapEditor
{
    partial class frmAbout : Form
    {
        public frmAbout()
        {
            InitializeComponent();

            this.Text = String.Format("About");
            this.labelVersion.Text = String.Format("Version {0}", this.AssemblyVersion);
            this.labelCopyright.Text = this.AssemblyCopyright;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }
    }
}
