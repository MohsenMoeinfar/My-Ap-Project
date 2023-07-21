using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Ai;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;

Class1 mm = new Class1();

using TodoContext MyTodoContext = new TodoContext();
void AddTask(ToDo task)
{
        try{
        MyTodoContext.ToDos.Add(task);
        MyTodoContext.SaveChanges();
        }
        catch(Exception e)
        {
               Console.WriteLine(e.Message);
        }
}
void DeleteAllTasks()
{
    var AllTasks = MyTodoContext.ToDos.ToList();
    if (AllTasks.Count > 0)
    {
        MyTodoContext.ToDos.RemoveRange(AllTasks);
        MyTodoContext.SaveChanges();
    }
}
void DeleteTask(int taskId)
{
  
        var taskToDelete = MyTodoContext.ToDos.FirstOrDefault(t => t.Id == taskId);

        if (taskToDelete != null)
        {
            MyTodoContext.ToDos.Remove(taskToDelete);
            MyTodoContext.SaveChanges();
        }
}
void DeleteInterest (int taskid)
{
        var interesttodelete = MyTodoContext.Interests.FirstOrDefault(t=> t.Id == taskid);
        if(interesttodelete != null)
        {
                MyTodoContext.Interests.Remove(interesttodelete);
                MyTodoContext.SaveChanges();
        }
}
void EditTask(ToDo updatedTask)
{
  
    
        var taskToEdit = MyTodoContext.ToDos.FirstOrDefault(t => t.Id == updatedTask.Id);

        if (taskToEdit != null)
        {
            taskToEdit.Name = updatedTask.Name;
            taskToEdit.Description = updatedTask.Description;
            taskToEdit.DueDate = updatedTask.DueDate;
            taskToEdit.IsComplete = updatedTask.IsComplete;

            MyTodoContext.SaveChanges();
        }
}
List<ToDo> GetAllTasks()
{
    
        return MyTodoContext.ToDos.ToList();

}
List<ToDo> SearchTasksByName(string name)
{
        return MyTodoContext.ToDos.Where(t => t.Name.ToLower().Contains(name.ToLower())).ToList();
}
ToDo SearchTaskById(int id)
{
    
        return MyTodoContext.ToDos.FirstOrDefault(t => t.Id == id);

}
void SetTaskStatus(int taskId, bool isComplete)
{
        var taskToEdit = MyTodoContext.ToDos.FirstOrDefault(t => t.Id == taskId);

        if (taskToEdit != null)
        {
            taskToEdit.IsComplete = isComplete;
            MyTodoContext.SaveChanges();
        }
        
}
void AddInterest(UserInterest Ui)
{
        MyTodoContext.Interests.Add(Ui);
        MyTodoContext.SaveChanges();
}
List<UserInterest> GetAllInterests()
{
        return MyTodoContext.Interests.ToList();
}
UserInterest GetInterestById(int id)
{
        return MyTodoContext.Interests.FirstOrDefault(i=> i.Id == id);
}
void EditInterest(int id , UserInterest UpdInt)
{
        UserInterest InterestToEdit = MyTodoContext.Interests.FirstOrDefault(i=> i.Id == id);
        if (InterestToEdit != null)
        {
            InterestToEdit.MusicInterest = UpdInt.MusicInterest;
            InterestToEdit.MovieInterest = UpdInt.MovieInterest;
            InterestToEdit.SportInterest = UpdInt.SportInterest;
            MyTodoContext.SaveChanges();
        }
}
void AddPrompts(Prompts Prm)
{
        MyTodoContext.Prompts_Data.Add(Prm);
        MyTodoContext.SaveChanges();
}
void DeletePrompt(int PrId)
{
        var PrToDelete = MyTodoContext.Prompts_Data.FirstOrDefault(t => t.ID == PrId);
        if (PrToDelete != null)
        {
            MyTodoContext.Prompts_Data.Remove(PrToDelete);
            MyTodoContext.SaveChanges();
        }

}
List<Prompts> GetAllPrompts()
{
        return MyTodoContext.Prompts_Data.ToList();
}
async Task<List<string>> Categorization(int Works , string Tasks)
{
        string MainPrompt = @$"I have these {Works} tasks, can you categorize and topic-model them for me?
        Tasks: {Tasks}";
        List<string> AAbbbb = new List<string>();
        foreach(var pr in  GetAllPrompts())
        {
                if(MainPrompt == pr.StorePrompts) 
                {
                    AAbbbb.Add(pr.Ans);
                       return AAbbbb;
                }
                
        }
        AAbbbb.Add(@$"{await mm.UseChatGPT(MainPrompt)}");
        AddPrompts(new Prompts(GetAllPrompts().Count +1  , MainPrompt , AAbbbb[0]));
        return AAbbbb;
}

