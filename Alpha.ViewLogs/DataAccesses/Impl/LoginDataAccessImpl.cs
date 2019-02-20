namespace Alpha.ViewLogs.DataAccesses.Impl
{
    using System.Collections.Generic;
    using Fanex.Common;
    using Fanex.Data;
    using Fanex.Data.Repository;
    using Models;

    public class LoginDataAccessImpl : LogDataAccessImpl, LogDataAccess
    {
        public LoginDataAccessImpl(IDynamicRepository dynamicRepository) : base(dynamicRepository)
        {
        }

        /// <summary>
        /// Get Log in Logs
        /// Must manual mapping because when there is no data, sp doesn't return data table
        /// and make auto-mapping function error
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public override IEnumerable<Log> GetLogs(ICriteria criteria)
        {
            var logs = new List<Log>();
            if (criteria == null
                || !criteria.IsValid())
            {
                return logs;
            }

            using (var objectDb = ObjectDbFactory.CreateInstance(criteria.GetSettingKey()))
            {
                using (var reader = objectDb.ExecuteReader(criteria))
                {
                    while (reader.Read())
                    {
                        logs.Add(new LoginLog
                        {
                            LogId = Converter.To<long>(reader["LogID"]),
                            UserId = Converter.ToInt(reader["UserID"]),
                            Username = Converter.ToString2(reader["Username"]),
                            Details = Converter.ToString2(reader["Doing"]),
                            LogDate = Converter.ToDateTime(reader["LogDate"]),
                            ActionId = Converter.ToInt(reader["ActionID"]),
                            IsInternal = (byte)Converter.ToInt(reader["IsInternal"]),
                            ActionName = Converter.ToString2(reader["ActionName"]),
                            StoreProcedureName = Converter.ToString2(reader["SPName"]),
                            IP = Converter.ToString2(reader["IP"]),
                            Browser = Converter.ToString2(reader["Browser"]),
                            Domain = Converter.ToString2(reader["Domain"]),
                            Country = Converter.ToString2(reader["CountryCode"])
                        });
                    }
                }

                return logs;
            }
        }
    }
}