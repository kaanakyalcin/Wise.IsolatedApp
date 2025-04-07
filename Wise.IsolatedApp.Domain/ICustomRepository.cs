namespace Wise.IsolatedApp.Domain
{
    public interface ICustomRepository
    {
        string GetData(string secretValue);
    }
}
