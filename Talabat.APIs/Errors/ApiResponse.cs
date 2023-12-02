namespace Talabat.APIs.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string?  Message { get; set; }
        public ApiResponse(int statusCode, string? message=null)
        {
            StatusCode = statusCode;
            Message = message?? GetDefaultMessage(statusCode);
        }

        private string? GetDefaultMessage(int statusCode)
        {
            return statusCode switch
            {
                404 => "Resource was not found",
                400=> "A bad Request , you have made",
                401=>"Authroized, you are not",
                500=>"Errors are the path in the dark side, Errors lead to anger, Anger leads to hate, Hate leads to career change"
            };
        }
    }
}
