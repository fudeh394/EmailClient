
using EmailPushJob.Models;
using System.Data.Entity;


namespace EmailPushJob.Entities
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
       
    }

   
}
