﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using stay_link.Server.Data;

#nullable disable

namespace stay_link.Server.Migrations
{
    [DbContext(typeof(BookingContext))]
    [Migration("20250329114054_adjustingNavProperties")]
    partial class adjustingNavProperties
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BookingBookingFeature", b =>
                {
                    b.Property<int>("BookingPreferencesId")
                        .HasColumnType("integer")
                        .HasColumnName("bookingpreferencesid");

                    b.Property<int>("BookingsID")
                        .HasColumnType("integer")
                        .HasColumnName("bookingsid");

                    b.HasKey("BookingPreferencesId", "BookingsID");

                    b.HasIndex("BookingsID");

                    b.ToTable("bookingbookingfeature");
                });

            modelBuilder.Entity("BookingRoomFeature", b =>
                {
                    b.Property<int>("BookingID")
                        .HasColumnType("integer")
                        .HasColumnName("bookingid");

                    b.Property<int>("RoomFeatureID")
                        .HasColumnType("integer")
                        .HasColumnName("roomfeatureid");

                    b.HasKey("BookingID", "RoomFeatureID");

                    b.HasIndex("RoomFeatureID");

                    b.ToTable("bookingroomfeature");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text")
                        .HasColumnName("concurrencystamp");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("name");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("normalizedname");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("aspnetroles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text")
                        .HasColumnName("claimtype");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text")
                        .HasColumnName("claimvalue");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("roleid");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("aspnetroleclaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text")
                        .HasColumnName("claimtype");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text")
                        .HasColumnName("claimvalue");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("userid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("aspnetuserclaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text")
                        .HasColumnName("loginprovider");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text")
                        .HasColumnName("providerkey");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text")
                        .HasColumnName("providerdisplayname");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("userid");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("aspnetuserlogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text")
                        .HasColumnName("userid");

                    b.Property<string>("RoleId")
                        .HasColumnType("text")
                        .HasColumnName("roleid");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("aspnetuserroles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text")
                        .HasColumnName("userid");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text")
                        .HasColumnName("loginprovider");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Value")
                        .HasColumnType("text")
                        .HasColumnName("value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("aspnetusertokens", (string)null);
                });

            modelBuilder.Entity("RoomRoomFeature", b =>
                {
                    b.Property<int>("RoomFeatureID")
                        .HasColumnType("integer")
                        .HasColumnName("roomfeatureid");

                    b.Property<int>("RoomID")
                        .HasColumnType("integer")
                        .HasColumnName("roomid");

                    b.HasKey("RoomFeatureID", "RoomID");

                    b.HasIndex("RoomID");

                    b.ToTable("roomroomfeature");
                });

            modelBuilder.Entity("stay_link.Server.Auth.Model.BookingUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer")
                        .HasColumnName("accessfailedcount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text")
                        .HasColumnName("concurrencystamp");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("email");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean")
                        .HasColumnName("emailconfirmed");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean")
                        .HasColumnName("lockoutenabled");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("lockoutend");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("normalizedemail");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("normalizedusername");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text")
                        .HasColumnName("passwordhash");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text")
                        .HasColumnName("phonenumber");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean")
                        .HasColumnName("phonenumberconfirmed");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text")
                        .HasColumnName("securitystamp");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean")
                        .HasColumnName("twofactorenabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("username");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("aspnetusers", (string)null);
                });

            modelBuilder.Entity("stay_link.Server.Auth.Session", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTimeOffset>("ExpiresAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("expiresat");

                    b.Property<DateTimeOffset>("InitiatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("initiatedat");

                    b.Property<bool>("IsRevoked")
                        .HasColumnType("boolean")
                        .HasColumnName("isrevoked");

                    b.Property<string>("LastRefreshToken")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("lastrefreshtoken");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("userid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("sessions");
                });

            modelBuilder.Entity("stay_link.Server.Models.Booking", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<int?>("BookingID")
                        .HasColumnType("integer")
                        .HasColumnName("bookingid");

                    b.Property<int>("BreakfastRequests")
                        .HasColumnType("integer")
                        .HasColumnName("breakfastrequests");

                    b.Property<DateOnly>("CheckInDate")
                        .HasColumnType("date")
                        .HasColumnName("checkindate");

                    b.Property<DateOnly>("CheckOutDate")
                        .HasColumnType("date")
                        .HasColumnName("checkoutdate");

                    b.Property<int>("HotelId")
                        .HasColumnType("integer")
                        .HasColumnName("hotelid");

                    b.Property<int>("RoomId")
                        .HasColumnType("integer")
                        .HasColumnName("roomid");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("status");

                    b.Property<int>("TotalGuests")
                        .HasColumnType("integer")
                        .HasColumnName("totalguests");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("numeric")
                        .HasColumnName("totalprice");

                    b.Property<string>("UserID")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("userid");

                    b.HasKey("ID");

                    b.HasIndex("BookingID");

                    b.HasIndex("RoomId");

                    b.ToTable("bookings");
                });

            modelBuilder.Entity("stay_link.Server.Models.BookingFeature", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<decimal?>("ExtraCost")
                        .HasColumnType("numeric")
                        .HasColumnName("extracost");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("bookingfeature");
                });

            modelBuilder.Entity("stay_link.Server.Models.Hotel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("address");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("imageurl");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.HasKey("ID");

                    b.ToTable("hotels");
                });

            modelBuilder.Entity("stay_link.Server.Models.Room", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<int>("FloorNumber")
                        .HasColumnType("integer")
                        .HasColumnName("floornumber");

                    b.Property<int>("HotelID")
                        .HasColumnType("integer")
                        .HasColumnName("hotelid");

                    b.Property<int>("MaxOccupancy")
                        .HasColumnType("integer")
                        .HasColumnName("maxoccupancy");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric")
                        .HasColumnName("price");

                    b.Property<string>("RoomType")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("roomtype");

                    b.Property<string>("Summary")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("summary");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.HasKey("ID");

                    b.ToTable("rooms");
                });

            modelBuilder.Entity("stay_link.Server.Models.RoomFeature", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<decimal?>("ExtraCost")
                        .HasColumnType("numeric")
                        .HasColumnName("extracost");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("roomfeatures");
                });

            modelBuilder.Entity("stay_link.Server.Models.RoomUsage", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("CleaningState")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("cleaningstate");

                    b.Property<double>("GeneralWear")
                        .HasColumnType("double precision")
                        .HasColumnName("generalwear");

                    b.Property<int>("RoomID")
                        .HasColumnType("integer")
                        .HasColumnName("roomid");

                    b.Property<int>("TimesBookedSinceMaintenance")
                        .HasColumnType("integer")
                        .HasColumnName("timesbookedsincemaintenance");

                    b.Property<int>("TimesBookedThisYear")
                        .HasColumnType("integer")
                        .HasColumnName("timesbookedthisyear");

                    b.HasKey("ID");

                    b.HasIndex("RoomID")
                        .IsUnique();

                    b.ToTable("roomusages");
                });

            modelBuilder.Entity("BookingBookingFeature", b =>
                {
                    b.HasOne("stay_link.Server.Models.BookingFeature", null)
                        .WithMany()
                        .HasForeignKey("BookingPreferencesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("stay_link.Server.Models.Booking", null)
                        .WithMany()
                        .HasForeignKey("BookingsID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BookingRoomFeature", b =>
                {
                    b.HasOne("stay_link.Server.Models.Booking", null)
                        .WithMany()
                        .HasForeignKey("BookingID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("stay_link.Server.Models.RoomFeature", null)
                        .WithMany()
                        .HasForeignKey("RoomFeatureID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("stay_link.Server.Auth.Model.BookingUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("stay_link.Server.Auth.Model.BookingUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("stay_link.Server.Auth.Model.BookingUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("stay_link.Server.Auth.Model.BookingUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RoomRoomFeature", b =>
                {
                    b.HasOne("stay_link.Server.Models.RoomFeature", null)
                        .WithMany()
                        .HasForeignKey("RoomFeatureID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("stay_link.Server.Models.Room", null)
                        .WithMany()
                        .HasForeignKey("RoomID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("stay_link.Server.Auth.Session", b =>
                {
                    b.HasOne("stay_link.Server.Auth.Model.BookingUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("stay_link.Server.Models.Booking", b =>
                {
                    b.HasOne("stay_link.Server.Models.Booking", null)
                        .WithMany("Rooms")
                        .HasForeignKey("BookingID");

                    b.HasOne("stay_link.Server.Models.Room", null)
                        .WithMany("Bookings")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("stay_link.Server.Models.RoomUsage", b =>
                {
                    b.HasOne("stay_link.Server.Models.Room", "Room")
                        .WithOne("RoomUsage")
                        .HasForeignKey("stay_link.Server.Models.RoomUsage", "RoomID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Room");
                });

            modelBuilder.Entity("stay_link.Server.Models.Booking", b =>
                {
                    b.Navigation("Rooms");
                });

            modelBuilder.Entity("stay_link.Server.Models.Room", b =>
                {
                    b.Navigation("Bookings");

                    b.Navigation("RoomUsage")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
