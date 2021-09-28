using MediatR;
using Core.Dtos;

namespace Core.Modules.GroupModule.List
{
    public class ListGroupQuery : IRequest<GroupDto[]>
    {
    }
}
