﻿using System;
using System.Collections.Generic;
using GymFitness.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GymFitness.Infrastructure.Data;

public partial class GymbotDbContext : DbContext
{
    public GymbotDbContext()
    {
    }

    public GymbotDbContext(DbContextOptions<GymbotDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<AppointmentType> AppointmentTypes { get; set; }

    public virtual DbSet<ChatHistory> ChatHistories { get; set; }

    public virtual DbSet<DeviceToken> DeviceTokens { get; set; }

    public virtual DbSet<Exercise> Exercises { get; set; }

    public virtual DbSet<ExerciseCategory> ExerciseCategories { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<MuscleGroup> MuscleGroups { get; set; }

    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

    public virtual DbSet<RegisteredDevice> RegisteredDevices { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Specialization> Specializations { get; set; }

    public virtual DbSet<Staff> Staffs { get; set; }

    public virtual DbSet<StaffSpecialization> StaffSpecializations { get; set; }

    public virtual DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserMeasurement> UserMeasurements { get; set; }

    public virtual DbSet<UserSubscription> UserSubscriptions { get; set; }

    public virtual DbSet<WorkoutLog> WorkoutLogs { get; set; }

    public virtual DbSet<WorkoutPlan> WorkoutPlans { get; set; }

    public virtual DbSet<WorkoutPlanExercise> WorkoutPlanExercises { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.AppointmentId).HasName("PK__appointm__A50828FC989615CD");

            entity.ToTable("appointments");

            entity.Property(e => e.AppointmentId).HasColumnName("appointment_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.EndTime).HasColumnName("end_time");
            entity.Property(e => e.Location)
                .HasMaxLength(100)
                .HasColumnName("location");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.StaffId).HasColumnName("staff_id");
            entity.Property(e => e.StartTime).HasColumnName("start_time");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasColumnName("status");
            entity.Property(e => e.TypeId).HasColumnName("type_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Staff).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.StaffId)
                .HasConstraintName("FK__appointme__staff__71D1E811");

            entity.HasOne(d => d.Type).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("FK__appointme__type___72C60C4A");

            entity.HasOne(d => d.User).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__appointme__user___73BA3083");
        });

        modelBuilder.Entity<AppointmentType>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("PK__appointm__2C000598DD3241A4");

            entity.ToTable("appointment_types");

            entity.Property(e => e.TypeId).HasColumnName("type_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.DurationMinutes).HasColumnName("duration_minutes");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
        });

        modelBuilder.Entity<ChatHistory>(entity =>
        {
            entity.HasKey(e => e.ChatId).HasName("PK__chat_his__FD040B17902CE272");

            entity.ToTable("chat_histories");

            entity.Property(e => e.ChatId).HasColumnName("chat_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.IsUserMessage).HasColumnName("is_user_message");
            entity.Property(e => e.MessageText).HasColumnName("message_text");
            entity.Property(e => e.MessageType)
                .HasMaxLength(50)
                .HasColumnName("message_type");
            entity.Property(e => e.StaffId).HasColumnName("staff_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Staff).WithMany(p => p.ChatHistories)
                .HasForeignKey(d => d.StaffId)
                .HasConstraintName("FK__chat_hist__staff__74AE54BC");

            entity.HasOne(d => d.User).WithMany(p => p.ChatHistories)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__chat_hist__user___75A278F5");
        });

        modelBuilder.Entity<DeviceToken>(entity =>
        {
            entity.ToTable("device_token");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.DeviceToken1)
                .IsUnicode(false)
                .HasColumnName("device_token");
        });

        modelBuilder.Entity<Exercise>(entity =>
        {
            entity.HasKey(e => e.ExerciseId).HasName("PK__exercise__C121418EE7EE2EF4");

            entity.ToTable("exercises");

            entity.Property(e => e.ExerciseId).HasColumnName("exercise_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.DifficultyLevel).HasColumnName("difficulty_level");
            entity.Property(e => e.EquipmentNeeded).HasColumnName("equipment_needed");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("image_url");
            entity.Property(e => e.Instructions).HasColumnName("instructions");
            entity.Property(e => e.MuscleGroupId).HasColumnName("muscle_group_id");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
            entity.Property(e => e.Precautions).HasColumnName("precautions");
            entity.Property(e => e.VideoUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("video_url");

            entity.HasOne(d => d.Category).WithMany(p => p.Exercises)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__exercises__categ__76969D2E");

            entity.HasOne(d => d.MuscleGroup).WithMany(p => p.Exercises)
                .HasForeignKey(d => d.MuscleGroupId)
                .HasConstraintName("FK__exercises__muscl__778AC167");
        });

        modelBuilder.Entity<ExerciseCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__exercise__D54EE9B4AD5C5745");

            entity.ToTable("exercise_categories");

            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.InvoiceId).HasName("PK__invoices__F58DFD49F853254D");

            entity.ToTable("invoices");

            entity.Property(e => e.InvoiceId).HasColumnName("invoice_id");
            entity.Property(e => e.Amount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("amount");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.DueDate).HasColumnName("due_date");
            entity.Property(e => e.PaidDate).HasColumnName("paid_date");
            entity.Property(e => e.PaymentMethodId).HasColumnName("payment_method_id");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasColumnName("status");
            entity.Property(e => e.SubscriptionId).HasColumnName("subscription_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.PaymentMethod).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.PaymentMethodId)
                .HasConstraintName("FK__invoices__paymen__787EE5A0");

            entity.HasOne(d => d.Subscription).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.SubscriptionId)
                .HasConstraintName("FK__invoices__subscr__797309D9");

            entity.HasOne(d => d.User).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__invoices__user_i__7A672E12");
        });

        modelBuilder.Entity<MuscleGroup>(entity =>
        {
            entity.HasKey(e => e.MuscleGroupId).HasName("PK__muscle_g__9DFCBF402BA68820");

            entity.ToTable("muscle_groups");

            entity.Property(e => e.MuscleGroupId).HasColumnName("muscle_group_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("image_url");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<PaymentMethod>(entity =>
        {
            entity.HasKey(e => e.MethodId).HasName("PK__payment___747727B62F880F3E");

            entity.ToTable("payment_methods");

            entity.Property(e => e.MethodId).HasColumnName("method_id");
            entity.Property(e => e.Detail)
                .HasMaxLength(50)
                .HasColumnName("detail");
            entity.Property(e => e.MethodName)
                .HasMaxLength(50)
                .HasColumnName("method_name");
        });

        modelBuilder.Entity<RegisteredDevice>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__register__E059842FBFDFDA6B");

            entity.ToTable("registered_devices");

            entity.Property(e => e.NotificationId).HasColumnName("notification_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.IsRead)
                .HasDefaultValue(false)
                .HasColumnName("is_read");
            entity.Property(e => e.Message).HasColumnName("message");
            entity.Property(e => e.StaffId).HasColumnName("staff_id");
            entity.Property(e => e.Title)
                .HasMaxLength(200)
                .HasColumnName("title");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Staff).WithMany(p => p.RegisteredDevices)
                .HasForeignKey(d => d.StaffId)
                .HasConstraintName("FK__registere__staff__7B5B524B");

            entity.HasOne(d => d.User).WithMany(p => p.RegisteredDevices)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__registere__user___7C4F7684");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__roles__760965CCF5E0785A");

            entity.ToTable("roles");

            entity.HasIndex(e => e.Name, "UQ__roles__72E12F1B6DC896AB").IsUnique();

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Specialization>(entity =>
        {
            entity.HasKey(e => e.SpecializationId).HasName("PK__speciali__0E5BB650DB1D2F2C");

            entity.ToTable("specializations");

            entity.Property(e => e.SpecializationId).HasColumnName("specialization_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.StaffId).HasName("PK__staffs__1963DD9CC11CA6CF");

            entity.ToTable("staffs");

            entity.HasIndex(e => e.Email, "UQ__staffs__AB6E6164EB4115BA").IsUnique();

            entity.Property(e => e.StaffId)
                .ValueGeneratedNever()
                .HasColumnName("staff_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.Department)
                .HasMaxLength(50)
                .HasColumnName("department");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .HasColumnName("first_name");
            entity.Property(e => e.HireDate).HasColumnName("hire_date");
            entity.Property(e => e.LastLogin).HasColumnName("last_login");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .HasColumnName("last_name");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Salary)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("salary");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasColumnName("status");
            entity.Property(e => e.SupervisorId).HasColumnName("supervisor_id");
            entity.Property(e => e.TerminationDate).HasColumnName("termination_date");

            entity.HasOne(d => d.Role).WithMany(p => p.Staff)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__staffs__role_id__7F2BE32F");

            entity.HasOne(d => d.Supervisor).WithMany(p => p.InverseSupervisor)
                .HasForeignKey(d => d.SupervisorId)
                .HasConstraintName("FK__staffs__supervis__00200768");
        });

        modelBuilder.Entity<StaffSpecialization>(entity =>
        {
            entity.HasKey(e => new { e.StaffId, e.SpecializationId }).HasName("PK__staff_sp__098666F9954E7B26");

            entity.ToTable("staff_specializations");

            entity.Property(e => e.StaffId).HasColumnName("staff_id");
            entity.Property(e => e.SpecializationId).HasColumnName("specialization_id");
            entity.Property(e => e.CertificationDate).HasColumnName("certification_date");
            entity.Property(e => e.CertificationNumber)
                .HasMaxLength(100)
                .HasColumnName("certification_number");
            entity.Property(e => e.ExpiryDate).HasColumnName("expiry_date");
            entity.Property(e => e.VerificationStatus)
                .HasMaxLength(20)
                .HasColumnName("verification_status");

            entity.HasOne(d => d.Specialization).WithMany(p => p.StaffSpecializations)
                .HasForeignKey(d => d.SpecializationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__staff_spe__speci__7D439ABD");

            entity.HasOne(d => d.Staff).WithMany(p => p.StaffSpecializations)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__staff_spe__staff__7E37BEF6");
        });

        modelBuilder.Entity<SubscriptionPlan>(entity =>
        {
            entity.HasKey(e => e.SubscriptionPlanId).HasName("PK__subscrip__BE9F8F1D7AEEB6A1");

            entity.ToTable("subscription_plans");

            entity.Property(e => e.SubscriptionPlanId).HasColumnName("subscription_plan_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.DurationMonths).HasColumnName("duration_months");
            entity.Property(e => e.Features).HasColumnName("features");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.MaxSessionsPerMonth).HasColumnName("max_sessions_per_month");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__transact__85C600AF5E3A00D7");

            entity.ToTable("transactions");

            entity.Property(e => e.TransactionId).HasColumnName("transaction_id");
            entity.Property(e => e.Amount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("amount");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.InvoiceId).HasColumnName("invoice_id");
            entity.Property(e => e.PaymentMethodId).HasColumnName("payment_method_id");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasColumnName("status");
            entity.Property(e => e.TransactionReference)
                .HasMaxLength(255)
                .HasColumnName("transaction_reference");

            entity.HasOne(d => d.Invoice).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.InvoiceId)
                .HasConstraintName("FK__transacti__invoi__01142BA1");

            entity.HasOne(d => d.PaymentMethod).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.PaymentMethodId)
                .HasConstraintName("FK__transacti__payme__02084FDA");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__users__B9BE370FEE1D468A");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "UQ__users__AB6E61645454A577").IsUnique();

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("user_id");
            entity.Property(e => e.AddressLine1)
                .HasMaxLength(255)
                .HasColumnName("address_line1");
            entity.Property(e => e.AddressLine2)
                .HasMaxLength(255)
                .HasColumnName("address_line2");
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .HasColumnName("city");
            entity.Property(e => e.Country)
                .HasMaxLength(100)
                .HasColumnName("country");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("created_at");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.EmergencyContactName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("emergency_contact_name");
            entity.Property(e => e.EmergencyContactPhone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("emergency_contact_phone");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .HasColumnName("first_name");
            entity.Property(e => e.Gender)
                .HasMaxLength(20)
                .HasColumnName("gender");
            entity.Property(e => e.LastLogin).HasColumnName("last_login");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .HasColumnName("last_name");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.PostalCode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("postal_code");
            entity.Property(e => e.ProfilePictureUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("profile_picture_url");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.State)
                .HasMaxLength(100)
                .HasColumnName("state");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasColumnName("status");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__users__role_id__05D8E0BE");
        });

        modelBuilder.Entity<UserMeasurement>(entity =>
        {
            entity.HasKey(e => e.MeasurementId).HasName("PK__user_mea__E3D1E1C12D57495A");

            entity.ToTable("user_measurements");

            entity.Property(e => e.MeasurementId).HasColumnName("measurement_id");
            entity.Property(e => e.ArmsCm)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("arms_cm");
            entity.Property(e => e.BodyFatPercentage)
                .HasColumnType("decimal(4, 2)")
                .HasColumnName("body_fat_percentage");
            entity.Property(e => e.ChestCm)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("chest_cm");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Height)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("height");
            entity.Property(e => e.HipsCm)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("hips_cm");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.ThighsCm)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("thighs_cm");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.WaistCm)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("waist_cm");
            entity.Property(e => e.Weight)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("weight");

            entity.HasOne(d => d.User).WithMany(p => p.UserMeasurements)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__user_meas__user___02FC7413");
        });

