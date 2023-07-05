namespace API.Errors
{
    public class APIResponse
    {
        public APIResponse(int statesCode,string message = null)
        {
            StatesCode = statesCode;
            Message = message??GetDefaultMessageForStatesCode(StatesCode);
        }
        public int StatesCode { get; set; }
        public string Message { get; set; }
        private string GetDefaultMessageForStatesCode(int statesCode)
        {
            return statesCode switch
            {
                400 => "A Bad Request",
                401 => "You are Not Authorized",
                404 => "Not Found Response",
                500 => "Server Error Occured",
                _=> "null"
            };
        }
    }
}
