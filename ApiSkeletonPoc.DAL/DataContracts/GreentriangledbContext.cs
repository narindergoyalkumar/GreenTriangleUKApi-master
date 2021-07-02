using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ApiSkeletonPoc.DAL.DataContracts
{
    public partial class GreenTriangleDbContext : DbContext
    {
        public GreenTriangleDbContext()
        {
        }

        public GreenTriangleDbContext(DbContextOptions<GreenTriangleDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<AddressType> AddressType { get; set; }
        public virtual DbSet<AmazonMwsConfig> AmazonMwsConfig { get; set; }
        public virtual DbSet<AmazonMwsProductsReOrderSettings> AmazonMwsProductsReOrderSettings { get; set; }
        public virtual DbSet<AmazonProducts> AmazonProducts { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<ClientModuleMapping> ClientModuleMapping { get; set; }
        public virtual DbSet<Contact> Contact { get; set; }
        public virtual DbSet<ContactNotes> ContactNotes { get; set; }
        public virtual DbSet<ContactType> ContactType { get; set; }
        public virtual DbSet<CustomField> CustomField { get; set; }
        public virtual DbSet<CustomFieldType> CustomFieldType { get; set; }
        public virtual DbSet<CustomFieldValue> CustomFieldValue { get; set; }
        public virtual DbSet<DeliveryStatusType> DeliveryStatusType { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Individual> Individual { get; set; }
        public virtual DbSet<Job> Job { get; set; }
        public virtual DbSet<JobEstimatorCategory> JobEstimatorCategory { get; set; }
        public virtual DbSet<JobEstimatorClient> JobEstimatorClient { get; set; }
        public virtual DbSet<JobEstimatorProductStyle> JobEstimatorProductStyle { get; set; }
        public virtual DbSet<JobEstimatorProductType> JobEstimatorProductType { get; set; }
        public virtual DbSet<JobFrequency> JobFrequency { get; set; }
        public virtual DbSet<JobNotes> JobNotes { get; set; }
        public virtual DbSet<JobStatus> JobStatus { get; set; }
        public virtual DbSet<JobType> JobType { get; set; }
        public virtual DbSet<LogEntry> LogEntry { get; set; }
        public virtual DbSet<Message> Message { get; set; }
        public virtual DbSet<Module> Module { get; set; }
        public virtual DbSet<MwsreportRequestInfo> MwsreportRequestInfo { get; set; }
        public virtual DbSet<Organization> Organization { get; set; }
        public virtual DbSet<Phone> Phone { get; set; }
        public virtual DbSet<PhoneType> PhoneType { get; set; }
        public virtual DbSet<SignatureDoc> SignatureDoc { get; set; }
        public virtual DbSet<SocialMedia> SocialMedia { get; set; }
        public virtual DbSet<SocialMediaType> SocialMediaType { get; set; }
        public virtual DbSet<Title> Title { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        public virtual DbSet<UserRoleMapping> UserRoleMapping { get; set; }
        public virtual DbSet<Valve> Valve { get; set; }
        public virtual DbSet<ValveEvent> ValveEvent { get; set; }
        public virtual DbSet<ValveEventType> ValveEventType { get; set; }
        public virtual DbSet<VisibleTo> VisibleTo { get; set; }
        public virtual DbSet<Visit> Visit { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("server=.\\SQLEXPRESS;database=GreenTriangleDb;Integrated Security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasIndex(e => e.AddressTypeId)
                    .HasName("fkIdx_Address_AddressTypeId");

                entity.HasIndex(e => e.ContactId)
                    .HasName("fkIdx_Address_ContactId");

                entity.Property(e => e.AddressId).HasColumnName("Address_ID");

                entity.Property(e => e.AddressTypeId).HasColumnName("Address_type_ID");

                entity.Property(e => e.City)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ContactId).HasColumnName("Contact_ID");

                entity.Property(e => e.Country)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.HouseNumberName)
                    .IsRequired()
                    .HasColumnName("House_number_name")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.LineOne)
                    .HasColumnName("Line_one")
                    .IsUnicode(false);

                entity.Property(e => e.Postcode)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.RecordCreatedDate)
                    .HasColumnName("Record_created_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.RecordUpdatedDate)
                    .HasColumnName("Record_updated_date")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.AddressType)
                    .WithMany(p => p.Address)
                    .HasForeignKey(d => d.AddressTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Address_AddressTypeId");

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.Address)
                    .HasForeignKey(d => d.ContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contact_ContactId");
            });

            modelBuilder.Entity<AddressType>(entity =>
            {
                entity.ToTable("Address_type");

                entity.Property(e => e.AddressTypeId).HasColumnName("Address_type_ID");

                entity.Property(e => e.AddressType1)
                    .IsRequired()
                    .HasColumnName("Address_type")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AmazonMwsConfig>(entity =>
            {
                entity.HasKey(e => e.MwsConfigId);

                entity.Property(e => e.MwsConfigId).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.MwsSellerName)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.MwsauthToken).HasColumnName("MWSAuthToken");
            });

            modelBuilder.Entity<AmazonMwsProductsReOrderSettings>(entity =>
            {
                entity.HasKey(e => e.ReOrderConfigId);

                entity.Property(e => e.ReOrderConfigId).HasColumnName("ReOrderConfig_Id");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ReOderDaysAlarmColorCode).HasMaxLength(50);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AmazonMwsProductsReOrderSettings)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AmazonMwsProductsReOrderSettings_User");
            });

            modelBuilder.Entity<AmazonProducts>(entity =>
            {
                entity.Property(e => e.Asin).IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CustomizedImage).IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).IsUnicode(false);

                entity.Property(e => e.OriginalImage).IsUnicode(false);

                entity.Property(e => e.ShortName).IsUnicode(false);

                entity.Property(e => e.WeeksOfCoverT180).IsUnicode(false);

                entity.Property(e => e.WeeksOfCoverT30).IsUnicode(false);

                entity.Property(e => e.WeeksOfCoverT365).IsUnicode(false);

                entity.Property(e => e.WeeksOfCoverT7).IsUnicode(false);

                entity.Property(e => e.WeeksOfCoverT90).IsUnicode(false);
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.Property(e => e.ClientId).HasColumnName("Client_ID");

                entity.Property(e => e.Name).IsUnicode(false);

                entity.Property(e => e.RecordCreatedDate)
                    .HasColumnName("Record_created_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.RecordUpdatedDate)
                    .HasColumnName("Record_updated_date")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<ClientModuleMapping>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.ClientModuleMapping)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK_UserModuleMapping_User");

                entity.HasOne(d => d.Module)
                    .WithMany(p => p.ClientModuleMapping)
                    .HasForeignKey(d => d.ModuleId)
                    .HasConstraintName("FK_UserModuleMapping_Module");
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.HasIndex(e => e.ClientId)
                    .HasName("fkIdx_Contact_ClientId");

                entity.HasIndex(e => e.ContactTypeId)
                    .HasName("fkIdx_Contact_ContactTypeId");

                entity.HasIndex(e => e.IndividualId)
                    .HasName("fkIdx_Contact_IndividualId");

                entity.HasIndex(e => e.OrgId)
                    .HasName("fkIdx_Contact_OrganisationId");

                entity.Property(e => e.ContactId).HasColumnName("Contact_ID");

                entity.Property(e => e.ClientId).HasColumnName("Client_ID");

                entity.Property(e => e.ContactTypeId).HasColumnName("Contact_type_ID");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IndividualId).HasColumnName("Individual_ID");

                entity.Property(e => e.OrgId).HasColumnName("Org_ID");

                entity.Property(e => e.RecordCreatedDate)
                    .HasColumnName("Record_created_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.RecordUpdatedDate)
                    .HasColumnName("Record_updated_date")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Contact)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contact_ClientId");

                entity.HasOne(d => d.ContactType)
                    .WithMany(p => p.Contact)
                    .HasForeignKey(d => d.ContactTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contact_ContactTypeId");

                entity.HasOne(d => d.Individual)
                    .WithMany(p => p.Contact)
                    .HasForeignKey(d => d.IndividualId)
                    .HasConstraintName("FK_Contact_IndividualId");

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.Contact)
                    .HasForeignKey(d => d.OrgId)
                    .HasConstraintName("FK_Contact_OrganisationId");
            });

            modelBuilder.Entity<ContactNotes>(entity =>
            {
                entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDateTime).HasColumnType("datetime");

                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.ContactNotes)
                    .HasForeignKey(d => d.ContactId)
                    .HasConstraintName("FK_ContactNotes_Contact");
            });

            modelBuilder.Entity<ContactType>(entity =>
            {
                entity.ToTable("Contact_type");

                entity.Property(e => e.ContactTypeId).HasColumnName("Contact_type_ID");

                entity.Property(e => e.ContactType1)
                    .IsRequired()
                    .HasColumnName("Contact_type")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CustomField>(entity =>
            {
                entity.Property(e => e.CustomFieldId).HasColumnName("CustomField_Id");

                entity.Property(e => e.ControlType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CustomFieldKey).IsUnicode(false);

                entity.Property(e => e.CustomFieldName).IsUnicode(false);

                entity.Property(e => e.Options).IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.VisibleToId).HasColumnName("VisibleTo_Id");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.CustomField)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK_CustomField_Value_Client");

                entity.HasOne(d => d.VisibleTo)
                    .WithMany(p => p.CustomField)
                    .HasForeignKey(d => d.VisibleToId)
                    .HasConstraintName("FK_CustomField_Value_VisibleTo");
            });

            modelBuilder.Entity<CustomFieldType>(entity =>
            {
                entity.Property(e => e.CustomFieldTypeId).HasColumnName("CustomField_Type_Id");

                entity.Property(e => e.CustomFieldType1)
                    .HasColumnName("CustomField_Type")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CustomFieldValue>(entity =>
            {
                entity.Property(e => e.CustomFieldValueId).HasColumnName("CustomFieldValue_Id");

                entity.Property(e => e.ContactId).HasColumnName("Contact_Id");

                entity.Property(e => e.CustomFieldId).HasColumnName("CustomField_Id");

                entity.Property(e => e.CustomFieldValue1).HasColumnName("CustomFieldValue");

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.CustomFieldValue)
                    .HasForeignKey(d => d.ContactId)
                    .HasConstraintName("FK_CustomFieldValue_Contact");

                entity.HasOne(d => d.CustomField)
                    .WithMany(p => p.CustomFieldValue)
                    .HasForeignKey(d => d.CustomFieldId)
                    .HasConstraintName("FK_CustomFieldValue_CustomField");
            });

            modelBuilder.Entity<DeliveryStatusType>(entity =>
            {
                entity.Property(e => e.DeliveryStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.EmployeeId).HasColumnName("Employee_ID");

                entity.Property(e => e.ClientId).HasColumnName("Client_Id");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("First_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasColumnName("Last_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NiNumber)
                    .IsRequired()
                    .HasColumnName("NI_Number")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.RecordCreatedDate)
                    .HasColumnName("Record_created_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.RecordUpdatedDate)
                    .HasColumnName("Record_updated_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_Client");
            });

            modelBuilder.Entity<Individual>(entity =>
            {
                entity.Property(e => e.IndividualId).HasColumnName("Individual_ID");

                entity.Property(e => e.AffiliateKey)
                    .HasColumnName("Affiliate_key")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("First_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.JobTitle)
                    .HasColumnName("Job_title")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasColumnName("Last_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OrgId).HasColumnName("Org_ID");

                entity.Property(e => e.RecordCreatedDate)
                    .HasColumnName("Record_created_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.RecordUpdatedDate)
                    .HasColumnName("Record_updated_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.TitleId).HasColumnName("Title_ID");

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.Individual)
                    .HasForeignKey(d => d.OrgId)
                    .HasConstraintName("FK_Individual_OrganisationId");

                entity.HasOne(d => d.Title)
                    .WithMany(p => p.Individual)
                    .HasForeignKey(d => d.TitleId)
                    .HasConstraintName("FK_Individual_TitleId");
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.Day)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.EndTime)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDateTime).HasColumnType("datetime");

                entity.Property(e => e.Name).IsUnicode(false);

                entity.Property(e => e.Reference).IsUnicode(false);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.StartTime)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.Job)
                    .HasForeignKey(d => d.ContactId)
                    .HasConstraintName("FK_Job_Contact");

                entity.HasOne(d => d.JobFrequency)
                    .WithMany(p => p.Job)
                    .HasForeignKey(d => d.JobFrequencyId)
                    .HasConstraintName("FK_Job_JobFrequency");

                entity.HasOne(d => d.JobStatus)
                    .WithMany(p => p.Job)
                    .HasForeignKey(d => d.JobStatusId)
                    .HasConstraintName("FK_Job_JobStatus");

                entity.HasOne(d => d.JobType)
                    .WithMany(p => p.Job)
                    .HasForeignKey(d => d.JobTypeId)
                    .HasConstraintName("FK_Job_JobType");
            });

            modelBuilder.Entity<JobEstimatorCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryId);

                entity.Property(e => e.CategoryId).HasColumnName("Category_ID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<JobEstimatorClient>(entity =>
            {
                entity.HasKey(e => e.ClientId);

                entity.HasIndex(e => e.ClientKey)
                    .HasName("IX_JobEstimatorClientKey_Unique")
                    .IsUnique();

                entity.Property(e => e.ClientId).HasColumnName("Client_ID");

                entity.Property(e => e.CategoryId).HasColumnName("Category_ID");

                entity.Property(e => e.ClientKey).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Disclaimer).IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Note).IsUnicode(false);

                entity.Property(e => e.Recipients).IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("User_ID");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.JobEstimatorClient)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_JobEstimatorClient_JobEstimatorCategory");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.JobEstimatorClient)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_JobEstimatorClient_User");
            });

            modelBuilder.Entity<JobEstimatorProductStyle>(entity =>
            {
                entity.HasKey(e => e.ProductStyleId);

                entity.Property(e => e.ProductStyleId).HasColumnName("ProductStyle_ID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.GroundCost).HasColumnType("money");

                entity.Property(e => e.GroundCostExcavate).HasColumnType("money");

                entity.Property(e => e.GroundCostFlat).HasColumnType("money");

                entity.Property(e => e.JobEstimatorClientId).HasColumnName("JobEstimatorClient_ID");

                entity.Property(e => e.LabourCost).HasColumnType("money");

                entity.Property(e => e.MaterialCost).HasColumnType("money");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ProductTypeId).HasColumnName("ProductType_ID");

                entity.Property(e => e.StyleImage)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UnitCost).HasColumnType("money");

                entity.HasOne(d => d.JobEstimatorClient)
                    .WithMany(p => p.JobEstimatorProductStyle)
                    .HasForeignKey(d => d.JobEstimatorClientId)
                    .HasConstraintName("FK_JobEstimatorProductStyle_JobEstimatorClient");

                entity.HasOne(d => d.ProductType)
                    .WithMany(p => p.JobEstimatorProductStyle)
                    .HasForeignKey(d => d.ProductTypeId)
                    .HasConstraintName("FK_JobEstimatorProductStyle_JobEstimatorProductType");
            });

            modelBuilder.Entity<JobEstimatorProductType>(entity =>
            {
                entity.HasKey(e => e.ProductTypeId)
                    .HasName("PK_JobEstimatorProduct");

                entity.Property(e => e.ProductTypeId).HasColumnName("ProductType_ID");

                entity.Property(e => e.CalculationMethod)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CategoryId).HasColumnName("Category_ID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.JobEstimatorProductType)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_JobEstimatorProduct_JobEstimatorCategory");
            });

            modelBuilder.Entity<JobFrequency>(entity =>
            {
                entity.Property(e => e.JobFrequencyId).ValueGeneratedNever();

                entity.Property(e => e.Frequency)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<JobNotes>(entity =>
            {
                entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDateTime).HasColumnType("datetime");

                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.JobNotes)
                    .HasForeignKey(d => d.JobId)
                    .HasConstraintName("FK_JobNotes_Job");
            });

            modelBuilder.Entity<JobStatus>(entity =>
            {
                entity.Property(e => e.JobStatusId).ValueGeneratedNever();

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<JobType>(entity =>
            {
                entity.Property(e => e.JobTypeId).ValueGeneratedNever();

                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LogEntry>(entity =>
            {
                entity.Property(e => e.LogText).IsUnicode(false);

                entity.Property(e => e.LoggedDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.Property(e => e.ContactId).HasColumnName("Contact_Id");

                entity.Property(e => e.DeliveredTime).HasColumnType("datetime");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.Receiver)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Sender)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SentDateTime).HasColumnType("datetime");

                entity.Property(e => e.TextMagicMessageId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.Message)
                    .HasForeignKey(d => d.ContactId)
                    .HasConstraintName("FK_Message_ContactId");

                entity.HasOne(d => d.DeliveryStatusType)
                    .WithMany(p => p.Message)
                    .HasForeignKey(d => d.DeliveryStatusTypeId)
                    .HasConstraintName("FK_Message_DeliveryStatusType");
            });

            modelBuilder.Entity<Module>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DisplayName).IsUnicode(false);

                entity.Property(e => e.MatIcon)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModuleRoute)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<MwsreportRequestInfo>(entity =>
            {
                entity.HasKey(e => e.ReportRequestInfoId);

                entity.ToTable("MWSReportRequestInfo");

                entity.Property(e => e.CompletedDate).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.GeneratedReportId).IsUnicode(false);

                entity.Property(e => e.ReportProcessingStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ReportRequestId).IsUnicode(false);

                entity.Property(e => e.ReportType)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.SubmittedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Organization>(entity =>
            {
                entity.HasKey(e => e.OrgId)
                    .HasName("PK_Organisation");

                entity.Property(e => e.OrgId).HasColumnName("Org_ID");

                entity.Property(e => e.OrgName)
                    .IsRequired()
                    .HasColumnName("Org_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RecordCreatedDate)
                    .HasColumnName("Record_created_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.RecordUpdatedDate)
                    .HasColumnName("Record_updated_date")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<Phone>(entity =>
            {
                entity.HasIndex(e => e.ContactId)
                    .HasName("fkIdx_Phone_ContactId");

                entity.HasIndex(e => e.PhoneTypeId)
                    .HasName("fkIdx_Phone_PhoneTypeId");

                entity.Property(e => e.PhoneId).HasColumnName("Phone_ID");

                entity.Property(e => e.ContactId).HasColumnName("Contact_ID");

                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneTypeId).HasColumnName("Phone_type_ID");

                entity.Property(e => e.RecordCreatedDate)
                    .HasColumnName("Record_created_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.RecordUpdatedDate)
                    .HasColumnName("Record_updated_date")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.Phone)
                    .HasForeignKey(d => d.ContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Phone_ContactId");

                entity.HasOne(d => d.PhoneType)
                    .WithMany(p => p.Phone)
                    .HasForeignKey(d => d.PhoneTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Phone_PhoneTypeId");
            });

            modelBuilder.Entity<PhoneType>(entity =>
            {
                entity.ToTable("Phone_type");

                entity.Property(e => e.PhoneTypeId).HasColumnName("Phone_type_ID");

                entity.Property(e => e.PhoneType1)
                    .IsRequired()
                    .HasColumnName("Phone_type")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SignatureDoc>(entity =>
            {
                entity.Property(e => e.SignatureDocId)
                    .HasColumnName("SignatureDoc_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Extension)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Link).IsUnicode(false);

                entity.Property(e => e.Name).IsUnicode(false);

                entity.Property(e => e.OwnerEmail).IsUnicode(false);

                entity.Property(e => e.OwnerName).IsUnicode(false);

                entity.Property(e => e.ReceiverEmail).IsUnicode(false);

                entity.Property(e => e.ReceiverName).IsUnicode(false);
            });

            modelBuilder.Entity<SocialMedia>(entity =>
            {
                entity.ToTable("Social_Media");

                entity.HasIndex(e => e.ContactId)
                    .HasName("fkIdx_SocialMediaType_ContactId");

                entity.HasIndex(e => e.SocialMediaTypeId)
                    .HasName("fkIdx_SocialMedia_SocialMediaTypeId");

                entity.Property(e => e.SocialMediaId).HasColumnName("Social_media_ID");

                entity.Property(e => e.ContactId).HasColumnName("Contact_ID");

                entity.Property(e => e.Link)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.RecordCreatedDate)
                    .HasColumnName("Record_created_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.RecordUpdatedDate)
                    .HasColumnName("Record_updated_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.SocialMediaTypeId).HasColumnName("SocialMedia_type_ID");

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.SocialMedia)
                    .HasForeignKey(d => d.ContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SocialMedia_ContactId");

                entity.HasOne(d => d.SocialMediaType)
                    .WithMany(p => p.SocialMedia)
                    .HasForeignKey(d => d.SocialMediaTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SocialMedia_SocialMediaTypeId");
            });

            modelBuilder.Entity<SocialMediaType>(entity =>
            {
                entity.ToTable("SocialMedia_type");

                entity.Property(e => e.SocialMediaTypeId).HasColumnName("SocialMedia_type_ID");

                entity.Property(e => e.SocialMediaType1)
                    .IsRequired()
                    .HasColumnName("SocialMedia_type")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Title>(entity =>
            {
                entity.Property(e => e.TitleId).HasColumnName("Title_ID");

                entity.Property(e => e.Title1)
                    .IsRequired()
                    .HasColumnName("Title")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.FirstName).IsUnicode(false);

                entity.Property(e => e.LastName).IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.UserName).IsUnicode(false);

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK_User_User");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => e.RoleId);

                entity.Property(e => e.Role)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserRoleMapping>(entity =>
            {
                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRoleMapping)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserRoleMapping_User");

                entity.HasOne(d => d.UserRole)
                    .WithMany(p => p.UserRoleMapping)
                    .HasForeignKey(d => d.UserRoleId)
                    .HasConstraintName("FK_UserRoleMapping_UserRole");
            });

            modelBuilder.Entity<Valve>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("IX_Valve")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AssetId)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.BvId)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.BvcontrolNumber).HasColumnName("BVControlNumber");

                entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.Direction)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DmaName)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ModifedDate).HasColumnType("datetime");

                entity.Property(e => e.Qrid)
                    .HasColumnName("QRID")
                    .IsUnicode(false);

                entity.Property(e => e.ValveId).IsUnicode(false);

                entity.Property(e => e.ValveSize).IsUnicode(false);

                entity.Property(e => e.Latitude).HasColumnName("Latitude");

                entity.Property(e => e.Longitude).HasColumnName("Longitude");
            });

            modelBuilder.Entity<ValveEvent>(entity =>
            {
                entity.HasKey(e => e.EventId);

                entity.ToTable("Valve_Event");

                entity.Property(e => e.EventId).HasColumnName("Event_Id");

                entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.DateTimeStamp).HasColumnType("datetime");

                entity.Property(e => e.EngineerId)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.EventTypeNavigation)
                    .WithMany(p => p.ValveEvent)
                    .HasForeignKey(d => d.EventType)
                    .HasConstraintName("FK_Valve_Event_Valve_Event");

                entity.HasOne(d => d.Valve)
                    .WithMany(p => p.ValveEvent)
                    .HasForeignKey(d => d.ValveId)
                    .HasConstraintName("FK_Valve_Event_Valve");
            });

            modelBuilder.Entity<ValveEventType>(entity =>
            {
                entity.Property(e => e.ValveEventTypeId).ValueGeneratedNever();

                entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Type)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VisibleTo>(entity =>
            {
                entity.Property(e => e.VisibleToId).HasColumnName("VisibleTo_Id");

                entity.Property(e => e.VisibleTo1)
                    .HasColumnName("VisibleTo")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Visit>(entity =>
            {
                entity.HasIndex(e => e.ContactId)
                    .HasName("fkIdx_Visit_ContactId");

                entity.Property(e => e.VisitId).HasColumnName("Visit_ID");

                entity.Property(e => e.ContactId).HasColumnName("Contact_ID");

                entity.Property(e => e.EmployeeId).HasColumnName("Employee_ID");

                entity.Property(e => e.RecordCreatedDate)
                    .HasColumnName("Record_created_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.RecordUpdatedDate)
                    .HasColumnName("Record_updated_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.VisitBookedFlg)
                    .HasColumnName("visit_booked_flg")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.VisitDate)
                    .HasColumnName("visit_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.VisitDue)
                    .HasColumnName("visit_due")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.Visit)
                    .HasForeignKey(d => d.ContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Visit_ContactId");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Visit)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_Contact_EmployeeId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