        modelBuilder.Entity<UserSubscription>(entity =>
        {
            entity.HasKey(e => e.SubscriptionId).HasName("PK__user_sub__863A7EC11D24D2DE");

            entity.ToTable("user_subscriptions");

            entity.Property(e => e.SubscriptionId).HasColumnName("subscription_id");
            entity.Property(e => e.AutoRenew)
                .HasDefaultValue(true)
                .HasColumnName("auto_renew");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.PaymentFrequency)
                .HasMaxLength(20)
                .HasColumnName("payment_frequency");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasColumnName("status");
            entity.Property(e => e.Sub)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("sub");
            entity.Property(e => e.SubscriptionPlanId).HasColumnName("subscription_plan_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.SubscriptionPlan).WithMany(p => p.UserSubscriptions)
                .HasForeignKey(d => d.SubscriptionPlanId)
                .HasConstraintName("FK_user_subscriptions_subscription_plans");

            entity.HasOne(d => d.User).WithMany(p => p.UserSubscriptions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__user_subs__user___04E4BC85");
        });

        modelBuilder.Entity<WorkoutLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__workout___9E2397E02DEBF542");

            entity.ToTable("workout_logs");

            entity.Property(e => e.LogId).HasColumnName("log_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.DifficultyRating).HasColumnName("difficulty_rating");
            entity.Property(e => e.DurationMinutes).HasColumnName("duration_minutes");
            entity.Property(e => e.ExerciseId).HasColumnName("exercise_id");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.RepsCompleted).HasColumnName("reps_completed");
            entity.Property(e => e.SetsCompleted).HasColumnName("sets_completed");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.WeightUsed)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("weight_used");

