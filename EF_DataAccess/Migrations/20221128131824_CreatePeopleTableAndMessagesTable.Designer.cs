// <auto-generated />
using System;
using ConsoleChatApp;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ConsoleChatApp.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20221128131824_CreatePeopleTableAndMessagesTable")]
    partial class CreatePeopleTableAndMessagesTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("ConsoleChatApp.Entities.Message", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<int>("MesssageRecipientId")
                        .HasColumnType("int");

                    b.Property<int>("MesssageReciverId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("MesssageRecipientId");

                    b.HasIndex("MesssageReciverId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("ConsoleChatApp.Entities.Person", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("EmailAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsLogged")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("People");
                });

            modelBuilder.Entity("ConsoleChatApp.Entities.Message", b =>
                {
                    b.HasOne("ConsoleChatApp.Entities.Person", "MesssageRecipient")
                        .WithMany()
                        .HasForeignKey("MesssageRecipientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConsoleChatApp.Entities.Person", "MesssageReciver")
                        .WithMany()
                        .HasForeignKey("MesssageReciverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MesssageRecipient");

                    b.Navigation("MesssageReciver");
                });
#pragma warning restore 612, 618
        }
    }
}
