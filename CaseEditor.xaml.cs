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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.IO;


namespace CaseManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class CaseEditor : Window
    {
        public CaseEditor()
        {
            InitializeComponent();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {


        }

        private void ListBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg1 = new Microsoft.Win32.OpenFileDialog();
            //string SavePath = "../";
            dlg1.InitialDirectory = Directory.GetCurrentDirectory(); // Default file name 
            // Show open file dialog box
            Nullable<bool> result = dlg1.ShowDialog();
            // Process open file dialog box results
            if (result == true)
            {
                string file1 = dlg1.FileName;
                ListBox1.Items.Insert(0, file1);
            }

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var newW = new CaseViewer();

            this.Close();
            //newW.Owner = this;
            newW.Show(); // works

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (!File.Exists("record.xml"))
            {
                System.IO.Directory.CreateDirectory("1");
                XmlTextWriter tw = null;
                tw = new XmlTextWriter("record.xml", null);
                tw.Formatting = Formatting.Indented;
                tw.WriteStartDocument();
                tw.WriteStartElement("Parent");
                tw.WriteStartElement("Child");
                tw.WriteAttributeString("id", "1");
                tw.WriteElementString("Number", TextBox1.Text);
                tw.WriteElementString("Name", TextBox2.Text);
                tw.WriteElementString("Note", TextBox3.Text);
                tw.WriteElementString("User", "Admin");
                tw.WriteElementString("Date", DateTime.Now.ToString("M/d/yyyy"));
                tw.WriteStartElement("Files");
                for (int index = 0; index < ListBox1.Items.Count; index++)
                {
                    String PathName = ListBox1.Items.GetItemAt(index).ToString();
                    String fileName = System.IO.Path.GetFileName(PathName);
                    tw.WriteElementString("File", fileName);
                    String destFile = "1\\" + fileName;
                    System.IO.File.Copy(PathName, destFile, true);
                }
                //  tw.WriteElementString("File", "file1.txt");
                //tw.WriteElementString("File", "file2.txt");
                tw.WriteEndElement();
                tw.WriteEndElement();
                tw.WriteEndDocument();
                tw.Flush();
                tw.Close();
            }
            else
            {
                XmlDocument doc = new XmlDocument();
                doc.PreserveWhitespace = false;
                doc.Load("record.xml");
                XmlNode root = doc.DocumentElement;

                XmlNode lc = doc.LastChild;

                XmlNodeList elemList = lc.SelectNodes("Child");
                String attrVal = "";
                for (int i = 0; i < elemList.Count; i++)
                {
                    attrVal = elemList[i].Attributes["id"].Value;
                }
                int idnum = (int)Convert.ToInt32(attrVal);
                idnum++;
                attrVal = idnum.ToString();

                System.IO.Directory.CreateDirectory(attrVal);

                Console.WriteLine("Display the modified XML...");
                XmlElement number = doc.CreateElement("Number");
                number.InnerText = TextBox1.Text;
                XmlElement name = doc.CreateElement("Name");
                name.InnerText = TextBox2.Text;
                XmlElement note = doc.CreateElement("Note");
                note.InnerText = TextBox3.Text;
                XmlElement user = doc.CreateElement("User");
                user.InnerText = "Admin";
                XmlElement date = doc.CreateElement("Date");
                date.InnerText = DateTime.Now.ToString("M/d/yyyy");

                XmlElement files = doc.CreateElement("Files");
                for (int index = 0; index < ListBox1.Items.Count; index++)
                {
                    String PathName = ListBox1.Items.GetItemAt(index).ToString();
                    String fileName = System.IO.Path.GetFileName(PathName);
                    String destFile = attrVal + "\\" + fileName;
                    System.IO.File.Copy(PathName, destFile, true);
                    XmlElement file = doc.CreateElement("File");
                    file.InnerText = fileName;
                    files.AppendChild(file);
                }
                XmlElement elem = doc.CreateElement("Child");
                elem.SetAttribute("id", attrVal);
                elem.AppendChild(number);
                elem.AppendChild(name);
                elem.AppendChild(note);
                elem.AppendChild(user);
                elem.AppendChild(date);
                elem.AppendChild(files);
                //String sss=root.LastChild.OuterXml;
                root.AppendChild(elem);
                doc.Save("record.xml");
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {

        }
    }
}
