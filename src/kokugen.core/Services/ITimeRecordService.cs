using System;
using System.Collections.Generic;
using System.Linq;
using Kokugen.Core.Domain;
using Kokugen.Core.Persistence.Repositories.Kokugen.Core.Persistence.Repositories;

namespace Kokugen.Core.Services
{
    public interface ITimeRecordService
    {
        IEnumerable<TimeRecord> GetAllTimeRecords();

        void DeleteTimeRecord(Guid id);
        TimeRecord GetTimeRecord(Guid id);
        void Save(TimeRecord timeRecord);
        IEnumerable<TimeRecord> FindAll(User user, int limit);
    }

    public class TimeRecordService : ITimeRecordService
    {
        private readonly ITimeRecordRepository _timeRecordRepository;

        public TimeRecordService(ITimeRecordRepository timeRecordRepository)
        {
            _timeRecordRepository = timeRecordRepository;
        }


        public IEnumerable<TimeRecord> GetAllTimeRecords()
        {
            return _timeRecordRepository.Query();
        }

        public void DeleteTimeRecord(Guid id)
        {
            var timeRecord = _timeRecordRepository.Get(id);
            _timeRecordRepository.Delete(timeRecord);
        }

        public TimeRecord GetTimeRecord(Guid id)
        {
            return _timeRecordRepository.Get(id);
        }

        public void Save(TimeRecord timeRecord)
        {
            _timeRecordRepository.Save(timeRecord);
        }

        public IEnumerable<TimeRecord> FindAll(User user, int limit)
        {
            return _timeRecordRepository.Query()
                .Where(x => x.User == user)
                .OrderByDescending(x => x.StartTime)
                .Take(limit)
                .ToList();
        }
    }
}