async Task<List<string>> AskStartingGuide(string TaskName)
{
        string GoalPromptt = @$"I have to do the task below but I don't know where to start. Can you guide me?
        Task: {TaskName}
        ";
        List<string> AAbbb = new List<string>();
        foreach(var pr in GetAllPrompts())
        {
                if(GoalPromptt == pr.StorePrompts)
                {
                        AAbbb.Add(pr.Ans);
                       return AAbbb;
                }
        }
        AAbbb.Add(@$"{await mm.UseChatGPT(GoalPromptt)}");
        AddPrompts(new Prompts(GetAllPrompts().Count+1 , GoalPromptt , AAbbb[0]));
        return AAbbb;

}
async Task<List<string>> IdontKnowWhatToDo(string Tasks)
{
        string GoallPrompt = @$"I have a lot of work to do today and I don't know where to start. Can you help me prioritize my tasks? My tasks are: {Tasks}  and Tell me why you are suggesting this order to me.";
        List<string> AAbb = new List<string>();
        foreach(var pr in  GetAllPrompts())
        {
                if(GoallPrompt == pr.StorePrompts)
                {
                       AAbb.Add(pr.Ans);
                       return AAbb;
                }
        }
        AAbb.Add(@$"{await mm.UseChatGPT(GoallPrompt)}");
        AddPrompts(new Prompts(GetAllPrompts().Count+ 1 , GoallPrompt , AAbb[0] ));
        return AAbb;
}
async Task<List<string>> InspireTaskAsync(string Interest , string TaskName)
{
  string GoalPrompt = @$"I have to do the task below, 
  but I don't feel like doing it. 
  I'm a big fan of {Interest}. 
  Can you give me enough motivation to do this task using {Interest}?
  Task: {TaskName}.";
  List<string> AAb = new List<string>();
  foreach(var pr in GetAllPrompts())
  {
        if(GoalPrompt == pr.StorePrompts)
        {
              AAb.Add(pr.Ans);
              return AAb;
        }
  }
  AAb.Add(@$"{await mm.UseChatGPT(GoalPrompt)}");

  AddPrompts(new Prompts(GetAllPrompts().Count+1 , GoalPrompt , AAb[0]));
  return AAb;
}
async Task<List<ToDo>>  BreakTask(string TaskName , int SubTaskMinutes)
{
    int LastTaskId = await MyTodoContext.ToDos.OrderByDescending(t => t.Id).Select(t => t.Id).FirstOrDefaultAsync();
    int NextTaskId = LastTaskId + 1;
    
    string MainPrompt = $@"Please break down the following task into {SubTaskMinutes} minute subtasks (max subtasks = 10). Please use the JSON template below.
    [
        {{
            ""Name"": ""<name_1>"", 
            ""Id"": {NextTaskId}, 
            ""Description"": ""<description_1>"",
            ""IsComplete"": false,
            ""DueDate"": ""2023-06-30T10:00:00Z""
        }},
        {{
            ""Name"": ""<name_2>"", 
            ""Id"": {(NextTaskId + 1)}, 
            ""Description"": ""<description_2>"",
            ""IsComplete"": false,
            ""DueDate"": ""2023-06-30T10:00:00Z""
        }},
        ...
        {{
            ""Name"": ""<name_10>"", 
            ""Id"": {(NextTaskId + 9)}, 
            ""Description"": ""<description_10>"",
            ""IsComplete"": false,
            ""DueDate"": ""2023-06-30T10:00:00Z""
        }}
    ]

    The task is: {TaskName}.";

    string FakeMainPrompt = $"{TaskName} {SubTaskMinutes}";
     foreach(var pr in GetAllPrompts())
  {
        if(FakeMainPrompt == pr.StorePrompts)
        {
               int start =  pr.Ans.IndexOf('[');
               string CleanAns = pr.Ans.Substring(start);
               return JsonConvert.DeserializeObject<List<ToDo>>(CleanAns);
        }
  }
        var Jsonn = await mm.UseChatGPT(MainPrompt);
        int TargetAnsId = Jsonn.IndexOf('[');
        string TargetAns =  Jsonn.Substring(TargetAnsId);
        AddPrompts(new Prompts(GetAllPrompts().Count+1 , FakeMainPrompt , TargetAns));
        var Tasks = JsonConvert.DeserializeObject<List<ToDo>>(TargetAns);
        // foreach(var t in Tasks)
        //         AddTask(t);
        return Tasks;
}
var Builder = WebApplication.CreateBuilder(args);
Builder.Services.AddSignalR();
Builder.Services.AddSwaggerGen();
Builder.Services.AddEndpointsApiExplorer();
// Builder.Services.AddServerSideBlazor();
Builder.Services.AddCors();


