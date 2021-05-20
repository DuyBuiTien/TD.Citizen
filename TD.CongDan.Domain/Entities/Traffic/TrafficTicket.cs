using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TD.Libs.Abstractions.Domain;

namespace TD.CongDan.Domain.Entities.Traffic
{
    //Vi phạm giao thông
    public class TrafficTicket : AuditableEntity
    {
        public string LicensePlateNumber { get; set; }
        //hành vi vi phạm
        public string Behaviour { get; set; }
        //Địa điểm
        public string Location { get; set; }
        //Thiết bị phát hiện
        public string Device { get; set; }
        //Đơn vị phát hiện
        public string Unit { get; set; }
        //Số điện thoại liên hệ
        public string PhoneNumber { get; set; }
        //Tiền phạt
        public int Price { get; set; }
        //Ảnh vi phạm
        public string Images { get; set; }
        //Trạng thái - 0: Chưa xử lý, 1 = : đang xử lý, 2 = đã xử lý
        public int Status { get; set; }
        //Mô tả thêm
        public string Description { get; set; }

       
    }
}
