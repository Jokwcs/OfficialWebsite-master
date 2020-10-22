using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Limit.OfficialSite.EntityFrameworkCore.Extensions
{
    public static class EfCoreExtensions
    {
        /// <summary>
        /// 执行sql返回DataTable 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="sql"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public static DataTable SqlQuery(this ApplicationDbContext dbContext, string sql, params object[] commandParameters)
        {
            var dt = new DataTable();

            using var connection = dbContext.Database.GetDbConnection();

            using var cmd = connection.CreateCommand();

            dbContext.Database.OpenConnection();

            cmd.CommandText = sql;

            if (commandParameters != null && commandParameters.Length > 0)
            {
                cmd.Parameters.AddRange(commandParameters);
            }

            using var reader = cmd.ExecuteReader();

            dt.Load(reader);

            return dt;
        }

        /// <summary>
        /// 执行多条sql
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="sqlList"></param>
        [Obsolete]
        public static int ExecuteListSqlCommand(this ApplicationDbContext dbContext, List<string> sqlList)
        {
            var rerunInt = 0;

            try
            {
                using var trans = dbContext.Database.BeginTransaction();
                sqlList.ForEach(cmd => rerunInt += dbContext.Database.ExecuteSqlCommand(cmd));
                dbContext.Database.CommitTransaction();
            }
            catch (DbException)
            {
                try
                {
                    dbContext.Database.RollbackTransaction();
                }
                catch (DbException)
                {

                }
            }

            return rerunInt;
        }

        /// <summary>
        /// 执行sql返回list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbContext"></param>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static List<T> SqlQuery<T>(this ApplicationDbContext dbContext, string sql, params object[] parameters) where T : class, new()
        {
            var dt = SqlQuery(dbContext, sql, parameters);
            return dt.ToList<T>();
        }

        /// <summary>
        /// DataTable转list 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> ToList<T>(this DataTable dt) where T : class, new()
        {
            var propertyInfos = typeof(T).GetProperties();

            var list = new List<T>();

            foreach (DataRow row in dt.Rows)
            {
                var t = new T();

                foreach (var p in propertyInfos)
                {
                    if (dt.Columns.IndexOf(p.Name) != -1 && row[p.Name] != DBNull.Value)
                    {
                        p.SetValue(t, row[p.Name], null);
                    }
                }

                list.Add(t);
            }

            return list;
        }
    }
}