var App = Builder.Build();
App.UseCors(policy => 
     policy.WithOrigins("http://localhost:5121", "https://localhost:5001")
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials());
    
  

App.UseSwagger();
App.UseSwaggerUI();
App.MapDelete("/DeleteTask" , (int id) => DeleteTask(id));
App.MapDelete("/DeleteAllTasks" , () => DeleteAllTasks());
App.MapDelete("/DeleteInterest" , (int id) => DeleteInterest(id));
App.MapDelete("/DeletePrompts" , (int id) => DeletePrompt(id));
App.MapPut("/EditTask", (ToDo t ) => EditTask(t));
App.MapPut("/EditInterest" , (int id , UserInterest upd) => EditInterest(id,upd));
App.MapPost("/AddTask" , (ToDo t) => AddTask(t));
App.MapPost("/AddInterest" , (UserInterest Usi) => AddInterest(Usi));
App.MapPost("/AddPrompts" , (Prompts P) => AddPrompts(P));
App.MapGet("/GetAllTasks" , () => GetAllTasks());
App.MapGet("/GetAllPrompts" , () => GetAllPrompts());
App.MapGet("/GetAllInterests" , () => GetAllInterests());
App.MapGet("/GetInterestById" , (int id) => GetInterestById(id));
App.MapGet("/SearchTasksByName", (string name) => SearchTasksByName(name));
App.MapGet("/SearchTaskById" , (int id) => SearchTaskById(id));
App.MapGet("/BreakTask" , (string TaskName , int SubMin) => BreakTask(TaskName , SubMin));
App.MapGet("/InspireTaskAsync" , (string interestt  , string taskname) => InspireTaskAsync(interestt,taskname));
App.MapGet("/IdontKnowWhatToDo" , (string Tasks) => IdontKnowWhatToDo(Tasks));
App.MapGet("/AskStartingGuide" , (string TaskName) => AskStartingGuide(TaskName));
App.MapGet("/Categorization" , (int Count , string Tasks) => Categorization(Count , Tasks));
App.MapPut("/SetTaskStatus" , (int Tid  , bool IsComOrNot) => SetTaskStatus(Tid , IsComOrNot));
App.MapHub<Dhbs<ToDo>>("/Dhbs");
App.MapHub<Dhbs<int>>("/Dhbs2");
App.MapHub<Dhbs<List<ToDo>>>("/Dhbs3");


App.Run();


public class Dhbs<T> : Hub
    {
        

        public async Task AddItem(T item)
        {
           
            await Clients.All.SendAsync("OnItemAdded", item);
        }

        public async Task RemoveItem(T item)
        {
            await Clients.All.SendAsync("OnItemRemoved", item);
        }

        public async Task UpdateItem(T item)
        {
            await Clients.All.SendAsync("OnItemUpdated", item);
        }
    }
 