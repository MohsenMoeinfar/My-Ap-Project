using Microsoft.EntityFrameworkCore;
public class ToDo
{
    public override string ToString()
    {
        return $"{this.Name}  {this.Id}  {this.DueDate}  {this.Description}  {this.IsComplete}";
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsComplete { get; set; }
    public DateTime DueDate{get;set;}
    public ToDo()
{
}


    public ToDo(string name, int id, string description, bool isComplete, DateTime duedate)
    {
        this.Name = name;
        this.Id = id;
        this.Description = description;
        this.IsComplete = isComplete;
        this.DueDate = duedate;
    }
}

public class TodoContext : DbContext
{
    public DbSet<ToDo> ToDos { get; set; }
    public DbSet<UserInterest> Interests { get; set; } 
    public DbSet<Prompts> Prompts_Data {get; set;}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=todos.db");
    }
}
public class Prompts
{
    public Prompts(){}
    public int ID{get; set;}
    public string StorePrompts{get; set;}
    public string Ans{get; set;}
    public Prompts(int id , string prm , string ans)
    {
        ID = id;
        StorePrompts = prm;
        Ans =ans;

    }

}
public class UserInterest
{
    public UserInterest
    (){}
    public int Id{get; set;}
    public string MovieInterest{get; set;}
    public string MusicInterest{get; set;}
    public string SportInterest { get; set; }
    public UserInterest(int id, string movieInterest, string musicInterest, string sportInterest)
    {
        Id = id;
        MovieInterest = movieInterest;
        MusicInterest = musicInterest;
        SportInterest = sportInterest;
    }


}