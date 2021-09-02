using Shared.Enums;

namespace Core.ModelResponse
{
    public class ActionResponse
    {
        public State State { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public object Result { get; set; }
    }
}
