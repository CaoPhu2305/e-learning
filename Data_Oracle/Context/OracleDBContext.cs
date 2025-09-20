using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Data_Oracle.Entities;

namespace Data_Oracle.Context
{
    public class OracleDBContext : DbContext
    {

        public OracleDBContext(): base("name=OracleDbContext") { }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User>  Users { get; set; }

        public DbSet<UserRole> UserRole { get; set; }

        public DbSet<Permission> Permissions { get; set; }

        public DbSet<UserInfo> UserInfos { get; set; }

        public DbSet<Resources> Resources { get; set; }

        public DbSet<RolePermissionResources> RolePermissionResourcesList { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("E_LEARNINGDB");


            modelBuilder.Entity<RolePermissionResources>()
                .HasKey(rpr => new { rpr.RoleID, rpr.ResourcesID, rpr.PermissionID });

            modelBuilder.Entity<RolePermissionResources>()
                .HasRequired(rpr => rpr.Role)
                .WithMany(rpr => rpr.RolePermissionResources)
                .HasForeignKey(rpr => rpr.RoleID);


            modelBuilder.Entity<RolePermissionResources>()
                .HasRequired(rpr => rpr.Permission)
                .WithMany(rpr => rpr.RolePermissionResources)
                .HasForeignKey(rpr => rpr.PermissionID);


            modelBuilder.Entity<RolePermissionResources>()
                .HasRequired(rpr => rpr.Resources)
                .WithMany(rpr => rpr.RolePermissionResources)
                .HasForeignKey(rpr => rpr.ResourcesID);

            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.RoleID, ur.UserID });

            modelBuilder.Entity<UserRole>()
                .HasRequired(ur => ur.Role)
                .WithMany(r => r.UserRole)
                .HasForeignKey(ur => ur.RoleID);
               
            modelBuilder.Entity<UserRole>()
                .HasRequired(u => u.User)
                .WithMany(ur => ur.UserRole)
                .HasForeignKey(ur => ur.UserID);

            modelBuilder.Entity<User>()
                .HasOptional(u => u.UserInfo)
                .WithRequired(u => u.User);

        }

    }
}
