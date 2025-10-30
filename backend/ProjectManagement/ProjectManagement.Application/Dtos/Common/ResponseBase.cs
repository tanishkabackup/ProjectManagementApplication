namespace ProjectManagement.Application.Dtos.Common
{
    public class ResponseBase
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = null!;
    }
}
