namespace Wise.IsolatedApp.Domain
{
    public class CustomRepository : ICustomRepository
    {
        public string GetData(string secretValue)
        {
            return "This is a response from CustomRepository.";
        }
    }
}
