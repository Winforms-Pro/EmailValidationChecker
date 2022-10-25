using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;
using xNetStandard;
using HtmlAgilityPack;

namespace EmailValidationChecker
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void buttonCheck_Click(object sender, EventArgs e)
        {
            try
            {
                HttpRequest httpRequest = new HttpRequest();
                httpRequest.Cookies = new CookieDictionary();
                httpRequest.UserAgent = Http.ChromeUserAgent();
                RequestParams para = new RequestParams();
                para["email"] = textBoxEmail.Text;

                string html = httpRequest.Post("https://www.infobyip.com/verifyemailaccount.php", para).ToString();


                var page = new HtmlAgilityPack.HtmlDocument();
                page.LoadHtml(html);
                var list = "";
                if (page.DocumentNode.SelectSingleNode("//td[@id='output']//td[@class='left']//img[4]") != null)
                {
                    list = page.DocumentNode.SelectSingleNode("//td[@id='output']//td[@class='left']//img[4]").NextSibling.InnerText;
                }
                else
                {
                    list = page.DocumentNode.SelectSingleNode("//td[@id='output']//td[@class='left']").InnerText;
                    string[] arr = list.Split('.');
                    int count = arr.Length - 1;
                    list = arr[count];

                }

                labelResult.Text = list;

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
