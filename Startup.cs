using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using apiMyApp.Repositories;
using apiMyApp.Repositories.db;
using Microsoft.EntityFrameworkCore;
using apiMyApp.Entity;
// using DBAccessSample.Data;

namespace apiMyApp
{
  //アプリケーションの動作を定義するクラス。
  public class Startup
  {
    //設定プロパティ
    public IConfiguration Configuration { get; }
    //コンストラクト
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }


    // This method gets called by the runtime. Use this method to add services to the container.
    //DIを設定する場所()
    public void ConfigureServices(IServiceCollection services)
    {
      //左はインターフェース、右は実装部分。
      services.AddSingleton<MysqlDb>();
      services.AddSingleton<IItemsRepository, ItemRepositoryImp>();
      services.AddControllers();
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "apiMyApp", Version = "v1" });
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "apiMyApp v1"));
      }

      if (env.IsDevelopment())
      {
        app.UseHttpsRedirection();
      }

      app.UseHttpsRedirection();

      app.UseRouting();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
