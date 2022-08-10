using System;

namespace MVC_NotePad.Libs
{
    /// <summary>
    /// HTML헬퍼
    /// </summary>
    public class HTMLHelper
    {
        /// <summary>
        /// 인코딩하기
        /// </summary>
        /// <param name="source">소스 문자열</param>
        /// <returns>인코딩 문자열</returns>
        public static string EnCode(string source)
        {
            string target = string.Empty;

            if(string.IsNullOrEmpty(source))
            {
                target = string.Empty;
            }
            else
            {
                //왼쪽이 오른쪽을 치환해줌
                target = source;
                target = target.Replace("&"   , "&amp;" );  
                target = target.Replace(">"   , "&gt;"  );  
                target = target.Replace("<"   , "&lt;"  );
                target = target.Replace("\r\n", "<br />");
                target = target.Replace("\""  , "&#34;" );
            }
            return target;
        }

        /// <summary>
        /// 탭/공백 포함 인코딩하기 
        /// </summary>
        /// <param name="source">소스 문자열</param>
        /// <returns>인코딩 문자열</returns>

        public static string EncodeIncludingTabAndSpace(string source)
        {
            return EnCode(source)
                .Replace("\t"     , "&nbsp;&nbsp;&nbsp;&nbsp;") //&nbsp;는 하나당 한칸의 공백 제공해주는 특수문자이다.
                .Replace(" " + " ", "&nbsp;&nbsp;");
        }

        internal static string Encode(string mailAddress)
        {
            throw new NotImplementedException();
        }
    }
}
