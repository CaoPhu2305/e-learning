using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace e_learning.Models
{
    public class QuestionItem
    {
        public string Content { get; set; }
        public List<AnswerItem> Answers { get; set; }


    }
}