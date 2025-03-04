using SGDPEDIDOS.Application.Wrappers;
using SGDPEDIDOS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SGDPEDIDOS.Application.Interfaces.Repositories
{
    public interface IRepositoryAsync<T> where T : class
    {
        IQueryable<T> OdataQuery();
        Task<T> GetByIdAsync(dynamic id);
        Task<T> GetByIdAsyncdual(dynamic idregion, dynamic idprovincia);
        Task<List<T>> ListAllAsync(IEnumerable<Expression<Func<T, object>>> includes = null);
        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entity);
        
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(T entity); 
        Task<List<T>> WhereAllAsync(Expression<Func<T, bool>> where = null, Expression<Func<T, object>> include = null);
        Task<List<T>> GelAllAsync(Expression<Func<T, bool>> where = null, Expression<Func<T, object>> include = null);

        Task<T> WhereAsync(Expression<Func<T, bool>> where = null, IEnumerable<Expression<Func<T, object>>> includes = null);

        Task<bool> Exists(Expression<Func<T, bool>> where = null);
        Task<PagedResponse<IList<T>>> GetPagedList(int pageNumber, int pageSize, List<Expression<Func<T, bool>>> filter = null,
                                              Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                              IEnumerable<Expression<Func<T, object>>> includes = null,
                                              Expression<Func<T, T>> selector = null);
        Task<PagedResponse<IList<T>>> GetPagedListDescending(int pageNumber, int pageSize, List<Expression<Func<T, bool>>> filter = null,
                                                   Func<IQueryable<T>, IOrderedQueryable<T>> OrderByDescending = null,
                                                   IEnumerable<Expression<Func<T, object>>> includes = null,
                                                   Expression<Func<T, T>> selector = null);
        
    }
}
