namespace Data_Oracle.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<Data_Oracle.Context.OracleDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;

            // Cho phép tự động thêm/xóa/đổi cột nếu cần
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Data_Oracle.Context.OracleDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
