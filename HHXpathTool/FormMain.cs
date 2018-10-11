using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;

namespace HHXpath
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void btn_Check_Click(object sender, EventArgs e)
        {
            CheckXpath();
        }

        private void txt_Xpath_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CheckXpath();
            }

        }
        public void CheckXpath()
        {
            HtmlAgilityPack.HtmlDocument htmlDocument = new HtmlAgilityPack.HtmlDocument();
            htmlDocument.LoadHtml(this.richtbXpath.Text);
            try
            {
                HtmlNodeCollection htmlNodeCollection = htmlDocument.DocumentNode.SelectNodes(this.txt_Xpath.Text.Trim());
                this.richtbRsult.Text = "";
                bool flag = htmlNodeCollection == null;
                if (flag)
                {
                    this.richtbRsult.Text = @"=====未匹配到节点，请检查=====";
                }
                else
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    int num = 0;
                    foreach (HtmlNode current in ((IEnumerable<HtmlNode>)htmlNodeCollection))
                    {
                        num++;
                        bool flag2 = !string.IsNullOrEmpty(this.txt_Attr.Text);
                        if (flag2)
                        {
                            try
                            {
                                stringBuilder.Append(string.Concat(new object[]
                                {
                                    "[",
                                    num,
                                    "]     ",
                                    current.Attributes[this.txt_Attr.Text].Value
                                }));
                                string attributeValue = current.GetAttributeValue("href", "");
                            }
                            catch (Exception)
                            {
                                stringBuilder.Append("[" + num + "]=====未查询到此属性，请检查=====");
                            }
                        }
                        else
                        {
                            stringBuilder.Append(string.Concat(new object[]
                            {
                                "[",
                                num,
                                "]     ",
                                current.InnerText
                            }));
                        }
                        stringBuilder.Append("\r\n--------------------------------------------------\r\n");
                    }
                    this.richtbRsult.Text = stringBuilder.ToString();
                }
            }
            catch (Exception ex)
            {
                this.richtbRsult.Text = ex.Message;
            }
        }
    }
}
