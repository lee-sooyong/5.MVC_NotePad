using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
                    break;
                case BoardWriteFormType.Reply:
                    break;
                default:
                    break;
            }
        }
    }
}
