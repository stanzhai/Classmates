using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Classmates
{
    class Path
    {
        public static string getPath()
        {
            if (Application.StartupPath.EndsWith("\\"))
            {
                return Application.StartupPath;
            }
            else
            {
                return Application.StartupPath + "\\";
            }
        }
    }

    public enum SaveMode
    {
        /// <summary>
        /// 保存所有信息
        /// </summary>
        SaveInfo,
        /// <summary>
        /// 导出某位同学的信息
        /// </summary>
        SaveOneInfo,
        /// <summary>
        /// 导出为网页
        /// </summary>
        SaveToPage
    }
}
