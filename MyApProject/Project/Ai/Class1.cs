namespace Ai;
using OpenAI_API;
using OpenAI_API.Completions;

public class Class1
{
    OpenAIAPI openai = new OpenAIAPI(OpenAIKey);
    CompletionRequest completionRequest = new CompletionRequest();
  
    public static string OpenAIKey = "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";

public async Task<string> UseChatGPT(string  chatMessages)
{
     string outputResult = "";

        completionRequest.Prompt = chatMessages;
        completionRequest.Model = OpenAI_API.Models.Model.DavinciText;
        completionRequest.MaxTokens = 1024;
        completionRequest.NumChoicesPerPrompt = 1;

        var completions = await openai.Completions.CreateCompletionAsync(completionRequest);

        foreach (var completion in completions.Completions)
        {
            outputResult += completion.Text;
        }

        return outputResult;
}
}
