using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using HackSystem.WebAPI.Services.API.DataServices;
using HackSystem.WebAPI.DataAccess;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace HackSystem.WebAPI.Services.DataServices
{
    public class DataServiceBase<TEntity> : IDataServiceBase<TEntity>
        where TEntity : class
    {
        protected readonly ILogger<DataServiceBase<TEntity>> logger;
        protected readonly HackSystemDBContext hackSystemDBContext;

        public DataServiceBase(
            ILogger<DataServiceBase<TEntity>> logger,
            HackSystemDBContext hackSystemDBContext)
        {
            this.logger = logger;
            this.hackSystemDBContext = hackSystemDBContext;
            this.logger.LogDebug($"构造数据持久化服务：{this.GetType().FullName} ({this.GetHashCode():X})");
        }

        #region 增加

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            var result = await this.hackSystemDBContext.Set<TEntity>().AddAsync(entity);
            this.hackSystemDBContext.SaveChanges();
            return result.Entity;
        }

        /// <summary>
        /// 添加实体集合
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual async Task<int> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await this.hackSystemDBContext.Set<TEntity>().AddRangeAsync(entities);
            var result = await this.hackSystemDBContext.SaveChangesAsync();
            return result;
        }
        #endregion

        #region 更新

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual TEntity Update(TEntity entity)
        {
            var result = this.hackSystemDBContext.Set<TEntity>().Update(entity);
            this.hackSystemDBContext.SaveChanges();
            return result.Entity;
        }

        /// <summary>
        /// 更新实体集合
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual int UpdateRange(IEnumerable<TEntity> entities)
        {
            this.hackSystemDBContext.Set<TEntity>().UpdateRange(entities);
            var results = this.hackSystemDBContext.SaveChanges();
            return results;
        }
        #endregion

        #region 删除

        /// <summary>
        /// 移除实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual TEntity Remove(TEntity entity)
        {
            var result = this.hackSystemDBContext.Set<TEntity>().Remove(entity);
            this.hackSystemDBContext.SaveChanges();
            return result.Entity;
        }

        /// <summary>
        /// 移除实体集合
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual int RemoveRange(IEnumerable<TEntity> entities)
        {
            this.hackSystemDBContext.Set<TEntity>().RemoveRange(entities);
            var results = this.hackSystemDBContext.SaveChanges();
            return results;
        }
        #endregion

        #region 加载

        /// <summary>
        /// 加载
        /// </summary>
        public virtual void Load()
            => this.hackSystemDBContext.Set<TEntity>().Load();

        /// <summary>
        /// 加载
        /// </summary>
        /// <returns></returns>
        public virtual Task LoadAsync()
            => this.hackSystemDBContext.Set<TEntity>().LoadAsync();
        #endregion

        #region 遍历

        /// <summary>
        /// 全部
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual Task<bool> AllAsync(Expression<Func<TEntity, bool>> expression)
            => this.hackSystemDBContext.Set<TEntity>().AllAsync(expression);

        /// <summary>
        /// 任一
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression)
            => this.hackSystemDBContext.Set<TEntity>().AnyAsync(expression);

        /// <summary>
        /// 遍历
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public virtual Task ForEachAsync(Action<TEntity> action)
            => this.hackSystemDBContext.Set<TEntity>().ForEachAsync(action);
        #endregion

        #region 极值

        /// <summary>
        /// 最大值
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual Task<TResult> MaxAsync<TResult>(Expression<Func<TEntity, TResult>> expression)
            => this.hackSystemDBContext.Set<TEntity>().MaxAsync(expression);

        /// <summary>
        /// 最小值
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual Task<TResult> MinAsync<TResult>(Expression<Func<TEntity, TResult>> expression)
            => this.hackSystemDBContext.Set<TEntity>().MinAsync(expression);
        #endregion

        #region 计数

        /// <summary>
        /// 计算数量
        /// </summary>
        /// <returns></returns>
        public virtual Task<int> CountAsync()
            => this.hackSystemDBContext.Set<TEntity>().CountAsync();

        /// <summary>
        /// 计算数量
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> expression)
            => this.hackSystemDBContext.Set<TEntity>().CountAsync(expression);
        #endregion

        #region 包含

        /// <summary>
        /// 包含
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual Task<bool> ContainsAsync(TEntity entity)
            => this.hackSystemDBContext.Set<TEntity>().ContainsAsync(entity);
        #endregion

        #region 主键查询

        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        /// <remarks>Find 方法会优先在内存中搜索是否存在已经缓存的实体，内存中不存在时才会查询数据库</remarks>
        public virtual TEntity Find(params object[] keys)
            => this.hackSystemDBContext.Set<TEntity>().Find(keys);

        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public virtual Task<TEntity> FindAsync(params object[] keys)
            => this.hackSystemDBContext.Set<TEntity>().FindAsync(keys).AsTask();
        #endregion

        #region 单值查找

        /// <summary>
        /// 第一个元素
        /// </summary>
        /// <returns></returns>
        public virtual Task<TEntity> FirstAsync()
            => this.hackSystemDBContext.Set<TEntity>().FirstAsync();

        /// <summary>
        /// 第一个元素
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> expression)
            => this.hackSystemDBContext.Set<TEntity>().FirstAsync(expression);

        /// <summary>
        /// 第一个或默认元素
        /// </summary>
        /// <returns></returns>
        public virtual Task<TEntity> FirstOrDefaultAsync()
            => this.hackSystemDBContext.Set<TEntity>().FirstOrDefaultAsync();

        /// <summary>
        /// 第一个或默认元素
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression)
            => this.hackSystemDBContext.Set<TEntity>().FirstOrDefaultAsync(expression);

        /// <summary>
        /// 唯一的元素
        /// </summary>
        /// <returns></returns>
        public virtual Task<TEntity> SingleAsync()
            => this.hackSystemDBContext.Set<TEntity>().SingleAsync();

        /// <summary>
        /// 唯一的元素
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> expression)
            => this.hackSystemDBContext.Set<TEntity>().SingleAsync(expression);

        /// <summary>
        /// 唯一的或默认元素
        /// </summary>
        /// <returns></returns>
        public virtual Task<TEntity> SingleOrDefaultAsync()
            => this.hackSystemDBContext.Set<TEntity>().SingleOrDefaultAsync();

        /// <summary>
        /// 唯一的或默认元素
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> expression)
            => this.hackSystemDBContext.Set<TEntity>().FirstOrDefaultAsync(expression);
        #endregion

        #region SQL

        /// <summary>
        /// 执行SQL命令
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public virtual int ExecuteSqlRaw(string sql, params object[] parameters)
            => this.hackSystemDBContext.Database.ExecuteSqlRaw(sql, parameters);
        #endregion

        #region 集合转换

        /// <summary>
        /// 返回 IQueryable
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<TEntity> AsQueryable()
            => this.hackSystemDBContext.Set<TEntity>().AsQueryable();

        /// <summary>
        /// 返回 IEnumerable
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> AsEnumerable()
            => this.hackSystemDBContext.Set<TEntity>().AsEnumerable();

        /// <summary>
        /// 返回 ParallelQuery
        /// </summary>
        /// <returns></returns>
        public virtual ParallelQuery<TEntity> AsParallel()
            => this.hackSystemDBContext.Set<TEntity>().AsParallel();

        /// <summary>
        /// 返回数组
        /// </summary>
        /// <returns></returns>
        public virtual Task<TEntity[]> ToArrayAsync()
            => this.hackSystemDBContext.Set<TEntity>().ToArrayAsync();

        /// <summary>
        /// 返回字典
        /// </summary>
        /// <typeparam name="TKey">字典键类型</typeparam>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public virtual Task<Dictionary<TKey, TEntity>> ToDictionaryAsync<TKey>(Func<TEntity, TKey> keySelector)
            => this.hackSystemDBContext.Set<TEntity>().ToDictionaryAsync(keySelector);

        /// <summary>
        /// 返回列表
        /// </summary>
        /// <returns></returns>
        public virtual Task<List<TEntity>> ToListAsync()
            => this.hackSystemDBContext.Set<TEntity>().ToListAsync();
        #endregion

        #region 跳跃取值

        /// <summary>
        /// 跳过
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public virtual IQueryable<TEntity> Skip(int count)
            => this.hackSystemDBContext.Set<TEntity>().Skip(count);

        /// <summary>
        /// 取值
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public virtual IQueryable<TEntity> Take(int count)
            => this.hackSystemDBContext.Set<TEntity>().Take(count);
        #endregion

        #region 累加

        /// <summary>
        /// 累加
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual Task<decimal> SumAsync(Expression<Func<TEntity, decimal>> expression)
            => this.hackSystemDBContext.Set<TEntity>().SumAsync(expression);

        /// <summary>
        /// 累加
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual Task<decimal?> SumAsync(Expression<Func<TEntity, decimal?>> expression)
            => this.hackSystemDBContext.Set<TEntity>().SumAsync(expression);

        /// <summary>
        /// 累加
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual Task<double> SumAsync(Expression<Func<TEntity, double>> expression)
            => this.hackSystemDBContext.Set<TEntity>().SumAsync(expression);

        /// <summary>
        /// 累加
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual Task<double?> SumAsync(Expression<Func<TEntity, double?>> expression)
            => this.hackSystemDBContext.Set<TEntity>().SumAsync(expression);

        /// <summary>
        /// 累加
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual Task<float> SumAsync(Expression<Func<TEntity, float>> expression)
            => this.hackSystemDBContext.Set<TEntity>().SumAsync(expression);

        /// <summary>
        /// 累加
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual Task<float?> SumAsync(Expression<Func<TEntity, float?>> expression)
            => this.hackSystemDBContext.Set<TEntity>().SumAsync(expression);

        /// <summary>
        /// 累加
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual Task<long> SumAsync(Expression<Func<TEntity, long>> expression)
            => this.hackSystemDBContext.Set<TEntity>().SumAsync(expression);

        /// <summary>
        /// 累加
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual Task<long?> SumAsync(Expression<Func<TEntity, long?>> expression)
            => this.hackSystemDBContext.Set<TEntity>().SumAsync(expression);

        /// <summary>
        /// 累加
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual Task<int> SumAsync(Expression<Func<TEntity, int>> expression)
            => this.hackSystemDBContext.Set<TEntity>().SumAsync(expression);

        /// <summary>
        /// 累加
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual Task<int?> SumAsync(Expression<Func<TEntity, int?>> expression)
            => this.hackSystemDBContext.Set<TEntity>().SumAsync(expression);
        #endregion

        #region 平均值

        /// <summary>
        /// 平均值
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual Task<decimal> AverageAsync(Expression<Func<TEntity, decimal>> expression)
            => this.hackSystemDBContext.Set<TEntity>().AverageAsync(expression);

        /// <summary>
        /// 平均值
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual Task<decimal?> AverageAsync(Expression<Func<TEntity, decimal?>> expression)
            => this.hackSystemDBContext.Set<TEntity>().AverageAsync(expression);

        /// <summary>
        /// 平均值
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual Task<double> AverageAsync(Expression<Func<TEntity, double>> expression)
            => this.hackSystemDBContext.Set<TEntity>().AverageAsync(expression);

        /// <summary>
        /// 平均值
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual Task<double?> AverageAsync(Expression<Func<TEntity, double?>> expression)
            => this.hackSystemDBContext.Set<TEntity>().AverageAsync(expression);

        /// <summary>
        /// 平均值
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual Task<float> AverageAsync(Expression<Func<TEntity, float>> expression)
            => this.hackSystemDBContext.Set<TEntity>().AverageAsync(expression);

        /// <summary>
        /// 平均值
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual Task<float?> AverageAsync(Expression<Func<TEntity, float?>> expression)
            => this.hackSystemDBContext.Set<TEntity>().AverageAsync(expression);

        /// <summary>
        /// 平均值
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual Task<double> AverageAsync(Expression<Func<TEntity, long>> expression)
            => this.hackSystemDBContext.Set<TEntity>().AverageAsync(expression);

        /// <summary>
        /// 平均值
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual Task<double?> AverageAsync(Expression<Func<TEntity, long?>> expression)
            => this.hackSystemDBContext.Set<TEntity>().AverageAsync(expression);

        /// <summary>
        /// 平均值
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual Task<double> AverageAsync(Expression<Func<TEntity, int>> expression)
            => this.hackSystemDBContext.Set<TEntity>().AverageAsync(expression);

        /// <summary>
        /// 平均值
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual Task<double?> AverageAsync(Expression<Func<TEntity, int?>> expression)
            => this.hackSystemDBContext.Set<TEntity>().AverageAsync(expression);
        #endregion

        #region 保存

        /// <summary>
        /// 保存变化
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()
            => this.hackSystemDBContext.SaveChanges();

        /// <summary>
        /// 保存变化
        /// </summary>
        /// <returns></returns>
        public Task<int> SaveChangesAsync()
            => this.hackSystemDBContext.SaveChangesAsync();
        #endregion

        #region 事务

        /// <summary>
        /// 执行事务
        /// </summary>
        /// <typeparam name="TDelegate">委托类型</typeparam>
        /// <typeparam name="TResult">返回值类型</typeparam>
        /// <param name="delegate"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public virtual TResult Transact<TDelegate, TResult>(TDelegate @delegate, params object[] parameters)
            where TDelegate : Delegate
        {
            using var transaction = this.hackSystemDBContext.Database.BeginTransaction();
            try
            {
                var result = (TResult)@delegate.DynamicInvoke(parameters);

                this.hackSystemDBContext.SaveChanges();
                transaction.Commit();

                return result;
            }
            catch
            {
                transaction.Rollback();

                throw;
            }
        }
        #endregion
    }
}
