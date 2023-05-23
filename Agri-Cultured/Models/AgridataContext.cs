﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Agri_Cultured.Models;

public partial class AgridataContext : DbContext
{
    public AgridataContext()
    {
    }

    public AgridataContext(DbContextOptions<AgridataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aspnetrole> Aspnetroles { get; set; }

    public virtual DbSet<Aspnetroleclaim> Aspnetroleclaims { get; set; }

    public virtual DbSet<Aspnetuser> Aspnetusers { get; set; }

    public virtual DbSet<Aspnetuserclaim> Aspnetuserclaims { get; set; }

    public virtual DbSet<Aspnetuserlogin> Aspnetuserlogins { get; set; }

    public virtual DbSet<Aspnetusertoken> Aspnetusertokens { get; set; }

    public virtual DbSet<Efmigrationshistory> Efmigrationshistories { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<Expence> Expences { get; set; }

    public virtual DbSet<FertPest> FertPests { get; set; }

    public virtual DbSet<Income> Incomes { get; set; }

    public virtual DbSet<Irrigation> Irrigations { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<Plant> Plants { get; set; }

    public virtual DbSet<PlantsHasUser> PlantsHasUsers { get; set; }

    public virtual DbSet<Task> Tasks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql("name=ConnectionStrings:DefaultConnection", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.32-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Aspnetrole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("aspnetroles");

            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<Aspnetroleclaim>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("aspnetroleclaims");

            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.Aspnetroleclaims)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_AspNetRoleClaims_AspNetRoles_RoleId");
        });

        modelBuilder.Entity<Aspnetuser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("aspnetusers");

            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.LockoutEnd).HasMaxLength(6);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "Aspnetuserrole",
                    r => r.HasOne<Aspnetrole>().WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK_AspNetUserRoles_AspNetRoles_RoleId"),
                    l => l.HasOne<Aspnetuser>().WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_AspNetUserRoles_AspNetUsers_UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("aspnetuserroles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<Aspnetuserclaim>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("aspnetuserclaims");

            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.Aspnetuserclaims)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_AspNetUserClaims_AspNetUsers_UserId");
        });

        modelBuilder.Entity<Aspnetuserlogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("aspnetuserlogins");

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.ProviderKey).HasMaxLength(128);

            entity.HasOne(d => d.User).WithMany(p => p.Aspnetuserlogins)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_AspNetUserLogins_AspNetUsers_UserId");
        });

        modelBuilder.Entity<Aspnetusertoken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

            entity.ToTable("aspnetusertokens");

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.Name).HasMaxLength(128);

            entity.HasOne(d => d.User).WithMany(p => p.Aspnetusertokens)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_AspNetUserTokens_AspNetUsers_UserId");
        });

        modelBuilder.Entity<Efmigrationshistory>(entity =>
        {
            entity.HasKey(e => e.MigrationId).HasName("PRIMARY");

            entity.ToTable("__efmigrationshistory");

            entity.Property(e => e.MigrationId).HasMaxLength(150);
            entity.Property(e => e.ProductVersion).HasMaxLength(32);
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.EventId).HasName("PRIMARY");

            entity.ToTable("events");

            entity.HasIndex(e => e.EventId, "event_id_UNIQUE").IsUnique();

            entity.Property(e => e.EventId).HasColumnName("event_id");
            entity.Property(e => e.Comments)
                .HasMaxLength(250)
                .HasColumnName("comments");
            entity.Property(e => e.Damage)
                .HasMaxLength(100)
                .HasColumnName("damage");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
            entity.Property(e => e.PercDamage).HasColumnName("perc_damage");

            entity.HasMany(d => d.PlantsHasUsers).WithMany(p => p.EventsEvents)
                .UsingEntity<Dictionary<string, object>>(
                    "EventsHasPlant",
                    r => r.HasOne<PlantsHasUser>().WithMany()
                        .HasForeignKey("PlantsHasUserPlantsPlantId", "PlantsHasUserAspnetusersId")
                        .HasConstraintName("fk_events_has_plants_has_user_plants_has_user1"),
                    l => l.HasOne<Event>().WithMany()
                        .HasForeignKey("EventsEventId")
                        .HasConstraintName("fk_events_has_plants_has_user_events1"),
                    j =>
                    {
                        j.HasKey("EventsEventId", "PlantsHasUserPlantsPlantId", "PlantsHasUserAspnetusersId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });
                        j.ToTable("events_has_plants");
                        j.HasIndex(new[] { "EventsEventId" }, "fk_events_has_plants_has_user_events1_idx");
                        j.HasIndex(new[] { "PlantsHasUserPlantsPlantId", "PlantsHasUserAspnetusersId" }, "fk_events_has_plants_has_user_plants_has_user1_idx");
                        j.IndexerProperty<int>("EventsEventId").HasColumnName("events_event_id");
                        j.IndexerProperty<int>("PlantsHasUserPlantsPlantId").HasColumnName("plants_has_user_plants_plant_Id");
                        j.IndexerProperty<string>("PlantsHasUserAspnetusersId").HasColumnName("plants_has_user_aspnetusers_Id");
                    });
        });

        modelBuilder.Entity<Expence>(entity =>
        {
            entity.HasKey(e => e.ExpencesId).HasName("PRIMARY");

            entity.ToTable("expences");

            entity.HasIndex(e => e.ExpencesId, "expences_Id_UNIQUE").IsUnique();

            entity.Property(e => e.ExpencesId).HasColumnName("expences_Id");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Expence1).HasColumnName("expence");
            entity.Property(e => e.ExpenceType)
                .HasMaxLength(45)
                .HasColumnName("expence_type");

            entity.HasMany(d => d.PlantsHasUsers).WithMany(p => p.ExpencesExpences)
                .UsingEntity<Dictionary<string, object>>(
                    "ExpencesHasPlant",
                    r => r.HasOne<PlantsHasUser>().WithMany()
                        .HasForeignKey("PlantsHasUserPlantsPlantId", "PlantsHasUserAspnetusersId")
                        .HasConstraintName("fk_expences_has_plants_has_user_plants_has_user1"),
                    l => l.HasOne<Expence>().WithMany()
                        .HasForeignKey("ExpencesExpencesId")
                        .HasConstraintName("fk_expences_has_plants_has_user_expences1"),
                    j =>
                    {
                        j.HasKey("ExpencesExpencesId", "PlantsHasUserPlantsPlantId", "PlantsHasUserAspnetusersId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });
                        j.ToTable("expences_has_plants");
                        j.HasIndex(new[] { "ExpencesExpencesId" }, "fk_expences_has_plants_has_user_expences1_idx");
                        j.HasIndex(new[] { "PlantsHasUserPlantsPlantId", "PlantsHasUserAspnetusersId" }, "fk_expences_has_plants_has_user_plants_has_user1_idx");
                        j.IndexerProperty<int>("ExpencesExpencesId").HasColumnName("expences_expences_Id");
                        j.IndexerProperty<int>("PlantsHasUserPlantsPlantId").HasColumnName("plants_has_user_plants_plant_Id");
                        j.IndexerProperty<string>("PlantsHasUserAspnetusersId").HasColumnName("plants_has_user_aspnetusers_Id");
                    });
        });

        modelBuilder.Entity<FertPest>(entity =>
        {
            entity.HasKey(e => e.FertPestId).HasName("PRIMARY");

            entity.ToTable("fert_pest");

            entity.HasIndex(e => e.FertPestId, "fert_pest_Id_UNIQUE").IsUnique();

            entity.HasIndex(e => e.ProductName, "product_name_UNIQUE").IsUnique();

            entity.Property(e => e.FertPestId).HasColumnName("fert_pest_Id");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.ProductName)
                .HasMaxLength(45)
                .HasColumnName("product_name");
            entity.Property(e => e.Type).HasColumnName("type");
        });

        modelBuilder.Entity<Income>(entity =>
        {
            entity.HasKey(e => e.IncomeId).HasName("PRIMARY");

            entity.ToTable("income");

            entity.HasIndex(e => e.IncomeId, "income_id_UNIQUE").IsUnique();

            entity.Property(e => e.IncomeId).HasColumnName("income_id");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Income1).HasColumnName("income");

            entity.HasMany(d => d.PlantsHasUsers).WithMany(p => p.IncomeIncomes)
                .UsingEntity<Dictionary<string, object>>(
                    "IncomeHasPlant",
                    r => r.HasOne<PlantsHasUser>().WithMany()
                        .HasForeignKey("PlantsHasUserPlantsPlantId", "PlantsHasUserAspnetusersId")
                        .HasConstraintName("fk_income_has_plants_has_user_plants_has_user1"),
                    l => l.HasOne<Income>().WithMany()
                        .HasForeignKey("IncomeIncomeId")
                        .HasConstraintName("fk_income_has_plants_has_user_income1"),
                    j =>
                    {
                        j.HasKey("IncomeIncomeId", "PlantsHasUserPlantsPlantId", "PlantsHasUserAspnetusersId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });
                        j.ToTable("income_has_plants");
                        j.HasIndex(new[] { "IncomeIncomeId" }, "fk_income_has_plants_has_user_income1_idx");
                        j.HasIndex(new[] { "PlantsHasUserPlantsPlantId", "PlantsHasUserAspnetusersId" }, "fk_income_has_plants_has_user_plants_has_user1_idx");
                        j.IndexerProperty<int>("IncomeIncomeId").HasColumnName("income_income_id");
                        j.IndexerProperty<int>("PlantsHasUserPlantsPlantId").HasColumnName("plants_has_user_plants_plant_Id");
                        j.IndexerProperty<string>("PlantsHasUserAspnetusersId").HasColumnName("plants_has_user_aspnetusers_Id");
                    });
        });

        modelBuilder.Entity<Irrigation>(entity =>
        {
            entity.HasKey(e => e.IrrigationsId).HasName("PRIMARY");

            entity.ToTable("irrigation");

            entity.HasIndex(e => e.IrrigationsId, "irrigations_Id_UNIQUE").IsUnique();

            entity.Property(e => e.IrrigationsId).HasColumnName("irrigations_Id");
            entity.Property(e => e.Cost).HasColumnName("cost");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Hours).HasColumnName("hours");

            entity.HasMany(d => d.PlantsHasUsers).WithMany(p => p.IrrigationIrrigations)
                .UsingEntity<Dictionary<string, object>>(
                    "OpIrrigation",
                    r => r.HasOne<PlantsHasUser>().WithMany()
                        .HasForeignKey("PlantsHasUserPlantsPlantId", "PlantsHasUserAspnetusersId")
                        .HasConstraintName("fk_irrigation_has_plants_has_user_plants_has_user1"),
                    l => l.HasOne<Irrigation>().WithMany()
                        .HasForeignKey("IrrigationIrrigationsId")
                        .HasConstraintName("fk_irrigation_has_plants_has_user_irrigation1"),
                    j =>
                    {
                        j.HasKey("IrrigationIrrigationsId", "PlantsHasUserPlantsPlantId", "PlantsHasUserAspnetusersId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });
                        j.ToTable("op_irrigation");
                        j.HasIndex(new[] { "IrrigationIrrigationsId" }, "fk_irrigation_has_plants_has_user_irrigation1_idx");
                        j.HasIndex(new[] { "PlantsHasUserPlantsPlantId", "PlantsHasUserAspnetusersId" }, "fk_irrigation_has_plants_has_user_plants_has_user1_idx");
                        j.IndexerProperty<int>("IrrigationIrrigationsId").HasColumnName("irrigation_irrigations_Id");
                        j.IndexerProperty<int>("PlantsHasUserPlantsPlantId").HasColumnName("plants_has_user_plants_plant_Id");
                        j.IndexerProperty<string>("PlantsHasUserAspnetusersId").HasColumnName("plants_has_user_aspnetusers_Id");
                    });
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => new { e.ItemId, e.FertPestFertPestId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("items");

            entity.HasIndex(e => e.FertPestFertPestId, "fk_items_fert_pest1_idx");

            entity.HasIndex(e => e.ItemId, "item_id_UNIQUE").IsUnique();

            entity.HasIndex(e => e.ItemName, "item_name_UNIQUE").IsUnique();

            entity.Property(e => e.ItemId)
                .ValueGeneratedOnAdd()
                .HasColumnName("item_id");
            entity.Property(e => e.FertPestFertPestId).HasColumnName("fert_pest_fert_pest_Id");
            entity.Property(e => e.Cost).HasColumnName("cost");
            entity.Property(e => e.ItemDescription)
                .HasMaxLength(45)
                .HasColumnName("item_description");
            entity.Property(e => e.ItemName)
                .HasMaxLength(45)
                .HasColumnName("item_name");

            entity.HasOne(d => d.FertPestFertPest).WithMany(p => p.Items)
                .HasForeignKey(d => d.FertPestFertPestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_items_fert_pest1");
        });

        modelBuilder.Entity<Plant>(entity =>
        {
            entity.HasKey(e => e.PlantId).HasName("PRIMARY");

            entity.ToTable("plants");

            entity.HasIndex(e => e.PlantId, "plant_Id_UNIQUE").IsUnique();

            entity.HasIndex(e => e.PlantName, "plant_name_UNIQUE").IsUnique();

            entity.Property(e => e.PlantId).HasColumnName("plant_Id");
            entity.Property(e => e.PlantName)
                .HasMaxLength(45)
                .HasColumnName("plant_name");
            entity.Property(e => e.PlantType)
                .HasMaxLength(45)
                .HasColumnName("plant_type");
        });

        modelBuilder.Entity<PlantsHasUser>(entity =>
        {
            entity.HasKey(e => new { e.PlantsPlantId, e.AspnetusersId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("plants_has_user");

            entity.HasIndex(e => e.AspnetusersId, "fk_plants_has_aspnetusers_aspnetusers1_idx");

            entity.HasIndex(e => e.PlantsPlantId, "fk_plants_has_aspnetusers_plants1_idx");

            entity.Property(e => e.PlantsPlantId).HasColumnName("plants_plant_Id");
            entity.Property(e => e.AspnetusersId).HasColumnName("aspnetusers_Id");
            entity.Property(e => e.DatePlanted).HasColumnName("date_planted");
            entity.Property(e => e.Location)
                .HasMaxLength(45)
                .HasColumnName("location");

            entity.HasOne(d => d.Aspnetusers).WithMany(p => p.PlantsHasUsers)
                .HasForeignKey(d => d.AspnetusersId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_plants_has_aspnetusers_aspnetusers1");

            entity.HasOne(d => d.PlantsPlant).WithMany(p => p.PlantsHasUsers)
                .HasForeignKey(d => d.PlantsPlantId)
                .HasConstraintName("fk_plants_has_aspnetusers_plants1");

            entity.HasMany(d => d.FertPestFertPests).WithMany(p => p.PlantsHasUsers)
                .UsingEntity<Dictionary<string, object>>(
                    "OpFertPest",
                    r => r.HasOne<FertPest>().WithMany()
                        .HasForeignKey("FertPestFertPestId")
                        .HasConstraintName("fk_plants_has_user_has_fert_pest_fert_pest1"),
                    l => l.HasOne<PlantsHasUser>().WithMany()
                        .HasForeignKey("PlantsHasUserPlantsPlantId", "PlantsHasUserAspnetusersId")
                        .HasConstraintName("fk_plants_has_user_has_fert_pest_plants_has_user1"),
                    j =>
                    {
                        j.HasKey("PlantsHasUserPlantsPlantId", "PlantsHasUserAspnetusersId", "FertPestFertPestId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });
                        j.ToTable("op_fert_pest");
                        j.HasIndex(new[] { "FertPestFertPestId" }, "fk_plants_has_user_has_fert_pest_fert_pest1_idx");
                        j.HasIndex(new[] { "PlantsHasUserPlantsPlantId", "PlantsHasUserAspnetusersId" }, "fk_plants_has_user_has_fert_pest_plants_has_user1_idx");
                        j.IndexerProperty<int>("PlantsHasUserPlantsPlantId").HasColumnName("plants_has_user_plants_plant_Id");
                        j.IndexerProperty<string>("PlantsHasUserAspnetusersId").HasColumnName("plants_has_user_aspnetusers_Id");
                        j.IndexerProperty<int>("FertPestFertPestId").HasColumnName("fert_pest_fert_pest_Id");
                    });
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.HasKey(e => e.TaskId).HasName("PRIMARY");

            entity.ToTable("tasks");

            entity.HasIndex(e => e.TaskId, "task_id_UNIQUE").IsUnique();

            entity.HasIndex(e => e.TaskName, "task_name_UNIQUE").IsUnique();

            entity.Property(e => e.TaskId).HasColumnName("task_id");
            entity.Property(e => e.DateStarted).HasColumnName("date_started");
            entity.Property(e => e.TaskName)
                .HasMaxLength(45)
                .HasColumnName("task_name");
            entity.Property(e => e.WorkerNumber).HasColumnName("worker_number");

            entity.HasMany(d => d.PlantsHasUsers).WithMany(p => p.TasksTasks)
                .UsingEntity<Dictionary<string, object>>(
                    "OpTask",
                    r => r.HasOne<PlantsHasUser>().WithMany()
                        .HasForeignKey("PlantsHasUserPlantsPlantId", "PlantsHasUserAspnetusersId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_tasks_has_plants_has_user_plants_has_user1"),
                    l => l.HasOne<Task>().WithMany()
                        .HasForeignKey("TasksTaskId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_tasks_has_plants_has_user_tasks1"),
                    j =>
                    {
                        j.HasKey("TasksTaskId", "PlantsHasUserPlantsPlantId", "PlantsHasUserAspnetusersId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });
                        j.ToTable("op_tasks");
                        j.HasIndex(new[] { "PlantsHasUserPlantsPlantId", "PlantsHasUserAspnetusersId" }, "fk_tasks_has_plants_has_user_plants_has_user1_idx");
                        j.HasIndex(new[] { "TasksTaskId" }, "fk_tasks_has_plants_has_user_tasks1_idx");
                        j.IndexerProperty<int>("TasksTaskId").HasColumnName("tasks_task_id");
                        j.IndexerProperty<int>("PlantsHasUserPlantsPlantId").HasColumnName("plants_has_user_plants_plant_Id");
                        j.IndexerProperty<string>("PlantsHasUserAspnetusersId").HasColumnName("plants_has_user_aspnetusers_Id");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}