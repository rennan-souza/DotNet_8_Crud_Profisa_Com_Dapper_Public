namespace CrudProfisaComDapper.Exception
{
    public class CustomException : IOException
    {
        public int StatusCode { get; }

        public CustomException(string message, int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
