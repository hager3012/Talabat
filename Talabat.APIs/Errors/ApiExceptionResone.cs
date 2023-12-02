namespace Talabat.APIs.Errors
{
    public class ApiExceptionResone:ApiResponse
    {
        private readonly string? _details;

        public ApiExceptionResone(int statuCode, string? message=null,string? Details=null):base(statuCode, message)
        {
            _details = Details;
        }
    }
}
