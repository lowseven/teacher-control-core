﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TeacherControl.Core.DTOs
{
    public class UserAnswerDTO
    {
        public IEnumerable<QuestionAnswerDTO> Answers { get; set; }
        public IEnumerable<QuestionAnswerMatchDTO> Matches { get; set; }
        public IEnumerable<UserOpenResponseDTO> OpenResponses { get; set; }

    }
}
