using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace e_learning.Models
{
    public class CreateChapterViewModel
    {

        public int CourseId { get; set; } // Để biết thuộc khóa nào

        [Required(ErrorMessage = "Nhập tên chương")]
        [Display(Name = "Tên chương")]
        public string ChapterName { get; set; }

        [Required(ErrorMessage = "Nhập mục tiêu chương")]
        [Display(Name = "Học xong chương này sẽ làm được gì?")]
        public string ChapterCompleted { get; set; }

    }
}