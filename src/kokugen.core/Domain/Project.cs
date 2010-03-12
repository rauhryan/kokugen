using System;
using System.Collections.Generic;
using Kokugen.Core.Validation;

namespace Kokugen.Core.Domain
{
    public class Project : Entity
    {
        private IList<TimeRecord> _timeRecords = new List<TimeRecord>();
        private IList<BoardColumn> _boardColumns = new List<BoardColumn>();

        [Required]
        public virtual string Name { get; set; }
        public virtual DateTime? StartDate { get; set; }
        public virtual DateTime? EndDate { get; set; }
        public virtual int NumberOfSessions { get; set; }
        public virtual double AverageTimeSpentPerSession { get; set; }
        public virtual double TotalTime { get; set; }

        public virtual string Description { get; set; }

        public virtual Company Company { get; set; }

        public virtual IEnumerable<TimeRecord> GetTimeRecords()
        {
            return _timeRecords;
        }

        public virtual void AddTime(TimeRecord timeRecord)
        {
            if(_timeRecords.Contains(timeRecord)) return;

            //timeRecord.Project = this;
            _timeRecords.Add(timeRecord);
        }

        public virtual void RemoveTime(TimeRecord timeRecord)
        {
            if(!_timeRecords.Contains(timeRecord)) return;

            _timeRecords.Remove(timeRecord);
        }

        public virtual IEnumerable<BoardColumn> GetBoardColumns()
        {
            return _boardColumns;
        }

        public virtual void AddBoardColumn(BoardColumn boardColumn)
        {
            if(_boardColumns.Contains(boardColumn)) return;

            boardColumn.Project = this;
            _boardColumns.Add(boardColumn);
        }

        public virtual void RemoveBoardColumn(BoardColumn boardColumn)
        {
            if(!_boardColumns.Contains(boardColumn)) return;

            _boardColumns.Remove(boardColumn);
        }

        public virtual void ComputeTotalTimeAndSessions()
        {
            int i = 0;
            foreach (var timeRecord in _timeRecords)
            {
                ++i;
                TotalTime += timeRecord.Duration;
            }
            NumberOfSessions = i;
        }

        public virtual void ComputeAverageTimeSpentPerSession()
        {
            ComputeTotalTimeAndSessions();

            AverageTimeSpentPerSession = TotalTime/(Double)NumberOfSessions;
            
        }

    }
}