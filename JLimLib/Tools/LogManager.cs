using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace JLimLib.Tools
{
    public enum LogType { Daily, Monthly }

    public class LogManager
    {
        private string _path;

        #region Constructors
        public LogManager(string path, LogType logType)
        {
            _path = path;
            _SetLogPath(logType);
        }
        public LogManager() : this(Path.Combine(Application.Root, "Log"), LogType.Daily)
        { }

        #endregion

        #region Methods
        private void _SetLogPath(LogType logType)
        {
            string path = String.Empty; // 로그파일이 저장될 루트경로의 중간 경로.
            string name = String.Empty;

            switch (logType)
            {
                case LogType.Daily:
                    path = String.Format(@"{0}\{1}\", DateTime.Now.Year, DateTime.Now.ToString("MM"));
                    name = DateTime.Now.ToString("yyyyMMdd") + ".txt";
                    break;
                case LogType.Monthly:
                    path = String.Format(@"{0}\", DateTime.Now.Year);
                    name = DateTime.Now.ToString("yyyyMM") + ".txt";
                    break;
            }

            _path = Path.Combine(_path, path); // 컴바인 기능 확인.

            if (!Directory.Exists(_path))
                Directory.CreateDirectory(_path);

            _path = Path.Combine(_path, name); // 파일 명까지 들어간 full 경로.

        }
        public void Write(string data)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(_path, true)) // 경로에 있는 파일을 열어서 append 모드로 접근.
                {
                    writer.Write(data);
                }
            }
            catch (Exception ex) { }
        }
        public void WriteLine(string data)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(_path, true)) // 경로에 있는 파일을 열어서 append 모드로 접근.
                {
                    writer.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss\t") + data);
                }
            }
            catch (Exception ex) { }

        }
        #endregion
    }
}
