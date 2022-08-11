using MVC_NotePad.Models.NoticeMng;
using System.Collections.Generic;

namespace MVC_NotePad.Models
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
        /// <param name="notice">게시판</param>
        void WriteNotice(NoticeModel notice);

        /// <summary>
        /// 게시판 수정하기
        /// </summary>
        /// <param name="notice">게시판</param>
        /// <returns>처리 레코드 수</returns>
        int UpdateNotice(NoticeModel notice);

        /// <summary>
        /// 게시판 답변하기
        /// </summary>
        /// <param name="notice">게시판</param>
        void ReplyNotice(NoticeModel notice);

        /// <summary>
        /// 게시판 삭제하기
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="password">패스워드</param>
        /// <returns>처리 레코드 수</returns>
        int DeleteNotice(int id, string password);

        /// <summary>
        /// 카운트 구하기
        /// </summary>
        int GetCount();

        /// <summary>
        /// 카운트 구하기
        /// </summary>
        /// <param name="searchField">검색 필드</param>
        /// <param name="searchQuery">검색 쿼리</param>
        /// <returns>카운트</returns>
        int GetCount(string searchField, string searchQuery);

        /// <summary>
        /// 게시판 리스트 구하기
        /// </summary>
        /// <param name="pageIndex">페이지 인덱스</param>
        List<NoticeModel> GetNoticeList(int pageIndex);

        /// <summary>
        /// 게시판 리스트 구하기
        /// </summary>
        /// <param name="pageIndex">페이지 인덱스</param>
        /// <param name="searchField">검색 필드</param>
        /// <param name="searchQuery">검색 쿼리</param>
        /// <returns>게시판 리스트</returns>
        List<NoticeModel> GetNoticeList(int pageIndex, string searchField, string searchQuery);

        /// <summary>
        /// 게시판 보기
        /// </summary>
        NoticeModel GetNotice(int id);

        /// <summary>
        /// 파일명 구하기
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>파일명</returns>
        string GetFileName(int id);

        /// <summary>
        /// 다운로드 카운트 수정하기
        /// </summary>
        /// <param name="id">ID</param>
        void UpdateDownloadCount(int id);

        /// <summary>
        /// 다운로드 카운트 수정하기
        /// </summary>
        /// <param name="fileName">파일명</param>
        void UpdateDownloadCount(string fileName);

        /// <summary>
        /// 사진이 있는 최근 게시판 리스트 구하기
        /// </summary>
        /// <returns>사진이 있는 최근 게시판 리스트</returns>
        List<NoticeModel> GetRecentPhotoNoticeList();

        /// <summary>
        /// 특정 카테고리 최근 게시판 리스트 구하기
        /// </summary>
        /// <returns>특정 카테고리 최근 게시판 리스트</returns>
        List<NoticeModel> GetRecentCategoryNoticeList(string category);

        /// <summary>
        /// 최근 게시판 리스트 구하기
        /// </summary>
        /// <returns>최근 게시판 리스트</returns>
        List<NoticeModel> GetRecentNoticeList();

        /// <summary>
        /// 최근 게시판 리스트 구하기
        /// </summary>
        /// <param name="noticeCount">게시판 수</param>
        /// <returns>최근 게시판 리스트</returns>
        List<NoticeModel> GetRecentNoticeList(int noticeCount);

        /// <summary>
        /// 게시판 고정하기
        /// </summary>
        /// <param name="id"></param>
        /// <remarks>특정 게시글을 공지사항으로 올리기</remarks>
        void PinNotice(int id);
    }
}
