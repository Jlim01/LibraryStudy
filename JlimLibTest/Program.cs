﻿using JLimLib.Extensions;
using JLimLib.Tools;
using Microsoft.Extensions.Configuration;

internal class Program
{


    private static void Main(string[] args)
    {
        string contents = "Hello there, <br />This is Derek";
        EmailManager emg = new();
        EmailManager.Send("receiver@test.com", "Hi...", contents);

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