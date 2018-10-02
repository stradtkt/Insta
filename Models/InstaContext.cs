using Microsoft.EntityFrameworkCore;
 
namespace Insta.Models
{
    public class InstaContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public InstaContext(DbContextOptions<InstaContext> options) : base(options) { }

        public DbSet<User> users {get;set;}
        public DbSet<Comment> comments {get;set;}
        public DbSet<Message> messages {get;set;}
        public DbSet<Like> likes {get;set;}
        public DbSet<Photo> photos {get;set;}
        public DbSet<UsersMessages> user_has_messages {get;set;}

    }
}