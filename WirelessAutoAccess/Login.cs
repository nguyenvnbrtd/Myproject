using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WirelessAutoAccess
{
    class Login
    {
        private WebBrowser wbWeb;

        public Login(WebBrowser wb)
        {
            this.WbWeb = wb;
        }

        public WebBrowser WbWeb
        {
            get { return wbWeb; }
            set { wbWeb = value; }
        }

        void setUser(string data)
        {

            HtmlElement element = WbWeb.Document.GetElementById("username");
            element.SetAttribute("value", data);
            MessageBox.Show("Login comepleted");
        }

        void setPassword(string data)
        {

            HtmlElement element = WbWeb.Document.GetElementById("password");
            element.SetAttribute("value", data);
            MessageBox.Show("Login comepleted");
        }

        void enter() {
            HtmlElement element = WbWeb.Document.GetElementById("subbmitbutton");
            element.InvokeMember("click");
            MessageBox.Show("Login comepleted");
        }
        public void loadUser()
        {
            setUser(Cons.userName);
            setPassword(Cons.passWord);
           enter();
        }
    }
}
