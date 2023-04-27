using IBatisNet.DataMapper;
using IBatisNet.DataMapper.Configuration;
using System;
using Glorysoft.BC.Entity;
using System.Collections;
using System.Collections.Generic;
using IBatisNet.DataMapper.MappedStatements;
using IBatisNet.DataMapper.Scope;
using System.Data;
using IBatisNet.Common.Exceptions;
using System.Web.Script.Serialization;

namespace Glorysoft.BC.Db.Contract
{
    public class AbstractDbService
    {
        protected static readonly log4net.ILog log = LogHelper.DBLog;
        protected static volatile ISqlMapper SqlMap;
        protected AbstractDbService()
        {
            if (SqlMap != null) return;
            lock (typeof(SqlMapper))
            {
                if (SqlMap == null) // double-check
                    InitMapper();
            }
        }
        private static void InitMapper()
        {
            try
            {
                var builder = new DomSqlMapBuilder();
                var file = AppDomain.CurrentDomain.BaseDirectory + @"Configuration\SqlMap.config ";
                SqlMap = builder.Configure(file);
            }
            catch(Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
            }
           
        }

        protected IList ExecuteQueryForList(string statementName, object parameterObject)
        {
            try
            {
                return SqlMap.QueryForList(statementName, parameterObject);
            }
            catch (Exception e)
            {              
                log.Debug(e.Message, e);
                return null;
            }
        }

        /// <summary>
        /// Simple convenience method to wrap the SqlMap method of the same name.
        /// Wraps the exception with a IBatisNetException to isolate the SqlMap framework.
        /// </summary>
        /// <param name="statementName"></param>
        /// <param name="parameterObject"></param>
        /// <param name="skipResults"></param>
        /// <param name="maxResults"></param>
        /// <returns></returns>
        protected IList ExecuteQueryForList(string statementName, object parameterObject, int skipResults, int maxResults)
        {
            try
            {
                return SqlMap.QueryForList(statementName, parameterObject, skipResults, maxResults);
            }
            catch (Exception e)
           {             
                log.Debug(e.Message, e);
                return null;
            }
        }


        protected IList<T> ExecuteQueryForList<T>(string statementName, object parameterObject)
        {
            try
            {
                var data = SqlMap.QueryForList<T>(statementName, parameterObject);

                JavaScriptSerializer js = new JavaScriptSerializer();
                var condi = "";
                try
                {
                    condi = js.Serialize(parameterObject);
                }
                catch { }
                log.Info(statementName + " Result:" + data.Count + " [" + condi + "]");

                return data;
            }
            catch (Exception e)
            {
                log.Debug(e.Message, e);
                return null;
            }
        }

        protected IList<T> ExecuteQueryForList<T>(string statementName, object parameterObject, int skipResults, int maxResults)
        {
            try
            {
                return SqlMap.QueryForList<T>(statementName, parameterObject, skipResults,
                                              maxResults);
            }
            catch (Exception e)
            {                
                log.Debug(e.Message, e);
                return null;
            }
        }

        /// <summary>
        /// Simple convenience method to wrap the SqlMap method of the same name.
        /// Wraps the exception with a IBatisNetException to isolate the SqlMap framework.
        /// </summary>
        /// <param name="statementName"></param>
        /// <param name="parameterObject"></param>
        /// <returns></returns>
        protected object ExecuteQueryForObject(string statementName, object parameterObject)
        {
            try
            {
                var data = SqlMap.QueryForObject(statementName, parameterObject);

                JavaScriptSerializer js = new JavaScriptSerializer();
                var condi = "";
                try
                {
                    condi = js.Serialize(parameterObject);
                }
                catch { }
                log.Info(statementName + " Result:" + (data != null ? 1 : 0) + " [" + condi + "]");

                return data;
            }
            catch (Exception e)
            {                
                log.Debug(e.Message, e);
                return null;
            }
        }

        /// <summary>
        /// Executes the query for a generic object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="statementName">Name of the statement.</param>
        /// <param name="parameterObject">The parameter object.</param>
        /// <returns></returns>
        protected T ExecuteQueryForObject<T>(string statementName, object parameterObject)
        {
            try
            {
                var data = SqlMap.QueryForObject<T>(statementName, parameterObject);

                JavaScriptSerializer js = new JavaScriptSerializer();
                var condi = "";
                try
                {
                    condi = js.Serialize(parameterObject);
                }
                catch { }
                log.Info(statementName + " Result:" + (data != null ? 1 : 0) + " [" + condi + "]");

                return data;
            }
            catch (Exception e)
            {
                //log.Error(e.Message, e);
                log.Debug("Error executing query '" + statementName + "' for object<T>.  Cause: " + e.Message, e);
                return default(T);
            }
        }


