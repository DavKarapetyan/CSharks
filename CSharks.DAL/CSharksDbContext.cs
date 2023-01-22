using CSharks.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharks.DAL
{
    public class CSharksDbContext : IdentityDbContext<User, IdentityRole<int>, int, IdentityUserClaim<int>, IdentityUserRole<int>, IdentityUserLogin<int>,IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public CSharksDbContext(DbContextOptions<CSharksDbContext> options) : base(options) { }
        
        public DbSet<News> News { get; set; }
        public DbSet<Characters> Characters { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionAnswer> QuestionAnswers { get; set; }
        public DbSet<QuizType> QuizTypes { get; set; }
        public DbSet<Translate> Translates { get; set; }
        public DbSet<Comics> Comics { get; set; }
    }
    
}
