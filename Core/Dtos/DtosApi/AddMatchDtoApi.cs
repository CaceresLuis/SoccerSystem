using System;

namespace Core.Dtos.DtosApi
{
    public class AddMatchDtoApi
    {
        public int VisitorId { get; set; }
        public int LocalId { get; set; }
        public DateTime Date { get; set; }
        public DateTime Hour { get; set; }
        public int GroupId { get; set; }
    }
}
