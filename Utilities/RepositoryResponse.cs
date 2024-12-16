namespace SumCalculator.Utilities;


/// <summary>
/// Defines a global return type for repository operations. Accepts an argument of type <typeparamref name="T"/> 
/// that specifies the return type. Provides methods to construct responses for successful or failed operations.
/// </summary>
/// <typeparam name="T">The type of data returned by the repository operation.</typeparam>
public class Repositoryresponse<T> where T : class
{
    public bool IsSuccessful { get; private set; }
    public T Data { get; private set; } = default!;
    public IList<string> Errors { get; private set; } = default!;

    public Repositoryresponse<T> CreateFailure(IList<string> errors)
    {
        return new Repositoryresponse<T>{ Errors = errors };
    }

    public Repositoryresponse<T> CreateFailure(string error)
    {
        return new Repositoryresponse<T>{Errors = [error] };
    }

    public Repositoryresponse<IList<T>> CreateSuccess(IList<T> data)
    {
        return new Repositoryresponse<IList<T>>{ IsSuccessful = true, Data = data };
    }

    public Repositoryresponse<T> CreateSuccess(T data)
    {
        return new Repositoryresponse<T>{ IsSuccessful = true, Data = data };
    }

}

