using CmdClient;
using System;
using System.Net;
using System.Linq;
using Microsoft.AspNetCore.SignalR.Client;

class Program
{
    static void Main(string[] args)
    {
        using HttpClient httpClient = new HttpClient();
        swaggerClient Client = new swaggerClient("http://localhost:5000/",httpClient);
        
        var Connection = new HubConnectionBuilder()
            .WithUrl("http://localhost:5000/Dhbs")
            .Build();
        var Connection2 = new HubConnectionBuilder()
            .WithUrl("http://localhost:5000/Dhbs2")
            .Build();
        var Connection3 = new HubConnectionBuilder()
            .WithUrl("http://localhost:5000/Dhbs3")
            .Build();
        Connection.On("OnItemAdded", (ToDo Item) =>
        {
            Console.WriteLine($"Item added: {Item.Id}  {Item.Name}");
           
        });
        Connection.On("OnItemUpdated", (ToDo Item) =>
        {
            Console.WriteLine($"Item Updated: {Item.Id}  {Item.Name}    {Item.IsComplete}");
           
        });
        Connection2.On("OnItemRemoved", (int Item) =>
        {
            Console.WriteLine($"Item Deleted: {Item}");
           
        });
        Connection3.On("OnItemAdded", (List<ToDo> Items) =>
        {
            foreach(var Item in Items)
            {
               Console.WriteLine($"Item added: {Item.Id}  {Item.Name}");
            }
           
        });
        Connection3.On("OnItemRemoved", (List<ToDo> Items) =>
        {
            foreach(var Item in Items)
            {
               Console.WriteLine($"Items deleted: {Item.Id}  {Item.Name}");
            }
           
        });
        Connection.StartAsync().Wait();
        Connection2.StartAsync().Wait();
        Connection3.StartAsync().Wait();
        Console.WriteLine("Welcome to  My  ToDo App!");
        while (true)
        {
            Console.WriteLine("What do you want to do? (add/delete/deletealltasks/edit/getall/searchbyid/searchbyname/setstatus/profile/ai/exit)");
            string Operation = Console.ReadLine().ToLower();
            switch (Operation)
            {
                case "add":
                    Console.Write("Name: ");
                    string name = Console.ReadLine();
                    Console.Write("Id: ");
                    int id = int.Parse(Console.ReadLine());
                    ToDo ttt = Client.SearchTaskByIdAsync(id).Result;
                    if (ttt != null)
                    {
                           Console.WriteLine("A task with this ID already exists!");
                           break;
                    }
                    Console.Write("Description: ");
                    string description = Console.ReadLine();
                    Console.Write("Is Complete? (true/false): ");
                    bool isComplete = bool.Parse(Console.ReadLine());
                    Console.Write("Due Date (yyyy-mm-dd): ");
                    DateTime dueDate = DateTime.Parse(Console.ReadLine());
                    Client.AddTaskAsync(new ToDo{
                        Name = name,
                        Id = id,
                        Description = description , 
                        IsComplete = isComplete , 
                        DueDate = dueDate 
                    }).Wait();
                    Connection.SendAsync("AddItem", new ToDo{
                              Name = name,
                              Id = id,
                              Description = description , 
                              IsComplete = isComplete , 
                              DueDate = dueDate 
                            }).Wait();
                    break;
                case "delete":
                    Console.Write("Task Id: ");
                    int taskId = int.Parse(Console.ReadLine());
                    ToDo TaskToDelete = Client.SearchTaskByIdAsync(taskId).Result;
                     if (TaskToDelete == null)
                     {
                            Console.WriteLine("No task with this ID exists!");
                             break;
                      }
                    Client.DeleteTaskAsync(taskId).Wait();
                    Connection2.SendAsync("RemoveItem" , taskId).Wait();
                    break;     
                case "deletealltasks":
                        var Goals2  =  Client.GetAllTasksAsync().Result.ToList();
                        Client.DeleteAllTasksAsync().Wait();
                        Connection3.SendAsync("RemoveItem",Goals2).Wait();
                        break;
                case "edit":
                    Console.Write("Task Id you want to edit: ");
                    int editedTaskId = int.Parse(Console.ReadLine());
                    var TaskEx = Client.SearchTaskByIdAsync(editedTaskId).Result;
                    if(TaskEx == null)
                    {
                        Console.WriteLine("No task with this ID exists!");
                        break;
                    }
                    Console.Write("Name: ");
                    string editedName = Console.ReadLine();
                    Console.Write("Description: ");
                    string editedDescription = Console.ReadLine();
                    Console.Write("Is Complete? (true/false): ");
                    bool editedIsComplete = bool.Parse(Console.ReadLine());
                    Console.Write("Due Date (yyyy-mm-dd): ");
                    DateTime editedDueDate = DateTime.Parse(Console.ReadLine());
                    Client.EditTaskAsync(new ToDo{
                        Name =editedName , 
                        Id = editedTaskId , 
                        Description =editedDescription , 
                        IsComplete = editedIsComplete , 
                        DueDate = editedDueDate
                    }).Wait();
                    Connection.SendAsync("UpdateItem", new ToDo{
                        Name =editedName , 
                        Id = editedTaskId , 
                        Description =editedDescription , 
                        IsComplete = editedIsComplete , 
                        DueDate = editedDueDate
                    }).Wait();
                    break;
                case "getall":
                    List<ToDo> allTasks = Client.GetAllTasksAsync().Result.ToList();
                    foreach (ToDo task in allTasks)
                    {
                        Console.WriteLine($"  {task.Id}   {task.Name}  {task.Description}  {task.IsComplete}   {task.DueDate}");
                    }
                    break;
                case "searchbyname":
                    Console.Write("Task Name: ");
                    string searchName = Console.ReadLine();
                    List<ToDo> searchedTasksByName = Client.SearchTasksByNameAsync(searchName).Result.ToList();
                    foreach (ToDo task in searchedTasksByName)
                    {
                        Console.WriteLine($"  {task.Id}   {task.Name}  {task.Description}  {task.IsComplete}   {task.DueDate}");
                    }
                    break;
                case "searchbyid":
                    Console.Write("Task Id: ");
                    int searchId = int.Parse(Console.ReadLine());
                    ToDo TaskToSearch = Client.SearchTaskByIdAsync(searchId).Result;
                     if (TaskToSearch == null)
                     {
                            Console.WriteLine("No task with this ID exists!");
                             break;
                      }
                    ToDo searchedTaskById =Client.SearchTaskByIdAsync(searchId).Result;
                    Console.WriteLine($"  {searchedTaskById.Id}   {searchedTaskById.Name}  {searchedTaskById.Description}  {searchedTaskById.IsComplete}   {searchedTaskById.DueDate}");

                    break;
                case "setstatus":
                    Console.Write("Task Id: ");
                    int statusTaskId = int.Parse(Console.ReadLine());
                    var TaskEx2 = Client.SearchTaskByIdAsync(statusTaskId).Result;
                    if(TaskEx2 == null)
                    {
                        Console.WriteLine("No task with this ID exists!");
                        break;
                    }
                    Console.Write("Is Complete? (true/false): ");
                    bool statusIsComplete = bool.Parse(Console.ReadLine());
                    Client.SetTaskStatusAsync(statusTaskId , statusIsComplete).Wait();
                    ToDo TTaskById =Client.SearchTaskByIdAsync(statusTaskId).Result;
                    Connection.SendAsync("UpdateItem", new ToDo{
                        Name = TTaskById.Name , 
                        Id = TTaskById.Id , 
                        Description =TTaskById.Description , 
                        IsComplete = TTaskById.IsComplete , 
                        DueDate = TTaskById.DueDate
                    }).Wait();
                    break;
                case "profile":
                     Console.WriteLine("Please record your interests in three topics: movies, music, and sports. Use add or edit");
                     string Operation2 = Console.ReadLine().ToLower();
                     int Count  = Client.GetAllInterestsAsync().Result.Count;
                     switch(Operation2)
                     {
                        
                        case "add" :
                            
                            if(Count==0)
                            {
                            Console.WriteLine("Your Interest movie:");
                             string Movie = Console.ReadLine();
                            Console.WriteLine("Your Interest music:");
                             string Music = Console.ReadLine();
                            Console.WriteLine("Your Interest Sport:");
                             string Sport = Console.ReadLine();
                            Client.AddInterestAsync(new UserInterest{
                            Id = 1,
                            MovieInterest = Movie,
                            MusicInterest = Music,
                            SportInterest = Sport
                            }).Wait();
                            Count++;
                            }
                            else{
                                Console.WriteLine("if you are here to update your information use edit option");
                            }
                            break;
                        case  "edit" : 
                            Console.WriteLine("Your New Interest movie:");
                             string NewMovie = Console.ReadLine();
                            Console.WriteLine("Your New  Interest music:");
                             string NewMusic = Console.ReadLine();
                            Console.WriteLine("Your  New Interest Sport:");
                             string NewSport = Console.ReadLine();
                             Client.EditInterestAsync(1 , new UserInterest
                             {
                                Id =1 ,
                                MovieInterest = NewMovie,
                                MusicInterest = NewMusic,
                                SportInterest = NewSport
                             }).Wait();
                             break;
                       
                     }
                     break;
                     
                      
                case "exit":
                   Console.WriteLine("Bye!");
                   return;
                case "ai":
                       Console.WriteLine("What do you want to do? (breaktask/inspiretask/idontknowwhattodo/AskStartingGuide/Categorization)");
                       string Operation3 = Console.ReadLine().ToLower();
                       switch(Operation3)
                       {
                           case "breaktask" :
                               Console.WriteLine("Please provide the name of the task that you want to convert into subtasks.");
                                string  TaskNameB = Console.ReadLine();
                                int Min = int.Parse(Console.ReadLine());
                                List<ToDo>  Goals  = Client.BreakTaskAsync(TaskNameB , Min).Result.ToList();
                                foreach (var task in Goals)
                               {
                                  Console.WriteLine($"  {task.Name}  {task.Description} ");
                                   
                               }
                               Console.WriteLine("do you want Save Tasks to DataBase(yes/no)");
                               string YesOrNo  = Console.ReadLine();
                               if(YesOrNo == "yes")
                               {
                                   foreach (var task in Goals)
                                   {
                                      
                                        Client.AddTaskAsync(task).Wait();
                                   }
                                      Connection3.SendAsync("AddItem",Goals).Wait();
                               }
                               break;
                            case "inspiretask" : 
                                   Console.WriteLine("Please select which interest you would like to be inspired and motivated by(movie/music/sport), and kindly complete your interests in the profile section.");
                                   string Operation4 = Console.ReadLine().ToLower();
                                   switch(Operation4)
                                   {
                                    case "movie":
                                         Console.WriteLine("Can you please tell me the task that you would like to be motivated to do by using your favorite movie?");
                                         string TaskNamee = Console.ReadLine();
                                         string MovieI  = Client.GetInterestByIdAsync(1).Result.MovieInterest;
                                        Console.WriteLine(Client.InspireTaskAsync(MovieI,TaskNamee).Result.ToList()[0]);
                                         break;
                                    case "music":
                                           Console.WriteLine("Can you please tell me the task that you would like to be motivated to do by using your favorite music?");
                                           string TaskName2  =  Console.ReadLine();
                                           string MusicI = Client.GetInterestByIdAsync(1).Result.MusicInterest;
                                           Console.WriteLine(Client.InspireTaskAsync(MusicI , TaskName2).Result.ToList()[0]);
                                           break;
                                    case "sport":
                                        Console.WriteLine("Can you please tell me the task that you would like to be motivated to do by using your favorite sport?");
                                           string TaskName3  =  Console.ReadLine();
                                           string SportI = Client.GetInterestByIdAsync(1).Result.SportInterest;
                                           Console.WriteLine(Client.InspireTaskAsync(SportI , TaskName3).Result.ToList()[0]);
                                           break;
                                   }
                                   break;
                            case "idontknowwhattodo":
                                             Console.WriteLine("Tell me the names of the tasks you want to do today but don't know where to begin");
                                             string TaskNames = Console.ReadLine();
                                             Console.WriteLine(Client.IdontKnowWhatToDoAsync(TaskNames).Result.ToList()[0]);
                                             break;
                            case "askstartingguide" : 
                                            Console.WriteLine("Tell me the name of that task which you don't know where to begin  i can help you  :)");
                                            string TaskNameA = Console.ReadLine();
                                            Console.WriteLine(Client.AskStartingGuideAsync(TaskNameA).Result.ToList()[0]);
                                            break;
                            case "categorization" : 
                                            Console.WriteLine("Please write the number of your tasks for me to categorize and topic-model them");
                                            int  AllTaskNumbers   = int.Parse(Console.ReadLine());
                                            Console.WriteLine("Please write the names of your tasks for me to categorize and topic-model them");
                                            string AllTasks = Console.ReadLine();
                                            Console.WriteLine(Client.CategorizationAsync(AllTaskNumbers,AllTasks).Result.ToList()[0]);
                                            break;    
                       }
                       break;
                    //  string  TASK = Console.ReadLine();
                    //  int min = int.Parse(Console.ReadLine());
                    // List<ToDo>  GoAL  = Client.BreakTaskAsync(TASK , min).Result.ToList();
                    // foreach (var task in GoAL)
                    // {
                    //      Console.WriteLine($"  {task.Id}   {task.Name}  {task.Description}  {task.IsComplete}   {task.DueDate}");
                    //      Client.AddTaskAsync(task).Wait();
                    // }
                    // break;

                default:
                   Console.WriteLine("Invalid Operation. Please try again.");
                   break;
             

            }
        }
    }
}
