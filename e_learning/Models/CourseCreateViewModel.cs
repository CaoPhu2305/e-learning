using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace e_learning.Models
{
    public class CourseCreateViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập tên khóa học")]
        [Display(Name = "Tên khóa học")]
        public string CourseName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập giá bán")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá bán không hợp lệ")]
        [Display(Name = "Giá bán (VND)")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mô tả")]
        [Display(Name = "Mô tả khóa học")]
        public string Description { get; set; }

        [Display(Name = "Ảnh bìa khóa học")]
        public HttpPostedFileBase ImageFile { get; set; }

        [Display(Name = "Cho phép học thử?")]
        public bool IsTrialAvailable { get; set; }

        // Các thông tin mặc định cho bảng COURSE_VIDEO (ẩn đi, tự sinh)
        public string VideoLevel { get; set; } = "Basic";
        public string VideoDuration { get; set; } = "0h 0m";

    }
}