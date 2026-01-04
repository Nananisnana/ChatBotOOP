using System.Threading.Tasks;

namespace ChatBotOOP
{
    public interface IChatService
    {
        
        Task<string> GetResponseAsync(string userMessage);
    }
}