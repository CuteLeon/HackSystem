using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HackSystem.WebAPI.DataAccess.API.DataServices
{
    public interface IDataServiceBase<TEntity> where TEntity : class
    {
        #region Add

        /// <summary>
        /// Add entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> AddAsync(TEntity entity);

        /// <summary>
        /// Add entities
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task<int> AddRangeAsync(IEnumerable<TEntity> entities);
        #endregion

        #region Update

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> UpdateAsync(TEntity entity);

        /// <summary>
        /// Update entities
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task<int> UpdateRangeAsync(IEnumerable<TEntity> entities);
        #endregion

        #region Delete

        /// <summary>
        /// Remove entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> RemoveAsync(TEntity entity);

        /// <summary>
        /// Remove entities
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task<int> RemoveRangeAsync(IEnumerable<TEntity> entities);
        #endregion

        #region Load

        /// <summary>
        /// Load
        /// </summary>
        void Load();

        /// <summary>
        /// Load
        /// </summary>
        /// <returns></returns>
        Task LoadAsync();
        #endregion

        #region For each

        /// <summary>
        /// All
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<bool> AllAsync(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// Any
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// For each
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        Task ForEachAsync(Action<TEntity> action);
        #endregion

        #region Extremum

        /// <summary>
        /// Max
        /// </summary>
        /// <typeparam name="TResult">Type</typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<TResult> MaxAsync<TResult>(Expression<Func<TEntity, TResult>> expression);

        /// <summary>
        /// Min
        /// </summary>
        /// <typeparam name="TResult">Type</typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<TResult> MinAsync<TResult>(Expression<Func<TEntity, TResult>> expression);
        #endregion

        #region Count

        /// <summary>
        /// Count
        /// </summary>
        /// <returns></returns>
        Task<int> CountAsync();

        /// <summary>
        /// Count
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> expression);
        #endregion

        #region Contain

        /// <summary>
        /// Contain
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> ContainsAsync(TEntity entity);
        #endregion

        #region Find by primary key

        /// <summary>
        /// Find
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        /// <remarks>Find method will try to search object in memory first, and search in database when not exist in memory</remarks>
        TEntity Find(params object[] keys);

        /// <summary>
        /// Find
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        Task<TEntity> FindAsync(params object[] keys);
        #endregion

        #region First

        /// <summary>
        /// First
        /// </summary>
        /// <returns></returns>
        Task<TEntity> FirstAsync();

        /// <summary>
        /// First
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// First or default
        /// </summary>
        /// <returns></returns>
        Task<TEntity> FirstOrDefaultAsync();

        /// <summary>
        /// First or default
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// Single
        /// </summary>
        /// <returns></returns>
        Task<TEntity> SingleAsync();

        /// <summary>
        /// Single
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// Single or default
        /// </summary>
        /// <returns></returns>
        Task<TEntity> SingleOrDefaultAsync();

        /// <summary>
        /// Single or default
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> expression);
        #endregion

        #region SQL

        /// <summary>
        /// Execute SQL command
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        int ExecuteSqlRaw(string sql, params object[] parameters);
        #endregion

        #region Collection cast

        /// <summary>
        /// Return IQueryable
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> AsQueryable();

        /// <summary>
        /// Return IEnumerable
        /// </summary>
        /// <returns></returns>
        IEnumerable<TEntity> AsEnumerable();

        /// <summary>
        /// Return ParallelQuery
        /// </summary>
        /// <returns></returns>
        ParallelQuery<TEntity> AsParallel();

        /// <summary>
        /// Return Array
        /// </summary>
        /// <returns></returns>
        Task<TEntity[]> ToArrayAsync();

        /// <summary>
        /// Return Dictionary
        /// </summary>
        /// <typeparam name="TKey">Type</typeparam>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        Task<Dictionary<TKey, TEntity>> ToDictionaryAsync<TKey>(Func<TEntity, TKey> keySelector);

        /// <summary>
        /// Return List
        /// </summary>
        /// <returns></returns>
        Task<List<TEntity>> ToListAsync();
        #endregion

        #region Skip and take

        /// <summary>
        /// Skip
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        IQueryable<TEntity> Skip(int count);

        /// <summary>
        /// Take
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        IQueryable<TEntity> Take(int count);
        #endregion

        #region Sum

        /// <summary>
        /// Sum
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<decimal> SumAsync(Expression<Func<TEntity, decimal>> expression);

        /// <summary>
        /// Sum
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<decimal?> SumAsync(Expression<Func<TEntity, decimal?>> expression);

        /// <summary>
        /// Sum
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<double> SumAsync(Expression<Func<TEntity, double>> expression);

        /// <summary>
        /// Sum
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<double?> SumAsync(Expression<Func<TEntity, double?>> expression);

        /// <summary>
        /// Sum
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<float> SumAsync(Expression<Func<TEntity, float>> expression);

        /// <summary>
        /// Sum
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<float?> SumAsync(Expression<Func<TEntity, float?>> expression);

        /// <summary>
        /// Sum
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<long> SumAsync(Expression<Func<TEntity, long>> expression);

        /// <summary>
        /// Sum
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<long?> SumAsync(Expression<Func<TEntity, long?>> expression);

        /// <summary>
        /// Sum
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<int> SumAsync(Expression<Func<TEntity, int>> expression);

        /// <summary>
        /// Sum
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<int?> SumAsync(Expression<Func<TEntity, int?>> expression);
        #endregion

        #region Average

        /// <summary>
        /// Average
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<decimal> AverageAsync(Expression<Func<TEntity, decimal>> expression);

        /// <summary>
        /// Average
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<decimal?> AverageAsync(Expression<Func<TEntity, decimal?>> expression);

        /// <summary>
        /// Average
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<double> AverageAsync(Expression<Func<TEntity, double>> expression);

        /// <summary>
        /// Average
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<double?> AverageAsync(Expression<Func<TEntity, double?>> expression);

        /// <summary>
        /// Average
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<float> AverageAsync(Expression<Func<TEntity, float>> expression);

        /// <summary>
        /// Average
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<float?> AverageAsync(Expression<Func<TEntity, float?>> expression);

        /// <summary>
        /// Average
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<double> AverageAsync(Expression<Func<TEntity, long>> expression);

        /// <summary>
        /// Average
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<double?> AverageAsync(Expression<Func<TEntity, long?>> expression);

        /// <summary>
        /// Average
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<double> AverageAsync(Expression<Func<TEntity, int>> expression);

        /// <summary>
        /// Average
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<double?> AverageAsync(Expression<Func<TEntity, int?>> expression);
        #endregion

        #region Save

        /// <summary>
        /// Save changes
        /// </summary>
        /// <returns></returns>
        int SaveChanges();

        /// <summary>
        /// Save changes
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChangesAsync();
        #endregion

        #region Transact

        /// <summary>
        /// Execute transact
        /// </summary>
        /// <typeparam name="TDelegate">Type of delegate</typeparam>
        /// <typeparam name="TResult">Type of result</typeparam>
        /// <param name="delegate"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        TResult Transact<TDelegate, TResult>(TDelegate @delegate, params object[] parameters)
            where TDelegate : Delegate;
        #endregion
    }
}
