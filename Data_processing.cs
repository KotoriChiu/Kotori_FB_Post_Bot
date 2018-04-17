using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp28
{
    class Program
    {
        static void Main(string[] args)
        {
            string exepath = System.Environment.CurrentDirectory;
            //string input = "";
            Console.Write("開始讀取ID...         ");
            StreamReader sr = new StreamReader(@exepath + "/data/message.txt", Encoding.Default);
            FileStream fileStream = new FileStream(@exepath + "/data/id.txt", FileMode.Create);
            fileStream.Close();
            while (!sr.EndOfStream)
            {
                string text = sr.ReadLine();
                string id = text.Substring(0, 16);
                StreamWriter textxt = new StreamWriter(@exepath + "/data/id.txt", true);
                textxt.WriteLine(id);
                textxt.Close();
            }
            Console.WriteLine("ID讀取完畢...");
            sr.Close();
            StreamReader rs = new StreamReader(@exepath + "/data/id.txt", Encoding.Default);
            string idds = rs.ReadLine();
            int abc = 0; //儲存紀錄有幾筆資料的變數
            //idds = rs.ReadLine();
            while (idds != null)
            {
                idds = rs.ReadLine();
                abc++;
            }
            abc--;
            rs.Close();
            Console.WriteLine("正將ID存入陣列...");
            StreamReader SR = new StreamReader(@exepath + "/data/id.txt", Encoding.Default);
            string idw = SR.ReadLine();
            //idw = SR.ReadLine();
            string[] ida =new string[abc];
            long[] idd = new long[abc];
            int i = 0;
            while (idw!=null)
            {
                idw = SR.ReadLine();
                if (idw != null)
                {
                    ida[i] = idw;
                    idd[i] = Convert.ToInt64(ida[i]); 
                    i++;
                }               
            }
            Array.Sort(idd);
            //foreach (long a in idd) Console.Write(a + " ");
            SR.Close();
            FileStream good = new FileStream(@exepath + "/data/id.txt", FileMode.Create);
            good.Close();
            string lov = "////////////////////////////////";
            StreamWriter love = new StreamWriter(@exepath + "/data/id.txt", true);
            love.WriteLine(lov);
            love.Close();
            int op;
            for(op = 0; op < abc; op++)
            {
                StreamWriter textxt = new StreamWriter(@exepath + "/data/id.txt", true);
                textxt.WriteLine(idd[op]);
                textxt.Close();
            }

            
            StreamReader data = new StreamReader(@exepath + "/data/message.txt", Encoding.Default);
            StreamReader dataid = new StreamReader(@exepath + "/data/id.txt", Encoding.Default);
            string textms = data.ReadLine();       //排成資料庫的值
            string textid = dataid.ReadLine();   //排成ID的值
            int c = 0;
            int d = 0;
            string[] message = new string[abc]; 
            while(textid != null)
            {
                if(c == 0)
                {
                textid = dataid.ReadLine();//讀取ID值
                    c++;
                }

                textms = data.ReadLine();
                string msid = textms.Substring(0, 16);
                if (msid == textid)
                {
                    message[d] = textms;
                    d++;
                    data.Close();
                    data = new StreamReader(@exepath + "/data/message.txt", Encoding.Default);
                    c--;
                }
            }
            data.Close();
            dataid.Close();
            FileStream newmess = new FileStream(@exepath + "/data/message.txt", FileMode.Create);
            newmess.Close();
            StreamWriter inmass = new StreamWriter(exepath + "/data/message.txt", true);
            inmass.WriteLine(lov);
            inmass.Close();
            for(op = 0;op < abc; op++)
            {
                StreamWriter textxt = new StreamWriter(@exepath + "/data/message.txt", true, Encoding.Default);
                textxt.WriteLine(message[op]);
                textxt.Close();
            }
            string asa = Console.ReadLine(); 
        }
    }
}