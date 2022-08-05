using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Data;
using System.Data.SqlClient;

namespace MVC_NotePad.Models.NoticeMng
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
        /// 게시판 제하기
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="password">패스워드</param>
        /// <returns>처리 레코드 수</returns>
        public int DeleteNotice(int id, string password)
        {
            return this.connection.Execute("DeleteNotice", new { ID = id, Password = password }, commandType: CommandType.StoredProcedure);
        }
    }
}
