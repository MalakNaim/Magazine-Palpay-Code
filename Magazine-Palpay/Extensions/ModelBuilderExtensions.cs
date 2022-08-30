// --------------------------------------------------------------------------------------------------
// <copyright file="ModelBuilderExtensions.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Linq;
using Magazine_Palpay.Web.IdentityModels;
using Magazine_Palpay.Web.Models;
using Magazine_Palpay.Web.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Magazine_Palpay.Web.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void ApplyMagazineConfiguration(this ModelBuilder builder, PersistenceSettings persistenceOptions)
        {
            // build model for MSSQL and Postgres

            if (persistenceOptions.UseOracle)
            {
                foreach (var property in builder.Model.GetEntityTypes()
                    .SelectMany(t => t.GetProperties())
                    .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
                {
                    property.SetColumnType("decimal(23,2)");
                }
            }

            builder.Entity<Post>(entity =>
            {
                entity.ToTable(name: "POST");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Head).HasColumnName("HEAD");
                entity.Property(e => e.Body).HasColumnName("BODY");
                entity.Property(e => e.PostTypeId).HasColumnName("POST_TYPE_ID");
                entity.Property(e => e.PostSubTypeId).HasColumnName("POST_SUB_TYPE_ID"); 
                entity.Property(e => e.MediaType).HasColumnName("MEDIA_TYPE");
                entity.Property(e => e.MainImage).HasColumnName("MAIN_IMAGE");
                entity.Property(e => e.OrderPlace).HasColumnName("ORDER_PLACE");
                entity.Property(e => e.PublishedPost).HasColumnName("PUBLISHED_POST");
                entity.Property(e => e.VideoLink).HasColumnName("VIDEO_LINK");
                entity.Property(e => e.EmbedVideoLink).HasColumnName("EMBED_VIDEO_LINK");
                entity.Property(e => e.CreatedAt).HasColumnName("CREATED_AT");
                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");
                entity.Property(e => e.UpdatedAt).HasColumnName("UPDATED_AT");
                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");
                entity.Property(e => e.IsDelete).HasColumnName("IS_DELETE");
                entity.HasOne(e => e.PostType)
                .WithMany(e => e.Post);
            }); 
            
            builder.Entity<PostPhoto>(entity =>
            {
                entity.ToTable(name: "POST_PHOTO");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.PostId).HasColumnName("POST_ID");
                entity.Property(e => e.Photo).HasColumnName("PHOTO");
                entity.Property(e => e.CreatedAt).HasColumnName("CREATED_AT");
                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");
                entity.Property(e => e.UpdatedAt).HasColumnName("UPDATED_AT");
                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");
                entity.Property(e => e.IsDelete).HasColumnName("IS_DELETE");
            });

            builder.Entity<PostType>(entity =>
            {
                entity.ToTable(name: "POST_TYPE");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Name).HasColumnName("NAME");
                entity.Property(e => e.ParentId).HasColumnName("PARENT_ID");
                entity.Property(e => e.CreatedAt).HasColumnName("CREATED_AT");
                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");
                entity.Property(e => e.UpdatedAt).HasColumnName("UPDATED_AT");
                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");
                entity.Property(e => e.IsDelete).HasColumnName("IS_DELETE");
                entity.HasMany(e => e.Post)
                .WithOne(e => e.PostType);
            });
            
            builder.Entity<Menu>(entity =>
            {
                entity.ToTable(name: "MENU");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Title).HasColumnName("TITLE");
                entity.Property(e => e.Category).HasColumnName("CATEGORY");
                entity.Property(e => e.SubCategory).HasColumnName("SUB_CATEGORY");
                entity.Property(e => e.Active).HasColumnName("ACTIVE");
                entity.Property(e => e.Order).HasColumnName("M_ORDER");
                entity.Property(e => e.Url).HasColumnName("URL");
                entity.Property(e => e.CreatedAt).HasColumnName("CREATED_AT");
                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");
                entity.Property(e => e.UpdatedAt).HasColumnName("UPDATED_AT");
                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");
                entity.Property(e => e.IsDelete).HasColumnName("IS_DELETE");
            });
            
            builder.Entity<MagazineSetting>(entity =>
            {
                entity.ToTable(name: "MAGAZINE_SETTING");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Title).HasColumnName("TITLE");
                entity.Property(e => e.Mobile).HasColumnName("MOBILE");
                entity.Property(e => e.Website).HasColumnName("WEBSITE");
                entity.Property(e => e.Instgram).HasColumnName("INSTGRAM");
                entity.Property(e => e.Twitter).HasColumnName("TWITTER");
                entity.Property(e => e.Email).HasColumnName("EMAIL");
                entity.Property(e => e.Facebook).HasColumnName("FACEBOOK");
                entity.Property(e => e.LinkedIn).HasColumnName("LINKEDIN");
                entity.Property(e => e.OtherUrls).HasColumnName("OTHER_URLS");
                entity.Property(e => e.Logo).HasColumnName("LOGO");
                entity.Property(e => e.LogoDark).HasColumnName("LOGO_DARK");
                entity.Property(e => e.CreatedAt).HasColumnName("CREATED_AT");
                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");
                entity.Property(e => e.UpdatedAt).HasColumnName("UPDATED_AT");
                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");
                entity.Property(e => e.IsDelete).HasColumnName("IS_DELETE");
            });
            
            builder.Entity<LastNews>(entity =>
            {
                entity.ToTable(name: "LAST_NEWS");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Title).HasColumnName("TITLE");
                entity.Property(e => e.CreatedAt).HasColumnName("CREATED_AT");
                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");
                entity.Property(e => e.UpdatedAt).HasColumnName("UPDATED_AT");
                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");
                entity.Property(e => e.IsDelete).HasColumnName("IS_DELETE");
            }); 
            
            builder.Entity<GalleryPhoto>(entity =>
            {
                entity.ToTable(name: "GALLERY_PHOTOS");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.GalleryId).HasColumnName("GALLERY_ID");
                entity.Property(e => e.Photo).HasColumnName("PHOTO");
                entity.Property(e => e.CreatedAt).HasColumnName("CREATED_AT");
                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");
                entity.Property(e => e.UpdatedAt).HasColumnName("UPDATED_AT");
                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");
                entity.Property(e => e.IsDelete).HasColumnName("IS_DELETE");
                entity.HasOne(e => e.Gallery)
                .WithMany(e => e.GalleryPhoto);
            }); 
            
            builder.Entity<Gallery>(entity =>
            {
                entity.ToTable(name: "GALLERY");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Title).HasColumnName("TITLE");
                entity.Property(e => e.Description).HasColumnName("DESCRIPTION");
                entity.Property(e => e.CreatedAt).HasColumnName("CREATED_AT");
                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");
                entity.Property(e => e.UpdatedAt).HasColumnName("UPDATED_AT");
                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");
                entity.Property(e => e.IsDelete).HasColumnName("IS_DELETE");
                entity.HasMany(e => e.GalleryPhoto)
               .WithOne(e => e.Gallery);
            }); 
            
            builder.Entity<Employee>(entity =>
            {
                entity.ToTable(name: "EMPLOYEE");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Name).HasColumnName("NAME");
                entity.Property(e => e.Mobile).HasColumnName("MOBILE");
                entity.Property(e => e.DOB).HasColumnName("DOB");
                entity.Property(e => e.DepartmentId).HasColumnName("DEPARTMENT_ID");
                entity.Property(e => e.Email).HasColumnName("EMAIL");
                entity.Property(e => e.JobTitle).HasColumnName("JOB_TITLE");
                entity.Property(e => e.JoinDate).HasColumnName("JOIN_DATE");
                entity.Property(e => e.Order).HasColumnName("E_ORDER");
                entity.Property(e => e.Photo).HasColumnName("PHOTO");
                entity.Property(e => e.CreatedAt).HasColumnName("CREATED_AT");
                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");
                entity.Property(e => e.UpdatedAt).HasColumnName("UPDATED_AT");
                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");
                entity.Property(e => e.IsDelete).HasColumnName("IS_DELETE");
                entity.HasOne(e => e.Department)
                .WithMany(e => e.Employee);
            }); 
            
            builder.Entity<Department>(entity =>
            {
                entity.ToTable(name: "DEPARTMENT");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Name).HasColumnName("NAME");
                entity.Property(e => e.CreatedAt).HasColumnName("CREATED_AT");
                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");
                entity.Property(e => e.UpdatedAt).HasColumnName("UPDATED_AT");
                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");
                entity.Property(e => e.IsDelete).HasColumnName("IS_DELETE");
                entity.HasMany(e => e.Employee)
               .WithOne(e => e.Department);
            });
            
            builder.Entity<BookCategory>(entity =>
            {
                entity.ToTable(name: "BOOK_CATEGORY");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Name).HasColumnName("NAME");
                entity.Property(e => e.CreatedAt).HasColumnName("CREATED_AT");
                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");
                entity.Property(e => e.UpdatedAt).HasColumnName("UPDATED_AT");
                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");
                entity.Property(e => e.IsDelete).HasColumnName("IS_DELETE");
                entity.HasMany(e => e.Book)
                .WithOne(e => e.BookCategory);
            }); 
            
            builder.Entity<Book>(entity =>
            {
                entity.ToTable(name: "BOOK");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Name).HasColumnName("NAME");
                entity.Property(e => e.Author).HasColumnName("AUTHOR");
                entity.Property(e => e.BookCategoryId).HasColumnName("BOOK_CATEGORY_ID");
                entity.Property(e => e.Image).HasColumnName("IMAGE");
                entity.Property(e => e.Link).HasColumnName("LINK");
                entity.Property(e => e.CreatedAt).HasColumnName("CREATED_AT");
                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");
                entity.Property(e => e.UpdatedAt).HasColumnName("UPDATED_AT");
                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");
                entity.Property(e => e.IsDelete).HasColumnName("IS_DELETE");
                entity.HasOne(e => e.BookCategory)
               .WithMany(e => e.Book);
            });
            
            builder.Entity<Ads>(entity =>
            {
                entity.ToTable(name: "ADS");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Head).HasColumnName("HEAD");
                entity.Property(e => e.Body).HasColumnName("BODY");
                entity.Property(e => e.StatDate).HasColumnName("STAT_DATE");
                entity.Property(e => e.EndDate).HasColumnName("END_DATE");
                entity.Property(e => e.Image).HasColumnName("IMAGE");
                entity.Property(e => e.Link).HasColumnName("LINK");
                entity.Property(e => e.Owner).HasColumnName("OWNER");
                entity.Property(e => e.Order).HasColumnName("A_ORDER");
                entity.Property(e => e.CreatedAt).HasColumnName("CREATED_AT");
                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");
                entity.Property(e => e.UpdatedAt).HasColumnName("UPDATED_AT");
                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");
                entity.Property(e => e.IsDelete).HasColumnName("IS_DELETE");
            });

            builder.Entity<FluentUser>(entity =>
            {
                entity.ToTable(name: "ASP_NET_USERS");
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.UserName).HasColumnName("USER_NAME");
                entity.Property(e => e.NormalizedUserName).HasColumnName("NORMALIZED_USER_NAME");
                entity.Property(e => e.Email).HasColumnName("EMAIL");
                entity.Property(e => e.NormalizedEmail).HasColumnName("NORMALIZED_EMAIL");
                entity.Property(e => e.EmailConfirmed).HasColumnName("EMAIL_CONFIRMED");
                entity.Property(e => e.PasswordHash).HasColumnName("PASSWORD_HASH");
                entity.Property(e => e.SecurityStamp).HasColumnName("SECURITY_STAMP");
                entity.Property(e => e.ConcurrencyStamp).HasColumnName("CONCURRENCY_STAMP");
                entity.Property(e => e.PhoneNumber).HasColumnName("PHONE_NUMBER");
                entity.Property(e => e.PhoneNumberConfirmed).HasColumnName("PHONE_NUMBER_CONFIRMED");
                entity.Property(e => e.TwoFactorEnabled).HasColumnName("TWO_FACTOR_ENABLED");
                entity.Property(e => e.LockoutEnd).HasColumnName("LOCKOUT_END");
                entity.Property(e => e.LockoutEnabled).HasColumnName("LOCKOUT_ENABLED");
                entity.Property(e => e.AccessFailedCount).HasColumnName("ACCESS_FAILED_COUNT");
            }); 
            
            builder.Entity<FluentRole>(entity =>
            {
                entity.ToTable(name: "ASP_NET_ROLES");
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Name).HasColumnName("NAME"); 
                entity.Property(e => e.ConcurrencyStamp).HasColumnName("CONCURRENCY_STAMP");
                entity.Property(e => e.NormalizedName).HasColumnName("NORMALIZED_NAME");
            });

            builder.Entity<FluentRoleClaim>(entity =>
            {
                entity.ToTable(name: "ASP_NET_ROLE_CLAIMS");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.ClaimType).HasColumnName("CLAIM_TYPE");
                entity.Property(e => e.ClaimValue).HasColumnName("CLAIM_VALUE");
                entity.Property(e => e.RoleId).HasColumnName("ROLE_ID");
            });

            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("ASP_NET_USER_ROLES");

                entity.Property(e => e.UserId).HasColumnName("USER_ID");
                entity.Property(e => e.RoleId).HasColumnName("ROLE_ID");
            });

            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("ASP_NET_USER_CLAIMS");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.UserId).HasColumnName("USER_ID");
                entity.Property(e => e.ClaimType).HasColumnName("CLAIM_TYPE");
                entity.Property(e => e.ClaimValue).HasColumnName("CLAIM_VALUE");
            });

            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("ASP_NET_USER_LOGINS");

                entity.Property(e => e.UserId).HasColumnName("USER_ID");
                entity.Property(e => e.LoginProvider).HasColumnName("LOGIN_PROVIDER");
                entity.Property(e => e.ProviderKey).HasColumnName("PROVIDER_KEY");
                entity.Property(e => e.ProviderDisplayName).HasColumnName("PROVIDER_DISPLAY_NAME");
            });
            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("ASP_NET_USER_TOKENS");

                entity.Property(e => e.UserId).HasColumnName("USER_ID");
                entity.Property(e => e.LoginProvider).HasColumnName("LOGIN_PROVIDER");
                entity.Property(e => e.Name).HasColumnName("NAME");
                entity.Property(e => e.Value).HasColumnName("VALUE");
            });
        }
    }
}