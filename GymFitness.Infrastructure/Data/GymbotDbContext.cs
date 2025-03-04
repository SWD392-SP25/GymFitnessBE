using System;
using System.Collections.Generic;
using GymFitness.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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

    public virtual DbSet<AuditLog> AuditLogs { get; set; }

    public virtual DbSet<ChatHistory> ChatHistories { get; set; }

    public virtual DbSet<Exercise> Exercises { get; set; }

    public virtual DbSet<ExerciseCategory> ExerciseCategories { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<MuscleGroup> MuscleGroups { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Specialization> Specializations { get; set; }

    public virtual DbSet<Staff> Staffs { get; set; }

    public virtual DbSet<StaffSchedule> StaffSchedules { get; set; }

    public virtual DbSet<StaffSpecialization> StaffSpecializations { get; set; }

    public virtual DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }

    public virtual DbSet<SystemSetting> SystemSettings { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserMeasurement> UserMeasurements { get; set; }

    public virtual DbSet<UserSubscription> UserSubscriptions { get; set; }

    public virtual DbSet<WorkoutLog> WorkoutLogs { get; set; }

    public virtual DbSet<WorkoutPlan> WorkoutPlans { get; set; }

    public virtual DbSet<WorkoutPlanExercise> WorkoutPlanExercises { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseSqlServer(GetConnectionString());

    private string GetConnectionString()
    {
        IConfiguration config = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true, true)
                    .Build();
        var strConn = config["ConnectionStrings:DefaultConnection"];

        return strConn;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.AppointmentId).HasName("PK__appointm__A50828FC78DDC8A4");

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
                .HasConstraintName("FK__appointme__staff__00200768");

            entity.HasOne(d => d.Type).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("FK__appointme__type___01142BA1");

            entity.HasOne(d => d.User).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__appointme__user___02084FDA");
        });

        modelBuilder.Entity<AppointmentType>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("PK__appointm__2C000598AAACF841");

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

        modelBuilder.Entity<AuditLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__audit_lo__9E2397E04FFEABCB");

            entity.ToTable("audit_logs");

            entity.Property(e => e.LogId).HasColumnName("log_id");
            entity.Property(e => e.Action)
                .HasMaxLength(100)
                .HasColumnName("action");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.IpAddress)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("ip_address");
            entity.Property(e => e.NewValues).HasColumnName("new_values");
            entity.Property(e => e.OldValues).HasColumnName("old_values");
            entity.Property(e => e.RecordId).HasColumnName("record_id");
            entity.Property(e => e.StaffId).HasColumnName("staff_id");
            entity.Property(e => e.TableName)
                .HasMaxLength(100)
                .HasColumnName("table_name");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Staff).WithMany(p => p.AuditLogs)
                .HasForeignKey(d => d.StaffId)
                .HasConstraintName("FK__audit_log__staff__02FC7413");

            entity.HasOne(d => d.User).WithMany(p => p.AuditLogs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__audit_log__user___03F0984C");
        });

        modelBuilder.Entity<ChatHistory>(entity =>
        {
            entity.HasKey(e => e.ChatId).HasName("PK__chat_his__FD040B177E760EA9");

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
                .HasConstraintName("FK__chat_hist__staff__04E4BC85");

            entity.HasOne(d => d.User).WithMany(p => p.ChatHistories)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__chat_hist__user___05D8E0BE");
        });

        modelBuilder.Entity<Exercise>(entity =>
        {
            entity.HasKey(e => e.ExerciseId).HasName("PK__exercise__C121418E18612C2B");

            entity.ToTable("exercises");

            entity.Property(e => e.ExerciseId).HasColumnName("exercise_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.DifficultyLevel)
                .HasMaxLength(20)
                .HasColumnName("difficulty_level");
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
                .HasConstraintName("FK__exercises__categ__06CD04F7");

            entity.HasOne(d => d.MuscleGroup).WithMany(p => p.Exercises)
                .HasForeignKey(d => d.MuscleGroupId)
                .HasConstraintName("FK__exercises__muscl__07C12930");
        });

        modelBuilder.Entity<ExerciseCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__exercise__D54EE9B413B56A8A");

            entity.ToTable("exercise_categories");

            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.InvoiceId).HasName("PK__invoices__F58DFD4959583694");

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
                .HasConstraintName("FK__invoices__paymen__08B54D69");

            entity.HasOne(d => d.Subscription).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.SubscriptionId)
                .HasConstraintName("FK__invoices__subscr__09A971A2");

            entity.HasOne(d => d.User).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__invoices__user_i__0A9D95DB");
        });

        modelBuilder.Entity<MuscleGroup>(entity =>
        {
            entity.HasKey(e => e.MuscleGroupId).HasName("PK__muscle_g__9DFCBF40F1E2EAF0");

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

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__notifica__E059842F0CA94DB9");

            entity.ToTable("notifications");

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

            entity.HasOne(d => d.Staff).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.StaffId)
                .HasConstraintName("FK__notificat__staff__0B91BA14");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__notificat__user___0C85DE4D");
        });

        modelBuilder.Entity<PaymentMethod>(entity =>
        {
            entity.HasKey(e => e.MethodId).HasName("PK__payment___747727B603906422");

            entity.ToTable("payment_methods");

            entity.Property(e => e.MethodId).HasColumnName("method_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.ExpiryDate).HasColumnName("expiry_date");
            entity.Property(e => e.IsDefault)
                .HasDefaultValue(false)
                .HasColumnName("is_default");
            entity.Property(e => e.LastFour)
                .HasMaxLength(4)
                .HasColumnName("last_four");
            entity.Property(e => e.Provider)
                .HasMaxLength(50)
                .HasColumnName("provider");
            entity.Property(e => e.TokenReference)
                .HasMaxLength(255)
                .HasColumnName("token_reference");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.PaymentMethods)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__payment_m__user___0D7A0286");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.PermissionId).HasName("PK__permissi__E5331AFA773BE053");

            entity.ToTable("permissions");

            entity.HasIndex(e => e.Name, "UQ__permissi__72E12F1B29AE3D32").IsUnique();

            entity.Property(e => e.PermissionId).HasColumnName("permission_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__roles__760965CC991A1343");

            entity.ToTable("roles");

            entity.HasIndex(e => e.Name, "UQ__roles__72E12F1B0EE20AB1").IsUnique();

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");

            entity.HasMany(d => d.Permissions).WithMany(p => p.Roles)
                .UsingEntity<Dictionary<string, object>>(
                    "RolePermission",
                    r => r.HasOne<Permission>().WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__role_perm__permi__0E6E26BF"),
                    l => l.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__role_perm__role___0F624AF8"),
                    j =>
                    {
                        j.HasKey("RoleId", "PermissionId").HasName("PK__role_per__C85A5463C58A847E");
                        j.ToTable("role_permissions");
                        j.IndexerProperty<int>("RoleId").HasColumnName("role_id");
                        j.IndexerProperty<int>("PermissionId").HasColumnName("permission_id");
                    });
        });

        modelBuilder.Entity<Specialization>(entity =>
        {
            entity.HasKey(e => e.SpecializationId).HasName("PK__speciali__0E5BB6501ACA1145");

            entity.ToTable("specializations");

            entity.Property(e => e.SpecializationId).HasColumnName("specialization_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.StaffId).HasName("PK__staffs__1963DD9CAD81D86B");

            entity.ToTable("staffs");

            entity.HasIndex(e => e.Email, "UQ__staffs__AB6E6164476952A6").IsUnique();

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
                .HasConstraintName("FK__staffs__role_id__1332DBDC");

            entity.HasOne(d => d.Supervisor).WithMany(p => p.InverseSupervisor)
                .HasForeignKey(d => d.SupervisorId)
                .HasConstraintName("FK__staffs__supervis__14270015");
        });

        modelBuilder.Entity<StaffSchedule>(entity =>
        {
            entity.HasKey(e => e.ScheduleId).HasName("PK__staff_sc__C46A8A6F85CD4A8B");

            entity.ToTable("staff_schedules");

            entity.Property(e => e.ScheduleId).HasColumnName("schedule_id");
            entity.Property(e => e.DayOfWeek).HasColumnName("day_of_week");
            entity.Property(e => e.EndTime).HasColumnName("end_time");
            entity.Property(e => e.IsAvailable)
                .HasDefaultValue(true)
                .HasColumnName("is_available");
            entity.Property(e => e.Location)
                .HasMaxLength(100)
                .HasColumnName("location");
            entity.Property(e => e.StaffId).HasColumnName("staff_id");
            entity.Property(e => e.StartTime).HasColumnName("start_time");

            entity.HasOne(d => d.Staff).WithMany(p => p.StaffSchedules)
                .HasForeignKey(d => d.StaffId)
                .HasConstraintName("FK__staff_sch__staff__10566F31");
        });

        modelBuilder.Entity<StaffSpecialization>(entity =>
        {
            entity.HasKey(e => new { e.StaffId, e.SpecializationId }).HasName("PK__staff_sp__098666F988951760");

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
                .HasConstraintName("FK__staff_spe__speci__114A936A");

            entity.HasOne(d => d.Staff).WithMany(p => p.StaffSpecializations)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__staff_spe__staff__123EB7A3");
        });

        modelBuilder.Entity<SubscriptionPlan>(entity =>
        {
            entity.HasKey(e => e.PlanId).HasName("PK__subscrip__BE9F8F1D1ED71177");

            entity.ToTable("subscription_plans");

            entity.Property(e => e.PlanId).HasColumnName("plan_id");
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

        modelBuilder.Entity<SystemSetting>(entity =>
        {
            entity.HasKey(e => e.SettingId).HasName("PK__system_s__256E1E321F06CE12");

            entity.ToTable("system_settings");

            entity.HasIndex(e => e.SystemKey, "UQ__system_s__B5998962F167B2AE").IsUnique();

            entity.Property(e => e.SettingId).HasColumnName("setting_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.SystemKey)
                .HasMaxLength(100)
                .HasColumnName("system_key");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.Value).HasColumnName("value");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.SystemSettings)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("FK__system_se__updat__151B244E");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__transact__85C600AFB6567943");

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
                .HasConstraintName("FK__transacti__invoi__160F4887");

            entity.HasOne(d => d.PaymentMethod).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.PaymentMethodId)
                .HasConstraintName("FK__transacti__payme__17036CC0");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__users__B9BE370F3B0920CE");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "UQ__users__AB6E616461C2FFF9").IsUnique();

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
                .HasConstraintName("FK__users__role_id__1AD3FDA4");
        });

        modelBuilder.Entity<UserMeasurement>(entity =>
        {
            entity.HasKey(e => e.MeasurementId).HasName("PK__user_mea__E3D1E1C14B6C1FB7");

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
                .HasConstraintName("FK__user_meas__user___17F790F9");
        });

        modelBuilder.Entity<UserSubscription>(entity =>
        {
            entity.HasKey(e => e.SubscriptionId).HasName("PK__user_sub__863A7EC1DD13FDAE");

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
            entity.Property(e => e.PlanId).HasColumnName("plan_id");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasColumnName("status");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Plan).WithMany(p => p.UserSubscriptions)
                .HasForeignKey(d => d.PlanId)
                .HasConstraintName("FK__user_subs__plan___18EBB532");

            entity.HasOne(d => d.User).WithMany(p => p.UserSubscriptions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__user_subs__user___19DFD96B");
        });

        modelBuilder.Entity<WorkoutLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__workout___9E2397E014600D6B");

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
                .HasConstraintName("FK__workout_l__exerc__1BC821DD");

            entity.HasOne(d => d.User).WithMany(p => p.WorkoutLogs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__workout_l__user___1CBC4616");
        });

        modelBuilder.Entity<WorkoutPlan>(entity =>
        {
            entity.HasKey(e => e.PlanId).HasName("PK__workout___BE9F8F1D135611B0");

            entity.ToTable("workout_plans");

            entity.Property(e => e.PlanId).HasColumnName("plan_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.DifficultyLevel)
                .HasMaxLength(20)
                .HasColumnName("difficulty_level");
            entity.Property(e => e.DurationWeeks).HasColumnName("duration_weeks");
            entity.Property(e => e.Goals).HasColumnName("goals");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
            entity.Property(e => e.Prerequisites).HasColumnName("prerequisites");
            entity.Property(e => e.TargetAudience).HasColumnName("target_audience");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.WorkoutPlans)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__workout_p__creat__1F98B2C1");
        });

        modelBuilder.Entity<WorkoutPlanExercise>(entity =>
        {
            entity.HasKey(e => new { e.PlanId, e.ExerciseId, e.WeekNumber, e.DayOfWeek }).HasName("PK__workout___04BCC65665D6202D");

            entity.ToTable("workout_plan_exercises");

            entity.Property(e => e.PlanId).HasColumnName("plan_id");
            entity.Property(e => e.ExerciseId).HasColumnName("exercise_id");
            entity.Property(e => e.WeekNumber).HasColumnName("week_number");
            entity.Property(e => e.DayOfWeek).HasColumnName("day_of_week");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.Reps).HasColumnName("reps");
            entity.Property(e => e.RestTimeSeconds).HasColumnName("rest_time_seconds");
            entity.Property(e => e.Sets).HasColumnName("sets");

            entity.HasOne(d => d.Exercise).WithMany(p => p.WorkoutPlanExercises)
                .HasForeignKey(d => d.ExerciseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__workout_p__exerc__1DB06A4F");

            entity.HasOne(d => d.Plan).WithMany(p => p.WorkoutPlanExercises)
                .HasForeignKey(d => d.PlanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__workout_p__plan___1EA48E88");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
