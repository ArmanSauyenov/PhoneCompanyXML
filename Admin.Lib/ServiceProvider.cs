using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Admin.Lib.Models;
using System.Xml;
using System.IO;

namespace Admin.Lib
{
    public class ServiceProvider
    {
        public ServiceProvider():this("")
        {

        }
        public ServiceProvider(string path)
        {
            if (string.IsNullOrEmpty(path))
                this.path = Path.Combine(@"\\dc\Студенты\ПКО\SEB-171.2\C#", "Operators.xml");
            else
                this.path = path;
        }
        List<Provider> Providers = new List<Provider>();
        List<int> ProvidersPrefix = new List<int>();
        public void AddProvider()
        {
            Provider prov = new Provider();

            Console.WriteLine("Vvedite nazvanie companii: ");
            prov.NameCompany = Console.ReadLine();

            Console.WriteLine("Vvedite Logo companii: ");
            prov.LogoUrl = Console.ReadLine();

            Console.WriteLine("Vvedite procent companii: ");
            prov.Percent = Double.Parse(Console.ReadLine());

            Console.WriteLine("Vvedite list prefix "+ "companii (for exit press - 'Enter'): ");

            bool exit = true;
            int pre = 0;
            do
            {
                exit = Int32.TryParse(Console.ReadLine(), out pre);

                    if (exit && isExistsPrefix(pre))
                    prov.Prefix.Add(pre);

            } while (exit);

            //bool proverka = isExists(prov);
            //if (proverka)
            if (isExistsProvider(prov))
            {
                Providers.Add(prov);
                ProvidersPrefix.AddRange(prov.Prefix);
                AddProviderToXML(prov);
            }
        }

        public void EditProvider()
        {
            //1 - найти
            Console.WriteLine("Введите наименование провайдера");
            SearchProviderByNameForEdit(Console.ReadLine());
        }
        public void DeleteProvider()
        {

        }
        public void SearchProviderByNameForEdit(string name)
        {
            XmlDocument xd = getDocument();
            XmlElement root = xd.DocumentElement;

            bool find = false;
            foreach (XmlElement item in root)
            {
                foreach (XmlNode i in item.ChildNodes)
                {
                    if (i.Name == "NameCompany" && i.InnerText == name)
                        find = true;
                }
                if (find)
                {
                    XmlElement el = Edit(item);
                    break;
                }
            }
            if (find)
                xd.Save(path);
        }
        private XmlElement Edit(XmlElement prov)
        {
            foreach (XmlElement item in prov.ChildNodes)
            {
                Console.WriteLine(item.Name+": ?("+item.InnerText+") - ");
                string cn = Console.ReadLine();
                if (!string.IsNullOrEmpty(cn))
                    item.InnerText = cn;
            }
            return prov;
        }

        private bool isExistsProvider(Provider pro)
        {
            if(Providers.Where(w=>w.NameCompany==pro.NameCompany).Count()>0)
            {
                Console.WriteLine("takoi provider uzhe est");
                return false;
            }
            return true;
        }
        private bool isExistsPrefix(int pref)
        {
            if (ProvidersPrefix.Where(item => item == pref).Count() > 0)
            {
                Console.WriteLine("takoi prefix sushestvuyet");
                return false;
            }
            return true;
        }

        private string path { get; set; }
        private void AddProviderToXML(Provider pro)
        {
            XmlDocument doc = getDocument();
            XmlElement elem = doc.CreateElement("Provider");

            XmlElement LogoUrl = doc.CreateElement("LogoUrl");
            LogoUrl.InnerText = pro.LogoUrl;

            XmlElement NameCompany = doc.CreateElement("NameCompany");
            NameCompany.InnerText = pro.NameCompany;

            XmlElement Percent = doc.CreateElement("Percent");
            Percent.InnerText = pro.Percent.ToString();

            XmlElement Prefixs = doc.CreateElement("Prefixs");
            foreach (int item in pro.Prefix)
            {
                XmlElement Prefix = doc.CreateElement("Prefix");
                Prefix.InnerText = item.ToString();
                Prefixs.AppendChild(Prefix);
            }
            elem.AppendChild(LogoUrl);
            elem.AppendChild(NameCompany);
            elem.AppendChild(Percent);
            elem.AppendChild(Prefixs);
            doc.DocumentElement.AppendChild(elem);
            doc.Save(path);
        }
        private XmlDocument getDocument()
        {
            XmlDocument xd = new XmlDocument();
            //\\dc\Студенты\ПКО\SEB - 171.2\C#
            
            FileInfo fi = new FileInfo(path);
            if (fi.Exists)
            {
                xd.Load(path);
            }
            else
            {
                //1
                //FileStream fs = fi.Create();
                //fs.Close();

                //2
                XmlElement xl = xd.CreateElement("Providers");
                xd.AppendChild(xl);
                xd.Save(path);
            }
            return xd;
        }
    }
}
