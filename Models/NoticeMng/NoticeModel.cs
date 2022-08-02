using System;
using System.ComponentModel.DataAnnotations;

namespace MVC_NotePad.Models.NoticeMng
{
    /// <summary>
    /// 게시글 모델
    /// </summary>
    public class NoticeModel
    {
        /// <summary>
        /// ID
        /// </summary>
        [Display(Name = "ID")]
        public int ID { get; set; }

        /// <summary>
        /// 작성자명
        /// </summary>
        [Display(Name ="작성자명")]
        [Required(ErrorMessage ="작성자명을 입력해 주시기 바랍니다.")]
        public string Name { get; set; }

        /// <summary>
        /// 메일 주소
        /// </summary>
        [EmailAddress(ErrorMessage ="메일 주소를 입력해 주시기 바랍니다.")]
        public string MailAddress { get; set; }

        /// <summary>
        /// 제목
        /// </summary>
        [Display(Name ="제목")]
        [Required(ErrorMessage ="제목을 입력해 주시기 바랍니다.")]
        public string Title { get; set; }

        /// <summary>
        /// 작성일
        /// </summary>
        [Display(Name ="작성일")]
        public DateTime WriteDate { get; set; }

        /// <summary>
        /// 작성 IP
        /// </summary>
        public string WriteIP { get; set; }

        /// <summary>
        /// 내용
        /// </summary>
        [Display(Name ="내용")]
        [Required(ErrorMessage ="내용을 입력해 주시기 바랍니다.")]
        public string Content { get; set; }

        /// <summary>
        /// 패스워드
        /// </summary>
        [Display(Name ="패스워드")]
        [Required(ErrorMessage ="패스워드를 입력해 주시기 바랍니다.")]
        public string Password { get; set; }

        /// <summary>
        /// 조회 수
        /// </summary>
        [Display(Name ="조회 수")]
        public int ReadCount { get; set; }

        /// <summary>
        /// 인코딩
        /// </summary>
        [Display(Name = "인코딩")]
        public string Encoding { get; set; } = "Text";

        /// <summary>
        /// 홈페이지
        /// </summary>
        public string Homepage { get; set; }

        /// <summary>
        /// 수정일
        /// </summary>
        public DateTime UpdateDate { get; set; }

        /// <summary>
        /// 수정 IP
        /// </summary>
        public string UpdateIP { get; set; }

        /// <summary>
        /// 파일명
        /// </summary>
        [Display(Name ="파일명")]
        public string FileName { get; set; }

        /// <summary>
        /// 파일 크기
        /// </summary>
        public int FileSize { get; set; }

        /// <summary>
        /// 다운로드 수
        /// </summary>
        public int DownloadCount { get; set; }

        /// <summary>
        /// 참조 ID
        /// </summary>
        public int ReferenceID { get; set; }

        /// <summary>
        /// 답변 레벨
        /// </summary>
        public int ReplyLevel { get; set; }

        /// <summary>
        /// 답변 수
        /// </summary>
        public int ReplyOrder { get; set; }

        /// <summary>
        /// 부모 ID
        /// </summary>
        public int ParentID { get; set; }

        /// <summary>
        /// 댓글 수
        /// </summary>
        public int CommentCount { get; set; }

        /// <summary>
        /// 카테고리
        /// </summary>
        public string Category { get; set; } = "FREE";

    }
}
