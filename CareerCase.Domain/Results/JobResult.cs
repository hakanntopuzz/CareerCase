using System;

namespace CareerCase.Domain.Results
{
    public class JobResult
    {
        public string Position { get; set; }
        public string Description { get; set; }
        public int Quality { get; set; }
        public string Benefits { get; set; }
        public string WorkType { get; set; }
        public int Pay { get; set; }
        public DateTime EndDate { get; set; }
    }
}