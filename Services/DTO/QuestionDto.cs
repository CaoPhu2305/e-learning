using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public class QuestionDto
    {

        public string Content { get; set; }
        public List<AnswerDto> Answers { get; set; }

    }
}
