using CSharks.BLL.Services;
using CSharks.BLL.Services.Interfaces;
using CSharks.DAL;
using CSharks.DAL.Entities;
using CSharks.DAL.Repositories;
using CSharks.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using System.Globalization;
using CSharks.Aplication.Hubs;

namespace CSharks.Aplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
            builder.Services.AddControllersWithViews().AddDataAnnotationsLocalization().AddViewLocalization();
            builder.Services.AddDbContext<CSharksDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("CSharksDatabase")));
            builder.Services.AddScoped<ICharacterRepository, CharacterRepository>();
            builder.Services.AddScoped<ICommentRepository, CommentRepository>();
            builder.Services.AddScoped<INewsRepository, NewsRepository>();
            builder.Services.AddScoped<IQuestionAnswerRepository, QuestionAnswerRepository>();
            builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
            builder.Services.AddScoped<IQuizTypeRepository, QuizTypeRepository>();
            builder.Services.AddScoped<ITranslateRepository, TranslateRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<ICharacterService, CharacterService>();
            builder.Services.AddScoped<ICommentService, CommentService>();
            builder.Services.AddScoped<INewsService, NewsService>();
            builder.Services.AddScoped<IQuestionAnswerService, QuestionAnswerService>();
            builder.Services.AddScoped<IQuestionService, QuestionService>();
            builder.Services.AddScoped<IQuizTypeService, QuizTypeService>();
            builder.Services.AddScoped<ITranslateService, TranslateService>();
            builder.Services.AddIdentity<User, IdentityRole<int>>().AddEntityFrameworkStores<CSharksDbContext>().AddTokenProvider<DataProtectorTokenProvider<User>>(TokenOptions.DefaultProvider);
            builder.Services.AddScoped<IComicsRepository, ComicsRepository>();
            builder.Services.AddScoped<IComicsService, ComicsService>();
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddScoped<IMessageRepository, MessageRepository>();
            builder.Services.AddScoped<IMessageService, MessageService>();
            builder.Services.AddScoped<IQuizScoreRepository, QuizScoreRepository>();
            builder.Services.AddScoped<IQuizScoreService, QuizScoreService>();
            builder.Services.AddSignalR();
            builder.Services.AddAuthentication()
            .AddGoogle(opts =>
            {
                opts.ClientId = "17087847832-7tnmqghn8ln7qm0qcpqpgn1ima3l7g0g.apps.googleusercontent.com";
                opts.ClientSecret = "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";
                opts.SignInScheme = IdentityConstants.ExternalScheme;
            });
            builder.Services.AddRouting(options => options.LowercaseUrls = true);
            builder.Services.ConfigureApplicationCookie(options => 
            {
                options.LoginPath = "/Admin/Account/Login";
                options.AccessDeniedPath = "/";
            });
            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                var suportedCultures = new[]
                {
                    new CultureInfo("am"),
                    new CultureInfo("ru"),
                    new CultureInfo("en")
                };

                options.DefaultRequestCulture = new RequestCulture("en");
                options.SupportedCultures = suportedCultures;
                options.SupportedUICultures = suportedCultures;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseDeveloperExceptionPage();
            app.UseRequestLocalization();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthorization();
            app.MapHub<ChatHub>("/chathub");
            app.MapControllerRoute(
                name: "Admin",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            app.MapControllerRoute(
                name: "default",
                pattern: "/{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}