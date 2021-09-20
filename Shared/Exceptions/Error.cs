using Shared.Enums;

namespace Shared.Exceptions
{
    public class Error
    {
        public string Code { get; set; }
        public string Title { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public State State { get; set; }
    }
}
