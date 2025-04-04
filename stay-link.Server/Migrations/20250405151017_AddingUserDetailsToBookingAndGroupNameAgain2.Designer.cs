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
    [Migration("20250405151017_AddingUserDetailsToBookingAndGroupNameAgain2")]
    partial class AddingUserDetailsToBookingAndGroupNameAgain2
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
                        .HasColumnName("booking_preferences_id");

                    b.Property<int>("BookingsId")
                        .HasColumnType("integer")
                        .HasColumnName("bookings_id");

                    b.HasKey("BookingPreferencesId", "BookingsId");

                    b.HasIndex("BookingsId")
                        .HasDatabaseName("i_x_booking_booking_feature_bookings_id");

                    b.ToTable("booking_booking_feature");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text")
                        .HasColumnName("concurrency_stamp");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("name");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("normalized_name");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("role_name_index");

                    b.ToTable("roles", (string)null);
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
                        .HasColumnName("claim_type");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text")
                        .HasColumnName("claim_value");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("role_id");

                    b.HasKey("Id");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("i_x_role_claims_role_id");

                    b.ToTable("role_claims", (string)null);
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
                        .HasColumnName("claim_type");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text")
                        .HasColumnName("claim_value");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("i_x_user_claims_user_id");

                    b.ToTable("user_claims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text")
                        .HasColumnName("login_provider");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text")
                        .HasColumnName("provider_key");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text")
                        .HasColumnName("provider_display_name");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("user_id");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId")
                        .HasDatabaseName("i_x_user_logins_user_id");

                    b.ToTable("user_logins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text")
                        .HasColumnName("user_id");

                    b.Property<string>("RoleId")
                        .HasColumnType("text")
                        .HasColumnName("role_id");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("i_x_user_roles_role_id");

                    b.ToTable("user_roles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text")
                        .HasColumnName("user_id");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text")
                        .HasColumnName("login_provider");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Value")
                        .HasColumnType("text")
                        .HasColumnName("value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("user_tokens", (string)null);
                });

            modelBuilder.Entity("booking_room_features", b =>
                {
                    b.Property<int>("booking_id")
                        .HasColumnType("integer")
                        .HasColumnName("booking_id");

                    b.Property<int>("room_feature_id")
                        .HasColumnType("integer")
                        .HasColumnName("room_feature_id");

                    b.HasKey("booking_id", "room_feature_id");

                    b.HasIndex("room_feature_id")
                        .HasDatabaseName("i_x_booking_room_features_room_feature_id");

                    b.ToTable("booking_room_features");
                });

            modelBuilder.Entity("booking_rooms", b =>
                {
                    b.Property<int>("booking_id")
                        .HasColumnType("integer")
                        .HasColumnName("booking_id");

                    b.Property<int>("room_id")
                        .HasColumnType("integer")
                        .HasColumnName("room_id");

                    b.HasKey("booking_id", "room_id");

                    b.HasIndex("room_id")
                        .HasDatabaseName("i_x_booking_rooms_room_id");

                    b.ToTable("booking_rooms");
                });

            modelBuilder.Entity("room_room_features", b =>
                {
                    b.Property<int>("room_feature_id")
                        .HasColumnType("integer")
                        .HasColumnName("room_feature_id");

                    b.Property<int>("room_id")
                        .HasColumnType("integer")
                        .HasColumnName("room_id");

                    b.HasKey("room_feature_id", "room_id");

                    b.HasIndex("room_id")
                        .HasDatabaseName("i_x_room_room_features_room_id");

                    b.ToTable("room_room_features");
                });

            modelBuilder.Entity("stay_link.Server.Models.Booking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("BreakfastRequests")
                        .HasColumnType("integer")
                        .HasColumnName("breakfast_requests");

                    b.Property<DateOnly>("CheckInDate")
                        .HasColumnType("date")
                        .HasColumnName("check_in_date");

                    b.Property<DateOnly>("CheckOutDate")
                        .HasColumnType("date")
                        .HasColumnName("check_out_date");

                    b.Property<string>("GroupName")
                        .HasColumnType("text")
                        .HasColumnName("group_name");

                    b.Property<int>("HotelId")
                        .HasColumnType("integer")
                        .HasColumnName("hotel_id");

                    b.Property<int>("RoomId")
                        .HasColumnType("integer")
                        .HasColumnName("room_id");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("status");

                    b.Property<int>("TotalGuests")
                        .HasColumnType("integer")
                        .HasColumnName("total_guests");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("numeric")
                        .HasColumnName("total_price");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("i_x_bookings_user_id");

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
                        .HasColumnName("extra_cost");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("booking_feature");
                });

            modelBuilder.Entity("stay_link.Server.Models.BookingUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer")
                        .HasColumnName("access_failed_count");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text")
                        .HasColumnName("concurrency_stamp");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("email");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean")
                        .HasColumnName("email_confirmed");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("last_name");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean")
                        .HasColumnName("lockout_enabled");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("lockout_end");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("normalized_email");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("normalized_user_name");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text")
                        .HasColumnName("password_hash");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text")
                        .HasColumnName("phone_number");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean")
                        .HasColumnName("phone_number_confirmed");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text")
                        .HasColumnName("security_stamp");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean")
                        .HasColumnName("two_factor_enabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("user_name");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("email_index");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("user_name_index");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("stay_link.Server.Models.Hotel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("address");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("image_url");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("hotels");
                });

            modelBuilder.Entity("stay_link.Server.Models.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("FloorNumber")
                        .HasColumnType("integer")
                        .HasColumnName("floor_number");

                    b.Property<int>("HotelId")
                        .HasColumnType("integer")
                        .HasColumnName("hotel_id");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("text")
                        .HasColumnName("image_url");

                    b.Property<int>("MaxOccupancy")
                        .HasColumnType("integer")
                        .HasColumnName("max_occupancy");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric")
                        .HasColumnName("price");

                    b.Property<string>("RoomType")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("room_type");

                    b.Property<string>("Summary")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("summary");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.HasKey("Id");

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
                        .HasColumnName("extra_cost");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("room_features");
                });

            modelBuilder.Entity("stay_link.Server.Models.RoomUsage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CleaningState")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("cleaning_state");

                    b.Property<double>("GeneralWear")
                        .HasColumnType("double precision")
                        .HasColumnName("general_wear");

                    b.Property<int>("RoomId")
                        .HasColumnType("integer")
                        .HasColumnName("room_id");

                    b.Property<int>("TimesBookedSinceMaintenance")
                        .HasColumnType("integer")
                        .HasColumnName("times_booked_since_maintenance");

                    b.Property<int>("TimesBookedThisYear")
                        .HasColumnType("integer")
                        .HasColumnName("times_booked_this_year");

                    b.HasKey("Id");

                    b.HasIndex("RoomId")
                        .IsUnique()
                        .HasDatabaseName("i_x_room_usages_room_id");

                    b.ToTable("room_usages");
                });

            modelBuilder.Entity("stay_link.Server.Models.Session", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTimeOffset>("ExpiresAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("expires_at");

                    b.Property<DateTimeOffset>("InitiatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("initiated_at");

                    b.Property<bool>("IsRevoked")
                        .HasColumnType("boolean")
                        .HasColumnName("is_revoked");

                    b.Property<string>("LastRefreshToken")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("last_refresh_token");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("i_x_sessions_user_id");

                    b.ToTable("sessions");
                });

            modelBuilder.Entity("BookingBookingFeature", b =>
                {
                    b.HasOne("stay_link.Server.Models.BookingFeature", null)
                        .WithMany()
                        .HasForeignKey("BookingPreferencesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("f_k_booking_booking_feature__booking_feature_booking_preferences_~");

                    b.HasOne("stay_link.Server.Models.Booking", null)
                        .WithMany()
                        .HasForeignKey("BookingsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("f_k_booking_booking_feature__bookings_bookings_id");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("f_k_role_claims_roles_role_id");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("stay_link.Server.Models.BookingUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("f_k_user_claims__users_user_id");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("stay_link.Server.Models.BookingUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("f_k_user_logins__users_user_id");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("f_k_user_roles_roles_role_id");

                    b.HasOne("stay_link.Server.Models.BookingUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("f_k_user_roles__users_user_id");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("stay_link.Server.Models.BookingUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("f_k_user_tokens__users_user_id");
                });

            modelBuilder.Entity("booking_room_features", b =>
                {
                    b.HasOne("stay_link.Server.Models.Booking", null)
                        .WithMany()
                        .HasForeignKey("booking_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("f_k_booking_room_features__bookings_booking_id");

                    b.HasOne("stay_link.Server.Models.RoomFeature", null)
                        .WithMany()
                        .HasForeignKey("room_feature_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("f_k_booking_room_features__room_features_room_feature_id");
                });

            modelBuilder.Entity("booking_rooms", b =>
                {
                    b.HasOne("stay_link.Server.Models.Booking", null)
                        .WithMany()
                        .HasForeignKey("booking_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("f_k_booking_rooms__bookings_booking_id");

                    b.HasOne("stay_link.Server.Models.Room", null)
                        .WithMany()
                        .HasForeignKey("room_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("f_k_booking_rooms__rooms_room_id");
                });

            modelBuilder.Entity("room_room_features", b =>
                {
                    b.HasOne("stay_link.Server.Models.RoomFeature", null)
                        .WithMany()
                        .HasForeignKey("room_feature_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("f_k_room_room_features__room_features_room_feature_id");

                    b.HasOne("stay_link.Server.Models.Room", null)
                        .WithMany()
                        .HasForeignKey("room_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("f_k_room_room_features__rooms_room_id");
                });

            modelBuilder.Entity("stay_link.Server.Models.Booking", b =>
                {
                    b.HasOne("stay_link.Server.Models.BookingUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("f_k_bookings__users_user_id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("stay_link.Server.Models.RoomUsage", b =>
                {
                    b.HasOne("stay_link.Server.Models.Room", "Room")
                        .WithOne("RoomUsage")
                        .HasForeignKey("stay_link.Server.Models.RoomUsage", "RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("f_k_room_usages_rooms_room_id");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("stay_link.Server.Models.Session", b =>
                {
                    b.HasOne("stay_link.Server.Models.BookingUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("f_k_sessions_users_user_id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("stay_link.Server.Models.Room", b =>
                {
                    b.Navigation("RoomUsage")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
