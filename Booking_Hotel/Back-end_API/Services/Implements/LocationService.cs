using Azure.Core;
using Back_end_API.Context;
using Back_end_API.Entites;
using Back_end_API.Payload.Response;
using Back_end_API.Services.Interface;

namespace Back_end_API.Services.Implements
{
    public class LocationService : ILocationService
    {
        private readonly AppDbContext _Context;
        private readonly ResponseObject<Locations> _responseObject;
        public LocationService(AppDbContext context, ResponseObject<Locations> responseObject)
        {
            _Context = context;
            _responseObject = responseObject;
        }
        //Thêm địa điểm
        public ResponseObject<Locations> AddLocation(string name, string description)
        {
            if(_Context.Locations.Any(x => x.LocationName.ToLower() == name.ToLower()))
            {
                return _responseObject.ResponseError(StatusCodes.Status400BadRequest, "Địa điểm này đã tồn tại", null);
            }
            Locations loca = new Locations
            {
                LocationName = name,
                Description = description
            };
            _Context.Locations.Add(loca);
            _Context.SaveChanges();
            return _responseObject.ResponseSuccess("Thêm địa điểm mới thành công!", loca);
        }
        //Sửa địa điểm
        public ResponseObject<Locations> UpdateLocation(int id, Locations location)
        {
            var loca = _Context.Locations.SingleOrDefault(x => x.ID == id);
            if(loca != null)
            {
                if (_Context.Locations.Any(x =>x.LocationName.Equals(location.LocationName, StringComparison.OrdinalIgnoreCase) && !loca.LocationName.Equals(location.LocationName, StringComparison.OrdinalIgnoreCase)))
                {
                    return _responseObject.ResponseError(StatusCodes.Status400BadRequest, "Địa điểm này đã tồn tại", null);
                }
                loca.LocationName = location.LocationName;
                loca.Description = location.Description;
                _Context.Locations.Update(location);
                _Context.SaveChanges();
                return _responseObject.ResponseSuccess("Sửa địa điểm thành công!", location);
            }
            return _responseObject.ResponseError(StatusCodes.Status404NotFound, "ID này không tồn tại", null);
        }
        //Xóa địa điểm
        public ResponseObject<IEnumerable<Locations>> DeleteLocation(int id)
        {
            var loca = _Context.Locations.SingleOrDefault(x => x.ID == id);
            if (loca != null)
            {
                _Context.Locations.Remove(loca);
                _Context.SaveChanges();
                var listLoca = _Context.Locations.AsQueryable().ToList();
                return new ResponseObject<IEnumerable<Locations>>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Xóa địa điểm thành công!",
                    Data = listLoca
                };
            }
            return new ResponseObject<IEnumerable<Locations>>
            {
                Status = StatusCodes.Status404NotFound,
                Message = "ID này không tồn tại",
                Data = null
            };
        }
    }
}
