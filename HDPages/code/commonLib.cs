using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using HDBusiness;
namespace YDCode
{
    public class commonLib
    {
        #region TF转换

        public static bool intToTF(object gender)
        {
            if (Convert.ToInt32(gender) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string TFToint(bool gender)
        {
            if (gender)
            {
                return "1";
            }
            else
            {
                return "0";
            }
        }

        public static string FTToint(bool gender)
        {
            if (gender)
            {
                return "0";
            }
            else
            {
                return "1";
            }
        }

        public static string intToSex(object gender)
        {
            if (Convert.ToInt32(gender) == 1)
            {
                return "男";
            }
            else
            {
                return "女";
            }
        }

        #endregion

        #region 人民币大写
        public static string numberToDaXie(string money)
        {
            string s = double.Parse(money).ToString("#L#E#D#C#K#E#D#C#J#E#D#C#I#E#D#C#H#E#D#C#G#E#D#C#F#E#D#C#.0B0A");
            string d = Regex.Replace(s, @"((?<=-|^)[^1-9]*)|((?'z'0)[0A-E]*((?=[1-9])|(?'-z'(?=[F-L.]|$))))|((?'b'[F-L])(?'z'0)[0A-L]*((?=[1-9])|(?'-z'(?=[.]|$))))", "${b}${z}");
            return Regex.Replace(d, ".", delegate (Match m) { return "负圆空零壹贰叁肆伍陆柒捌玖空空空空空空空分角拾佰仟万億兆京垓秭穰"[m.Value[0] - '-'].ToString(); });
        }

        #endregion

        #region 字符串转换
        public static List<string> stringTolist(string str,char oldchar,char newchar)
        {// '\0'
            List<string> strList = new List<string>();

            str = str.Replace(oldchar, newchar);
            for (int i = 0; i < str.Length; i++)
            {
                strList.Add(str[i].ToString().Trim());
            }

            return strList;
        }

        public static List<string> stringTolist(string str, char charsplit)
        {
            List<string> strList = new List<string>();

            string[] strarr = str.Split(charsplit);
            for (int i = 0; i < strarr.Length; i++)
            {
                strList.Add(strarr[i].ToString().Trim());
            }

            return strList;
        }

        #endregion

        #region 图片格式验证

        /// <summary>
        /// 所有允许上传的图片格式
        /// </summary>
        /// 

        protected readonly static List<string> VALID_FILE_TYPES = new List<string>(new xparams().getparamData("h008").ToLower().Split(','));  //new List<string> { "jpg", "bmp", "gif", "jpeg", "png" };

        /// <summary>
        /// 验证上传图片的格式
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool ValidateImgType(string fileName)
        {
            string fileType = String.Empty;
            int lastDotIndex = fileName.LastIndexOf(".");

            if (lastDotIndex >= 0)
            {
                fileType = fileName.Substring(lastDotIndex + 1).ToLower();
            }

            if (VALID_FILE_TYPES.Contains(fileType))
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        protected readonly static List<string> VALID_CAD_TYPES = new List<string> { "dwg", "dxf", "mxg", "exb"};

        /// <summary>
        /// 验证上传图片的格式
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool ValidateCADType(string fileName)
        {
            string fileType = String.Empty;
            int lastDotIndex = fileName.LastIndexOf(".");

            if (lastDotIndex >= 0)
            {
                fileType = fileName.Substring(lastDotIndex + 1).ToLower();
            }

            if (VALID_CAD_TYPES.Contains(fileType))
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        #endregion
    }
}
