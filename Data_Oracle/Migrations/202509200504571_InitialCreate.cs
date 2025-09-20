namespace Data_Oracle.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
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
                "E_LEARNINGDB.RESOURCES",
                c => new
                    {
                        RESOURCES_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RESOURCES_NAME = c.String(nullable: false, maxLength: 20),
                        RESOURCES_DESCRIPTION = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.RESOURCES_ID);
            
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
                "E_LEARNINGDB.USERS",
                c => new
                    {
                        USER_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        USER_NAME = c.String(nullable: false, maxLength: 50),
                        EMAIL = c.String(nullable: false, maxLength: 80),
                        HASHPASSWORD = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.USER_ID);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("E_LEARNINGDB.ROLE_PERMISSION_RESOURCES", "RoleID", "E_LEARNINGDB.ROLE");
            DropForeignKey("E_LEARNINGDB.USER_ROLE", "USER_ID", "E_LEARNINGDB.USERS");
            DropForeignKey("E_LEARNINGDB.USER_INFO", "USER_ID", "E_LEARNINGDB.USERS");
            DropForeignKey("E_LEARNINGDB.USER_ROLE", "ROLE_ID", "E_LEARNINGDB.ROLE");
            DropForeignKey("E_LEARNINGDB.ROLE_PERMISSION_RESOURCES", "ResourcesID", "E_LEARNINGDB.RESOURCES");
            DropForeignKey("E_LEARNINGDB.ROLE_PERMISSION_RESOURCES", "PermissionID", "E_LEARNINGDB.PERMISSION");
            DropIndex("E_LEARNINGDB.USER_INFO", new[] { "USER_ID" });
            DropIndex("E_LEARNINGDB.USER_ROLE", new[] { "ROLE_ID" });
            DropIndex("E_LEARNINGDB.USER_ROLE", new[] { "USER_ID" });
            DropIndex("E_LEARNINGDB.ROLE_PERMISSION_RESOURCES", new[] { "ResourcesID" });
            DropIndex("E_LEARNINGDB.ROLE_PERMISSION_RESOURCES", new[] { "RoleID" });
            DropIndex("E_LEARNINGDB.ROLE_PERMISSION_RESOURCES", new[] { "PermissionID" });
            DropTable("E_LEARNINGDB.USER_INFO");
            DropTable("E_LEARNINGDB.USERS");
            DropTable("E_LEARNINGDB.USER_ROLE");
            DropTable("E_LEARNINGDB.ROLE");
            DropTable("E_LEARNINGDB.RESOURCES");
            DropTable("E_LEARNINGDB.ROLE_PERMISSION_RESOURCES");
            DropTable("E_LEARNINGDB.PERMISSION");
        }
    }
}
