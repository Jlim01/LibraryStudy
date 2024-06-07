using JLimLib.Tools;

LogManager logManager = new();
logManager.WriteLine("Begin Processing...");
for(int idx = 0; idx < 10; ++idx)
{
    logManager.WriteLine($"Processing ---  {idx}");
    System.Threading.Thread.Sleep(500);
    logManager.WriteLine($"Done -- {idx}");
}
logManager.WriteLine("End Processing");