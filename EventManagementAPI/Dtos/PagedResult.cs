//Ignore Spelling: Dtos
namespace EventManagementAPI.Dtos
{
    //this is a container that sends page of results + pagination info to the client instead of sending all 10,000 pages it'll send 10 pages etc
    public class PagedResult<T> {
        //this is a template that can be any data which in our case it will be EventDetailsResponse, UserResponse, RegistrationReponse
        public List<T> Items { get; set; } = new();
        //total number of records in the Db
        public int TotalCount { get; set; }
        //the page it is on
        public int Page { get; set; }
        //how many pages in total
        public int PageSize { get; set; }
    }
}
