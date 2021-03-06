﻿// <auto-generated />
using System;
using CoreDX.Application.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CoreDX.Application.DbMigration.Application
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CoreDX.Domain.Entity.App.Management.Menu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreationTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<int?>("CreatorId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset>("LastModificationTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<int?>("LastModifierId")
                        .HasColumnType("int");

                    b.Property<short>("Order")
                        .HasColumnType("smallint");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Menus");

                    b.HasAnnotation("IsTreeEntity", "");
                });

            modelBuilder.Entity("CoreDX.Domain.Entity.App.Management.MenuItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreationTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<int?>("CreatorId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset>("LastModificationTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<int?>("LastModifierId")
                        .HasColumnType("int");

                    b.Property<string>("Link")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MenuId")
                        .HasColumnType("int");

                    b.Property<short>("Order")
                        .HasColumnType("smallint");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MenuId");

                    b.ToTable("MenuItems");
                });

            modelBuilder.Entity("CoreDX.Domain.Entity.App.Sample.Domain", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreationTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<int?>("CreatorId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset>("LastModificationTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<int?>("LastModifierId")
                        .HasColumnType("int");

                    b.Property<string>("SampleColumn")
                        .HasColumnName("SC")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("DbDescription", "示例列");

                    b.HasKey("Id");

                    b.ToTable("Domains");

                    b.HasAnnotation("DbDescription", "示例实体");
                });

            modelBuilder.Entity("CoreDX.Domain.Entity.App.Sample.TreeDomain", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreationTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<int?>("CreatorId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset>("LastModificationTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<int?>("LastModifierId")
                        .HasColumnType("int");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.Property<string>("SampleColumn")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("DbDescription", "示例列");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("TreeDomains");

                    b.HasAnnotation("DbDescription", "示例树形实体");

                    b.HasAnnotation("IsTreeEntity", "");
                });

            modelBuilder.Entity("CoreDX.Domain.Entity.App.Management.Menu", b =>
                {
                    b.HasOne("CoreDX.Domain.Entity.App.Management.Menu", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");

                    b.OwnsOne("CoreDX.Domain.Entity.App.Management.MenuIcon", "MenuIcon", b1 =>
                        {
                            b1.Property<int>("MenuId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("Type")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Value")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("MenuId");

                            b1.ToTable("Menus");

                            b1.WithOwner()
                                .HasForeignKey("MenuId");
                        });
                });

            modelBuilder.Entity("CoreDX.Domain.Entity.App.Management.MenuItem", b =>
                {
                    b.HasOne("CoreDX.Domain.Entity.App.Management.Menu", "Menu")
                        .WithMany("Items")
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("CoreDX.Domain.Entity.App.Management.MenuItemIcon", "MenuItemIcon", b1 =>
                        {
                            b1.Property<int>("MenuItemId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("Type")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Value")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("MenuItemId");

                            b1.ToTable("MenuItems");

                            b1.WithOwner()
                                .HasForeignKey("MenuItemId");
                        });
                });

            modelBuilder.Entity("CoreDX.Domain.Entity.App.Sample.Domain", b =>
                {
                    b.OwnsOne("CoreDX.Domain.Entity.App.Sample.ComplexEntityProperty", "ComplexProperty", b1 =>
                        {
                            b1.Property<int>("DomainId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("C1")
                                .HasColumnType("nvarchar(max)")
                                .HasAnnotation("DbDescription", "示例复杂实体属性列1");

                            b1.Property<string>("C2")
                                .HasColumnType("nvarchar(max)")
                                .HasAnnotation("DbDescription", "示例复杂实体属性列2");

                            b1.HasKey("DomainId");

                            b1.ToTable("Domains");

                            b1.HasAnnotation("DbDescription", "示例复杂实体属性类（分表存储时会成为表说明，内部属性存储到主表时没有作用）");

                            b1.WithOwner()
                                .HasForeignKey("DomainId");

                            b1.OwnsOne("CoreDX.Domain.Entity.App.Sample.ComplexEntityProperty2", "ComplexProperty2", b2 =>
                                {
                                    b2.Property<int>("ComplexEntityPropertyDomainId")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("int")
                                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                                    b2.Property<string>("C3")
                                        .HasColumnType("nvarchar(max)")
                                        .HasAnnotation("DbDescription", "示例复杂实体属性列3");

                                    b2.Property<string>("C4")
                                        .HasColumnType("nvarchar(max)")
                                        .HasAnnotation("DbDescription", "示例复杂实体属性列4");

                                    b2.HasKey("ComplexEntityPropertyDomainId");

                                    b2.ToTable("Domains");

                                    b2.HasAnnotation("DbDescription", "示例复杂实体属性类2（分表存储时会成为表说明，内部属性存储到主表时没有作用，上级表（ComplexEntityProperty的属性所在的表）分表存储时内部属性存储到上级表时会覆盖上级表的表说明）");

                                    b2.WithOwner()
                                        .HasForeignKey("ComplexEntityPropertyDomainId");
                                });
                        });
                });

            modelBuilder.Entity("CoreDX.Domain.Entity.App.Sample.TreeDomain", b =>
                {
                    b.HasOne("CoreDX.Domain.Entity.App.Sample.TreeDomain", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");
                });
#pragma warning restore 612, 618
        }
    }
}
