using System;

namespace CareerCase.Domain.Entities
{
    public class Job : EntityBase<int>
    {
        public int CompanyId { get; set; }
        public string Position { get; set; }
        public string Description { get; set; }
        public DateTime EndDate { get; set; }
        public int Quality { get; set; }
        public string Benefits { get; set; }
        public string WorkType { get; set; }
        public int Pay { get; set; }
    }
}