﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaskManagementSystem.Infrastructure.Contexts;

namespace TaskManagementSystem.Infrastructure.Migrations
{
    [DbContext(typeof(TasksContext))]
    [Migration("20181228043506_TasksTableMigration")]
    partial class TasksTableMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TaskManagementSystem.Infrastructure.Entities.ManagedTask", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("DateOfCompletion");

                    b.Property<DateTime>("DateOfIssue");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<Guid?>("ParentTaskId");

                    b.HasKey("Id");

                    b.HasIndex("ParentTaskId");

                    b.ToTable("ManagedTask");
                });

            modelBuilder.Entity("TaskManagementSystem.Infrastructure.Entities.ManagedTask", b =>
                {
                    b.HasOne("TaskManagementSystem.Infrastructure.Entities.ManagedTask", "ParentTask")
                        .WithMany("SubTasks")
                        .HasForeignKey("ParentTaskId");
                });
#pragma warning restore 612, 618
        }
    }
}
