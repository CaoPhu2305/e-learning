using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace e_learning.Models
{
    public class QuestionViewModel
    {

        public int QuestionId { get; set; }
        public int QuestionNumber { get; set; } // Số thứ tự (1, 2, 3...)
        public string PassageText { get; set; } // Đoạn văn (nếu có)
        public string QuestionText { get; set; } // Nội dung câu hỏi

        // Mỗi câu hỏi có 1 danh sách các lựa chọn trả lời
        public List<AnswerViewModel> Options { get; set; }

        // Thuộc tính này để lưu lại lựa chọn của người dùng
        public int UserSelectedAnswerId { get; set; }

    }
}