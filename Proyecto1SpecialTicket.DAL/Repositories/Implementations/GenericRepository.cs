using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Proyecto1SpecialTicket.DAL.DataContext;
using Proyecto1SpecialTicket.DAL.Repositories.Interfaces;

namespace Proyecto1SpecialTicket.DAL.Repositories.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly specialticketContext _context;

        /// <summary>
        /// Contructor of GenericRepository
        /// </summary>
        public GenericRepository(specialticketContext context)
        {
            _context = context;
        }

        /// <summary>
        /// This method is to add a record.
        /// </summary>
        /// <param name="entity"></param>
        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            _context.SaveChanges();
            return entity;
        }

        /// <summary>
        /// This method is to add a range of records.
        /// </summary>
        /// <param name="entities"></param>
        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
        }

        /// <summary>
        /// This method is to get the records using an expression.
        /// </summary>02051857
        /// <param name="expression"></param>
        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().Where(expression).ToListAsync();
        }

        /// <summary>
        /// This method is to get the ordered records using an expression.
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="orderByExpression"></param>
        /// <param name="descending"></param>
        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression, Expression<Func<T, object>> orderByExpression, bool descending)
        {
            return descending ? await _context.Set<T>().Where(expression).OrderByDescending(orderByExpression).ToListAsync() 
                              : await _context.Set<T>().Where(expression).OrderBy(orderByExpression).ToListAsync();
        }

        /// <summary>
        /// This method is to get all the records.
        /// </summary>
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        /// <summary>
        /// This method is to get a single record using ID.
        /// </summary>
        /// <param name="id"></param>
        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        /// <summary>
        /// This method is to get a single record using ID.
        /// </summary>
        /// <param name="id"></param>
        public async Task<T> GetByIdAsync(long id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        /// <summary>
        /// This method is to get a single record using ID.
        /// </summary>
        /// <param name="id"></param>
        public async Task<T> GetByIdAsync(sbyte id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        /// <summary>
        /// This method is used to update a record.
        /// </summary>
        /// <param name="entity"></param>
        public async Task<T> UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// This method is used to update a set of records.
        /// </summary>
        /// <param name="entities"></param>
        public async Task UpdateRangeAsync(IEnumerable<T> entities)
        {
            _context.Set<T>().UpdateRange(entities);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// This method will return a single entity object matching the expression.
        /// If the expression matches more than one records, an exception will be generated.
        /// </summary>
        /// <param name="expression"></param>
        public async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().SingleOrDefaultAsync(expression);
        }

        /// <summary>
        /// This method will return a single entity object matching the expression.
        /// </summary>
        /// <param name="expression"></param>
        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression, Expression<Func<T, object>> orderByExpression)
        {
            return await _context.Set<T>().Where(expression).OrderByDescending(orderByExpression).FirstOrDefaultAsync();
        }

        /// <summary>
        /// This method will return single entity with applied order and ascending/descending conditions.
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="orderByExpression"></param>
        /// <param name="isAscending"></param>
        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression, Expression<Func<T, object>> orderByExpression, bool isAscending)
        {
            if (isAscending)
            {
                return await _context.Set<T>().OrderBy(orderByExpression).FirstOrDefaultAsync(expression);
            }
            return await _context.Set<T>().OrderByDescending(orderByExpression).FirstOrDefaultAsync(expression);
        }

	}
}