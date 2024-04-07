using Back_end_API.Entites;
using Back_end_API.Payload.Response;

namespace Back_end_API.Services.Interface
{
    public interface ILocationService
    {
        ResponseObject<Locations> AddLocation(string name, string description);
        ResponseObject<Locations> UpdateLocation(int id, Locations loca);
        ResponseObject<IEnumerable<Locations>> DeleteLocation(int id);
    }
}
