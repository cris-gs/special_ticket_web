using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1SpecialTicket.DAL.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T:class
    {
        /// <summary>
        /// This method is to get a single record using ID.
        /// </summary>
        /// <param name="id"></param>
        Task<T> GetByIdAsync(int id);


        /// <summary>
        /// This method is to get a single record using ID.
        /// </summary>
        /// <param name="id"></param>
        Task<T> GetByIdAsync(long id);


        /// <summary>
        /// This method is to get a single record using ID.
        /// </summary>
        /// <param name="id"></param>
        Task<T> GetByIdAsync(sbyte id);

        /// <summary>
        /// This method is to get all the records.
        /// </summary>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// This method is to get the records using an expression.
        /// </summary>
        /// <param name="expression"></param>
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression);

        /// <summary>
        /// This method is to add a record.
        /// </summary>
        /// <param name="entity"></param>
        Task<T> AddAsync(T entity);

        /// <summary>
        /// This method is to add a range of records.
        /// </summary>
        /// <param name="entities"></param>
        Task AddRangeAsync(IEnumerable<T> entities);

        /// <summary>
        /// This method is used to update a record.
        /// </summary>
        /// <param name="entity"></param>
        Task<T> UpdateAsync(T entity);

        /// <summary>
        /// This method is used to update a set of records.
        /// </summary>
        /// <param name="entities"></param 
        Task UpdateRangeAsync(IEnumerable<T> entities);

        /// <summary>
        /// This method will return a single entity object matching the expression.
        /// If the expression matches more than one records, an exception will be generated.
        /// </summary>
        /// <param name="expression"></param>
        Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> expression);

        /// <summary>
        /// This method will return a single entity object matching the expression.
        /// </summary>
        /// <param name="expression"></param>
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression, Expression<Func<T, object>> orderByExpression);

        /// <summary>
        /// This method will return single entity with applied order and ascending/descending conditions.
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="orderByExpression"></param>
        /// <param name="isAscending"></param>
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression, Expression<Func<T, object>> orderByExpression, bool isAscending);
    }
}
