using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace e_learning.Models
{
    public class CreateLessonViewModel
    {

        public int ChapterId { get; set; } // Để biết thuộc chương nào
        public int CourseId { get; set; }  // Để redirect về đúng trang

        [Required(ErrorMessage = "Nhập tên bài học")]
        [Display(Name = "Tên bài học")]
        public string LessonName { get; set; }

        [Display(Name = "Mô tả bài học")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Nhập mục tiêu bài học")]
        [Display(Name = "Học xong bài này sẽ đạt được gì?")]
        public string LessonCompleted { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn video")]
        [Display(Name = "Video bài giảng (MP4)")]
        public HttpPostedFileBase VideoFile { get; set; }

    }
}