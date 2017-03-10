using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Classmates
{
    public class Message
    {
        #region 消息常量
        public const int WM_MOUSEWHEEL = 0x020A;

        #endregion
    }

    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }
    }
}
