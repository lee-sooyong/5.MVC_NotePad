using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace MVC_NotePad.Models
{
    public class NoticeRepository : INoticeRepository
    {
        /// <summary>
        /// 구성
        /// </summary>
        private IConfiguration configuration;

        /// <summary>
        /// 로그 기록기
        /// </summary>
        private ILogger<NoticeRepository> logger;

        /// <summary>
        /// 연결
        /// </summary>
        private SqlConnection connection;

        /// <summary>
        /// 생성자
        /// </summary>
        public NoticeRepository(IConfiguration config, ILogger<NoticeRepository> logger)
        {
            this.configuration = config;
            this.connection = new SqlConnection(this.configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
            this.logger = logger;
        }

        /// <summary>
        /// 게시판 저장하기
        /// </summary>
        /// <param name="notice">게시판</param>
        /// <param name="formType">폼 타입</param>
        /// <returns>처리 레코드 수</returns>
        public int SaveNotice(NoticeModel notice, BoardWriteFormType formType)
        {
            int recordCount = 0;

            DynamicParameters dynamicParameters = new DynamicParameters();

            dynamicParameters.Add("@Name", value: notice.Name, dbType: DbType.String);
            dynamicParameters.Add("@MailAddress", value: notice.MailAddress, dbType: DbType.String);
            dynamicParameters.Add("@Title", value: notice.Title, dbType: DbType.String);
            dynamicParameters.Add("@Content", value: notice.Content, dbType: DbType.String);
            dynamicParameters.Add("@Password", value: notice.Password, dbType: DbType.String);
            dynamicParameters.Add("@Encoding", value: notice.Encoding, dbType: DbType.String);
            dynamicParameters.Add("@Homepage", value: notice.Homepage, dbType: DbType.String);
            dynamicParameters.Add("@FileName", value: notice.FileName, dbType: DbType.String);
            dynamicParameters.Add("@FileSize", value: notice.FileSize, dbType: DbType.Int32);

            switch (formType)
            {
                case BoardWriteFormType.Write:
                    dynamicParameters.Add("@WriteIP", value: notice.WriteIP, dbType: DbType.String);

                    recordCount = this.connection.Execute("WriteNotice", dynamicParameters, commandType: CommandType.StoredProcedure);

                    break;

                case BoardWriteFormType.Modify:
                    dynamicParameters.Add("@UpdateIP", value: notice.UpdateIP, dbType: DbType.String);
                    dynamicParameters.Add("@ID", value: notice.ID, dbType: DbType.Int32);

                    recordCount = this.connection.Execute("UpdateNotice", dynamicParameters, commandType: CommandType.StoredProcedure);

                    break;

                case BoardWriteFormType.Reply:
                    dynamicParameters.Add("@WriteIP", value: notice.WriteIP, dbType: DbType.String);
                    dynamicParameters.Add("@ParentID", value: notice.ParentID, dbType: DbType.Int32);

                    recordCount = this.connection.Execute("ReplyNotice", dynamicParameters, commandType: CommandType.StoredProcedure);

                    break;
            }
            return recordCount;
        }

        /// <summary>
        /// 게시판 쓰기
        /// </summary>
        /// <param name="notice">게시판</param>
        public void WriteNotice(NoticeModel notice)
        {
            this.logger.LogInformation("EXECITE WRITE NOTICE");

            try
            {
                SaveNotice(notice, BoardWriteFormType.Write);
            }
            catch (Exception e)
            {
                this.logger.LogError($"ERROR WRITE NOTICE : {e}");
            }
        }

        /// <summary>
        /// 게시판 수정하기
        /// </summary>
        /// <param name="notice">게시판</param>
        /// <returns>처리 레코드 수</returns>
        public int UpdateNotice(NoticeModel notice)
        {
            int recordCount = 0;

            this.logger.LogInformation("EXECUTE UPDATE NOTICE");

            try
            {
                recordCount = SaveNotice(notice, BoardWriteFormType.Modify);
            }
            catch (Exception e)
            {
                this.logger.LogError($"ERROR EXECUTE UPDATE NOTICE {e}");
            }
            return recordCount;
        }

        /// <summary>
        /// 게시판 답변하기
        /// </summary>
        /// <param name="notice">게시판</param>
        public void ReplyNotice(NoticeModel notice)
        {
            this.logger.LogInformation("EXECUTE REPLY NOTICE");

            try
            {
                SaveNotice(notice, BoardWriteFormType.Reply);
            }
            catch (Exception e)
            {
                this.logger.LogError($"ERROR EXECUTE REPLY NOTICE {e}");
                throw;
            }
        }

        /// <summary>
        /// 게시판 삭제하기
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="password">패스워드</param>
        /// <returns>처리 레코드 수</returns>
        public int DeleteNotice(int id, string password)
        {
            return this.connection.Execute("DeleteNotice", new { ID = id, Password = password }, commandType: CommandType.StoredProcedure);
        }
        
        /// <summary>
        /// 카운트 구하기
        /// </summary>
        public int GetCount()
        {
            this.logger.LogInformation("EXECUTE GET COUNT");

            try
            {
                return this.connection.Query<int>("SELECT COUNT(*) FROM dbo.Notice").SingleOrDefault();
            }
            catch (Exception e)
            {
                this.logger.LogError($"ERROR GET COUNT : {e}");

                return -1;
            }
        }

        /// <summary>
        /// 카운트 구하기
        /// </summary>
        /// <param name="searchField">검색 필드</param>
        /// <param name="searchQuery">검색 쿼리</param>
        /// <returns>카운트</returns>
        public int GetCount(string searchField, string searchQuery)
        {
            this.logger.LogInformation("EXECUTE GET COUNT (SEARCH FIELD, SEARCH QUERY)");

            try
            {
                return this.connection.Query<int>
                (
                    "SearchNoticeCount",
                    new { SearchField = searchField, SearchQuery = searchQuery },
                    commandType: CommandType.StoredProcedure
                )
                .SingleOrDefault();
            }
            catch (Exception e)
            {
                this.logger.LogError($"ERROR GET COUNT (SEARCH FIELD, SEARCH QUERY) : {e}");

                return -1;
            }
        }

        /// <summary>
        /// 게시판 리스트 구하기
        /// </summary>
        /// <param name="pageIndex">페이지 인덱스</param>
        public List<NoticeModel> GetNoticeList(int pageIndex)
        {
            this.logger.LogInformation("EXECUTE GET NOTICE LIST");

            try
            {
                DynamicParameters dynamicParameters = new DynamicParameters(new { Page = pageIndex });

                return this.connection.Query<NoticeModel>("ListNotice", dynamicParameters, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception e)
            {
                this.logger.LogError($"ERROR GET NOTICE LIST : {e}");

                return null;
            }
        }

        /// <summary>
        /// 게시판 리스트 구하기
        /// </summary>
        /// <param name="pageIndex">페이지 인덱스</param>
        /// <param name="searchField">검색 필드</param>
        /// <param name="searchQuery">검색 쿼리</param>
        /// <returns>게시판 리스트</returns>
        public List<NoticeModel> GetNoticeList(int pageIndex, string searchField, string searchQuery)
        {
            DynamicParameters dynamicParameters = new DynamicParameters
            (
                new
                {
                    Page = pageIndex,
                    SearchField = searchField,
                    SearchQuery = searchQuery
                }
            );

            return this.connection.Query<NoticeModel>
            (
                "SearchNotice",
                dynamicParameters, commandType: CommandType.StoredProcedure).ToList();
        }

        
        /// <summary>
        /// 게시판 보기
        /// </summary>
        public NoticeModel GetNotice(int id)
        {
            DynamicParameters dynamicParameters = new DynamicParameters(new { ID = id });

            return this.connection.Query<NoticeModel>("ViewNotice", dynamicParameters, commandType: CommandType.StoredProcedure).SingleOrDefault();
        }

        /// <summary>
        /// 파일명 구하기
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>파일명</returns>
        public string GetFileName(int id)
        {
            return this.connection.Query<string>("SELECT FileName FROM dbo.Notice WHERE ID = @ID", new { ID = id }).SingleOrDefault();
        }

        /// <summary>
        /// 다운로드 카운트 수정하기
        /// </summary>
        /// <param name="id">ID</param>
        public void UpdateDownloadCount(int id)
        {
            DynamicParameters dynamicParameters = new DynamicParameters(new { ID = id });

            this.connection.Execute("UPDATE dbo.Notice SET DownloadCount = DownloadCount + 1 WHERE ID = @ID", dynamicParameters, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// 다운로드 카운트 수정하기
        /// </summary>
        /// <param name="fileName">파일명</param>
        public void UpdateDownloadCount(string fileName)
        {
            this.connection.Execute("UPDATE dbo.Notice SET DownloadCount = DownloadCount + 1 WHERE FileName = @FileName", new { FileName = fileName });
        }

        /// <summary>
        /// 사진이 있는 최근 게시판 리스트 구하기
        /// </summary>
        /// <returns>사진이 있는 최근 게시판 리스트 구하기</returns>
        public List<NoticeModel> GetRecentPhotoNoticeList()
        {
            string sql = @"
SELECT TOP 4
    ID
    ,Title
    ,FileName
    ,FileSize
FROM dbo.Notice
WHERE FileName LIKE '%.png'
OR    FileName LIKE '%.jpg'
OR    FileName LIKE '%.jpeg'
OR    FileName LIKE '%.gif'
ORDER BY ID DESC
";
            return this.connection.Query<NoticeModel>(sql).ToList();
        }

        /// <summary>
        /// 특정 카테고리 최근 게시판 리스트 구하기
        /// </summary>
        /// <returns>특정 카테고리 최근 게시판 리스트 구하기</returns>
        public List<NoticeModel> GetRecentCategoryNoticeList(string category)
        {
            string sql = @"
SELECT TOP 3
    ID
   ,Title
   ,Name
   ,WriteDate
   ,FileName
   ,FileSize
   ,ReadCount
   ,CommentCount
   ,ReplyLevel
FROM  dbo.Notice
WHERE Category = @Category
ORDER BY ID DESC
";

            return this.connection.Query<NoticeModel>(sql, new { Category = category }).ToList();
        }

        /// <summary>
        /// 최근 게시판 리스트 구하기
        /// </summary>
        /// <returns>최근 게시판 리스트 구하기</returns>
        public List<NoticeModel> GetRecentNoticeList()
        {
            string sql = "SELECT TOP 3 ID, Title, Name, WriteDate, FROM dbo.Notice ORDER BY ID DESC";

            return this.connection.Query<NoticeModel>(sql).ToList();
        }

        /// <summary>
        /// 최근 게시판 리스트 구하기
        /// </summary>
        /// <param name="noticeCount">게시판 수</param>
        /// <returns>최근 게시판 리스트</returns>
        public List<NoticeModel> GetRecentNoticeList(int noticeCount)
        {
            string sql = $"SELECT TOP {noticeCount} ID, Title, Name, WriteDate FROM dbo.Notice ORDER BY ID DESC";

            return this.connection.Query<NoticeModel>(sql).ToList();
        }

        /// <summary>
        /// 게시판 고정하기
        /// </summary>
        /// <param name="id">ID</param>
        /// <remarks>특정 게시글을 공지사항으로 올리기</remarks>
        public void PinNotice(int id)
        {
            this.connection.Execute("UPDATE dbo.Notice SET CATEGORY = 'Notice' WHERE ID = @ID", new { ID = id });
        }
    }
}
