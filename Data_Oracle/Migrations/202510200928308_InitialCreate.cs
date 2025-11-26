namespace Data_Oracle.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "E_LEARNINGDB.ANSWER_OPTIONS",
                c => new
                    {
                        ANSWER_OPTIONS_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ANSWER_OPTIONS_NAME = c.String(nullable: false, maxLength: 50),
                        IS_CORRECT = c.Decimal(nullable: false, precision: 1, scale: 0),
                        QUESTIONS_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ANSWER_OPTIONS_ID)
                .ForeignKey("E_LEARNINGDB.QUESTIONS", t => t.QUESTIONS_ID, cascadeDelete: true)
                .Index(t => t.QUESTIONS_ID);
            
            CreateTable(
                "E_LEARNINGDB.QUESTIONS",
                c => new
                    {
                        QUESTIONS_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        QUIZZES_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        QUESTIONS_CONTENT = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.QUESTIONS_ID)
                .ForeignKey("E_LEARNINGDB.QUIZZES", t => t.QUIZZES_ID, cascadeDelete: true)
                .Index(t => t.QUIZZES_ID);
            
            CreateTable(
                "E_LEARNINGDB.QUIZZES",
                c => new
                    {
                        QUIZZES_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        QUIZZES_NAME = c.String(nullable: false, maxLength: 50),
                        QUIZZES_TYPE = c.String(nullable: false, maxLength: 50),
                        PASS_SCORE_PERCENT = c.Single(nullable: false),
                        LESSION_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.QUIZZES_ID)
                .ForeignKey("E_LEARNINGDB.LESSION", t => t.LESSION_ID, cascadeDelete: true)
                .Index(t => t.LESSION_ID);
            
            CreateTable(
                "E_LEARNINGDB.LESSION",
                c => new
                    {
                        LESSION_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LESSION_NAME = c.String(nullable: false, maxLength: 50),
                        CHAPTER_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VIDEO_DATA = c.Binary(),
                    })
                .PrimaryKey(t => t.LESSION_ID)
                .ForeignKey("E_LEARNINGDB.CHAPTER", t => t.CHAPTER_ID, cascadeDelete: true)
                .Index(t => t.CHAPTER_ID);
            
            CreateTable(
                "E_LEARNINGDB.CHAPTER",
                c => new
                    {
                        CHAPTER_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        COURSE_VIDEO_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CHAPTER_NAME = c.String(nullable: false, maxLength: 50),
                        CHAPTER_INDEX = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.CHAPTER_ID)
                .ForeignKey("E_LEARNINGDB.COURSE_VIDEO", t => t.COURSE_VIDEO_ID, cascadeDelete: true)
                .Index(t => t.COURSE_VIDEO_ID);
            
            CreateTable(
                "E_LEARNINGDB.COURSE_VIDEO",
                c => new
                    {
                        COURSE_VIDEO_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        COURSE_VIDEO_NAME = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.COURSE_VIDEO_ID)
                .ForeignKey("E_LEARNINGDB.COURSE", t => t.COURSE_VIDEO_ID)
                .Index(t => t.COURSE_VIDEO_ID);
            
            CreateTable(
                "E_LEARNINGDB.COURSE",
                c => new
                    {
                        COURSE_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        COURSE_TYPE_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        COURSE_STATUS_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        COURSE_NAME = c.String(nullable: false, maxLength: 50),
                        PRICE = c.Double(nullable: false),
                        DESCRIPTION = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.COURSE_ID)
                .ForeignKey("E_LEARNINGDB.COURSE_STATUS", t => t.COURSE_STATUS_ID, cascadeDelete: true)
                .ForeignKey("E_LEARNINGDB.COURSE_TYPE", t => t.COURSE_TYPE_ID, cascadeDelete: true)
                .Index(t => t.COURSE_TYPE_ID)
                .Index(t => t.COURSE_STATUS_ID);
            
            CreateTable(
                "E_LEARNINGDB.COURSE_PROMOTION",
                c => new
                    {
                        COURSE_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PROMOTION_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.COURSE_ID, t.PROMOTION_ID })
                .ForeignKey("E_LEARNINGDB.COURSE", t => t.COURSE_ID, cascadeDelete: true)
                .ForeignKey("E_LEARNINGDB.PROMOTION", t => t.PROMOTION_ID, cascadeDelete: true)
                .Index(t => t.COURSE_ID)
                .Index(t => t.PROMOTION_ID);
            
            CreateTable(
                "E_LEARNINGDB.PROMOTION",
                c => new
                    {
                        PROMOTION_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PROMOTION_NAME = c.String(nullable: false, maxLength: 30),
                        DISCOUNT_VALUE = c.Decimal(nullable: false, precision: 18, scale: 2),
                        START_DATE = c.DateTime(nullable: false),
                        END_DATE = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PROMOTION_ID);
            
            CreateTable(
                "E_LEARNINGDB.ORDER_DETAIL",
                c => new
                    {
                        ORDER_DETAIL_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ORDER_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        COURSE_ID = c.Decimal(name: "COURSE_ID", nullable: false, precision: 18, scale: 2),
                        PRICE_AT_PURCHASE = c.Double(nullable: false),
                        PROMOTION_ID = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ORDER_DETAIL_ID)
                .ForeignKey("E_LEARNINGDB.COURSE", t => t.COURSE_ID, cascadeDelete: true)
                .ForeignKey("E_LEARNINGDB.ORDER", t => t.ORDER_ID, cascadeDelete: true)
                .ForeignKey("E_LEARNINGDB.PROMOTION", t => t.PROMOTION_ID)
                .Index(t => t.ORDER_ID)
                .Index(t => t.COURSE_ID, name: "IX_COURSE_ID")
                .Index(t => t.PROMOTION_ID);
            
            CreateTable(
                "E_LEARNINGDB.ORDER",
                c => new
                    {
                        ORDER_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        USER_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ORDER_STATUS_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ORDER_ID)
                .ForeignKey("E_LEARNINGDB.ORDER_STATUS", t => t.ORDER_STATUS_ID, cascadeDelete: true)
                .ForeignKey("E_LEARNINGDB.USERS", t => t.USER_ID, cascadeDelete: true)
                .Index(t => t.USER_ID)
                .Index(t => t.ORDER_STATUS_ID);
            
            CreateTable(
                "E_LEARNINGDB.ORDER_STATUS",
                c => new
                    {
                        ORDER_STATUS_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ORDER_STATUS_NAME = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ORDER_STATUS_ID);
            
            CreateTable(
                "E_LEARNINGDB.USERS",
                c => new
                    {
                        USER_ID = c.Decimal(nullable: false, precision: 18, scale: 2, identity: true),
                        USER_NAME = c.String(nullable: false, maxLength: 50),
                        EMAIL = c.String(nullable: false, maxLength: 80),
                        HASH_PASS_WORD = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.USER_ID);
            
            CreateTable(
                "E_LEARNINGDB.ATTENDANCE",
                c => new
                    {
                        ATTENDANCE_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        USER_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SESSION_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TIME_JOIN = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ATTENDANCE_ID)
                .ForeignKey("E_LEARNINGDB.SESSION", t => t.SESSION_ID, cascadeDelete: true)
                .ForeignKey("E_LEARNINGDB.USERS", t => t.USER_ID, cascadeDelete: true)
                .Index(t => t.USER_ID)
                .Index(t => t.SESSION_ID);
            
            CreateTable(
                "E_LEARNINGDB.SESSION",
                c => new
                    {
                        SESSION_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SESSION_NAME = c.String(nullable: false, maxLength: 50),
                        START_TIME = c.DateTime(nullable: false),
                        END_TIME = c.DateTime(nullable: false),
                        MEETING_LINK = c.String(nullable: false),
                        INTERACTIVE_CLASS_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.SESSION_ID)
                .ForeignKey("E_LEARNINGDB.INTERACTIVE_CLASS", t => t.INTERACTIVE_CLASS_ID, cascadeDelete: true)
                .Index(t => t.INTERACTIVE_CLASS_ID);
            
            CreateTable(
                "E_LEARNINGDB.ASSIGNMENT",
                c => new
                    {
                        ASSIGNMENT_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DEAD_LINE = c.DateTime(nullable: false),
                        SESSION_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ASSIGNMENT_ID)
                .ForeignKey("E_LEARNINGDB.SESSION", t => t.SESSION_ID, cascadeDelete: true)
                .Index(t => t.SESSION_ID);
            
            CreateTable(
                "E_LEARNINGDB.SUBMISSION",
                c => new
                    {
                        SUBMISSION_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ASSIGNMENT_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        USER_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FILE = c.Binary(),
                        TIME_SUBMISSION = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SUBMISSION_ID)
                .ForeignKey("E_LEARNINGDB.ASSIGNMENT", t => t.ASSIGNMENT_ID, cascadeDelete: true)
                .ForeignKey("E_LEARNINGDB.USERS", t => t.USER_ID, cascadeDelete: true)
                .Index(t => t.ASSIGNMENT_ID)
                .Index(t => t.USER_ID);
            
            CreateTable(
                "E_LEARNINGDB.GRADE",
                c => new
                    {
                        GRADE_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        USER_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SUBMISSION_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SCORE = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FEED_BACK = c.String(),
                    })
                .PrimaryKey(t => t.GRADE_ID)
                .ForeignKey("E_LEARNINGDB.SUBMISSION", t => t.SUBMISSION_ID, cascadeDelete: true)
                .ForeignKey("E_LEARNINGDB.USERS", t => t.USER_ID, cascadeDelete: true)
                .Index(t => t.USER_ID)
                .Index(t => t.SUBMISSION_ID);
            
            CreateTable(
                "E_LEARNINGDB.INTERACTIVE_CLASS",
                c => new
                    {
                        INTERACTIVE_CLASS_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        INTERACTIVE_CLASS_NAME = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.INTERACTIVE_CLASS_ID)
                .ForeignKey("E_LEARNINGDB.COURSE", t => t.INTERACTIVE_CLASS_ID)
                .Index(t => t.INTERACTIVE_CLASS_ID);
            
            CreateTable(
                "E_LEARNINGDB.ENROLLMENTS",
                c => new
                    {
                        ENROLLMENTS_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ENROLLMENTS_NAME = c.String(nullable: false, maxLength: 50),
                        USER_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ENROLLMENT_STATUS_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        COURSE_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ENROLLMENTS_DATE = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ENROLLMENTS_ID)
                .ForeignKey("E_LEARNINGDB.COURSE", t => t.COURSE_ID, cascadeDelete: true)
                .ForeignKey("E_LEARNINGDB.ENROLLMENT_STATUS", t => t.ENROLLMENT_STATUS_ID, cascadeDelete: true)
                .ForeignKey("E_LEARNINGDB.USERS", t => t.USER_ID, cascadeDelete: true)
                .Index(t => t.USER_ID)
                .Index(t => t.ENROLLMENT_STATUS_ID)
                .Index(t => t.COURSE_ID);
            
            CreateTable(
                "E_LEARNINGDB.ENROLLMENT_STATUS",
                c => new
                    {
                        ENROLLMENT_STATUS_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ENROLLMENT_STATUS_NAME = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ENROLLMENT_STATUS_ID);
            
            CreateTable(
                "E_LEARNINGDB.LECTURE_COURSE",
                c => new
                    {
                        USER_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        COURSE_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CREATE_AT_TIME = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.USER_ID, t.COURSE_ID })
                .ForeignKey("E_LEARNINGDB.COURSE", t => t.COURSE_ID, cascadeDelete: true)
                .ForeignKey("E_LEARNINGDB.USERS", t => t.USER_ID, cascadeDelete: true)
                .Index(t => t.USER_ID)
                .Index(t => t.COURSE_ID);
            
            CreateTable(
                "E_LEARNINGDB.QUIZ_ATTEMPT",
                c => new
                    {
                        QUIZ_ATTEMPT_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        QUIZZES_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        USER_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        QUIZ_ATTEMPT_DATE = c.DateTime(nullable: false),
                        QUIZ_ATTEMPT_SCORE = c.Single(nullable: false),
                        QUIZ_ATTEMPT_IS_PASS = c.Decimal(nullable: false, precision: 1, scale: 0),
                    })
                .PrimaryKey(t => t.QUIZ_ATTEMPT_ID)
                .ForeignKey("E_LEARNINGDB.QUIZZES", t => t.QUIZZES_ID, cascadeDelete: true)
                .ForeignKey("E_LEARNINGDB.USERS", t => t.USER_ID, cascadeDelete: true)
                .Index(t => t.QUIZZES_ID)
                .Index(t => t.USER_ID);
            
            CreateTable(
                "E_LEARNINGDB.USER_ANSWERS",
                c => new
                    {
                        USER_ANSWERS_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ANSWER_OPTIONS_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        QUESTIONS_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        QUIZ_ATTEMPT_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        User_UserID = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.USER_ANSWERS_ID)
                .ForeignKey("E_LEARNINGDB.ANSWER_OPTIONS", t => t.ANSWER_OPTIONS_ID, cascadeDelete: true)
                .ForeignKey("E_LEARNINGDB.QUESTIONS", t => t.QUESTIONS_ID, cascadeDelete: true)
                .ForeignKey("E_LEARNINGDB.QUIZ_ATTEMPT", t => t.QUIZ_ATTEMPT_ID, cascadeDelete: true)
                .ForeignKey("E_LEARNINGDB.USERS", t => t.User_UserID)
                .Index(t => t.ANSWER_OPTIONS_ID)
                .Index(t => t.QUESTIONS_ID)
                .Index(t => t.QUIZ_ATTEMPT_ID)
                .Index(t => t.User_UserID);
            
            CreateTable(
                "E_LEARNINGDB.USER_INFO",
                c => new
                    {
                        USER_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ADDRESS = c.String(nullable: false, maxLength: 50),
                        PHONE = c.String(nullable: false, maxLength: 10),
                        GENDER = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.USER_ID)
                .ForeignKey("E_LEARNINGDB.USERS", t => t.USER_ID)
                .Index(t => t.USER_ID);
            
            CreateTable(
                "E_LEARNINGDB.USER_ROLE",
                c => new
                    {
                        USER_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ROLE_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.USER_ID, t.ROLE_ID })
                .ForeignKey("E_LEARNINGDB.ROLE", t => t.ROLE_ID, cascadeDelete: true)
                .ForeignKey("E_LEARNINGDB.USERS", t => t.USER_ID, cascadeDelete: true)
                .Index(t => t.USER_ID)
                .Index(t => t.ROLE_ID);
            
            CreateTable(
                "E_LEARNINGDB.ROLE",
                c => new
                    {
                        ROLE_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ROLE_NAME = c.String(nullable: false, maxLength: 15),
                        ROLE_DESCRIPTION = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.ROLE_ID);
            
            CreateTable(
                "E_LEARNINGDB.ROLE_PERMISSION_RESOURCES",
                c => new
                    {
                        PermissionID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RoleID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ResourcesID = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.PermissionID, t.RoleID, t.ResourcesID })
                .ForeignKey("E_LEARNINGDB.PERMISSION", t => t.PermissionID, cascadeDelete: true)
                .ForeignKey("E_LEARNINGDB.RESOURCES", t => t.ResourcesID, cascadeDelete: true)
                .ForeignKey("E_LEARNINGDB.ROLE", t => t.RoleID, cascadeDelete: true)
                .Index(t => t.PermissionID)
                .Index(t => t.RoleID)
                .Index(t => t.ResourcesID);
            
            CreateTable(
                "E_LEARNINGDB.PERMISSION",
                c => new
                    {
                        PERMISSION_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PERMISSION_NAME = c.String(nullable: false, maxLength: 30),
                        PERMISSION_DESCRIPTION = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.PERMISSION_ID);
            
            CreateTable(
                "E_LEARNINGDB.RESOURCES",
                c => new
                    {
                        RESOURCES_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RESOURCES_NAME = c.String(nullable: false, maxLength: 20),
                        RESOURCES_DESCRIPTION = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.RESOURCES_ID);
            
            CreateTable(
                "E_LEARNINGDB.COURSE_STATUS",
                c => new
                    {
                        COURSE_STATUS_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        COURSE_STATUS_NAME = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.COURSE_STATUS_ID);
            
            CreateTable(
                "E_LEARNINGDB.COURSE_TYPE",
                c => new
                    {
                        COURSE_TYPE_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        COURSE_TYPE_NAME = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.COURSE_TYPE_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("E_LEARNINGDB.ANSWER_OPTIONS", "QUESTIONS_ID", "E_LEARNINGDB.QUESTIONS");
            DropForeignKey("E_LEARNINGDB.QUESTIONS", "QUIZZES_ID", "E_LEARNINGDB.QUIZZES");
            DropForeignKey("E_LEARNINGDB.QUIZZES", "LESSION_ID", "E_LEARNINGDB.LESSION");
            DropForeignKey("E_LEARNINGDB.LESSION", "CHAPTER_ID", "E_LEARNINGDB.CHAPTER");
            DropForeignKey("E_LEARNINGDB.CHAPTER", "COURSE_VIDEO_ID", "E_LEARNINGDB.COURSE_VIDEO");
            DropForeignKey("E_LEARNINGDB.COURSE_VIDEO", "COURSE_VIDEO_ID", "E_LEARNINGDB.COURSE");
            DropForeignKey("E_LEARNINGDB.INTERACTIVE_CLASS", "INTERACTIVE_CLASS_ID", "E_LEARNINGDB.COURSE");
            DropForeignKey("E_LEARNINGDB.COURSE", "COURSE_TYPE_ID", "E_LEARNINGDB.COURSE_TYPE");
            DropForeignKey("E_LEARNINGDB.COURSE", "COURSE_STATUS_ID", "E_LEARNINGDB.COURSE_STATUS");
            DropForeignKey("E_LEARNINGDB.COURSE_PROMOTION", "PROMOTION_ID", "E_LEARNINGDB.PROMOTION");
            DropForeignKey("E_LEARNINGDB.ORDER_DETAIL", "PROMOTION_ID", "E_LEARNINGDB.PROMOTION");
            DropForeignKey("E_LEARNINGDB.ORDER_DETAIL", "ORDER_ID", "E_LEARNINGDB.ORDER");
            DropForeignKey("E_LEARNINGDB.ORDER", "USER_ID", "E_LEARNINGDB.USERS");
            DropForeignKey("E_LEARNINGDB.USER_ROLE", "USER_ID", "E_LEARNINGDB.USERS");
            DropForeignKey("E_LEARNINGDB.USER_ROLE", "ROLE_ID", "E_LEARNINGDB.ROLE");
            DropForeignKey("E_LEARNINGDB.ROLE_PERMISSION_RESOURCES", "RoleID", "E_LEARNINGDB.ROLE");
            DropForeignKey("E_LEARNINGDB.ROLE_PERMISSION_RESOURCES", "ResourcesID", "E_LEARNINGDB.RESOURCES");
            DropForeignKey("E_LEARNINGDB.ROLE_PERMISSION_RESOURCES", "PermissionID", "E_LEARNINGDB.PERMISSION");
            DropForeignKey("E_LEARNINGDB.USER_INFO", "USER_ID", "E_LEARNINGDB.USERS");
            DropForeignKey("E_LEARNINGDB.USER_ANSWERS", "User_UserID", "E_LEARNINGDB.USERS");
            DropForeignKey("E_LEARNINGDB.USER_ANSWERS", "QUIZ_ATTEMPT_ID", "E_LEARNINGDB.QUIZ_ATTEMPT");
            DropForeignKey("E_LEARNINGDB.USER_ANSWERS", "QUESTIONS_ID", "E_LEARNINGDB.QUESTIONS");
            DropForeignKey("E_LEARNINGDB.USER_ANSWERS", "ANSWER_OPTIONS_ID", "E_LEARNINGDB.ANSWER_OPTIONS");
            DropForeignKey("E_LEARNINGDB.QUIZ_ATTEMPT", "USER_ID", "E_LEARNINGDB.USERS");
            DropForeignKey("E_LEARNINGDB.QUIZ_ATTEMPT", "QUIZZES_ID", "E_LEARNINGDB.QUIZZES");
            DropForeignKey("E_LEARNINGDB.LECTURE_COURSE", "USER_ID", "E_LEARNINGDB.USERS");
            DropForeignKey("E_LEARNINGDB.LECTURE_COURSE", "COURSE_ID", "E_LEARNINGDB.COURSE");
            DropForeignKey("E_LEARNINGDB.ENROLLMENTS", "USER_ID", "E_LEARNINGDB.USERS");
            DropForeignKey("E_LEARNINGDB.ENROLLMENTS", "ENROLLMENT_STATUS_ID", "E_LEARNINGDB.ENROLLMENT_STATUS");
            DropForeignKey("E_LEARNINGDB.ENROLLMENTS", "COURSE_ID", "E_LEARNINGDB.COURSE");
            DropForeignKey("E_LEARNINGDB.ATTENDANCE", "USER_ID", "E_LEARNINGDB.USERS");
            DropForeignKey("E_LEARNINGDB.ATTENDANCE", "SESSION_ID", "E_LEARNINGDB.SESSION");
            DropForeignKey("E_LEARNINGDB.SESSION", "INTERACTIVE_CLASS_ID", "E_LEARNINGDB.INTERACTIVE_CLASS");
            DropForeignKey("E_LEARNINGDB.SUBMISSION", "USER_ID", "E_LEARNINGDB.USERS");
            DropForeignKey("E_LEARNINGDB.GRADE", "USER_ID", "E_LEARNINGDB.USERS");
            DropForeignKey("E_LEARNINGDB.GRADE", "SUBMISSION_ID", "E_LEARNINGDB.SUBMISSION");
            DropForeignKey("E_LEARNINGDB.SUBMISSION", "ASSIGNMENT_ID", "E_LEARNINGDB.ASSIGNMENT");
            DropForeignKey("E_LEARNINGDB.ASSIGNMENT", "SESSION_ID", "E_LEARNINGDB.SESSION");
            DropForeignKey("E_LEARNINGDB.ORDER", "ORDER_STATUS_ID", "E_LEARNINGDB.ORDER_STATUS");
            DropForeignKey("E_LEARNINGDB.ORDER_DETAIL", "COURSE_ID ", "E_LEARNINGDB.COURSE");
            DropForeignKey("E_LEARNINGDB.COURSE_PROMOTION", "COURSE_ID", "E_LEARNINGDB.COURSE");
            DropIndex("E_LEARNINGDB.ROLE_PERMISSION_RESOURCES", new[] { "ResourcesID" });
            DropIndex("E_LEARNINGDB.ROLE_PERMISSION_RESOURCES", new[] { "RoleID" });
            DropIndex("E_LEARNINGDB.ROLE_PERMISSION_RESOURCES", new[] { "PermissionID" });
            DropIndex("E_LEARNINGDB.USER_ROLE", new[] { "ROLE_ID" });
            DropIndex("E_LEARNINGDB.USER_ROLE", new[] { "USER_ID" });
            DropIndex("E_LEARNINGDB.USER_INFO", new[] { "USER_ID" });
            DropIndex("E_LEARNINGDB.USER_ANSWERS", new[] { "User_UserID" });
            DropIndex("E_LEARNINGDB.USER_ANSWERS", new[] { "QUIZ_ATTEMPT_ID" });
            DropIndex("E_LEARNINGDB.USER_ANSWERS", new[] { "QUESTIONS_ID" });
            DropIndex("E_LEARNINGDB.USER_ANSWERS", new[] { "ANSWER_OPTIONS_ID" });
            DropIndex("E_LEARNINGDB.QUIZ_ATTEMPT", new[] { "USER_ID" });
            DropIndex("E_LEARNINGDB.QUIZ_ATTEMPT", new[] { "QUIZZES_ID" });
            DropIndex("E_LEARNINGDB.LECTURE_COURSE", new[] { "COURSE_ID" });
            DropIndex("E_LEARNINGDB.LECTURE_COURSE", new[] { "USER_ID" });
            DropIndex("E_LEARNINGDB.ENROLLMENTS", new[] { "COURSE_ID" });
            DropIndex("E_LEARNINGDB.ENROLLMENTS", new[] { "ENROLLMENT_STATUS_ID" });
            DropIndex("E_LEARNINGDB.ENROLLMENTS", new[] { "USER_ID" });
            DropIndex("E_LEARNINGDB.INTERACTIVE_CLASS", new[] { "INTERACTIVE_CLASS_ID" });
            DropIndex("E_LEARNINGDB.GRADE", new[] { "SUBMISSION_ID" });
            DropIndex("E_LEARNINGDB.GRADE", new[] { "USER_ID" });
            DropIndex("E_LEARNINGDB.SUBMISSION", new[] { "USER_ID" });
            DropIndex("E_LEARNINGDB.SUBMISSION", new[] { "ASSIGNMENT_ID" });
            DropIndex("E_LEARNINGDB.ASSIGNMENT", new[] { "SESSION_ID" });
            DropIndex("E_LEARNINGDB.SESSION", new[] { "INTERACTIVE_CLASS_ID" });
            DropIndex("E_LEARNINGDB.ATTENDANCE", new[] { "SESSION_ID" });
            DropIndex("E_LEARNINGDB.ATTENDANCE", new[] { "USER_ID" });
            DropIndex("E_LEARNINGDB.ORDER", new[] { "ORDER_STATUS_ID" });
            DropIndex("E_LEARNINGDB.ORDER", new[] { "USER_ID" });
            DropIndex("E_LEARNINGDB.ORDER_DETAIL", new[] { "PROMOTION_ID" });
            DropIndex("E_LEARNINGDB.ORDER_DETAIL", "IX_COURSE_ID");
            DropIndex("E_LEARNINGDB.ORDER_DETAIL", new[] { "ORDER_ID" });
            DropIndex("E_LEARNINGDB.COURSE_PROMOTION", new[] { "PROMOTION_ID" });
            DropIndex("E_LEARNINGDB.COURSE_PROMOTION", new[] { "COURSE_ID" });
            DropIndex("E_LEARNINGDB.COURSE", new[] { "COURSE_STATUS_ID" });
            DropIndex("E_LEARNINGDB.COURSE", new[] { "COURSE_TYPE_ID" });
            DropIndex("E_LEARNINGDB.COURSE_VIDEO", new[] { "COURSE_VIDEO_ID" });
            DropIndex("E_LEARNINGDB.CHAPTER", new[] { "COURSE_VIDEO_ID" });
            DropIndex("E_LEARNINGDB.LESSION", new[] { "CHAPTER_ID" });
            DropIndex("E_LEARNINGDB.QUIZZES", new[] { "LESSION_ID" });
            DropIndex("E_LEARNINGDB.QUESTIONS", new[] { "QUIZZES_ID" });
            DropIndex("E_LEARNINGDB.ANSWER_OPTIONS", new[] { "QUESTIONS_ID" });
            DropTable("E_LEARNINGDB.COURSE_TYPE");
            DropTable("E_LEARNINGDB.COURSE_STATUS");
            DropTable("E_LEARNINGDB.RESOURCES");
            DropTable("E_LEARNINGDB.PERMISSION");
            DropTable("E_LEARNINGDB.ROLE_PERMISSION_RESOURCES");
            DropTable("E_LEARNINGDB.ROLE");
            DropTable("E_LEARNINGDB.USER_ROLE");
            DropTable("E_LEARNINGDB.USER_INFO");
            DropTable("E_LEARNINGDB.USER_ANSWERS");
            DropTable("E_LEARNINGDB.QUIZ_ATTEMPT");
            DropTable("E_LEARNINGDB.LECTURE_COURSE");
            DropTable("E_LEARNINGDB.ENROLLMENT_STATUS");
            DropTable("E_LEARNINGDB.ENROLLMENTS");
            DropTable("E_LEARNINGDB.INTERACTIVE_CLASS");
            DropTable("E_LEARNINGDB.GRADE");
            DropTable("E_LEARNINGDB.SUBMISSION");
            DropTable("E_LEARNINGDB.ASSIGNMENT");
            DropTable("E_LEARNINGDB.SESSION");
            DropTable("E_LEARNINGDB.ATTENDANCE");
            DropTable("E_LEARNINGDB.USERS");
            DropTable("E_LEARNINGDB.ORDER_STATUS");
            DropTable("E_LEARNINGDB.ORDER");
            DropTable("E_LEARNINGDB.ORDER_DETAIL");
            DropTable("E_LEARNINGDB.PROMOTION");
            DropTable("E_LEARNINGDB.COURSE_PROMOTION");
            DropTable("E_LEARNINGDB.COURSE");
            DropTable("E_LEARNINGDB.COURSE_VIDEO");
            DropTable("E_LEARNINGDB.CHAPTER");
            DropTable("E_LEARNINGDB.LESSION");
            DropTable("E_LEARNINGDB.QUIZZES");
            DropTable("E_LEARNINGDB.QUESTIONS");
            DropTable("E_LEARNINGDB.ANSWER_OPTIONS");
        }
    }
}
