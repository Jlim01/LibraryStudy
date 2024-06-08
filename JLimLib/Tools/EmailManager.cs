using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace JLimLib.Tools
{
    public class EmailManager
    {
        internal static IConfiguration Config { get; private set; }


        #region Static Method
        //객체없이 사용 가능하게 static function 생성                 cc 참조,  bcc 숨은참조
        public static void Send(string from, string to, string subject, string contents, string cc, string bcc)  // 보내는 사람이 매개변수로 들어오니 config로부터 sender를 읽어올 필요없음/
        {
            try
            {
                if (String.IsNullOrEmpty(from))
                    throw new ArgumentNullException("Sender is empty.");
                if (String.IsNullOrEmpty(to))
                    throw new ArgumentNullException("To is empty.");
                if (Config is null)
                {
                    Config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build();
                }
                if (Config.GetSection("SMTP") == null) return;

                //메일 보내줄 서버.
                string smtpHost = Config.GetSection("SMTP")["SMTPHost"]; // 메일을 실제로 보내주는 서버 도메인명.

                int smtpPort = 0;
                if (Config.GetSection("SMTP")["SMTPPort"] == null ||
                    int.TryParse(Config.GetSection("SMTP")["SMTPPort"], out smtpPort) == false)
                {
                    smtpPort = 25;
                }
                //host명과 port로 아무나 가서 메일을 보내면 안됨. 예) 스팸.
                //보통 메일 서버는 메일 요청을 하는 프로그램이 인증된 것인지 확인. -> id, password
                string smtpId = Config.GetSection("SMTP")["SMTPID"];
                string smtpPwd = Config.GetSection("SMTP")["SMTPPassword"];


                //메일을 보내는 시퀀스
                //MailAddress에는 보내는 사람, 받는 사람, 참조, 숨은 참조등을 설정 시 사용
                //mailMsg 객체에 메일을 보내는 모든 값들 할당.
                MailMessage mailMsg = new MailMessage();
                mailMsg.From = new MailAddress(from); //보내는 사람
                                                      //mailMsg.To.Add(new MailAddress(to));
                mailMsg.To.Add(to);

                if (!String.IsNullOrEmpty(cc))
                    mailMsg.CC.Add(cc);
                if (!String.IsNullOrEmpty(bcc))
                    mailMsg.Bcc.Add(bcc);

                mailMsg.Subject = subject;
                mailMsg.IsBodyHtml = true; // 이메일 내용이 html인지 text인지 설정하는 flag.
                mailMsg.Body = contents;
                mailMsg.Priority = MailPriority.Normal; // 메일 중요도

                //메일 송신 목적 smtp 서버에 메일 넘겨주기.
                SmtpClient smtpClient = new SmtpClient();

                //메일을 보내라고 요청하는 사람이 허락된 사람인지 나타내기 위해 id, pwd이용해 인증서를 만든다.
                smtpClient.Credentials = new NetworkCredential(smtpId, smtpPwd);
                //어느 서버에 보낼지 서버정보 할당.
                smtpClient.Host = smtpHost;
                smtpClient.Port = smtpPort;
                //서버에 메일 보내기
                smtpClient.Send(mailMsg);
            }
            catch (Exception ex)
            {

            }
        }

        public static void Send(string from, string to, string subject, string contents)
        {
            Send(from, to, subject, contents, null, null);
        }

        public static void Send(string to, string subject, string contents)  // 보내는 사람이 매개변수로 들어오니 config로부터 sender를 읽어올 필요없음/
        {
            if (Config is null)
            {
                Config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            }
            if (Config.GetSection("SMTP") == null) return;
            string sender = Config.GetSection("SMTP")["SMTPSender"];
            Send(sender, to, subject, contents);
        }
    }
    #endregion
}


#if false
public class EmailManager
{
    internal static IConfiguration Config { get; private set; }

    //객체없이 사용 가능하게 static function 생성
    public static void Send(string to, string subject, string contents)
    {
        if (Config is null)
        {
            Config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }
        if (Config.GetSection("SMTP") == null)
        {

        }
        /*xml이 아닌 json config파일로 키값 불러옴.   23:31*/
        //string sender = "do_not_reply@test.com";
        string sender = Config.GetSection("SMTP")["SMTPSender"];
        //메일 보내줄 서버.
        string smtpHost = Config.GetSection("SMTP")["SMTPHost"]; // 메일을 실제로 보내주는 서버 도메인명.

        int smtpPort = 0;
        if (Config.GetSection("SMTP")["SMTPPort"] == null ||
            int.TryParse(Config.GetSection("SMTP")["SMTPPort"], out smtpPort) == false)
        {
            smtpPort = 25;
        }
        //host명과 port로 아무나 가서 메일을 보내면 안됨. 예) 스팸.
        //보통 메일 서버는 메일 요청을 하는 프로그램이 인증된 것인지 확인. -> id, password
        string smtpId = Config.GetSection("SMTP")["SMTPID"];
        string smtpPwd = Config.GetSection("SMTP")["SMTPPassword"];


        //메일을 보내는 시퀀스
        //MailAddress에는 보내는 사람, 받는 사람, 참조, 숨은 참조등을 설정 시 사용
        //mailMsg 객체에 메일을 보내는 모든 값들 할당.
        MailMessage mailMsg = new MailMessage();
        mailMsg.From = new MailAddress(sender); //보내는 사람
        mailMsg.To.Add(new MailAddress(to));
        mailMsg.To.Add(to);

        mailMsg.Subject = subject;
        mailMsg.IsBodyHtml = true; // 이메일 내용이 html인지 text인지 설정하는 flag.
        mailMsg.Body = contents;
        mailMsg.Priority = MailPriority.Normal; // 메일 중요도

        //메일 송신 목적 smtp 서버에 메일 넘겨주기.
        SmtpClient smtpClient = new SmtpClient();

        //메일을 보내라고 요청하는 사람이 허락된 사람인지 나타내기 위해 id, pwd이용해 인증서를 만든다.
        smtpClient.Credentials = new NetworkCredential(smtpId, smtpPwd);
        //어느 서버에 보낼지 서버정보 할당.
        smtpClient.Host = smtpHost;
        smtpClient.Port = smtpPort;
        //서버에 메일 보내기
        smtpClient.Send(mailMsg);
    }
}
#endif