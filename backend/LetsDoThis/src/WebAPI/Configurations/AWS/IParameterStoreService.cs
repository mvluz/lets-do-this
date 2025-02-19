public interface IParameterStoreService
{
    Task<string> GetParameterAsync(string name);
}