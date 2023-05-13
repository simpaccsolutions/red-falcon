namespace RedFalcon.Application.Interfaces.Validator
{
    public interface IValidate<T> where T : class
    {
        public Task<bool> ValidateData(T value);
    }
}
