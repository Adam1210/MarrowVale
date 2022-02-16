using MarrowVale.Business.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MarrowVale.Data.Contracts
{
    public interface IBaseRepository<T>
    {
        /// <summary>
        /// Gets By Id
        /// </summary>
        /// <typeparam name="Id">The id of the requested entity.</typeparam>
        /// <returns></returns>
        /// 
        Task<T> GetById(string id);
        /// <summary>
        /// Gets by Name
        /// </summary>
        /// <typeparam name="Name">The name of the requested entity.</typeparam>
        /// <returns></returns>
        /// 
        Task<T> GetByName(string name);
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns></returns>
        Task<IEnumerable<T>> All();

        /// <summary>
        /// Finds a collection of entities with the specified query.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        Task<IEnumerable<T>> Where(Expression<Func<T, bool>> query);
        /// <summary>
        /// Gets the by.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        Task<T> Single(Expression<Func<T, bool>> query);
        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="item">The item.</param>
        Task Add(T item);
        /// <summary>
        /// Updates the entities with specified query.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="item">The item.</param>
        Task Update(Expression<Func<T, bool>> query, T item);
        /// <summary>
        /// Updates the entities based on item id
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="item">The item.</param>
        Task Update(T item);
        /// <summary>
        /// Deletes the entities with the specified query.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="query">The query.</param>
        Task Delete(Expression<Func<T, bool>> query);
    }
}
