using MediatR;
using Core.Dtos.DtosApi;

namespace Core.Modules.GroupModule.List
{
    public class ListGroupQuery : IRequest<GroupFullDataApi[]>
    {
    }
}