            entity.HasOne(d => d.Exercise).WithMany(p => p.WorkoutLogs)
                .HasForeignKey(d => d.ExerciseId)
                .HasConstraintName("FK__workout_l__exerc__06CD04F7");

            entity.HasOne(d => d.User).WithMany(p => p.WorkoutLogs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__workout_l__user___07C12930");
        });

        modelBuilder.Entity<WorkoutPlan>(entity =>
        {
            entity.HasKey(e => e.PlanId).HasName("PK__workout___BE9F8F1D18CD8FB8");

            entity.ToTable("workout_plans");

            entity.Property(e => e.PlanId).HasColumnName("plan_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.DifficultyLevel).HasColumnName("difficulty_level");
            entity.Property(e => e.DurationWeeks).HasColumnName("duration_weeks");
            entity.Property(e => e.Goals).HasColumnName("goals");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
            entity.Property(e => e.Prerequisites).HasColumnName("prerequisites");
            entity.Property(e => e.SubscriptionPlanId).HasColumnName("subscription_plan_id");
            entity.Property(e => e.TargetAudience).HasColumnName("target_audience");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.WorkoutPlans)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__workout_p__creat__0A9D95DB");

            entity.HasOne(d => d.SubscriptionPlan).WithMany(p => p.WorkoutPlans)
                .HasForeignKey(d => d.SubscriptionPlanId)
                .HasConstraintName("FK_workout_plans_subscription_plans1");
        });

        modelBuilder.Entity<WorkoutPlanExercise>(entity =>
        {
            entity.HasKey(e => new { e.PlanId, e.ExerciseId, e.WeekNumber, e.DayOfWeek }).HasName("PK__workout___04BCC656783E6AC1");

            entity.ToTable("workout_plan_exercises");

            entity.Property(e => e.PlanId).HasColumnName("plan_id");
            entity.Property(e => e.ExerciseId).HasColumnName("exercise_id");
            entity.Property(e => e.WeekNumber).HasColumnName("week_number");
            entity.Property(e => e.DayOfWeek).HasColumnName("day_of_week");
            entity.Property(e => e.Notes)
                .HasMaxLength(255)
                .HasColumnName("notes");
            entity.Property(e => e.Reps).HasColumnName("reps");
            entity.Property(e => e.RestTimeSeconds).HasColumnName("rest_time_seconds");
            entity.Property(e => e.Sets).HasColumnName("sets");

            entity.HasOne(d => d.Exercise).WithMany(p => p.WorkoutPlanExercises)
                .HasForeignKey(d => d.ExerciseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__workout_p__exerc__08B54D69");

            entity.HasOne(d => d.Plan).WithMany(p => p.WorkoutPlanExercises)
                .HasForeignKey(d => d.PlanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__workout_p__plan___09A971A2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
