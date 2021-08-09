using Microsoft.Win32;
using System;
using System.Windows.Forms;

namespace WirelessAutoAccess
{
    public partial class Form1 : Form
    {
        WebBrowser wb;
        Login lg;
        public Form1()
        {
            
            InitializeComponent();
            wb = new WebBrowser();
            wb.Width = 900;
            wb.Height = 400;
           // wb.Visible = false;
           // wb.ScriptErrorsSuppressed = true;
            btnEnd.Enabled = false;
          
            wb.Navigate(Cons.link);
            wb.DocumentCompleted += Wb_DocumentCompleted;
            
            
            

            

            // StartWithOS();


        }
        static void StartWithOS()
        {
            RegistryKey regkey = Registry.CurrentUser.CreateSubKey("Software\\WifiAccess");
            RegistryKey regstart = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");
            string keyvalue = "1";
            try
            {
                regkey.SetValue("Index", keyvalue);
                regstart.SetValue("WifiAccess", Application.StartupPath + "\\" + Application.ProductName + ".exe");
                regkey.Close();
            }
            catch (System.Exception e)
            { }
                
        }

        private void Wb_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            this.Controls.Add(wb);
            lg = new Login(wb);
            btnEnd.Enabled = true;
            
            lg.loadUser();
        }

        private void BtnEnd_Click(object sender, EventArgs e)
        {
            
                Application.Exit();
            
        }

        
    }
}
