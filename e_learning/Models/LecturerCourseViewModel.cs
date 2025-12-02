using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace e_learning.Models
{
    public class LecturerCourseViewModel
    {
        // Dùng decimal vì ID trong Oracle Entity của bạn là decimal
        public decimal CourseID { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên khóa học")]
        [Display(Name = "Tên khóa học")]
        [MaxLength(100, ErrorMessage = "Tên khóa học không được quá 100 ký tự")]
        public string CourseName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mô tả")]
        [Display(Name = "Mô tả chi tiết")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập giá bán")]
        [Display(Name = "Giá bán (VND)")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá tiền phải lớn hơn hoặc bằng 0")]
        public double Price { get; set; }

        [Display(Name = "Ảnh đại diện")]
        // File này dùng để hứng dữ liệu upload từ Form
        public HttpPostedFileBase ImageFile { get; set; }

        // String này dùng để hiển thị ảnh cũ (nếu có) hoặc lưu tên ảnh xuống DB
        public string ImageName { get; set; }

        [Display(Name = "Cho phép học thử?")]
        public bool IsTrialAvailable { get; set; }
    }
}