
using AspNetCoreRateLimit;
using DOANV1.Entities.Extensions_Entities.DTO.DataDto1;
using DOANV1.Extensions.DATA.Current_Chart.Chart_Hub;
using DOANV1.Extensions.DATA.Features.HATEOAS.HATEOAS_Data_Link;
using DOANV1.Extensions.Features.HATEOAS.HATEOAS_Data_Link;
using DOANV1.Extensions.Features.ShapeData_Get;
using DOANV1.Extensions.Service.ActionFilters;
using DOANV1.Extensions.Service.Caching;
using DOANV1.Extensions.Service.LoggerService;
using DOANV1.Extensions.Service.Mapping;
using DOANV1.Extensions.Service.MongDb;
using DOANV1.Extensions.Service.RateLimiting;
using DOANV1.Extensions.Service.Swagger;
using DOANV1.Extensions.Static;
using DOANV1.Extensions.Static.Error.GlobalErrorHandling;
using DOANV1.Extensions.USER.ConfigureJWT_Login;
using DOANV1.Extensions.USER.Identity;
using DOANV1.Interface.IData;
using DOANV1.Interface.IService;
using DOANV1.Interface.IUser;
using DOANV1.Repository.Data;
using DOANV1.Repository.User.Login;
using Interface.IData_Repository;
using Interface.IService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;
using Repository.Data;
using System.IO;

namespace DOANV1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            //LOGGER_Service
            LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

            Configuration = configuration;
        }


        public IConfiguration Configuration { get; }



        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //______________________________________CẤU HÌNH DỊCH VU MỞ RỘNG ______________________________________


            //CORS
            services.ConfigureCors();

            //IIS
            services.ConfigureIISIntegration();

            // IR_chart_realtime
            services.AddSignalR();


            //HATEOAS__Chấp nhận tiêu đề đặt trước 
            services.HATEOAS_CustomMediaTypes();


            //CACHING__RESPONSE__trên 1 API  
            services.ConfigureResponseCaching();


            //CACHING__VALIDATION__trên 1 API 
            services.ValidateHttpCacheHeaders();


            // RATE LIMITTING AND THROTTLING__Triển khai bộ nhớ đệm để lưu trữ các bộ đếm và quy tắc hỗ trợ giới hạn request API 
            services.AddMemoryCache();

            // RATE LIMITTING AND THROTTLING__hỗ trợ giới hạn request API 
            services.ConfigureRateLimitingOptions();

            // RATE LIMITTING AND THROTTLING__hỗ trợ giới hạn request API 
            services.AddHttpContextAccessor();

            //___________________MongoClient_Data____________
            //ConnectMongoDbClient
            services.ConfigureMongoDbClient(Configuration);
            //________________________________________________



            //___________________Identity and JWT____________

            // Identity
            services.ConfigureIdentity();

            // Configure JWT
            services.ConfigureJWT(Configuration);
            //________________________________________________



            //SWAGGER
            services.ConfigureSwagger();



            // _______________________________________DỊCH VỤ LỚP________________________________________________________________________________________-


            //LOGGER_SERVICE
            services.AddScoped<ILoggerService, LoggerService>();


            //Automap
            services.AddAutoMapper(typeof(MappingProfile));


            // BỘ LỌC___Dùng cho phương thức Get_ID
            services.AddScoped<AsyncActionFilter>(); 

            // BỘ LỌC___Dùng cho phương thức Post,Put
            services.AddScoped<ValidationFilter>();

            // BỘ LỌC___Dùng cho phương thức Delete,Put
            //services.AddScoped<ValidateIdItemExists>(); 


            // ĐỊNH HÌNH DỮ LIỆU (DATA SHAPING)-- Dùng để lấy dữ liệu = cách truy vấn trường dữ liệu trong lớp DTO 
            services.AddScoped<IDataShaper<DataDto>, DataShaper<DataDto>>();



            // HATEOAS___ XÁc định phương tiện hợp lệ 
            services.AddScoped<ValidateMediaTypeAttribute_HATEOAS>();

            // HATEOAS___ Thêm lớp cấu hình liên kết trong phản hồi 
            services.AddScoped<HATEOAS_Data_Link>();


            //ConnectMongoDbClient
            services.AddSingleton<IData_Repository, Data_Repository>();

            services.AddSingleton<ICurrent_Repository, Current_Repository>();
            //services.AddScoped<Data_Repository>();

            //JWT_Login_Authen
            services.AddScoped<ILogin_JWT, Login_JWT>();





            // _______________________________________DỊCH VỤ TỪ PACKAGES_____________________________________________________________


            // VALIDATION___Model State
            services.Configure<ApiBehaviorOptions>(options => 
            {
                options.SuppressModelStateInvalidFilter = true;
            });



            // CHO PHÉP CHỨA HẬU TỐ ASYNC 
            services.AddControllers(options =>
            {
                options.SuppressAsyncSuffixInActionNames = false;   
                                                                    
            });




            services.AddControllers(config =>
            {
                // CONTENT NEGOTIATION__ĐỊNH DẠNG PHẢN HỒI XML____Accept header XML 
                config.RespectBrowserAcceptHeader = true;


                //CACHE__Trên toàn controller 
                config.CacheProfiles.Add("120SecondsDuration", new CacheProfile { Duration = 120 });   // khoảng thoegi gian = 120s

            })

            //  hỗ trợ phương thức Patch trong controller  
            .AddNewtonsoftJson();

            // Accept header XML
            //.AddXmlDataContractSerializerFormatters();






        }




        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerService logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DOANV1 v1"));
            }

            // HSTS_____ adds Strict-Transport-Security header
            else
            {
                app.UseHsts();
            }


            // ExceptionsHandling
            app.ConfigureExceptionHandler(logger);


            if (env.IsDevelopment()) // Cho phép chuyển hướng https nếu đây là chế độ development
            {
                app.UseHttpsRedirection();
            }

            app.UseStaticFiles();

            //CORS
            app.UseCors("CorsPolicy");

            // CHUYỂN TIẾP TIÊU ĐÈ PROXY ĐẾN YÊU CẦU HIỆN TẠI 
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });


            //CACHING__RESPONSE 
            app.UseResponseCaching();

            //CACHING__VALIDATION
            app.UseHttpCacheHeaders();


            //RATE LIMITTING AND THROTTLING
            app.UseIpRateLimiting();

            app.UseRouting();


            //Identity_Authentication
            app.UseAuthentication();


            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<Chart_Hub>("/chart");
            });




        }
    }
}
