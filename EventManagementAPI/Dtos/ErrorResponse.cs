//Ignore Spelling: Dtos
namespace EventManagementAPI.Dtos
{
    //this will be a object that is used to return error messages in a clean, consistent format
    public class ErrorResponse {
        //just need a getter no set since we only need to get the error
        public string Error { get; }
        //constructor that has to pass in a string that shows the error such as BadRequest()
        public ErrorResponse(string error) {
            Error = error;
        }
    }
}
