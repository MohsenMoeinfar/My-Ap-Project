using Ai;
using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;
int SubTaskMinutes = 20;
string TaskName ;
int NextTaskId =1;
// string mystr  =  @"
//   Task: learn computer.	


// Class1 mm = new Class1();

 string MainPrompt = $@"Please break down the following task into {SubTaskMinutes} minute subtasks(max subtasks = 10). Please use the JSON template below.
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
    ]";
   int start =MainPrompt.IndexOf('[');   //solve problem in breaktask in  Ai
   string cleanedData = MainPrompt.Substring(start);

Console.WriteLine(cleanedData);
