using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using MVC_NotePad.Models;
using MVC_NotePad.Settings;

namespace MVC_NotePad
{
    /// <summary>
    /// ����
    /// </summary>
    public class Startup
    {

        #region ���� - Configuration

        /// <summary>
        /// ����
        /// </summary>
        public IConfiguration Configuration { get; }

        #endregion

        #region ������ - Startup(configuration)

        /// <summary>
        /// ������
        /// </summary>
        /// <param name="configuration">����</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        #endregion

        #region ���� �÷��� �����ϱ� - ConfigureServices(services)

        /// <summary>
        /// ���� �÷��� �����ϱ�
        /// </summary>
        /// <param name="services">���� �÷���</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            services.AddSingleton<IConfiguration>(Configuration);
            services.Configure<MainSettings>(Configuration.GetSection(nameof(MainSettings)));
            services.AddTransient<INoticeRepository, NoticeRepository>();
            services.AddSingleton<ICommentRepository>(new CommentRepository(Configuration["ConnectionStrings:DefaultConnection"]));
        }

        #endregion
        #region �����ϱ� - Configure(app, environment)

        /// <summary>
        /// �����ϱ�
        /// </summary>
        /// <param name="app">���ø����̼� ����</param>
        /// <param name="environment">�� ȣ��Ʈ ȯ��</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints
            (
                endpoints =>
                {
                    endpoints.MapControllerRoute
                    (
                        name: "default",
                        pattern: "{controller=Home}/{action=Index}/{id?}"
                    );
                }
            );
        }

        #endregion
    }
}
