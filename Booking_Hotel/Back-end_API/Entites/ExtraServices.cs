using System.ComponentModel.DataAnnotations.Schema;

namespace Back_end_API.Entites
{
    [Table("ExtraServices_tbl")]
    public class ExtraServices:Base
    {
        //Bữa ăn
        public bool Meals { get; set; } = false;
        //Spa
        public bool Spa {  get; set; } = false;
        //Thuê xe đưa đón
        public bool CarRental { get; set; } = false;
        //Max services
        public bool Maximum { get; set; } = false;
        //Đậu xe
        public bool Parking { get; set; } = false;
        //Hướng dẫn viên du lịch
        public bool TourGuide { get; set; } = false;
        //Phòng gym
        public bool Gym { get; set; } = false;
    }
}
