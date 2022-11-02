using System.Collections.Generic;

namespace Sat.Recruitment.Business.Models
{
    public class ResultModel<T> where T : class
    {
        List<T> Data { get; set; }
        public bool IsSuccess { get; set; }
        public string Errors { get; set; }
    }
}
