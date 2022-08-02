using System.Collections.Generic;

namespace MVC_NotePad.Models.NoticeMng
{
    /// <summary>
    /// 게시판 저장소 인터페이스
    /// </summary>
    public interface INoticeRepository
    {
        /// <summary>
        /// 게시판 저장하기
        /// </summary>
        /// <param name="notice">게시판</param>
        /// <param name="formType">폼 타입</param>
        /// <returns>처리 레코드 수</returns>
        int SaveNotice(NoticeModel notice, BoardWriteFormType formType);

        /// <summary>
        /// 게시판 쓰기
        /// </summary>
        /// <param name="notice"></param>
        void WriteNotice(NoticeModel notice);
    }
}
