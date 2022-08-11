using System.Collections.Generic;

namespace MVC_NotePad.Models
{
    /// <summary>
    /// 게시판 댓글 모델
    /// </summary>
    public class NoticeCommentModel
    {
        /// <summary>
        /// 게시판 ID
        /// </summary>
        public int NoticeID { get; set; }

        /// <summary>
        /// 댓글 리스트
        /// </summary>
        public List<CommentModel> CommentList { get; set; }
    }
}
