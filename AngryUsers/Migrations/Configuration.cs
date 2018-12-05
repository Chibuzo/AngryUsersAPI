namespace AngryUsers.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Model;
    using System.Data.Entity.SqlServer;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AngryUsers.Models.AngryUsersContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            SetSqlGenerator("System.Data.SqlClient", new CustomSqlServerMigrationSqlGenerator());
        }

        protected override void Seed(AngryUsers.Models.AngryUsersContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }

    internal class CustomSqlServerMigrationSqlGenerator : SqlServerMigrationSqlGenerator
    {
        protected override void Generate(AddColumnOperation addColumnOperation)
        {
            SetCreatedAtColumn(addColumnOperation.Column);

            base.Generate(addColumnOperation);
        }

        //protected override void Generate(CreateTableOperation createTableOperation)
        //{
        //    SetCreatedAtColumn(createTableOperation.Columns);

        //    base.Generate(createTableOperation);
        //}

        //private void SetCreatedAtColumn(IList<ColumnModel> columns)
        //{
        //    throw new NotImplementedException();
        //}

        private static void SetCreatedUtcColumn(IEnumerable<ColumnModel> columns)
        {
            foreach (var columnModel in columns)
            {
                SetCreatedAtColumn(columnModel);
            }
        }

        private static void SetCreatedAtColumn(PropertyModel column)
        {
            if (column.Name == "CreatedAt")
            {
                column.DefaultValueSql = "GETUTCDATE()";
            }
        }
    }
}
