using System.IO;


namespace MVC_NotePad.Libs
{
    public class FileHelper
    {
        #region 파일명 구하기 - GetUniqueFileName(sourceDirectoryPath, fileName)

        /// <summary>
        /// 파일명 구하기
        /// </summary>
        /// <param name="sourceDirectoryPath">소스 디렉토리 경로</param>
        /// <param name="fileName">파일명</param>
        /// <returns>파일명</returns>
        public static string GetUniqueFileName(string sourceDirectoryPath, string fileName)
        {
            string temporaryName = Path.GetFileNameWithoutExtension(fileName);
            string fileExtension = Path.GetExtension(fileName);

            bool exist = true;

            int i = 0;

            while (exist)
            {
                //Path.Combine은 경로와 경로사이에 자동으로 \\을 추가해준다.
                if(File.Exists(Path.Combine(sourceDirectoryPath, fileName)))    //파일 찾기
                {
                    fileName = temporaryName + "(" + ++i + ")" + fileExtension;
                }
                else
                {
                    exist = false;
                }
            }
            return fileName;
        }
        #endregion
    }
}
