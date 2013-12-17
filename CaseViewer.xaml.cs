using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;
using System.IO;

namespace CaseManager
{
    /// <summary>
    /// Interaction logic for CaseViewer.xaml
    /// </summary>
    public class MyData
    {
        public string CaseNumber { get; set; }
        public string CaseName { get; set; }
        public string CaseNotes { get; set; }
        public string LastUpdatedBy { get; set; }
        public string LastUpdateDate { get; set; }

    }
    public partial class CaseViewer : Window
    {
        public CaseViewer()
        {
            InitializeComponent();
            MyData md = new MyData();
            md.CaseName = "dddd";


            string[] row = { "1", "case1", "case1", "case1", "case1" };
            ListViewItem ldv = new ListViewItem();

            XmlDocument doc = new XmlDocument();
            doc.PreserveWhitespace = false;
            doc.Load("record.xml");
            XmlNode root = doc.DocumentElement;
            XmlNodeList nodes = root.SelectNodes("Child");
            foreach (XmlNode node in nodes)
            {
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine(node.ChildNodes[i].InnerText);
                }

                MyData item = new MyData
                {
                    CaseNumber = node.ChildNodes[0].InnerText.ToString(),
                    CaseName = node.ChildNodes[1].InnerText.ToString(),
                    CaseNotes = node.ChildNodes[2].InnerText.ToString(),
                    LastUpdatedBy = node.ChildNodes[3].InnerText.ToString(),
                    LastUpdateDate = node.ChildNodes[4].InnerText.ToString()
                };
                lstvu1.Items.Add(item);
            }

        }

        public List<MyData> GetData()
        {
            List<MyData> myDataList = new List<MyData>();
            myDataList.Add(new MyData() { CaseNumber = "1", CaseName = "case1", CaseNotes = "case1 notes", LastUpdatedBy = "e1", LastUpdateDate = "07/29/2013" });
            myDataList.Add(new MyData() { CaseNumber = "2", CaseName = "case2", CaseNotes = "case1 notes", LastUpdatedBy = "e1", LastUpdateDate = "07/29/2013" });

            if (myDataList.Count > 0)
            {
                return myDataList;
            }
            else
                return null;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var newW = new CaseEditor();
            this.Close();
            newW.Show(); // works
        }




    }
}
