using Data_Oracle.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Oracle.Context
{
    public class OracleDBContext : DbContext
    {

        public OracleDBContext(): base("name=OracleDbContext") 
        {
            this.Database.CommandTimeout = 300;
            Database.SetInitializer<OracleDBContext>(null);
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User>  Users { get; set; }

        public DbSet<UserRole> UserRole { get; set; }

        public DbSet<Permission> Permissions { get; set; }

        public DbSet<UserInfo> UserInfos { get; set; }

        public DbSet<Resources> Resources { get; set; }

        public DbSet<RolePermissionResources> RolePermissionResourcesList { get; set; }

        public DbSet<AnswerOptions> AnswerOptions { get; set; }

        public DbSet<Assignment> Assignments { get; set; }

        public DbSet<Attendance> Attendances { get; set; }

        public DbSet<Chapter> Chapters { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<CoursePromotion> CoursePromotions { get; set; }

        public DbSet<CourseStatus> courseStatuses { get; set; }

        public DbSet<CourseType> CourseTypes { get; set; }

        public DbSet<CourseVideo> CoursesVideos { get; set; }

        public DbSet<Enrollments> Enrollments { get; set; }

        public DbSet<EnrollmentStatus> EnrollmentStatuses { get; set; }

        public DbSet<Grade> Grades { get; set; }

        public DbSet<InteractiveClass> InteractiveClasses { get; set; }

        public DbSet<LecturerCourse> lecturerCourses { get; set; }

        public DbSet<Lession> Lessions { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<OrderStatus> OrderStatuses { get; set; }

        public DbSet<Promotion> Promotions { get; set; }

        public DbSet<Questions> Questions { get; set; }

        public DbSet<QuizAttempt> QuizAttempts { get; set; }

        public DbSet<Quizzes> Quizzes { get; set; }
            
        public DbSet<Session> Sessions { get; set; }

        public DbSet<Submission> Submissions { get; set; }

        public DbSet<UserAnswers> UserAnswers { get; set; }



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

            // 1-1 
            modelBuilder.Entity<User>()
                .HasOptional(u => u.UserInfo)
                .WithRequired(u => u.User);

            // tiếp !!! 

            // 1 - 1
            modelBuilder.Entity<User>()
               .Property(u => u.UserID)
               .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);


            // User -- LectureCourse -- Course 

            modelBuilder.Entity<LecturerCourse>()
                .HasKey(lc => new { lc.CourseID, lc.UserID });

            modelBuilder.Entity<LecturerCourse>()
                .HasRequired(lc => lc.User)
                .WithMany(u => u.LecturerCourses)
                .HasForeignKey(lc => lc.UserID);

            modelBuilder.Entity<LecturerCourse>()
                .HasRequired(lc => lc.Course)
                .WithMany(c => c.LecturerCourses)
                .HasForeignKey(lc => lc.CourseID);

            // User -- Order 

            modelBuilder.Entity<Order>()
                .HasKey(o => o.OrderID);

            modelBuilder.Entity<Order>()
                .HasRequired(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserID);

            // OderStatus 

            modelBuilder.Entity<OrderStatus>()
                .HasKey(os => os.OrderStatusID);

            // OderStatus -- Order 

            modelBuilder.Entity<Order>()
                .HasRequired(o => o.OrderStatus)
                .WithMany(os => os.Orders)
                .HasForeignKey(o => o.OrderStatusID);

            // Order -- OderDetail 

            modelBuilder.Entity<OrderDetail>()
                .HasKey(od => od.OrderDetailID);

            modelBuilder.Entity<OrderDetail>()
                .HasRequired(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OderID);

            // OderDetail  -- Course
            modelBuilder.Entity<OrderDetail>()
                .HasRequired(od => od.Course)
                .WithMany(c => c.OrderDetails)
                .HasForeignKey(od => od.CourseID);

            // OderDetail -- Promotion null able

            modelBuilder.Entity<OrderDetail>()
                .HasOptional(od => od.Promotion)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(od => od.PromotionID);


            // Promotion 

            modelBuilder.Entity<Promotion>()
                .HasKey(p => p.PromotionID);

            // CoursePromotion 

            modelBuilder.Entity<CoursePromotion>()
                .HasKey(cp => new { cp.CourseID, cp.PromotionID });

            modelBuilder.Entity<CoursePromotion>()
                .HasRequired(cp => cp.Promotion)
                .WithMany(p => p.CoursePromotions)
                .HasForeignKey(cp => cp.PromotionID);

            modelBuilder.Entity<CoursePromotion>()
                .HasRequired(cp => cp.Course)
                .WithMany(c => c.CoursePromotions)
                .HasForeignKey(cp => cp.CourseID);

            // CourseStatus 

            modelBuilder.Entity<CourseStatus>()
                .HasKey(cs => cs.CourseStatusID);

            // CourseType 

            modelBuilder.Entity<CourseType>()
                .HasKey(cy => cy.CourcesTypeID);


            // Course 

            modelBuilder.Entity<Course>()
                .HasKey(c => c.CourseID);

            modelBuilder.Entity<Course>()
                .HasRequired(c => c.CourseStatus)
                .WithMany(cs => cs.Courses)
                .HasForeignKey(c => c.CourseStatusID);

            modelBuilder.Entity<Course>()
                .HasRequired(c => c.CourseType)
                .WithMany(ct => ct.Courses)
                .HasForeignKey(c => c.CourseTypeID);

            // EnrollmentsStatus

            modelBuilder.Entity<EnrollmentStatus>()
                .HasKey(es => es.EnrollmentStatusID);

            // Enrollments 


            modelBuilder.Entity<Enrollments>()
                .HasKey(e => e.EnrollmentsID);

            modelBuilder.Entity<Enrollments>()
                .HasRequired(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseID);

            modelBuilder.Entity<Enrollments>()
                .HasRequired(e => e.User)
                .WithMany(u => u.Enrollments)
                .HasForeignKey(e => e.UserID);

            modelBuilder.Entity<Enrollments>()
                .HasRequired(e => e.EnrollmentStatus)
                .WithMany(es => es.Enrollments)
                .HasForeignKey(e => e.EnrollmentStatusID);

            // Course nhánh video

            modelBuilder.Entity<Course>()
                .HasOptional(c => c.Video)
                .WithRequired(cv => cv.Course);

            // Video Chappter 

            modelBuilder.Entity<Chapter>()
                .HasKey(c => c.ChapterID);

            modelBuilder.Entity<Chapter>()
                .HasRequired(c => c.CourseVideo)
                .WithMany(cv => cv.Chapters)
                .HasForeignKey(c => c.CourcesVideoID);

            // Chapter -- Lession 

            modelBuilder.Entity<Lession>()
                .HasKey(l => l.LessionID);

            modelBuilder.Entity<Lession>()
                .HasRequired(l => l.Chapter)
                .WithMany(c => c.Lessions)
                .HasForeignKey(l => l.ChapterID);

            // Quizz 

            modelBuilder.Entity<Quizzes>()
                .HasKey(q => q.QuizzesID);

            modelBuilder.Entity<Quizzes>()
                .HasRequired(q => q.Lession)
                .WithMany(l => l.Quizzes)
                .HasForeignKey(q => q.LessionID);

            // Questions 

            modelBuilder.Entity<Questions>()
                .HasKey (qt => qt.QuestionsID);

            modelBuilder.Entity<Questions>()
                .HasRequired(qt => qt.Quizzes)
                .WithMany(q => q.Questions)
                .HasForeignKey (qt => qt.QuizzesID);

            // AnswerOptions

            modelBuilder.Entity<AnswerOptions>()
                .HasKey(ao => ao.AnswerOptionsID);

            modelBuilder.Entity<AnswerOptions>()
                .HasRequired(ao => ao.Questions)
                .WithMany(qt => qt.AnswerOptions)
                .HasForeignKey(ao => ao.QuestionsID);

            // QuizAttempt 

            modelBuilder.Entity<QuizAttempt>()
                .HasKey(qa => qa.QuizAttemptID);

            modelBuilder.Entity<QuizAttempt>()
                .HasRequired(qa => qa.User)
                .WithMany(u => u.QuizAttempts)
                .HasForeignKey(qa => qa.UserID);

            modelBuilder.Entity<QuizAttempt>()
                .HasRequired(qa => qa.Quizzes)
                .WithMany(q => q.QuizAttempts)
                .HasForeignKey(qa => qa.QuizzesID);

            // UserAnswers 

            modelBuilder.Entity<UserAnswers>()
                .HasKey(ua => ua.UserAnswersID);

            modelBuilder.Entity<UserAnswers>()
                .HasRequired(ua => ua.QuizAttempt)
                .WithMany(qa => qa.UserAnswers)
                .HasForeignKey(ua => ua.QuizAttemptID);

            modelBuilder.Entity<UserAnswers>()
                .HasRequired(ua => ua.AnswerOptions)
                .WithMany(ao => ao.UserAnswers)
                .HasForeignKey(ua => ua.AnswerOptionsID);

            modelBuilder.Entity<UserAnswers>()
                .HasRequired(ua => ua.Questions)
                .WithMany(q => q.UserAnswers)
                .HasForeignKey(ua => ua.QuestionsID);

            // InteractiveClass -- Course 1-1 

            modelBuilder.Entity<Course>()
                .HasOptional(c => c.InteractiveClass)
                .WithRequired(ic => ic.Course);

            // Sesion -- InteractiveClass 

            modelBuilder.Entity<Session>()
                .HasKey(s => s.SessionID);

            modelBuilder.Entity<Session>()
                .HasRequired(s => s.InteractiveClass)
                .WithMany(ic => ic.Sessions)
                .HasForeignKey(s => s.InteractiveClassID);

            // Assignment 

            modelBuilder.Entity<Assignment>()
                .HasKey(a => a.AssignmentID);

            modelBuilder.Entity<Assignment>()
                .HasRequired(a => a.Session)
                .WithMany(s => s.Assignments)
                .HasForeignKey(a => a.SessionID);

            // Submission 

            modelBuilder.Entity<Submission>()
                .HasKey(s => s.SubmissionID);

            modelBuilder.Entity<Submission>()
                .HasRequired(s => s.Assignment)
                .WithMany(a => a.Submissions)
                .HasForeignKey(s => s.AssignmentID);

            modelBuilder.Entity<Submission>()
                .HasRequired(s => s.User)
                .WithMany(u => u.Submissions)
                .HasForeignKey(s => s.UserID);

            // Grade 

            modelBuilder.Entity<Grade>()
                .HasKey(g => g.GradeID);

            modelBuilder.Entity<Grade>()
                .HasRequired(g => g.Submission)
                .WithMany(s => s.Grades)
                .HasForeignKey(g => g.SubmissionID);

            modelBuilder.Entity<Grade>()
                .HasRequired(g => g.User)
                .WithMany(s => s.Grades)
                .HasForeignKey(g => g.UserID);

            // Attendance 

            modelBuilder.Entity<Attendance>()
                .HasKey(a => a.AttendanceID);

            modelBuilder.Entity<Attendance>()
                .HasRequired(a => a.Session)
                .WithMany(s => s.Attendances)
                .HasForeignKey(a => a.SessionID);

            modelBuilder.Entity<Attendance>()
                .HasRequired(a => a.User)
                .WithMany(u => u.Attendances)
                .HasForeignKey(a => a.UserID);
        }

    }
}
