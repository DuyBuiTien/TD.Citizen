using Microsoft.EntityFrameworkCore;
using TD.CongDan.Domain.Entities;
using TD.CongDan.Domain.Entities.Company;

namespace TD.CongDan.Infrastructure.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AreaType>().HasData(
                 new AreaType() { Id = 1, Name = "Tỉnh", Code = "tinh", Description = "" },
                 new AreaType() { Id = 2, Name = "Thành phố", Code = "thanh-pho", Description = "" },
                 new AreaType() { Id = 3, Name = "Quận", Code = "quan", Description = "" },
                 new AreaType() { Id = 4, Name = "Huyện", Code = "huyen", Description = "" },
                 new AreaType() { Id = 5, Name = "Thị xã", Code = "thi-xa", Description = "" },
                 new AreaType() { Id = 6, Name = "Thị trấn", Code = "thi-tran", Description = "" },
                 new AreaType() { Id = 7, Name = "Phường", Code = "phuong", Description = "" },
                 new AreaType() { Id = 8, Name = "Xã", Code = "xa", Description = "" }
                 );
            modelBuilder.Entity<Category>().HasData(
                new Category() { Id = 1, Name = "Giao thông", Code = "giao-thong", Description = "" },
                new Category() { Id = 2, Name = "Y tế", Code = "y-te", Description = "" },
                new Category() { Id = 3, Name = "Giáo dục", Code = "giao-duc", Description = "" },
                new Category() { Id = 4, Name = "Môi trường", Code = "moi-truong", Description = "" },
                new Category() { Id = 5, Name = "Nông nghiệp", Code = "nong-nghiep", Description = "" },
                new Category() { Id = 6, Name = "Du lịch", Code = "du-lich", Description = "" },
                new Category() { Id = 7, Name = "Kinh tế", Code = "kinh-te", Description = "" },
                new Category() { Id = 8, Name = "Hành chính", Code = "hanh-chinh", Description = "" },
                new Category() { Id = 9, Name = "Khác", Code = "khac", Description = "" }
                );
            modelBuilder.Entity<PlaceType>().HasData(
                new PlaceType() { Id = 1, CategoryId = 2, Name = "Bệnh viện", Code = "benh-vien", Icon = "hospital", Description = "" },
                new PlaceType() { Id = 2, CategoryId = 2, Name = "Hiệu thuốc", Code = "benh-vien", Icon = "capsules", Description = "" },
                new PlaceType() { Id = 3, CategoryId = 2, Name = "Phòng khám", Code = "phong-kham", Icon = "stethoscope", Description = "" },
                new PlaceType() { Id = 4, CategoryId = 2, Name = "Trạm y tế", Code = "tram-y-te", Icon = "briefcase-medical", Description = "" },
                new PlaceType() { Id = 5, CategoryId = 1, Name = "Trạm xăng", Code = "tram-xang", Icon = "gas-pump", Description = "" },
                new PlaceType() { Id = 6, CategoryId = 1, Name = "Điểm đỗ xe", Code = "diem-do-xe", Icon = "parking", Description = "" },
                new PlaceType() { Id = 7, CategoryId = 1, Name = "Gara ô tô", Code = "gara-o-to", Icon = "tools", Description = "" },
                new PlaceType() { Id = 8, CategoryId = 1, Name = "Trạm thu phí", Code = "tram-thu-phi", Icon = "money-bill", Description = "" },
                new PlaceType() { Id = 9, CategoryId = 1, Name = "Điểm đen giao thông", Code = "diem-den-giao-thong", Icon = "money-bill", Description = "" },
                new PlaceType() { Id = 10, CategoryId = 6, Name = "Nhà hàng", Code = "nha-hang", Icon = "concierge-bell", Description = "" },
                new PlaceType() { Id = 11, CategoryId = 6, Name = "Khách sạn", Code = "khach-san", Icon = "hotel", Description = "" },
                new PlaceType() { Id = 12, CategoryId = 6, Name = "Điểm mua sắm", Code = "diem-mua-sam", Icon = "shopping-cart", Description = "" },
                new PlaceType() { Id = 13, CategoryId = 6, Name = "Địa điểm nổi tiếng", Code = "dia-diem-noi-tieng", Icon = "map-marked", Description = "" },
                new PlaceType() { Id = 14, CategoryId = 6, Name = "Di tích lịch sử", Code = "di-tich-lich-su", Icon = "monument", Description = "" },
                new PlaceType() { Id = 15, CategoryId = 6, Name = "Sự kiện dịp Tết", Code = "su-kien-dip-tet", Icon = "calendar-day", Description = "" },
                new PlaceType() { Id = 16, CategoryId = 6, Name = "Lễ hội", Code = "le-hoi", Icon = "mask", Description = "" },
                new PlaceType() { Id = 17, CategoryId = 6, Name = "Danh lam thắng cảnh", Code = "danh-lam-thang-canh", Icon = "gratipay", Description = "" },
                new PlaceType() { Id = 18, CategoryId = 6, Name = "Đền", Code = "den", Icon = "gopuram", Description = "" },
                new PlaceType() { Id = 19, CategoryId = 6, Name = "Chùa", Code = "chua", Icon = "gopuram", Description = "" },
                new PlaceType() { Id = 20, CategoryId = 6, Name = "Nhà thờ", Code = "nha-tho", Icon = "church", Description = "" },
                new PlaceType() { Id = 21, CategoryId = 6, Name = "Điểm ô nhiễm", Code = "diem-o-nhiem", Icon = "biohazard", Description = "" },
                new PlaceType() { Id = 22, CategoryId = 6, Name = "Điểm xử lý rác", Code = "diem-xu-ly-rac", Icon = "recycle", Description = "" },
                new PlaceType() { Id = 23, CategoryId = 7, Name = "Công ty", Code = "cong-ty", Icon = "building", Description = "" },
                new PlaceType() { Id = 24, CategoryId = 9, Name = "Khác", Code = "khac", Icon = "map-marker", Description = "" }
                );
            modelBuilder.Entity<Benefit>().HasData(
               new Benefit() { Id = 1, Name = "Thưởng", Code = "", Icon = "money-bill-wave", Description = "" },
               new Benefit() { Id = 2, Name = "Giải thưởng", Code = "", Icon = "award", Description = "" },
               new Benefit() { Id = 3, Name = "Du lịch", Code = "", Icon = "plane", Description = "" },
               new Benefit() { Id = 4, Name = "Phiếu giảm giá", Code = "", Icon = "piggy-bank", Description = "" },
               new Benefit() { Id = 5, Name = "Khám sức khỏe", Code = "", Icon = "user-nurse", Description = "" },
               new Benefit() { Id = 6, Name = "Thư viện", Code = "", Icon = "book-reader", Description = "" },
               new Benefit() { Id = 7, Name = "Hoạt động nhóm", Code = "", Icon = "swimmer", Description = "" },
               new Benefit() { Id = 8, Name = "Trông trẻ", Code = "", Icon = "baby-carriage", Description = "" },
               new Benefit() { Id = 9, Name = "Nghỉ phép", Code = "", Icon = "umbrella-beach", Description = "" },
               new Benefit() { Id = 10, Name = "Laptop", Code = "", Icon = "laptop", Description = "" },
               new Benefit() { Id = 11, Name = "Trợ cấp đi lại", Code = "", Icon = "taxi", Description = "" },
               new Benefit() { Id = 12, Name = "Đào tạo", Code = "", Icon = "chalkboard-teacher", Description = "" },
               new Benefit() { Id = 13, Name = "Điện thoại", Code = "", Icon = "mobile", Description = "" },
               new Benefit() { Id = 14, Name = "Căn-tin", Code = "", Icon = "hamburger", Description = "" },
               new Benefit() { Id = 15, Name = "Khác", Code = "", Icon = "ellipsis-h", Description = "" }
               );

            modelBuilder.Entity<Salary>().HasData(
                new Salary() { Id = 1, Name = "Dưới 3 triệu", Code = "", Description = "" },
                new Salary() { Id = 2, Name = "3 - 5 triệu", Code = "", Description = "" },
                new Salary() { Id = 3, Name = "5 -7 triệu", Code = "", Description = "" },
                new Salary() { Id = 4, Name = "7 - 10 triệu", Code = "", Description = "" },
                new Salary() { Id = 5, Name = "10 - 12 triệu", Code = "", Description = "" },
                new Salary() { Id = 6, Name = "12 - 15 triệu", Code = "", Description = "" },
                new Salary() { Id = 7, Name = "15 - 20 triệu", Code = "", Description = "" },
                new Salary() { Id = 8, Name = "20 - 25 triệu", Code = "", Description = "" },
                new Salary() { Id = 9, Name = "25 - 30 triệu", Code = "", Description = "" },
                new Salary() { Id = 10, Name = "Trên 30 triệu", Code = "", Description = "" },
                new Salary() { Id = 11, Name = "Thỏa thuận", Code = "", Description = "" }
            );

            modelBuilder.Entity<JobType>().HasData(
               new JobType() { Id = 1, Name = "Toàn thời gian", Code = "", Description = "" },
              new JobType() { Id = 2, Name = "Bán thời gian", Code = "", Description = "" },
              new JobType() { Id = 3, Name = "Thực tập", Code = "", Description = "" },
              new JobType() { Id = 4, Name = "Nghề tự do", Code = "", Description = "" },
              new JobType() { Id = 5, Name = "Hợp đồng thời vụ", Code = "", Description = "" },
              new JobType() { Id = 6, Name = "Khác", Code = "", Description = "" }
           );
            modelBuilder.Entity<JobPosition>().HasData(
              new JobPosition() { Id = 1, Name = "Mới tốt nghiệp", Code = "", Description = "" },
             new JobPosition() { Id = 2, Name = "Nhân viên", Code = "", Description = "" },
             new JobPosition() { Id = 3, Name = "Trưởng phòng", Code = "", Description = "" },
             new JobPosition() { Id = 4, Name = "Giám đốc và cấp cao hơn", Code = "", Description = "" },
             new JobPosition() { Id = 5, Name = "Khác", Code = "", Description = "" }
          );

            modelBuilder.Entity<JobName>().HasData(
              new JobName() { Id = 1, Name = "An toàn lao động", Code = "", Description = "" },
             new JobName() { Id = 2, Name = "Bán hàng kỹ thuật", Code = "", Description = "" },
             new JobName() { Id = 3, Name = "Bán lẻ/Bán sỉ", Code = "", Description = "" },
             new JobName() { Id = 4, Name = "Báo chí/Truyền hình", Code = "", Description = "" },
             new JobName() { Id = 5, Name = "Bảo hiểm", Code = "", Description = "" },
             new JobName() { Id = 6, Name = "Bảo trì/Sửa chữa", Code = "", Description = "" },
             new JobName() { Id = 7, Name = "Bất động sản", Code = "", Description = "" },
             new JobName() { Id = 8, Name = "Biên/Phiên dịch", Code = "", Description = "" },
             new JobName() { Id = 9, Name = "Bưu chính viễn thông", Code = "", Description = "" },
             new JobName() { Id = 10, Name = "Chứng khoán/Vàng/Ngoại tệ", Code = "", Description = "" },
             new JobName() { Id = 11, Name = "Cơ khí/Chế tạo/Tự động hóa", Code = "", Description = "" },
             new JobName() { Id = 12, Name = "Công nghệ cao", Code = "", Description = "" },
             new JobName() { Id = 13, Name = "Công nghệ ô tô", Code = "", Description = "" },
             new JobName() { Id = 14, Name = "Công nghệ thông tin", Code = "", Description = "" },
             new JobName() { Id = 15, Name = "Dầu khí, khóa chất", Code = "", Description = "" },
             new JobName() { Id = 16, Name = "Dịch vụ khách hàng", Code = "", Description = "" },
             new JobName() { Id = 17, Name = "Điện/Điện tử/Điện lạnh", Code = "", Description = "" },
             new JobName() { Id = 18, Name = "Du lịch", Code = "", Description = "" },
             new JobName() { Id = 19, Name = "Giáo dục/Đào tạo", Code = "", Description = "" },
             new JobName() { Id = 20, Name = "Tư vấn", Code = "", Description = "" },
             new JobName() { Id = 21, Name = "Vận tải/Kho vận", Code = "", Description = "" },
             new JobName() { Id = 22, Name = "Y tế/Dược", Code = "", Description = "" },
             new JobName() { Id = 23, Name = "Khác", Code = "", Description = "" }
          );
            modelBuilder.Entity<Industry>().HasData(
            new Industry() { Id = 1, Name = "Agency (Design/Development)", Code = "", Description = "" },
new Industry() { Id = 2, Name = "Agency (Marketing/Advertising)", Code = "", Description = "" },
new Industry() { Id = 3, Name = "Bán lẻ - Hàng tiêu dùng - FMCG", Code = "", Description = "" },
new Industry() { Id = 4, Name = "Bảo hiểm", Code = "", Description = "" },
new Industry() { Id = 5, Name = "Bảo trì / Sửa chữa", Code = "", Description = "" },
new Industry() { Id = 6, Name = "Bất động sản", Code = "", Description = "" },
new Industry() { Id = 7, Name = "Chứng khoán", Code = "", Description = "" },
new Industry() { Id = 8, Name = "Cơ khí", Code = "", Description = "" },
new Industry() { Id = 9, Name = "Cơ quan nhà nước", Code = "", Description = "" },
new Industry() { Id = 10, Name = "Du lịch", Code = "", Description = "" },
new Industry() { Id = 11, Name = "Dược phẩm / Y tế / Công nghệ sinh học", Code = "", Description = "" },
new Industry() { Id = 12, Name = "Điện tử / Điện lạnh", Code = "", Description = "" },
new Industry() { Id = 13, Name = "Giải trí", Code = "", Description = "" },
new Industry() { Id = 14, Name = "Giáo dục / Đào tạo", Code = "", Description = "" },
new Industry() { Id = 15, Name = "In ấn / Xuất bản", Code = "", Description = "" },
new Industry() { Id = 16, Name = "Internet / Online", Code = "", Description = "" },
new Industry() { Id = 17, Name = "IT - Phần cứng", Code = "", Description = "" },
new Industry() { Id = 18, Name = "IT - Phần mềm", Code = "", Description = "" },
new Industry() { Id = 19, Name = "Kế toán / Kiểm toán", Code = "", Description = "" },
new Industry() { Id = 20, Name = "Khác", Code = "", Description = "" },
new Industry() { Id = 21, Name = "Logistics - Vận tải", Code = "", Description = "" },
new Industry() { Id = 22, Name = "Luật", Code = "", Description = "" },
new Industry() { Id = 23, Name = "Marketing / Truyền thông / Quảng cáo", Code = "", Description = "" },
new Industry() { Id = 24, Name = "Môi trường", Code = "", Description = "" },
new Industry() { Id = 25, Name = "Năng lượng", Code = "", Description = "" },
new Industry() { Id = 26, Name = "Ngân hàng", Code = "", Description = "" },
new Industry() { Id = 27, Name = "Nhà hàng / Khách sạn", Code = "", Description = "" },
new Industry() { Id = 28, Name = "Nhân sự", Code = "", Description = "" },
new Industry() { Id = 29, Name = "Nông Lâm Ngư nghiệp", Code = "", Description = "" },
new Industry() { Id = 30, Name = "Sản xuất", Code = "", Description = "" },
new Industry() { Id = 31, Name = "Tài chính", Code = "", Description = "" },
new Industry() { Id = 32, Name = "Thiết kế / kiến trúc", Code = "", Description = "" },
new Industry() { Id = 33, Name = "Thời trang", Code = "", Description = "" },
new Industry() { Id = 34, Name = "Thương mại điện tử", Code = "", Description = "" },
new Industry() { Id = 35, Name = "Tổ chức phi lợi nhuận", Code = "", Description = "" },
new Industry() { Id = 36, Name = "Tự động hóa", Code = "", Description = "" },
new Industry() { Id = 37, Name = "Tư vấn", Code = "", Description = "" },
new Industry() { Id = 38, Name = "Viễn thông", Code = "", Description = "" },
new Industry() { Id = 39, Name = "Xây dựng", Code = "", Description = "" },
new Industry() { Id = 40, Name = "Xuất nhập khẩu", Code = "", Description = "" }
         );
            modelBuilder.Entity<Experience>().HasData(
   new Experience() { Id = 1, Name = "Chưa có kinh nghiệm", Code = "", Description = "" },
  new Experience() { Id = 2, Name = "Dưới 1 năm", Code = "", Description = "" },
  new Experience() { Id = 3, Name = "1 năm", Code = "", Description = "" },
  new Experience() { Id = 4, Name = "2 năm", Code = "", Description = "" },
  new Experience() { Id = 5, Name = "3 năm", Code = "", Description = "" },
  new Experience() { Id = 6, Name = "4 năm", Code = "", Description = "" },
  new Experience() { Id = 7, Name = "5 năm", Code = "", Description = "" },
  new Experience() { Id = 8, Name = "Trên 5 năm", Code = "", Description = "" }
);

            modelBuilder.Entity<Degree>().HasData(
   new Degree() { Id = 1, Name = "Trung học phổ thông", Code = "", Description = "" },
  new Degree() { Id = 2, Name = "Trung cấp", Code = "", Description = "" },
  new Degree() { Id = 3, Name = "Cao đẳng/Đại học", Code = "", Description = "" },
  new Degree() { Id = 4, Name = "Thạch sĩ", Code = "", Description = "" },
  new Degree() { Id = 5, Name = "Tiến sĩ", Code = "", Description = "" },
  new Degree() { Id = 6, Name = "Khác", Code = "", Description = "" }
);


            modelBuilder.Entity<IdentityType>().HasData(
   new IdentityType() { Id = 1, Name = "Chứng minh nhân dân", Code = "", Description = "" },
  new IdentityType() { Id = 2, Name = "Căn cước công dân", Code = "", Description = "" },
  new IdentityType() { Id = 3, Name = "Hộ chiếu", Code = "", Description = "" }
 
);

            modelBuilder.Entity<Gender>().HasData(
  new Gender() { Id = 1, Name = "Nam", Code = "", Description = "" },
 new Gender() { Id = 2, Name = "Nữ", Code = "", Description = "" },
 new Gender() { Id = 3, Name = "Khác", Code = "", Description = "" }


);
        }
    }
}