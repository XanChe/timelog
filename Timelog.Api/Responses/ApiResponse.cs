using Timelog.Core.Entities;

namespace Timelog.Api.Responses
{
    public enum ResponseStatus
    {
        success ,
        error 
    }
    public class ApiResponse
    {
        private readonly ResponseStatus status;
        public string Status 
        { get 
            { 
                switch (status)
                {
                    case ResponseStatus.success : return "success";
                    case ResponseStatus.error : return "error";
                    default : return "error";
                }
            } 
        }
        public string Message { get; }
        public string ResultGuid  { get; }        

        

        public ApiResponse(ResponseStatus status, string message, Entity? item = null)
        {
            this.status = status;
            Message = message;
            if (item != null)
            {
                ResultGuid = item.Id.ToString();
            }
            else
            {
                ResultGuid = "None";
            }
            
        }
    }
}
