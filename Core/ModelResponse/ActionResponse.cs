using Shared.Enums;

namespace Core.ModelResponse
{
    public class ActionResponse
    {
        public string Title { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public State State { get; set; }
    }
}
