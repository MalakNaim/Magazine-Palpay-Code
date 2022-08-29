// --------------------------------------------------------------------------------------------------
// <copyright file="ModelBuilderExtensions.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Linq;
using Magazine_Palpay.Web.Settings;
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

            builder.Entity<Data.Models.Post>(entity =>
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
            }); 
            
            builder.Entity<Data.Models.PostPhoto>(entity =>
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

            builder.Entity<Data.Models.PostType>(entity =>
            {
                entity.ToTable(name: "POST_TYPE");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Name).HasColumnName("NAME");
                entity.Property(e => e.CreatedAt).HasColumnName("CREATED_AT");
                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");
                entity.Property(e => e.UpdatedAt).HasColumnName("UPDATED_AT");
                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");
                entity.Property(e => e.IsDelete).HasColumnName("IS_DELETE");
            });
            
            builder.Entity<Data.Models.Menu>(entity =>
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
            
            builder.Entity<Data.Models.MagazineSetting>(entity =>
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
            
            builder.Entity<Data.Models.LastNews>(entity =>
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
            
            builder.Entity<Data.Models.GalleryPhoto>(entity =>
            {
                entity.ToTable(name: "GALLERY_PHOTO");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.GalleryId).HasColumnName("GALLERY_ID");
                entity.Property(e => e.Photo).HasColumnName("PHOTO");
                entity.Property(e => e.CreatedAt).HasColumnName("CREATED_AT");
                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");
                entity.Property(e => e.UpdatedAt).HasColumnName("UPDATED_AT");
                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");
                entity.Property(e => e.IsDelete).HasColumnName("IS_DELETE");
            }); 
            
            builder.Entity<Data.Models.Gallery>(entity =>
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
            }); 
            
            builder.Entity<Data.Models.Employee>(entity =>
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
            }); 
            
            builder.Entity<Data.Models.Department>(entity =>
            {
                entity.ToTable(name: "DEPARTMENT");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Name).HasColumnName("NAME");
                entity.Property(e => e.CreatedAt).HasColumnName("CREATED_AT");
                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");
                entity.Property(e => e.UpdatedAt).HasColumnName("UPDATED_AT");
                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");
                entity.Property(e => e.IsDelete).HasColumnName("IS_DELETE");
            });
            
            builder.Entity<Data.Models.BookCategory>(entity =>
            {
                entity.ToTable(name: "BOOK_CATEGORY");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Name).HasColumnName("NAME");
                entity.Property(e => e.CreatedAt).HasColumnName("CREATED_AT");
                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");
                entity.Property(e => e.UpdatedAt).HasColumnName("UPDATED_AT");
                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");
                entity.Property(e => e.IsDelete).HasColumnName("IS_DELETE");
            }); 
            
            builder.Entity<Data.Models.Book>(entity =>
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
            });
            
            builder.Entity<Data.Models.Ads>(entity =>
            {
                entity.ToTable(name: "ADS");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Head).HasColumnName("HEAD");
                entity.Property(e => e.Body).HasColumnName("BODY");
                entity.Property(e => e.StatDate).HasColumnName("STATE_DATE");
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
        }
    }
}