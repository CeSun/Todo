using Azure.Core;
using Azure.Identity;
using Microsoft.Graph;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Todo.Apis
{
    public class GraphHelper
    {
        private static DeviceCodeCredential tokenCredential;
        public static GraphServiceClient graphClient;

        public static async Task Initialize(string clientId,
                                      string[] scopes,
                                      Func<DeviceCodeInfo, CancellationToken, Task> callBack)
        {
            tokenCredential = new DeviceCodeCredential(callBack, "common", clientId);
            graphClient = new GraphServiceClient(tokenCredential, scopes);
            var context = new TokenRequestContext(scopes);
            await tokenCredential.GetTokenAsync(context);
        }

        public static async Task<string> GetAccessTokenAsync(string[] scopes)
        {
            var context = new TokenRequestContext(scopes);
            var response = await tokenCredential.GetTokenAsync(context);
            
            return response.Token;
        }


        public static async Task LoginAsync(string UserName, string Password)
        {
            var scopes = new[] { "Tasks.ReadWrite" };
            var cre = new UsernamePasswordCredential(UserName, Password, "common", "b600f125-dd3b-4d5b-a331-0bc8007795b6");
            graphClient = new GraphServiceClient(cre, scopes);
            var context = new TokenRequestContext(scopes);
            var response = await cre.GetTokenAsync(context);
           
        }
        public static async Task<ITodoListsCollectionPage> GetTaskLists()
        {
            var lists=  await graphClient.Me.Todo.Lists.Request().GetAsync();
            
            return lists;
        }

        public static async Task<ITodoTaskListTasksCollectionPage> GetTaskList(string listId)
        {
            var lists = await graphClient.Me.Todo.Lists[listId].Tasks.Request().GetAsync();
            return lists;
        }

        public static async Task<User> GetUser()
        {
            return await graphClient.Me.Request().GetAsync();
        }

        public static async Task AddTask(string TaskTitle, string id)
        {
            var todoTask = new TodoTask
            {Title = TaskTitle,};
            await graphClient.Me.Todo.Lists[id].Tasks.Request().AddAsync(todoTask);
        }

    }
}
