namespace API.Errors
{
    public class APIException:APIResponse
    {
        public APIException(int statesCode,string message = null,string details = null)
            :base(statesCode,message)
        {
            Details = details;
        }
        public string Details { get; set; }
    }
}
