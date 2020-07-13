using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using module_20.BLL.Interfaces;
using module_20.BLL.Services;
using module_20.DAL.Interfaces;
using module_20.DAL.Repositories;
using module_20.Web.Filters;
using module_20.Web.Formatters;
using System;
using System.IO;
using System.Reflection;

namespace module_20.Web.Extensions
{
    /// <summary>
    /// Class that contains add's services extension
    /// </summary>
    public static class AddServicesExtension
    {
        /// <summary>
        /// Add business logic services
        /// </summary>
        /// <param name="services">Services of api</param>
        public static void AddBLLServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ICourseService, CourseService>();
            services.AddTransient<IHomeworkService, HomeworkService>();
            services.AddTransient<ILecturerService, LecturerService>();
            services.AddTransient<ILectureService, LectureService>();
            services.AddTransient<IStudentService, StudentService>();
        }

        /// <summary>
        /// Add swagger
        /// </summary>
        /// <param name="services">Services of api</param>
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "module_20", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);

            });
        }

        /// <summary>
        /// Add MVC services
        /// </summary>
        /// <param name="services">Services of api</param>
        public static void AddMVCServices(this IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.OutputFormatters.Add(new TxtOutputAttendanceFormatter());
                options.Filters.Add(typeof(ErrorResponseFilter));
            });
        }

        /// <summary>
        /// Add controller services
        /// </summary>
        /// <param name="services">Services of api</param>
        public static void AddControllerServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
            services.AddControllers()
                .AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                .AddFormatterMappings(fm => fm.SetMediaTypeMappingForFormat("txt", "text/txt"));
        }
    }
}
