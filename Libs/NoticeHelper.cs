using System;
using System.IO;

namespace MVC_NotePad.Libs
{
    /// <summary>
    /// 게시판 헬퍼
    /// </summary>
    public class NoticeHelper
    {
        /// <summary>
        /// REPLY 이미지 HTML 구하기
        /// </summary>
        /// <param name="stepObject">단계 객체</param>
        /// <returns>REPLY 이미지 HTML</returns>
        public static string GetReplyImageHTML(object stepObject)
        {
            //지정된 32비트 부호 있는 정수로 변환
            int step = Convert.ToInt32(stepObject);

            string target = string.Empty;
            
            if(step == 0)
            {
                target = string.Empty;
            }
            else
            {
                for (int i = 0; i < step; i++)
                {
                    target = string.Format
                    (
                        "<img src=\"{0}\" height=\"{1}\" width=\"{2}\">", "/Image/NoticeBoard/blank.gif", "0", (step * 15)
                    );
                }
                target += "<img src=\"/Image/NoticeBoard/reply.gif\">";
            }
            return target;
        }

        /// <summary>
        /// 댓글 수 HTML 구하기
        /// </summary>
        /// <param name="commentCountObject">댓글 수 객체</param>
        /// <returns>댓글 수 HTML</returns>
        public static string GetCommentCountHTML(object commentCountObject)
        {
            string target = string.Empty;
            try
            {
                if(Convert.ToInt32(commentCountObject) > 0)
                {
                    target = "<img src=\"/Image/NoticeBoard/reply.gif\">";
                    target += " (" + commentCountObject.ToString() + ")";
                }
            }
            catch (Exception)
            {
                target = string.Empty;
            }
            return target;
        }

        /// <summary>
        /// 신규 이미지 HTML 구하기
        /// </summary>
        /// <param name="dateObject">날짜 객체</param>
        /// <returns>신규 이미지 HTML</returns>
        public static string GetNewImageHTML(object dateObject)
        {
            if(dateObject != null)
            {
                if(!string.IsNullOrEmpty(dateObject.ToString()))
                {
                    DateTime date = Convert.ToDateTime(dateObject);

                    TimeSpan timeSpan = DateTime.Now - date;

                    string target = string.Empty;

                    if(timeSpan.TotalMinutes <1440)
                    {
                        target = "<img src=\"/Image/NoticeBoard/reply.gif\">";
                    }
                    return target;
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// 날짜/시간 HTML 구하기
        /// </summary>
        /// <param name="dateTimeObject">날짜/시간 객체</param>
        /// <returns>날짜/시간 HTML</returns>
        public static string GetDateTimeHTML(object dateTimeObject)
        {
            if(dateTimeObject != null)
            {
                if (!string.IsNullOrEmpty(dateTimeObject.ToString()))
                {
                    string writeDateString = Convert.ToDateTime(dateTimeObject).ToString("yyyy-MM-dd");

                    if (writeDateString == DateTime.Now.ToString("yyyy-MM-dd"))
                    {
                        return Convert.ToDateTime(dateTimeObject).ToString("hh:mm:ss");
                    }
                    else
                    {
                        return writeDateString;
                    }
                }
            }

            return "-";
        }

        /// <summary>
        /// 파일 크기 문자열 구하기
        /// </summary>
        /// <param name="fileSize">파일 크기</param>
        /// <returns>파일 크기 문자열</returns>
        public static string GetFileSizeString(int fileSize)
        {
            string target = string.Empty;

            if(fileSize >= 1048576)
            {
                target = string.Format("{0:F} MB", (fileSize / 1048576));
            }
            else
            {
                if(fileSize >= 1024)
                {
                    target = string.Format("{0} KB", (fileSize / 1024));
                }
                else
                {
                    target = fileSize + " Byte(s)";
                }
            }
            return target;
        }

        /// <summary>
        /// 이미지 파일 여부 구하기
        /// </summary>
        /// <param name="filePath">파일 경로</param>
        /// <returns>이미지 파일 여부</returns>
        public static bool IsImageFile(string filePath)
        {
            //파일의 확장자를 가져올 때
            string fileExtesion = Path.GetExtension(filePath).Replace(".", "").ToLower(); //소문자 변환

            bool result = false;

            if (fileExtesion == "gif" || fileExtesion == "jpg" || fileExtesion == "jpeg" || fileExtesion == "png")
            {
                result = true;
            }
            else
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 파일 다운로드 링크 HTML 구하기
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="filePath">파일 경로</param>
        /// <param name="fileSizeString">파일 크기 문자열</param>
        /// <returns>파일 다운로드 링크HTML 구하기</returns>
        public static string GetFileDownloadLinkHTML(int id, string filePath, string fileSizeString)
        {
            return string.Empty;
        }
    }
}
