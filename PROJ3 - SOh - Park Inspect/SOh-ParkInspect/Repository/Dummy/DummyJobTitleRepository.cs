using System.Collections.Generic;
using Database;
using SOh_ParkInspect.Repository.Interface;

namespace SOh_ParkInspect.Repository.Dummy
{
    public class DummyJobTitleRepository : IJobTitleRepository
    {
        private readonly List<JobTitle> _jobTitles;

        public DummyJobTitleRepository()
        {
            _jobTitles = new List<JobTitle>
            {
                new JobTitle
                {
                    Description = "Inspectuer",
                    Name = "Inspecteur"
                },
                new JobTitle
                {
                    Description = "Management",
                    Name = "Mamagement"
                }
            };
        }

        public List<JobTitle> All()
        {
            return _jobTitles;
        }
    }
}