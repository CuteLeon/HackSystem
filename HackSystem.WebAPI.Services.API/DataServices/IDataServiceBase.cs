using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HackSystem.WebAPI.Services.API.DataServices
{
    public interface IDataServiceBase<TEntity> where TEntity : class
    {
        #region 增加

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> AddAsync(TEntity entity);

        /// <summary>
        /// 添加实体集合
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task<int> AddRangeAsync(IEnumerable<TEntity> entities);
        #endregion

        #region 更新

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        TEntity Update(TEntity entity);

        /// <summary>
        /// 更新实体集合
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        int UpdateRange(IEnumerable<TEntity> entities);
        #endregion

        #region 删除

        /// <summary>
        /// 移除实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        TEntity Remove(TEntity entity);

        /// <summary>
        /// 移除实体集合
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        int RemoveRange(IEnumerable<TEntity> entities);
        #endregion

        #region 加载

        /// <summary>
        /// 加载
        /// </summary>
        void Load();

        /// <summary>
        /// 加载
        /// </summary>
        /// <returns></returns>
        Task LoadAsync();
        #endregion

        #region 遍历

        /// <summary>
        /// 全部
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<bool> AllAsync(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// 任一
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// 遍历
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        Task ForEachAsync(Action<TEntity> action);
        #endregion

        #region 极值

        /// <summary>
        /// 最大值
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<TResult> MaxAsync<TResult>(Expression<Func<TEntity, TResult>> expression);

        /// <summary>
        /// 最小值
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<TResult> MinAsync<TResult>(Expression<Func<TEntity, TResult>> expression);
        #endregion

        #region 计数

        /// <summary>
        /// 计算数量
        /// </summary>
        /// <returns></returns>
        Task<int> CountAsync();

        /// <summary>
        /// 计算数量
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> expression);
        #endregion

        #region 包含

        /// <summary>
        /// 包含
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> ContainsAsync(TEntity entity);
        #endregion

        #region 主键查询

        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        /// <remarks>Find 方法会优先在内存中搜索是否存在已经缓存的实体，内存中不存在时才会查询数据库</remarks>
        TEntity Find(params object[] keys);

        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        Task<TEntity> FindAsync(params object[] keys);
        #endregion

        #region 单值查找

        /// <summary>
        /// 第一个元素
        /// </summary>
        /// <returns></returns>
        Task<TEntity> FirstAsync();

        /// <summary>
        /// 第一个元素
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// 第一个或默认元素
        /// </summary>
        /// <returns></returns>
        Task<TEntity> FirstOrDefaultAsync();

        /// <summary>
        /// 第一个或默认元素
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// 唯一的元素
        /// </summary>
        /// <returns></returns>
        Task<TEntity> SingleAsync();

        /// <summary>
        /// 唯一的元素
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// 唯一的或默认元素
        /// </summary>
        /// <returns></returns>
        Task<TEntity> SingleOrDefaultAsync();

        /// <summary>
        /// 唯一的或默认元素
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> expression);
        #endregion

        #region SQL

        /// <summary>
        /// 执行SQL命令
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        int ExecuteSqlRaw(string sql, params object[] parameters);
        #endregion

        #region 集合转换

        /// <summary>
        /// 返回 IQueryable
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> AsQueryable();

        /// <summary>
        /// 返回 IEnumerable
        /// </summary>
        /// <returns></returns>
        IEnumerable<TEntity> AsEnumerable();

        /// <summary>
        /// 返回 ParallelQuery
        /// </summary>
        /// <returns></returns>
        ParallelQuery<TEntity> AsParallel();

        /// <summary>
        /// 返回数组
        /// </summary>
        /// <returns></returns>
        Task<TEntity[]> ToArrayAsync();

        /// <summary>
        /// 返回字典
        /// </summary>
        /// <typeparam name="TKey">字典键类型</typeparam>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        Task<Dictionary<TKey, TEntity>> ToDictionaryAsync<TKey>(Func<TEntity, TKey> keySelector);

        /// <summary>
        /// 返回列表
        /// </summary>
        /// <returns></returns>
        Task<List<TEntity>> ToListAsync();
        #endregion

        #region 跳跃取值

        /// <summary>
        /// 跳过
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        IQueryable<TEntity> Skip(int count);

        /// <summary>
        /// 取值
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        IQueryable<TEntity> Take(int count);
        #endregion

        #region 累加

        /// <summary>
        /// 累加
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<decimal> SumAsync(Expression<Func<TEntity, decimal>> expression);

        /// <summary>
        /// 累加
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<decimal?> SumAsync(Expression<Func<TEntity, decimal?>> expression);

        /// <summary>
        /// 累加
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<double> SumAsync(Expression<Func<TEntity, double>> expression);

        /// <summary>
        /// 累加
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<double?> SumAsync(Expression<Func<TEntity, double?>> expression);

        /// <summary>
        /// 累加
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<float> SumAsync(Expression<Func<TEntity, float>> expression);

        /// <summary>
        /// 累加
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<float?> SumAsync(Expression<Func<TEntity, float?>> expression);

        /// <summary>
        /// 累加
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<long> SumAsync(Expression<Func<TEntity, long>> expression);

        /// <summary>
        /// 累加
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<long?> SumAsync(Expression<Func<TEntity, long?>> expression);

        /// <summary>
        /// 累加
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<int> SumAsync(Expression<Func<TEntity, int>> expression);

        /// <summary>
        /// 累加
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<int?> SumAsync(Expression<Func<TEntity, int?>> expression);
        #endregion

        #region 平均值

        /// <summary>
        /// 平均值
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<decimal> AverageAsync(Expression<Func<TEntity, decimal>> expression);

        /// <summary>
        /// 平均值
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<decimal?> AverageAsync(Expression<Func<TEntity, decimal?>> expression);

        /// <summary>
        /// 平均值
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<double> AverageAsync(Expression<Func<TEntity, double>> expression);

        /// <summary>
        /// 平均值
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<double?> AverageAsync(Expression<Func<TEntity, double?>> expression);

        /// <summary>
        /// 平均值
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<float> AverageAsync(Expression<Func<TEntity, float>> expression);

        /// <summary>
        /// 平均值
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<float?> AverageAsync(Expression<Func<TEntity, float?>> expression);

        /// <summary>
        /// 平均值
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<double> AverageAsync(Expression<Func<TEntity, long>> expression);

        /// <summary>
        /// 平均值
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<double?> AverageAsync(Expression<Func<TEntity, long?>> expression);

        /// <summary>
        /// 平均值
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<double> AverageAsync(Expression<Func<TEntity, int>> expression);

        /// <summary>
        /// 平均值
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<double?> AverageAsync(Expression<Func<TEntity, int?>> expression);
        #endregion

        #region 保存

        /// <summary>
        /// 保存变化
        /// </summary>
        /// <returns></returns>
        int SaveChanges();

        /// <summary>
        /// 保存变化
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChangesAsync();
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
        TResult Transact<TDelegate, TResult>(TDelegate @delegate, params object[] parameters)
            where TDelegate : Delegate;
        #endregion
    }
}
