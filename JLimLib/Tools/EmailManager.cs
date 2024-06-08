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
        public EmailManager()
        {
            Config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }
        //객체없이 사용 가능하게 static function 생성
        public static void Send(string to, string subject, string contents)
        {
            if (Config.GetSection("SMTP") == null)
            {

            }
            /*xml이 아닌 json config파일로 키값 불러옴.   23:31*/
            //string sender = "do_not_reply@test.com";
            string sender = Config.GetSection("SMTP")["SMTPSender"];
            //메일 보내줄 서버.
            string smtpHost = Config.GetSection("SMTP")["SMTPHost"]; // 메일을 실제로 보내주는 서버 도메인명.

            int smtpPort = 0;
            if (Config.GetSection("SMTP")["SMTPPort"] == null || int.TryParse(Config.GetSection("SMTP")["SMTPPort"] , out smtpPort) == false)
            {
                 smtpPort= 25; //원래는 default port는 25.
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
}
