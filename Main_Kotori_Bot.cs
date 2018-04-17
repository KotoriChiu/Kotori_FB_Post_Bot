using System;
using System.Collections.Generic;
using System.Configuration;
using System.Dynamic;
using System.Linq;
using System.IO;
using System.Net;
using System.Text;
using Facebook;
using System.Timers;

//使用者輸入的資訊 user-->帳號 email-->信箱 
namespace ConsoleApp14
{
    class Program
    {
        static void Main(string[] args)
        {
            start: //設定起始標籤
            string exepath = System.Environment.CurrentDirectory;
            Console.Write("歡迎使用 南小鳥開發的南小鳥發文機器人!!!\r\n");
            Console.Write("本軟體僅能\"發文\"並不能發圖片\r\n");
            Console.Write("本軟體的登入系統僅會要求提供appid 並不會存取應用程式密鑰以及發文ID\r\n");
            Console.Write("請輸入使用者帳號(初次使用請輸入\"註冊\"或\"register\"忘記密碼請輸入\"forget\"):");
            string user = Console.ReadLine();
            string password;
            string password01;

            if (user == "註冊" || user == "register" || user == "forget")
            {
                if (user == "forget")
                {
                    System.IO.StreamReader etext = new System.IO.StreamReader(@exepath + "/data/用戶帳密測試.txt", Encoding.Default, true);//讀取 
                    Console.Write("請輸入註冊時的信箱:");
                    string email = Console.ReadLine();
                    int ii = 0;
                    int oo = 0;
                    while (ii == oo)
                    {
                        string Eerxt = etext.ReadLine();
                        string emailnb = Eerxt.Substring(0, 2);
                        int Enb = int.Parse(emailnb);
                        string EMAIL = Eerxt.Substring(8, Enb);
                        if (email == EMAIL)
                        {
                            string usernb = Eerxt.Substring(2, 2);//取得帳號的字串長度
                            int Unb = int.Parse(usernb);
                            string User = Eerxt.Substring(8 + Enb, Unb);//取得帳號 //會用到
                            string passnb = Eerxt.Substring(4, 2);//取得原密碼字串長度
                            int Pnd = int.Parse(passnb);
                            string Passwd = Eerxt.Substring(8 + Enb + Unb, Pnd); //取得原密碼
                            string appidnb = Eerxt.Substring(6, 2); //取得應用程式ID長度
                            int Anb = int.Parse(appidnb);
                            string Appid = Eerxt.Substring(8 + Enb + Unb + Pnd, Anb);//取得發文ID 會用到
                            etext.Close();
                            StreamWriter textxt = new StreamWriter(@exepath + "/data/userpw.txt", true); //寫入userpw.txt
                            System.Net.Mail.SmtpClient MySmtp = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);
                            MySmtp.Credentials = new System.Net.NetworkCredential("zxcv88928@gmail.com", "as4121120955065969"); //這邊要填
                            MySmtp.EnableSsl = true;
                            string s = Guid.NewGuid().ToString("N");
                            byte[] bt = System.Text.UnicodeEncoding.Unicode.GetBytes(s);//產生64 byte
                            string c = System.Text.UnicodeEncoding.Unicode.GetString(bt);
                            byte[] be = Guid.NewGuid().ToByteArray();//生產一組亂數
                            textxt.WriteLine(emailnb + usernb + "32" + appidnb + EMAIL + User + c + Appid);
                            textxt.Close();
                            string recipient = "<" + email + ">";
                            MySmtp.Send("南小鳥排程發文機器人 開發者<zxcv88928@gmail.com>", "收件者" + recipient, "南小鳥排成發文機器人 忘記密碼", "親愛的使用者 " + User + "您好\r\n \r\n近期您似乎使用了忘記密碼服務\r\n以下是您的暫時密碼\r\n暫時密碼:" + c + "\r\n若您尚未使用忘記密碼功能 請刪除本信件 無須理會 謝謝!!\r\n \r\n \r\n南小鳥發文機器人開發者 敬上");
                            //寄亂數密碼信給使用者

                            Console.Write("已發出信件至" + email + " 請前往查看信件!! 並關閉程式重新開啟!!\r\n");
                            Console.ReadLine();
                        }
                        if (etext == null) //讀到無值
                        {
                            Console.WriteLine("此信箱尚未註冊，請先註冊\n");
                            goto start; //跳回起始標籤
                        }

                    }


                }
                else
                {
                    registered: //設定註冊標籤
                    Console.Write("確認註冊??(Y/N)");
                    string check = Console.ReadLine();
                    if (check == "Y" || check == "y" || check == "N" || check == "n")
                    {
                        if (check == "Y" || check == "y")
                        {
                            Console.Write("請輸入信箱:"); //int變數皆為讀取輸入字串的長度
                            string email = Console.ReadLine();
                            int emailnb = email.Length;
                            pleaseuser: //設置帳號輸入的標籤
                            Console.Write("請輸入帳號:");
                            string account = Console.ReadLine();
                            int accountnb = account.Length;

                            Passwordwrite: //設定密碼輸入標籤
                            Console.Write("請輸入密碼:");
                            password = Console.ReadLine();
                            Console.Write("請再輸入一次密碼:");
                            password01 = Console.ReadLine();
                            if (password != password01)
                            {
                                Console.WriteLine("前後兩次輸入的密碼不一，請重新輸入");
                                goto Passwordwrite; //跳回密碼輸入
                            }
                            int passwordnb = password.Length;
                            Console.Write("請輸入請輸入應用程式ID:");
                            string appid = Console.ReadLine();
                            int appitnb = appid.Length;
                            string anb;
                            if (accountnb < 10 || accountnb > 9 || accountnb > 19)
                            {
                                if (accountnb < 10)
                                {
                                    anb = 0 + accountnb.ToString();
                                    string apnb = appitnb.ToString();
                                    string enb = emailnb.ToString();
                                    string passnb = passwordnb.ToString();
                                    StreamWriter sww = new StreamWriter(@exepath + "/data/用戶帳密測試.txt", true);
                                    sww.WriteLine(enb + anb + passnb + apnb + email + account + password + appid); //寫入使用者註冊時的資料
                                    sww.Close(); //  將/data/用戶帳密測試.txt 關閉
                                }
                                if (accountnb > 9)
                                {
                                    anb = accountnb.ToString();
                                    string apnb = appitnb.ToString();
                                    string enb = emailnb.ToString();
                                    string passnb = passwordnb.ToString();
                                    StreamWriter sww = new StreamWriter(@exepath + "/data/用戶帳密測試.txt", true);
                                    sww.WriteLine(enb + anb + passnb + apnb + email + account + password + appid); //寫入使用者註冊時的資料
                                    sww.Close(); //  將/data/用戶帳密測試.txt 關閉
                                }
                                if (accountnb > 19)
                                {
                                    Console.WriteLine("請輸入小於字數20的帳號!!");
                                    goto pleaseuser;
                                }



                                System.Net.Mail.SmtpClient MySmtp = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);
                                MySmtp.Credentials = new System.Net.NetworkCredential("zxcv88928@gmail.com", "as4121120955065969"); //這邊要填
                                MySmtp.EnableSsl = true;
                                string recipient = "<" + email + ">"; //寄註冊信
                                MySmtp.Send("南小鳥排程發文機器人 開發者<zcvv88928@gmail.com>", "收件者" + recipient, "" + account + "您好,感謝您的註冊，請閱讀以下內容", "尊敬的" + account + "您好,您已經在本程式註冊 請注意以下事項\r\n1.本程式並不會保留您的發文密鑰以及發文ID\r\n2.本程式註冊時要求提供的應用程式ID資料僅會保留在您的主機上(詳細內容請參閱本程式資料夾內的\"說明.txt\"\r\n3.本程式目前僅於測試階段,若有相關錯誤 歡迎回報給開發者 email : zxcv88928@gmail.com\r\n \r\n南小鳥排程發文機器人 開發者 敬上");
                                Console.Write("註冊完成 請重新開啟程式!!");
                                string yeee = Console.ReadLine();
                            }
                        }
                        else
                        {
                            goto start; //返回起始標籤
                        }
                    }
                    else
                    {
                        goto registered; //跳回註冊標籤
                    }
                }
            }
            //以下會開始核對 帳密是否正確
            Console.Write("請輸入密碼:");
            string passwd = Console.ReadLine();

