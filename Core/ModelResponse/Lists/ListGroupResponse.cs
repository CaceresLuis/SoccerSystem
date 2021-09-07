using System.Collections.Generic;

namespace Core.ModelResponse.Lists
{
    public class ListGroupResponse
    {
        public IEnumerable<Group> Groups { get; set; }
        public ActionResponse Data { get; set; }
    }
}
