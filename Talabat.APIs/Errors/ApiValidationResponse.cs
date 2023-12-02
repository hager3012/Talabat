namespace Talabat.APIs.Errors
{
    public class ApiValidationResponse:ApiResponse
    {
        public IEnumerable<string> Errors { get; set; }

        public ApiValidationResponse(int StatusCode):base(StatusCode)
        {
            Errors = new List<string>();
        }
    }
}