            System.IO.StreamReader ete = new System.IO.StreamReader(@exepath + "/data/用戶帳密測試.txt", Encoding.Default);
            int aa = 0;
            int bb = 0;
            //string appidd = Console.ReadLine();
            while (aa == bb)
            {

                string Eerxt = ete.ReadLine();
                string emails = Eerxt.Substring(0, 2);
                string users = Eerxt.Substring(2, 2);
                string pass = Eerxt.Substring(4, 2);
                string appid = Eerxt.Substring(6, 2);

                int APD = int.Parse(appid);
                int EMAILS = int.Parse(emails);
                int USERS = int.Parse(users);
                int PASS = int.Parse(pass);

                string USER = Eerxt.Substring(8 + EMAILS, USERS);
                string PAS = Eerxt.Substring(8 + EMAILS + USERS, PASS);
                string appidd = Eerxt.Substring(8 + EMAILS + USERS + PASS, APD);

                if (USER == user && PAS == passwd)
                {
                    Setting:
                    Console.WriteLine("是否進入編輯文章內容設定??(y/n)");
                    string Echeck = Console.ReadLine();
                    if (Echeck == "y" || Echeck == "Y")
                    {
                        newmessage: //排程標籤
                        Console.Write("請輸入發文時間 \"年/月/日/時/分\"(如是個位數請自行補0):");
                        string time = Console.ReadLine();
                        // 擷取輸入時間的字串
                        string year = time.Substring(0, 4);
                        string month = time.Substring(5, 2);
                        string day = time.Substring(8, 2);
                        string hour = time.Substring(11, 2);
                        string min = time.Substring(14, 2);

                        Console.Write("請輸入發文內容(換行請用\"&&\"):");
                        string message = Console.ReadLine();
                        int Message = message.Length; //取得發文內容的字元數
                        string msgg = Message.ToString(); //將取得的字元數值轉變成字串
                        string mes = message.Replace("&&", "\r\n");
                        Console.Write("請確認內容有無須修改\r\n發文時間:" + year + "年 " + month + "月 " + day + "日 " + hour + "點 " + min + "分 \r\n 發文內容:\r\n" + mes + "\r\n");
                        check:
                        Console.Write("確認內容無誤?(y/n):");
                        string check01 = Console.ReadLine();

                        if (check01 == "y" || check01 == "Y")
                        {
                            if (Message < 100)
                            {
                                string Exepath = System.Environment.CurrentDirectory;
                                StreamWriter msg = new StreamWriter(@Exepath + "/data/message.txt", true, Encoding.GetEncoding(950));
                                //StreamWriter msg = new StreamWriter(@exepath + "/data/message.txt", true);
                                msg.WriteLine(year + month + day + hour + min + "16" + msgg + message);
                                Console.WriteLine("已儲存為排定文章!!");
                                msg.Close();
                            }
                            if (Message >= 100 && Message < 1000)
                            {
                                string Exepath = System.Environment.CurrentDirectory;
                                StreamWriter msg = new StreamWriter(@Exepath + "/data/messageh.txt", true, Encoding.GetEncoding(950));
                                //StreamWriter msg = new StreamWriter(@exepath + "/data/message.txt", true);
                                msg.WriteLine(year + month + day + hour + min + "17" + msgg + message);
                                Console.WriteLine("已儲存為排定文章!!");
                                msg.Close();
                            }
                            if (Message >= 1000 && Message < 10000)
                            {
                                string Exepath = System.Environment.CurrentDirectory;
                                StreamWriter msg = new StreamWriter(@Exepath + "/data/messaget.txt", true, Encoding.GetEncoding(950));
                                //StreamWriter msg = new StreamWriter(@exepath + "/data/message.txt", true);
                                msg.WriteLine(year + month + day + hour + min + "18" + msgg + message);
                                Console.WriteLine("已儲存為排定文章!!");
                                msg.Close();
                            }
                            /////////////////////////////////////////////////////////////////////////
                            check01:
                            Console.Write("是否繼續新增發文排程?(y/n):");
                            Echeck = Console.ReadLine();
                            if (Echeck == "y" || Echeck == "Y")
                            {
                                goto newmessage; //返回排程設定
                            }
                            else
                            {
                                if (Echeck != "n" && Echeck != "N" && Echeck != "y" && Echeck != "Y")
                                {
                                    Console.WriteLine("請輸入(y/n)進行選擇!!!!");
                                    goto check01;
                                }
                            }
                        }
                        else
                        {
                            if (check01 == "n" || check01 == "N")
                            {
                                goto newmessage;
                            }
                            else
                            {
                                Console.WriteLine("請輸入(y/n)來進行選擇!!");
                                goto check;
                            }
                        }
                    }
                    if (Echeck == "N" || Echeck == "n")
                    {
                        string AppSecret = "f66f9b6609047c8186a7c52c22c1fc2b";
                        string AppID = appidd;
                        string UserId = "776677685837902";
                        WebClient wc = new WebClient(); //每次重新取一次token
                        WebClient wd = new WebClient();
                        string result = wc.DownloadString("https://graph.facebook.com/oauth/access_token?client_id=" + AppID + "&client_secret=" + AppSecret + "&grant_type=client_credentials");//取得發文授權碼
                        string access_token = result.Substring(17, 43); //取得token字串 取17~43的字串
                                                                        //string access_token = "EAAC56nOhJREBAJ8ZB3DU04jvgClZBhs8l0aKBWlT5llQMEDabRHzZCmntR3uOJZC4DoIEx1rZAZCHN749haVlgTGxT3OXmhc4eRm1jhbGsARZA0HsKe2PRodZCdySvg4cMgf1G8F9rgKBRZBIjH2eXlhWCZB9Ev71zvOA18OFWtC3ViAZDZD";
                        Facebook.FacebookClient client = new FacebookClient(access_token);

                        System.IO.StreamReader text = new System.IO.StreamReader(@exepath + "/data/message.txt", Encoding.Default);

                        string line;
                        line = text.ReadLine();
                        int vga = 0;
                        int hdmi = 0;
                        int i = 1;
                        int o = 1;
                        // while ((line = text.ReadLine()) != null)
                        // {
                        //line = text.ReadLine();
                        Timer timer = new Timer();

                        timer.Enabled = true;

                        timer.Interval = 1000;

                        timer.Start();

                        timer.Elapsed += new ElapsedEventHandler(test);

                        Console.ReadKey();

                        void test(object source, ElapsedEventArgs e)
                        {
                            if (i == o) //計時器的迴圈 會使讀取txt一直重複執行 所以加個if來控制
                            {
                                line = text.ReadLine();
                                //Console.WriteLine(line + "\r\n");
                                vga++;   //當讀取一次資料 要顯示讀到的資料 但是計時器會使他 重複顯示 所以 這邊要放個變數 讓下面的顯示 會有所控制
                                i++;     //i++後 此if條件不成立 就會停止執行 直到 o++             
                            }

                            string year = line.Substring(0, 4);
                            string month = line.Substring(4, 2);
                            string day = line.Substring(6, 2);
                            string hour = line.Substring(8, 2);
                            string min = line.Substring(10, 2);
                            string a = line.Substring(12, 2); //文章訊息開頭
                            string b = line.Substring(14, 2); //文章訊息結尾
                            string timerid = line.Substring(0, 16); //排程ID的讀值

                            int mstart = int.Parse(a);
                            int mend = int.Parse(b);
                            int Year = int.Parse(year);
                            int Month = int.Parse(month);
                            int Day = int.Parse(day);
                            int Hour = int.Parse(hour);
                            int Min = int.Parse(min);

                            string message = line.Substring(mstart, mend);
                            string Message = message.Replace("&&", "\r\n"); //將取到的值當中的&&替換成\r\n換行
                                                                            // Console.Write(access_token); //驗證擷取的token字串是不是正確的
                            if (vga != hdmi) //起始值一樣 所以一開始條件不成立 直到讀到資料後 vga++ 條件成立 就會顯示並hdmi++再次讓條件不成立
                            {
                                Console.Write("\r\n");
                                Console.Write("排程ID:" + timerid + "\r\n");
                                Console.Write("發文時間:" + Year + "年" + Month + "月" + Day + "日" + Hour + "點" + Min + "分\r\n");
                                Console.Write("發文內容:" + Message + "\r\n");
                                hdmi++;
                                Console.ReadKey();
                            }
                            //下面的if是在做資料過濾的動作 只要讀到的資料 超過時間 就會直接跳下一筆資料
                            if (DateTime.Now.Year > Year || (DateTime.Now.Year == Year && DateTime.Now.Month > Month) || (DateTime.Now.Year == Year && DateTime.Now.Month == Month && DateTime.Now.Day > Day) || (DateTime.Now.Year == Year && DateTime.Now.Month == Month && DateTime.Now.Day == Day && DateTime.Now.Hour > Hour) || (DateTime.Now.Year == Year && DateTime.Now.Month == Month && DateTime.Now.Day == Day && DateTime.Now.Hour == Hour && DateTime.Now.Minute > Min))
                            {
                                Console.Write("此篇過期 將讀取下一篇排程文章\r\n");
                                Console.Write("\r\n");
                                o++;
                            }
                            else
                            {
                                //0
                                DateTime MyEndDate = new DateTime(Year, Month, Day, Hour, Min, 10);//年,月,日,時,分,秒
                                DateTime MyStartDate = DateTime.Now;
                                TimeSpan MySpan = MyEndDate.Subtract(MyStartDate);
                                string diffDay = Convert.ToString(MySpan.Days);
                                string diffHour = Convert.ToString(MySpan.Hours);
                                string diffMin = Convert.ToString(MySpan.Minutes);
                                string diffSec = Convert.ToString(MySpan.Seconds);
                                int Sec = int.Parse(diffSec);
                                int Minu = int.Parse(diffMin);
                                int Hou = int.Parse(diffHour);
                                int Da = int.Parse(diffDay);

                                if (Da >= 10 && Hou >= 10 && Minu >= 10 && Sec >= 10)
                                {
                                    String MyInfo = "距離發送還有 " + diffDay + " 天 " + diffHour + " 時 " + diffMin + " 分 " + diffSec + " 秒";
                                    Console.Write(MyInfo + "\r");
                                }
                                if (Da < 10 && Hou >= 10 && Minu >= 10 && Sec >= 10)
                                {
                                    String MyInfo = "距離發送還有 0" + diffDay + " 天 " + diffHour + " 時 " + diffMin + " 分 " + diffSec + " 秒";
                                    Console.Write(MyInfo + "\r");
                                }
                                if (Da >= 10 && Hou < 10 && Minu >= 10 && Sec >= 10)
                                {
                                    String MyInfo = "距離發送還有 " + diffDay + " 天 0" + diffHour + " 時 " + diffMin + " 分 " + diffSec + " 秒";
                                    Console.Write(MyInfo + "\r");
                                }
                                if (Da < 10 && Hou < 10 && Minu >= 10 && Sec >= 10)
                                {
                                    String MyInfo = "距離發送還有 0" + diffDay + " 天 0" + diffHour + " 時 " + diffMin + " 分 " + diffSec + " 秒";
                                    Console.Write(MyInfo + "\r");
                                }
                                if (Da >= 10 && Hou >= 10 && Minu < 10 && Sec >= 10)
                                {
                                    String MyInfo = "距離發送還有 " + diffDay + " 天 " + diffHour + " 時 0" + diffMin + " 分 " + diffSec + " 秒";
                                    Console.Write(MyInfo + "\r");
                                }
                                if (Da < 10 && Hou >= 10 && Minu < 10 && Sec >= 10)
                                {
                                    String MyInfo = "距離發送還有 0" + diffDay + " 天 " + diffHour + " 時 0" + diffMin + " 分 " + diffSec + " 秒";
                                    Console.Write(MyInfo + "\r");
                                }
                                if (Da >= 10 && Hou < 10 && Minu < 10 && Sec >= 10)
                                {
                                    String MyInfo = "距離發送還有 " + diffDay + " 天 0" + diffHour + " 時 0" + diffMin + " 分 " + diffSec + " 秒";
                                    Console.Write(MyInfo + "\r");
                                }
                                if (Da < 10 && Hou < 10 && Minu < 10 && Sec >= 10)
                                {
                                    String MyInfo = "距離發送還有 0" + diffDay + " 天 0" + diffHour + " 時 0" + diffMin + " 分 " + diffSec + " 秒";
                                    Console.Write(MyInfo + "\r");
                                }
                                if (Da >= 10 && Hou >= 10 && Minu >= 10 && Sec < 10)
                                {
                                    String MyInfo = "距離發送還有 " + diffDay + " 天 " + diffHour + " 時 " + diffMin + " 分 0" + diffSec + " 秒";
                                    Console.Write(MyInfo + "\r");
                                }
                                if (Da < 10 && Hou >= 10 && Minu >= 10 && Sec < 10)
                                {
                                    String MyInfo = "距離發送還有 0" + diffDay + " 天 " + diffHour + " 時 " + diffMin + " 分 0" + diffSec + " 秒";
                                    Console.Write(MyInfo + "\r");
                                }
                                if (Da >= 10 && Hou < 10 && Minu >= 10 && Sec < 10)
                                {
                                    String MyInfo = "距離發送還有 " + diffDay + " 天 0" + diffHour + " 時 " + diffMin + " 分 0" + diffSec + " 秒";
                                    Console.Write(MyInfo + "\r");
                                }
                                if (Da < 10 && Hou < 10 && Minu >= 10 && Sec < 10)
                                {
                                    String MyInfo = "距離發送還有 0" + diffDay + " 天 0" + diffHour + " 時 " + diffMin + " 分 0" + diffSec + " 秒";
                                    Console.Write(MyInfo + "\r");
                                }
                                if (Da >= 10 && Hou >= 10 && Minu < 10 && Sec < 10)
                                {
                                    String MyInfo = "距離發送還有 " + diffDay + " 天 " + diffHour + " 時 0" + diffMin + " 分 0" + diffSec + " 秒";
                                    Console.Write(MyInfo + "\r");
                                }
                                if (Da < 10 && Hou >= 10 && Minu < 10 && Sec < 10)
                                {
                                    String MyInfo = "距離發送還有 0" + diffDay + " 天 " + diffHour + " 時 0" + diffMin + " 分 0" + diffSec + " 秒";
                                    Console.Write(MyInfo + "\r");
                                }
                                if (Da >= 10 && Hou < 10 && Minu < 10 && Sec < 10)
                                {
                                    String MyInfo = "距離發送還有 " + diffDay + " 天 0" + diffHour + " 時 0" + diffMin + " 分 0" + diffSec + " 秒";
                                    Console.Write(MyInfo + "\r");
                                }
                                if (Da < 10 && Hou < 10 && Minu < 10 && Sec < 10)
                                {
                                    String MyInfo = "距離發送還有 0" + diffDay + " 天 0" + diffHour + " 時 0" + diffMin + " 分 0" + diffSec + " 秒";
                                    Console.Write(MyInfo + "\r");
                                }

                                if (DateTime.Now.Year == Year && DateTime.Now.Month == Month && DateTime.Now.Day == Day && DateTime.Now.Hour == Hour && DateTime.Now.Minute == Min && DateTime.Now.Second == 10)
                                {
                                    client.Post(UserId + "/feed", new { message = Message });//發出訊息
                                    Console.Write("\r\n");
                                    Console.Write("發文成功!!! 將讀取下一個排程ID\r\n");
                                    Console.Write("\r\n");
                                    o++;
                                }
                            }
                        }

                        text.Close();
                        Console.ReadLine();
                        //}
                        string appidnb = Eerxt.Substring(6, 2);
                        int APPID = int.Parse(appidnb);
                        appidd = Eerxt.Substring(8 + EMAILS + USERS + PASS, APPID);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("請輸入y或n 進行選擇!!! ");
                        goto Setting; //輸入y n以外的字元會重新詢問一次
                    }

                    //
                }
                else //亂數密碼的資料庫
                {
                    System.IO.StreamReader ttxt = new System.IO.StreamReader(@exepath + "/data/userpw.txt", Encoding.Default);
                    while (aa == bb)
                    {
                        string erxt = ttxt.ReadLine();
                        if (erxt == null) //讀到無值 
                        {
                            Console.Write("帳密輸入有誤!!!\r\n");
                            goto start;
                        }
                        string email = erxt.Substring(0, 2);
                        string User = erxt.Substring(2, 2);
                        string Pass = erxt.Substring(4, 2);
                        string Appid = erxt.Substring(6, 2);

                        int Apd = int.Parse(Appid);
                        int EMAIL = int.Parse(email);
                        int USERSS = int.Parse(User);
                        int PASSS = int.Parse(Pass);

                        string USERR = erxt.Substring(8 + EMAIL, USERSS);
                        string PASSSS = erxt.Substring(8 + EMAIL + USERSS, PASSS);
                        string appiddd = erxt.Substring(8 + EMAIL + USERSS + PASSS, Apd);

                        if (USERR == user && PASSSS == passwd)
                        {
                            Console.Write("是否進入編輯文章內容設定? (y/n)");
                            string Echeck = Console.ReadLine();
                            if (Echeck == "y" || Echeck == "Y")
                            {
                                newmessage: //排程標籤
                                Console.Write("請輸入發文時間 \"年/月/日/時/分\"(如是個位數請自行補0):");
                                string time = Console.ReadLine();
                                // 擷取輸入時間的字串
                                string year = time.Substring(0, 4);
                                string month = time.Substring(5, 2);
                                string day = time.Substring(8, 2);
                                string hour = time.Substring(11, 2);
                                string min = time.Substring(14, 2);

                                Console.Write("請輸入發文內容(換行請用\"&&\"):");
                                string message = Console.ReadLine();
                                int Message = message.Length; //取得發文內容的字元數
                                string msgg = Message.ToString();
                                string mes = message.Replace("&&", "\r\n");
                                Console.Write("請確認內容有無須修改\r\n發文時間:" + year + "年 " + month + "月 " + day + "日 " + hour + "點 " + min + "分 \r\n 發文內容:\r\n" + mes);
                                check:
                                Console.Write("確認內容無誤?(y/n):");
                                string check01 = Console.ReadLine();

                                if (check01 == "y" || check01 == "Y")
                                {
                                    StreamWriter msg = new StreamWriter(@exepath + "/data/message.txt", true);
                                    msg.WriteLine(year + month + day + hour + min + "16" + msgg + message);
                                    Console.WriteLine("已儲存為排定文章!!");
                                    check01:
                                    Console.Write("是否繼續新增發文排程?(y/n):");
                                    string newmes = Console.ReadLine();
                                    if (newmes == "y" && newmes == "Y")
                                    {
                                        goto newmessage; //返回排程設定
                                    }
                                    else if (newmes != "n" && newmes != "N" && newmes != "y" && newmes != "Y")
                                    {
                                        Console.WriteLine("請輸入(y/n)進行選擇!!!");
                                        goto check01;
                                    }
                                }
                                else
                                {
                                    if (check01 == "n" || check01 == "N")
                                    {
                                        goto newmessage;
                                    }
                                    else
                                    {
                                        Console.WriteLine("請輸入(y/n)來進行選擇!!!");
                                        goto check;
                                    }
                                }
                            }
                            if (Echeck == "N" || Echeck == "n")
                            {
                                Console.Write("請輸入發文密鑰(注意 由於本軟體不會存取發文密鑰 所以無法判斷密鑰是否正確!!):");
                                string AppSecret = Console.ReadLine();
                                Console.Write("請輸入發文ID(注意 由於本軟體不會存取發文ID 所以無法判斷ID是否正確!!)");
                                string UserId = Console.ReadLine();

                                string AppID = appiddd;
                                WebClient wc = new WebClient(); //每次重新取一次token
                                WebClient wd = new WebClient();
                                string result = wc.DownloadString("https://graph.facebook.com/oauth/access_token?client_id=" + AppID + "&client_secret=" + AppSecret + "&grant_type=client_credentials");//取得發文授權碼
                                string access_token = result.Substring(17, 43); //取得token字串 取17~43的字串                                                                          
                                Facebook.FacebookClient client = new FacebookClient(access_token);
                                System.IO.StreamReader text = new System.IO.StreamReader(@exepath + "/data/message.txt", Encoding.Default);

                                string line;
                                line = text.ReadLine();

                                int vga = 0;
                                int hdmi = 0;
                                int i = 1;
                                int o = 1;

                                // // // // // //
                                // 計時器~~~~~ //
                                // // // // // //

                                Timer timer = new Timer();

                                timer.Enabled = true;

                                timer.Interval = 1000;

                                timer.Start();

                                timer.Elapsed += new ElapsedEventHandler(test);

                                Console.ReadKey();

                                void test(object source, ElapsedEventArgs e)
                                {
                                    if (i == o) //計時器的迴圈 會使讀取txt一直重複執行 所以加個if來控制
                                    {
                                        line = text.ReadLine();
                                        //Console.WriteLine(line + "\r\n");
                                        vga++;   //當讀取一次資料 要顯示讀到的資料 但是計時器會使他 重複顯示 所以 這邊要放個變數 讓下面的顯示 會有所控制
                                        i++;     //i++後 此if條件不成立 就會停止執行 直到 o++             
                                    }

                                    string year = line.Substring(0, 4);
                                    string month = line.Substring(4, 2);
                                    string day = line.Substring(6, 2);
                                    string hour = line.Substring(8, 2);
                                    string min = line.Substring(10, 2);
                                    string a = line.Substring(12, 2); //文章訊息開頭
                                    string b = line.Substring(14, 2); //文章訊息結尾
                                    string timerid = line.Substring(0, 16); //排程ID的讀值

                                    int mstart = int.Parse(a);
                                    int mend = int.Parse(b);
                                    int Year = int.Parse(year);
                                    int Month = int.Parse(month);
                                    int Day = int.Parse(day);
                                    int Hour = int.Parse(hour);
                                    int Min = int.Parse(min);

                                    string message = line.Substring(mstart, mend);
                                    string Message = message.Replace("&&", "\r\n"); //將取到的值當中的&&替換成\r\n換行
                                                                                    // Console.Write(access_token); //驗證擷取的token字串是不是正確的
                                    if (vga != hdmi) //起始值一樣 所以一開始條件不成立 直到讀到資料後 vga++ 條件成立 就會顯示並hdmi++再次讓條件不成立
                                    {
                                        Console.Write("\r\n");
                                        Console.Write("排程ID:" + timerid + "\r\n");
                                        Console.Write("發文時間:" + Year + "年" + Month + "月" + Day + "日" + Hour + "點" + Min + "分\r\n");
                                        Console.Write("發文內容:" + Message + "\r\n");
                                        hdmi++;
                                        Console.ReadKey();
                                    }
                                    //下面的if是在做資料過濾的動作 只要讀到的資料 超過時間 就會直接跳下一筆資料
                                    if (DateTime.Now.Year > Year || (DateTime.Now.Year == Year && DateTime.Now.Month > Month) || (DateTime.Now.Year == Year && DateTime.Now.Month == Month && DateTime.Now.Day > Day) || (DateTime.Now.Year == Year && DateTime.Now.Month == Month && DateTime.Now.Day == Day && DateTime.Now.Hour > Hour) || (DateTime.Now.Year == Year && DateTime.Now.Month == Month && DateTime.Now.Day == Day && DateTime.Now.Hour == Hour && DateTime.Now.Minute > Min))
                                    {
                                        Console.Write("此篇過期 將讀取下一篇排程文章\r\n");
                                        Console.Write("\r\n");
                                        o++;
                                    }
                                    else
                                    {
                                        DateTime MyEndDate = new DateTime(Year, Month, Day, Hour, Min, 10);//年,月,日,時,分,秒
                                        DateTime MyStartDate = DateTime.Now;
                                        TimeSpan MySpan = MyEndDate.Subtract(MyStartDate);
                                        string diffDay = Convert.ToString(MySpan.Days);
                                        string diffHour = Convert.ToString(MySpan.Hours);
                                        string diffMin = Convert.ToString(MySpan.Minutes);
                                        string diffSec = Convert.ToString(MySpan.Seconds);
                                        int Sec = int.Parse(diffSec);
                                        int Minu = int.Parse(diffMin);
                                        int Hou = int.Parse(diffHour);
                                        int Da = int.Parse(diffDay);

                                        if (Da >= 10 && Hou >= 10 && Minu >= 10 && Sec >= 10) //各種可能的個位數組合 自動補0
                                        {
                                            String MyInfo = "距離發送還有 " + diffDay + " 天 " + diffHour + " 時 " + diffMin + " 分 " + diffSec + " 秒";
                                            Console.Write(MyInfo + "\r");
                                        }
                                        if (Da < 10 && Hou >= 10 && Minu >= 10 && Sec >= 10)
                                        {
                                            String MyInfo = "距離發送還有 0" + diffDay + " 天 " + diffHour + " 時 " + diffMin + " 分 " + diffSec + " 秒";
                                            Console.Write(MyInfo + "\r");
                                        }
                                        if (Da >= 10 && Hou < 10 && Minu >= 10 && Sec >= 10)
                                        {
                                            String MyInfo = "距離發送還有 " + diffDay + " 天 0" + diffHour + " 時 " + diffMin + " 分 " + diffSec + " 秒";
                                            Console.Write(MyInfo + "\r");
                                        }
                                        if (Da < 10 && Hou < 10 && Minu >= 10 && Sec >= 10)
                                        {
                                            String MyInfo = "距離發送還有 0" + diffDay + " 天 0" + diffHour + " 時 " + diffMin + " 分 " + diffSec + " 秒";
                                            Console.Write(MyInfo + "\r");
                                        }
                                        if (Da >= 10 && Hou >= 10 && Minu < 10 && Sec >= 10)
                                        {
                                            String MyInfo = "距離發送還有 " + diffDay + " 天 " + diffHour + " 時 0" + diffMin + " 分 " + diffSec + " 秒";
                                            Console.Write(MyInfo + "\r");
                                        }
                                        if (Da < 10 && Hou >= 10 && Minu < 10 && Sec >= 10)
                                        {
                                            String MyInfo = "距離發送還有 0" + diffDay + " 天 " + diffHour + " 時 0" + diffMin + " 分 " + diffSec + " 秒";
                                            Console.Write(MyInfo + "\r");
                                        }
                                        if (Da >= 10 && Hou < 10 && Minu < 10 && Sec >= 10)
                                        {
                                            String MyInfo = "距離發送還有 " + diffDay + " 天 0" + diffHour + " 時 0" + diffMin + " 分 " + diffSec + " 秒";
                                            Console.Write(MyInfo + "\r");
                                        }
                                        if (Da < 10 && Hou < 10 && Minu < 10 && Sec >= 10)
                                        {
                                            String MyInfo = "距離發送還有 0" + diffDay + " 天 0" + diffHour + " 時 0" + diffMin + " 分 " + diffSec + " 秒";
                                            Console.Write(MyInfo + "\r");
                                        }
                                        if (Da >= 10 && Hou >= 10 && Minu >= 10 && Sec < 10)
                                        {
                                            String MyInfo = "距離發送還有 " + diffDay + " 天 " + diffHour + " 時 " + diffMin + " 分 0" + diffSec + " 秒";
                                            Console.Write(MyInfo + "\r");
                                        }
                                        if (Da < 10 && Hou >= 10 && Minu >= 10 && Sec < 10)
                                        {
                                            String MyInfo = "距離發送還有 0" + diffDay + " 天 " + diffHour + " 時 " + diffMin + " 分 0" + diffSec + " 秒";
                                            Console.Write(MyInfo + "\r");
                                        }
                                        if (Da >= 10 && Hou < 10 && Minu >= 10 && Sec < 10)
                                        {
                                            String MyInfo = "距離發送還有 " + diffDay + " 天 0" + diffHour + " 時 " + diffMin + " 分 0" + diffSec + " 秒";
                                            Console.Write(MyInfo + "\r");
                                        }
                                        if (Da < 10 && Hou < 10 && Minu >= 10 && Sec < 10)
                                        {
                                            String MyInfo = "距離發送還有 0" + diffDay + " 天 0" + diffHour + " 時 " + diffMin + " 分 0" + diffSec + " 秒";
                                            Console.Write(MyInfo + "\r");
                                        }
                                        if (Da >= 10 && Hou >= 10 && Minu < 10 && Sec < 10)
                                        {
                                            String MyInfo = "距離發送還有 " + diffDay + " 天 " + diffHour + " 時 0" + diffMin + " 分 0" + diffSec + " 秒";
                                            Console.Write(MyInfo + "\r");
                                        }
                                        if (Da < 10 && Hou >= 10 && Minu < 10 && Sec < 10)
                                        {
                                            String MyInfo = "距離發送還有 0" + diffDay + " 天 " + diffHour + " 時 0" + diffMin + " 分 0" + diffSec + " 秒";
                                            Console.Write(MyInfo + "\r");
                                        }
                                        if (Da >= 10 && Hou < 10 && Minu < 10 && Sec < 10)
                                        {
                                            String MyInfo = "距離發送還有 " + diffDay + " 天 0" + diffHour + " 時 0" + diffMin + " 分 0" + diffSec + " 秒";
                                            Console.Write(MyInfo + "\r");
                                        }
                                        if (Da < 10 && Hou < 10 && Minu < 10 && Sec < 10)
                                        {
                                            String MyInfo = "距離發送還有 0" + diffDay + " 天 0" + diffHour + " 時 0" + diffMin + " 分 0" + diffSec + " 秒";
                                            Console.Write(MyInfo + "\r");
                                        }
                                        if (DateTime.Now.Year == Year && DateTime.Now.Month == Month && DateTime.Now.Day == Day && DateTime.Now.Hour == Hour && DateTime.Now.Minute == Min && DateTime.Now.Second == 10)
                                        {
                                            client.Post(UserId + "/feed", new { message = Message });//發出訊息
                                            Console.Write("\r\n");
                                            Console.Write("發文成功!!! 將讀取下一個排程ID\r\n");
                                            Console.Write("\r\n");
                                            o++;
                                        }
                                    }
                                }

                                text.Close();
                                Console.ReadLine();
                                //}
                                string appidnb = Eerxt.Substring(6, 2);
                                int APPID = int.Parse(appidnb);
                                appiddd = Eerxt.Substring(8 + EMAILS + USERS + PASS, APPID);
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}