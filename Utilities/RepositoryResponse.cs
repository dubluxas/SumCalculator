namespace SumCalculator.Utilities;

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

