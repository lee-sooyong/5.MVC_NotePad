using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace MVC_NotePad
{
    /// <summary>
    /// ���α׷�
    /// </summary>
    public class Program
    {
        #region ���α׷� �����ϱ� - Main(argumentArray)

        /// <summary>
        /// ���α׷� �����ϱ�
        /// </summary>
        /// <param name="argumentArray">���� �迭</param>
        public static void Main(string[] argumentArray)
        {
            CreateHostBuilder(argumentArray).Build().Run();
        }

        #endregion
        #region ȣ��Ʈ ���� �����ϱ� - CreateHostBuilder(argumentArray)

        /// <summary>
        /// ȣ��Ʈ ���� �����ϱ�
        /// </summary>
        /// <param name="argumentArray">���� �迭</param>
        /// <returns>ȣ��Ʈ ����</returns>
        public static IHostBuilder CreateHostBuilder(string[] argumentArray) =>
            Host.CreateDefaultBuilder(argumentArray)
                .ConfigureWebHostDefaults
                (
                    builder =>
                    {
                        builder.UseStartup<Startup>();
                    }
                );

        #endregion
    }
} 