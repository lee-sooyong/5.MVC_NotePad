using System;
using System.ComponentModel.DataAnnotations;

namespace MVC_NotePad.Models.NoticeMng
{
    /// <summary>
    /// 댓글 모델
    /// </summary>
    public class CommentModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 게시판 ID
        /// </summary>
        public int NoticeID { get; set; }

        /// <summary>
        /// 작성자명
        /// </summary>
        [Required(ErrorMessage ="작성자명을 입력해 주시기 바랍니다.")]
        public string Name { get; set; }

        /// <summary>
        /// 댓글
        /// </summary>
        [Required(ErrorMessage ="댓글을 입력해 주시기 바랍니다.")]
        public string Comment { get; set; }

        /// <summary>
        /// 작성일
        /// </summary>
        public DateTime WriteDate { get; set; }

        /// <summary>
        /// 패스워드
        /// </summary>
        [Required(ErrorMessage ="패스워드를 입력해 주시기 바랍니다.")]
        public string Password { get; set; }
    }
}
