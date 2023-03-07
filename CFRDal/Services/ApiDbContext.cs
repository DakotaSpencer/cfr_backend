using CFRDal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;

namespace CFRDal
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
        }

        public ApiDbContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseMySQL("Server=catgirl-film-reviews.ccskcsxljvdp.us-east-1.rds.amazonaws.com; Database=catgirl_film_reviews; Uid=admin; Pwd=tLXN4yhYHSkDK8R");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<Review>().ToTable("reviews");
            modelBuilder.Entity<Downvote>().ToTable("downvotes").HasNoKey();
            modelBuilder.Entity<Upvote>().ToTable("upvotes").HasNoKey();
            modelBuilder.Entity<ReviewData>().ToView("getReviewData");
        }

        public DbSet<Review> Reviews { get; set; }
        public DbSet<ReviewData> ReviewData { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Downvote> Downvotes { get; set; }
        public DbSet<Upvote> Upvotes { get; set; }
    }
}