using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace TaskManagementSystem.Infrastructure.Contexts
{
    class TasksContextFactory : IDesignTimeDbContextFactory<TasksContext>
    {
        public TasksContext CreateDbContext(string[] args)
        {
            return CreateContext();
        }

        public static TasksContext CreateContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TasksContext>();

            string projectPath = AppDomain.CurrentDomain.BaseDirectory.Split(new string[] { @"bin\" }, StringSplitOptions.None)[0].Replace("Infrastructure", "Web");
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(projectPath)
                .AddJsonFile("appsettings.json")
                .Build();
            string connectionString = configuration.GetConnectionString("DbConnection");

            optionsBuilder.UseSqlServer(connectionString);
            return new TasksContext(optionsBuilder.Options);
        }
    }
}
