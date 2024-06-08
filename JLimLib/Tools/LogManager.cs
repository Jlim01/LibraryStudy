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
        public LogManager(string path, LogType logType, string prefix, string postfix)
        {
            _path = path;
            _SetLogPath(logType, prefix, postfix);
        }

        public LogManager(string prefix, string postfix)
            : this(Path.Combine(Application.Root, "Log"), LogType.Daily, prefix, postfix) // prefix와 postfix만 받아들이는 생성자.
        {

        }

        public LogManager() : this(Path.Combine(Application.Root, "Log"), LogType.Daily, null, null)
        { }

        #endregion

        #region Methods
        private void _SetLogPath(LogType logType, string prefix, string postfix)
        {
            string path = String.Empty; // 로그파일이 저장될 루트경로의 중간 경로.
            string name = String.Empty;

            switch (logType)
            {
                case LogType.Daily:
                    path = String.Format(@"{0}\{1}\", DateTime.Now.Year, DateTime.Now.ToString("MM"));
                    name = DateTime.Now.ToString("yyyyMMdd");
                    break;
                case LogType.Monthly:
                    path = String.Format(@"{0}\", DateTime.Now.Year);
                    name = DateTime.Now.ToString("yyyyMM");
                    break;
            }

            _path = Path.Combine(_path, path); // 컴바인 기능 확인.

            if (!Directory.Exists(_path))
                Directory.CreateDirectory(_path);

            //하나의 폴더 안에있는 log파일에서 여러 파일들의 종료에 따라 구분하기 위해 postfix 또는 prefix를 붙인다.
            if(!String.IsNullOrEmpty(prefix))
                name = prefix + name;
            if (!String.IsNullOrEmpty(postfix))
                name = name + postfix;
            name += ".txt";

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
