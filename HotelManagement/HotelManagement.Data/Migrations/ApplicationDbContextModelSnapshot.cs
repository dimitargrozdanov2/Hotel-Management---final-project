﻿// <auto-generated />
using System;
using HotelManagement.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HotelManagement.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HotelManagement.DataModels.Business", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<string>("Description");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Location");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Businesses");

                    b.HasData(
                        new
                        {
                            Id = "14f77522-b07f-4ad8-855b-d93923bea56e",
                            CreatedOn = new DateTime(2019, 5, 3, 17, 10, 20, 0, DateTimeKind.Unspecified),
                            Description = "Located in the bustling Dubai Marina neighbourhood, Rove Dubai Marina is the perfect location to start your next adventure.",
                            IsDeleted = false,
                            Location = "United Arab Emirates, Dubai",
                            Name = "Rove Dubai Marina"
                        },
                        new
                        {
                            Id = "687af33b-3084-43b6-bacb-4c8847559ee4",
                            CreatedOn = new DateTime(2019, 4, 3, 15, 20, 11, 0, DateTimeKind.Unspecified),
                            Description = "This hotel is located on a front of the Palm Islands. Perfect for honeymoons, this hotel has 64 rooms, 26 suites, and four Moorish-style villas.",
                            IsDeleted = false,
                            Location = "United Arab Emirates, Dubai",
                            Name = "The Palm"
                        });
                });

            modelBuilder.Entity("HotelManagement.DataModels.Category", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = "450b6f6e-95b3-400d-a258-aafc6b6ecd07",
                            CreatedOn = new DateTime(2019, 5, 4, 16, 36, 5, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            Name = "TODO"
                        },
                        new
                        {
                            Id = "b73de489-d731-4f3a-8be7-a6613fe5eb6f",
                            CreatedOn = new DateTime(2019, 5, 2, 15, 26, 10, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            Name = "Maintenance"
                        },
                        new
                        {
                            Id = "5c97553f-1557-4352-9622-d5bdd37f44f4",
                            CreatedOn = new DateTime(2019, 5, 3, 18, 45, 23, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            Name = "Events"
                        });
                });

            modelBuilder.Entity("HotelManagement.DataModels.Feedback", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BusinessId");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name")
                        .HasMaxLength(50);

                    b.Property<string>("Number");

                    b.Property<double?>("Rating");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.ToTable("Feedback");

                    b.HasData(
                        new
                        {
                            Id = "b4f2f3c8-a725-44a0-8fd6-651ee45a9690",
                            BusinessId = "687af33b-3084-43b6-bacb-4c8847559ee4",
                            Comment = "Great location, really clean rooms, awesome staff!",
                            CreatedOn = new DateTime(2019, 5, 4, 16, 36, 5, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            Name = "Jeff Goldblum",
                            Number = "+359 896 71 99 88",
                            Rating = 5.0
                        },
                        new
                        {
                            Id = "1e67e958-37fc-46cc-a6b9-5d1f28e9e532",
                            BusinessId = "14f77522-b07f-4ad8-855b-d93923bea56e",
                            Comment = "We had a wonderful stay! The staff could not have been more helpful!",
                            CreatedOn = new DateTime(2019, 5, 2, 15, 26, 10, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            Name = "Scarlett Johansson",
                            Number = "+359 893 92 00 55",
                            Rating = 3.0
                        },
                        new
                        {
                            Id = "d2dd7ddf-ac78-42f1-963a-16f06406bd9d",
                            BusinessId = "14f77522-b07f-4ad8-855b-d93923bea56e",
                            Comment = "The hotel is in a very good location, visited the restaurant and had a really great time!",
                            CreatedOn = new DateTime(2019, 5, 3, 18, 45, 23, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            Name = "Sandra Bullock",
                            Number = "+359 898 11 23 44",
                            Rating = 1.0
                        });
                });

            modelBuilder.Entity("HotelManagement.DataModels.Image", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BusinessId");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.ToTable("Images");

                    b.HasData(
                        new
                        {
                            Id = "83705aa7-d523-4978-b799-84e15468a088",
                            BusinessId = "687af33b-3084-43b6-bacb-4c8847559ee4",
                            CreatedOn = new DateTime(2019, 5, 4, 16, 36, 5, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            Name = "The Palm_logo.jpg"
                        },
                        new
                        {
                            Id = "0f34be57-d215-43fb-b442-45f246b651d5",
                            BusinessId = "14f77522-b07f-4ad8-855b-d93923bea56e",
                            CreatedOn = new DateTime(2019, 3, 4, 15, 36, 5, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            Name = "Rove Dubai Marina_logo.jpg"
                        });
                });

            modelBuilder.Entity("HotelManagement.DataModels.Logbook", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BusinessId");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.ToTable("Logbooks");

                    b.HasData(
                        new
                        {
                            Id = "3d71d939-dc61-46f8-af46-ed6a618036c2",
                            BusinessId = "14f77522-b07f-4ad8-855b-d93923bea56e",
                            CreatedOn = new DateTime(2019, 5, 4, 14, 40, 5, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            Name = "Restaurant"
                        },
                        new
                        {
                            Id = "cc9ea717-1788-49d8-9a3d-bd0bc3eb73ae",
                            BusinessId = "687af33b-3084-43b6-bacb-4c8847559ee4",
                            CreatedOn = new DateTime(2019, 3, 4, 16, 20, 10, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            Name = "Lounge Bar"
                        });
                });

            modelBuilder.Entity("HotelManagement.DataModels.LogbookManagers", b =>
                {
                    b.Property<string>("ManagerId");

                    b.Property<string>("LogbookId");

                    b.HasKey("ManagerId", "LogbookId");

                    b.HasIndex("LogbookId");

                    b.ToTable("LogbookManagers");

                    b.HasData(
                        new
                        {
                            ManagerId = "6404c00f-c0e6-4a92-ad71-43b24f5f0e97",
                            LogbookId = "3d71d939-dc61-46f8-af46-ed6a618036c2"
                        });
                });

            modelBuilder.Entity("HotelManagement.DataModels.Note", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CategoryId");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("LogbookId");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<int>("PriorityType");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("LogbookId");

                    b.HasIndex("UserId");

                    b.ToTable("Notes");

                    b.HasData(
                        new
                        {
                            Id = "f7257688-84ea-4327-8841-ac78f3e8d2f6",
                            CategoryId = "450b6f6e-95b3-400d-a258-aafc6b6ecd07",
                            CreatedOn = new DateTime(2019, 5, 4, 16, 36, 5, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            LogbookId = "3d71d939-dc61-46f8-af46-ed6a618036c2",
                            PriorityType = 1,
                            Text = "Check reception documents!",
                            UserId = "6404c00f-c0e6-4a92-ad71-43b24f5f0e97"
                        });
                });

            modelBuilder.Entity("HotelManagement.DataModels.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasData(
                        new
                        {
                            Id = "6404c00f-c0e6-4a92-ad71-43b24f5f0e97",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "b318fa87-5d53-4bc3-8019-23d7aa37da20",
                            Email = "admin@admin.admin",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN@ADMIN.ADMIN",
                            NormalizedUserName = "ADMIN@ADMIN.ADMIN",
                            PasswordHash = "AQAAAAEAACcQAAAAEG5yDdPexa5KQHAhorczw165qs8mxitFXgC5YGEA1XRuewjCiAB4VDHSpuWZ1oY/Ow==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "7I2NUNAXILZUAHNGX7TRSNQCNRWCEOSX",
                            TwoFactorEnabled = false,
                            UserName = "admin@admin.admin"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");

                    b.HasData(
                        new
                        {
                            Id = "0e78767e-b4d2-456f-b029-5ad4c454589a",
                            ConcurrencyStamp = "6e63ab39-a514-4e99-ba86-296cfe8d15b5",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "ca8a9288-1bda-4320-8929-731d721be047",
                            ConcurrencyStamp = "190ecb23-4911-48ee-bf11-62b3157eb20b",
                            Name = "Manager",
                            NormalizedName = "MANAGER"
                        },
                        new
                        {
                            Id = "2c9e83b9-1015-42b6-9df5-ad54b3d98224",
                            ConcurrencyStamp = "846eb2cf-9cce-4b54-99db-f87cbde271df",
                            Name = "Moderator",
                            NormalizedName = "MODERATOR"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");

                    b.HasData(
                        new
                        {
                            UserId = "6404c00f-c0e6-4a92-ad71-43b24f5f0e97",
                            RoleId = "0e78767e-b4d2-456f-b029-5ad4c454589a"
                        },
                        new
                        {
                            UserId = "6404c00f-c0e6-4a92-ad71-43b24f5f0e97",
                            RoleId = "ca8a9288-1bda-4320-8929-731d721be047"
                        },
                        new
                        {
                            UserId = "6404c00f-c0e6-4a92-ad71-43b24f5f0e97",
                            RoleId = "2c9e83b9-1015-42b6-9df5-ad54b3d98224"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("HotelManagement.DataModels.Feedback", b =>
                {
                    b.HasOne("HotelManagement.DataModels.Business", "Business")
                        .WithMany("Feedback")
                        .HasForeignKey("BusinessId");
                });

            modelBuilder.Entity("HotelManagement.DataModels.Image", b =>
                {
                    b.HasOne("HotelManagement.DataModels.Business", "Business")
                        .WithMany("Images")
                        .HasForeignKey("BusinessId");
                });

            modelBuilder.Entity("HotelManagement.DataModels.Logbook", b =>
                {
                    b.HasOne("HotelManagement.DataModels.Business", "Business")
                        .WithMany("BusinessUnits")
                        .HasForeignKey("BusinessId");
                });

            modelBuilder.Entity("HotelManagement.DataModels.LogbookManagers", b =>
                {
                    b.HasOne("HotelManagement.DataModels.Logbook", "Logbook")
                        .WithMany("LogbookManagers")
                        .HasForeignKey("LogbookId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HotelManagement.DataModels.User", "Manager")
                        .WithMany("LogbookManagers")
                        .HasForeignKey("ManagerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HotelManagement.DataModels.Note", b =>
                {
                    b.HasOne("HotelManagement.DataModels.Category", "Category")
                        .WithMany("Notes")
                        .HasForeignKey("CategoryId");

                    b.HasOne("HotelManagement.DataModels.Logbook", "Logbook")
                        .WithMany("Notes")
                        .HasForeignKey("LogbookId");

                    b.HasOne("HotelManagement.DataModels.User", "User")
                        .WithMany("Notes")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("HotelManagement.DataModels.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("HotelManagement.DataModels.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HotelManagement.DataModels.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("HotelManagement.DataModels.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
