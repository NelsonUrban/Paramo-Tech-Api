using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Business.Dtos
{
    public class ResultDto
    {
        public bool IsSuccess { get; set; } = true;
        public string Errors { get; set; }
    }
}
