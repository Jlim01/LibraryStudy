using JLimLib.Extensions;
using JLimLib.Tools;
using Microsoft.Extensions.Configuration;

internal class Program
{


    private static void Main(string[] args)
    {


        string contents = "Hello there, <br />This is Derek";
        //EmailManager.Send("receiver@test.com", "Hi...", contents);
        //EmailManager.Send("from@test.com", "receiver@test.com", "Hi...", contents);
        //EmailManager.Send("from@test.com", "receiver@test.com", "Hi...", contents, "cc@test.com", "bcc@test.com");

        //EmailManager 객체로 서버 설정.
        //장점: EmailManager를 사용하는 유저 입장에서 MailMessage, SmtpClient, NetworkCredential등 사용할 필요없이 간소하게 사용할 수 있음.
        //EmailManager 안에 MailMessage와 SmtpClient 로직이 구성됨.
        EmailManager email = new EmailManager("smtp.com", 25, "id", "password");
        email.From = "sender@test.com";
        email.To.Add("receiver@test.com");
        email.Subject = "Subject";
        email.Body = contents;
        email.Send();

        //보내는 사람이 바뀌면 From을 바꿔주면 되나 받는 사람이 바뀐다면 email들어간 To를 클리어
        email.To.Clear();
        email.To.Add("receiver2@test.com");
        email.Subject = "Hi Desker";
        email.Send();


    }

    void Ex01()
    {
        LogManager logManager = new LogManager(null, "_JlimText");//prefix , postfix
        logManager.WriteLine("Begin Processing...");
        for (int idx = 0; idx < 10; ++idx)
        {
            logManager.WriteLine($"Processing ---  {idx}");
            System.Threading.Thread.Sleep(500);
            logManager.WriteLine($"Done -- {idx}");
        }
        logManager.WriteLine("End Processing");
    }

    void Ex02()
    {
        //#2  확장메서드 사용
        string temp = "2024-12-02";
        Console.WriteLine("IsNumeric? " + temp.IsNumeric());
        Console.WriteLine("IsDateTime? " + temp.IsDataTime());
    }




}
















/*
logManager.WriteConsole("test");  //마치 LogManager 클래스 내부 메서드 처럼 해당 클래스 외부에 있는 확장메서드를 사용할 수 있다.
//확장메서드는 static class 안에서 static function으로 선언되야한다.
public static class ExtensionTest
{
    public static void WriteConsole(this LogManager log, string data)  // 첫번째 인자: this키워드 붙여서 확장할 클래스나 타입이 와야함. 두번째 인자는 뭐든 상관없음.
    {
        log.Write(data);
        Console.Write(data);
    }
}
*/