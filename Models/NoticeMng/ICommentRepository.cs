using System.Collections.Generic;

namespace MVC_NotePad.Models.NoticeMng
{
    public interface ICommentRepository
    {
        /// <summary>
        /// 댓글 추가하기
        /// </summary>
        /// <param name="comment">댓글</param>
        void WriteComment(CommentModel comment);

        /// <summary>
        /// 댓글 리스트 구하기
        /// </summary>
        /// <param name="noticeID">게시판 ID</param>
        /// <returns>댓글 리스트</returns>
        List<CommentModel> GetCommentList(int noticeID);

        /// <summary>
        /// 카운트 구하기
        /// </summary>
        /// <param name="noticeID">게시판 ID</param>
        /// <param name="id">ID</param>
        /// <param name="password">패스워드</param>
        /// <returns></returns>
        int GetCount(int noticeID, int id, string password);

        /// <summary>
        /// 댓글 삭제하기
        /// </summary>
        /// <param name="noticeID">게시판 ID</param>
        /// <param name="id">ID</param>
        /// <param name="password">패스워드</param>
        /// <returns></returns>
        int DeleteComment(int noticeID, int id, string password);

        /// <summary>
        /// 최근 댓글 리스트 구하기
        /// </summary>
        /// <returns>최근 댓글 리스트 구하기</returns>
        List<CommentModel> GetRecentCommentList();
    }
}
