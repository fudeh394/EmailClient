
using Nibss.Net.EmailClient.Models;
using System.Data.Entity;


namespace Nibss.Net.EmailClient.Entities
{
    public class Database : DbContext
    {



        public Database() : base("name=DbConnectionString")
        {
        }


        public Database(string connection)
           : base(string.Format("{0}", connection))
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }

        public DbSet<Email> Emails { set; get; }
        //public DbSet<PortalSetting> PortalSettings { set; get; }
        //public DbSet<Request> Requests { set; get; }
        //public DbSet<HoRequest> HoRequests { set; get; }
        //public DbSet<VerifiedRecord> VerifiedRecords { set; get; }
        //public DbSet<UploadModel> UploadModels { set; get; }

        //public DbSet<Role> Roles { set; get; }
        //public DbSet<Resource> Resources { set; get; }
        //public DbSet<RoleResource> RoleResources { set; get; }
        //public DbSet<Group> Groups { get; set; }   
        //public DbSet<Audit> Audits { set; get; }




    }

   
}
