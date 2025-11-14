using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace e_learning.Models
{
    public class QuizViewModel
    {

        public int QuizId { get; set; }
        public string QuizTitle { get; set; }
        public int TimeLimitInSeconds { get; set; }

        // Đây là chìa khóa: một danh sách TẤT CẢ các câu hỏi
        public List<QuestionViewModel> Questions { get; set; }


    }
}