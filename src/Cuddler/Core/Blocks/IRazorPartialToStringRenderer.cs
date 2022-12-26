namespace Cuddler.Core.Blocks;

public interface IRazorPartialToStringRenderer
{
    Task<string> RenderPartialToStringAsync<TModel>(string partialName, TModel model);
}