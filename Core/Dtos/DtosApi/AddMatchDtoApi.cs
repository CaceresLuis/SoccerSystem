using System;

namespace Core.Dtos.DtosApi
{
    public class AddMatchDtoApi
    {
        public Guid VisitorId { get; set; }
        public Guid LocalId { get; set; }
        public DateTime Date { get; set; }
        public DateTime Hour { get; set; }
        public Guid GroupId { get; set; }
    }
}
