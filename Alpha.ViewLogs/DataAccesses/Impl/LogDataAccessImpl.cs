namespace Alpha.ViewLogs.DataAccesses.Impl
{
    using System.Collections.Generic;
    using Fanex.Data.Repository;
    using Models;

    public class LogDataAccessImpl : LogDataAccess
    {
        private readonly IDynamicRepository dynamicRepository;

        public LogDataAccessImpl(IDynamicRepository dynamicRepository)
        {
            this.dynamicRepository = dynamicRepository;
        }

        public int GetTotalRecordsOfLogs(ICriteria criteria)
        {
            return dynamicRepository.Count(criteria);
        }

        public virtual IEnumerable<Log> GetLogs(ICriteria criteria)
        {
            return dynamicRepository.Fetch<Log>(criteria);
        }
    }
}