        /// <summary>
        /// Simple convenience method to wrap the SqlMap method of the same name.
        /// Wraps the exception with a IBatisNetException to isolate the SqlMap framework.
        /// </summary>
        /// <param name="statementName"></param>
        /// <param name="parameterObject"></param>
        /// <returns></returns>
        protected int ExecuteUpdate(string statementName, object parameterObject)
        {
            try
            {
                var data = SqlMap.Update(statementName, parameterObject);

                JavaScriptSerializer js = new JavaScriptSerializer();
                var condi = "";
                try
                {
                    condi = js.Serialize(parameterObject);
                }
                catch { }
                log.Info(statementName + " Result:" + data + " [" + condi + "]");

                return data;
            }
            catch (Exception e)
            {
                log.Debug(e.Message, e);
                return 0;                
            }
        }

        /// <summary>
        /// Simple convenience method to wrap the SqlMap method of the same name.
        /// Wraps the exception with a IBatisNetException to isolate the SqlMap framework.
        /// </summary>
        /// <param name="statementName"></param>
        /// <param name="parameterObject"></param>
        /// <returns></returns>
        protected bool ExecuteInsert(string statementName, object parameterObject)
        {
            try
            {
                var data = SqlMap.Insert(statementName, parameterObject);

                JavaScriptSerializer js = new JavaScriptSerializer();
                var condi = "";
                try
                {
                    condi = js.Serialize(parameterObject);
                }
                catch { }
                log.Info(statementName + " Result:" + js.Serialize(data) + " [" + condi + "]");

                return true;
            }
            catch (Exception e)
            {
                log.Debug(e.Message, e);
                return false;
            }
        }

        /// <summary>
        /// Simple convenience method to wrap the SqlMap method of the same name.
        /// Wraps the exception with a IBatisNetException to isolate the SqlMap framework.
        /// </summary>
        /// <param name="statementName"></param>
        /// <param name="parameterObject"></param>
        /// <returns></returns>
        protected int ExecuteDelete(string statementName, object parameterObject)
        {
            try
            {
                var data = SqlMap.Delete(statementName, parameterObject);

                JavaScriptSerializer js = new JavaScriptSerializer();
                var condi = "";
                try
                {
                    condi = js.Serialize(parameterObject);
                }
                catch { }
                log.Info(statementName + " Result:" + data + " [" + condi + "]");

                return data;
            }
            catch (Exception e)
            {
                //throw new IBatisNetException("Error executing query '" + statementName + "' for Delete.  Cause: " + e.Message, e);
                log.Debug(e.Message, e);
                return 0;
            }
        }


        /// <summary>
        /// 通用得到参数化后的SQL(xml文件中参数要使用$标记的占位参数)
        /// </summary>
        /// <param name="statementName"></param>
        /// <param name="paramObject">语句所需要的参数</param>
        /// <returns>获得的SQL</returns>
        public string GetSql(string statementName, object paramObject)
        {
            try
            {
                IMappedStatement statement = SqlMap.GetMappedStatement(statementName);
                if (!SqlMap.IsSessionStarted)
                {
                    SqlMap.OpenConnection();
                }
                RequestScope scope = statement.Statement.Sql.GetRequestScope(statement,
                                                                             paramObject,
                                                                             SqlMap.
                                                                                 LocalSession);
                return scope.PreparedStatement.PreparedSql;
            }
            catch(Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
                return "";
            }
           
        }

        /// <summary>
        /// 通用的以DataTable的方式得到Select的结果(xml文件中参数要使用$标记的占位参数)
        /// </summary>
        /// <param name="statementName">语句ID</param>
        /// <param name="paramObject">语句所需要的参数</param>
        /// <returns>得到的DataTable</returns>
        public DataTable ExecuteDataTable(string statementName, object paramObject)
        {
            try
            {
                var ds = new DataSet();
                IMappedStatement statement = SqlMap.GetMappedStatement(statementName);
                if (!SqlMap.IsSessionStarted)
                {
                    SqlMap.OpenConnection();
                }
                RequestScope scope = statement.Statement.Sql.GetRequestScope(statement,
                                                                             paramObject,
                                                                             SqlMap.
                                                                                 LocalSession);

                statement.PreparedCommand.Create(scope, SqlMap.LocalSession,
                                                 statement.Statement, paramObject);

                IDbCommand dc =
                    SqlMap.LocalSession.CreateCommand(scope.IDbCommand.CommandType);

                dc.CommandText = scope.IDbCommand.CommandText;

                if (scope.IDbCommand.Parameters != null)
                {
                    foreach (IDbDataParameter para in scope.IDbCommand.Parameters)
                    {
                        IDbDataParameter param = SqlMap.LocalSession.CreateDataParameter();
                        param.ParameterName = para.ParameterName;
                        param.Value = para.Value;
                        dc.Parameters.Add(param);
                    }
                }
                IDbDataAdapter dda = SqlMap.LocalSession.CreateDataAdapter(dc);
                dda.Fill(ds);

                return ds.Tables[0];
            }
            catch (Exception e)
            {
                throw new IBatisNetException(
                    "Error executing query '" + statementName + "' for list.  Cause: " +
                    e.Message, e);
            }
        }
    }
}
