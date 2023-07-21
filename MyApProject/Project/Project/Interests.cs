//Main Interest Class Available in  Program.cs

// using Microsoft.EntityFrameworkCore;
// public class UserInterest
// {
//     public int Id{get; set;}
//     public string MovieInterest{get; set;}
//     public string MusicInterest{get; set;}
//      public string SportInterest { get; set; }
//      public UserInterest(int id, string movieInterest, string musicInterest, string sportInterest)
//     {
//         Id = id;
//         MovieInterest = movieInterest;
//         MusicInterest = musicInterest;
//         SportInterest = sportInterest;    
//     }


// }
// public class InterestDbContext : DbContext
// {
//     public DbSet<UserInterest> UserInterests { get; set; }

//     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//     {
//         optionsBuilder.UseSqlite("Data Source=userinterests.db");
//     }
// }