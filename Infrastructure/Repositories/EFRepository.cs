using Microsoft.EntityFrameworkCore;
using SGDPEDIDOS.Application.Interfaces.Repositories;
using SGDPEDIDOS.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SGDPEDIDOS.Infrastructure.Repositories
{
    //public class EFRepository<T> : IRepositoryAsync<T> where T : class
    //{
    //    protected readonly DbContext _dbContext;

    //    public EFRepository(DbContext dbContext)
    //    {
    //        _dbContext = dbContext;
    //    }

    //    public virtual IQueryable<T> OdataQuery()
    //    {
    //        return _dbContext.Set<T>().AsQueryable();
    //    }

    //    public virtual T GetById(dynamic id)
    //    {

    //        return _dbContext.Set<T>().Find(id);
    //    }

    //    public virtual async Task<T> GetByIdAsync(dynamic id)
    //    {
    //        return await _dbContext.Set<T>().FindAsync(id);
    //    }
    //    public virtual async Task<T> GetByIdAsyncdual(dynamic idregion, dynamic idprovincia)
    //    {
    //        return await _dbContext.Set<T>().FindAsync(idregion, idprovincia);
    //    }
    //    public IEnumerable<T> ListAll()
    //    {
    //        return _dbContext.Set<T>().AsEnumerable();
    //    }

    //    public async Task<List<T>> ListAllAsync(IEnumerable<Expression<Func<T, object>>> includes = null)
    //    {
    //        var query = _dbContext.Set<T>().AsQueryable();
    //        if (includes != null)
    //        {
    //            query = includes.Aggregate(query, (current, include) => current.Include(include));
    //        }

    //        return await query.ToListAsync();
    //    }


    //    public async Task<List<T>> WhereAllAsync(Expression<Func<T, bool>> where = null, Expression<Func<T, object>> include = null)
    //    {
    //        var query = _dbContext.Set<T>().AsQueryable();
    //        if (include != null)
    //        {

    //            return await query.Where(where).Include(include).AsNoTracking().ToListAsync();
    //        }
    //        else
    //        {

    //            return await query.Where(where).AsNoTracking().ToListAsync();
    //        }
    //    }
    //    public async Task<List<T>> GelAllAsync(Expression<Func<T, bool>> where = null, Expression<Func<T, object>> include = null)
    //    {
    //        var query = _dbContext.Set<T>().AsQueryable();
    //        if (include != null)
    //        {

    //            return await query.Where(where).Include(include).AsNoTracking().ToListAsync();
    //        }
    //        else
    //        {

    //            return await query.AsNoTracking().ToListAsync();
    //        }
    //    }
    //    public async Task<T> WhereAsync(Expression<Func<T, bool>> where = null, IEnumerable<Expression<Func<T, object>>> includes = null)
    //    {
    //        var query = _dbContext.Set<T>().AsQueryable();
    //        if (includes != null)
    //        {
    //            query = includes.Aggregate(query, (current, include) => current.Include(include));
    //        }
    //        return await query.Where(where).AsNoTracking().FirstOrDefaultAsync();
    //    }

    //    public async Task<bool> Exists(Expression<Func<T, bool>> where = null)
    //    {
    //        return await _dbContext.Set<T>().AnyAsync(where);
    //    }

    //    public T Add(T entity)
    //    {
    //        _dbContext.Set<T>().Add(entity);
    //        _dbContext.SaveChanges();

    //        return entity;
    //    }
    //    public async Task<T> AddAsync(T entity)
    //    {
    //        try
    //        {
    //            await _dbContext.Set<T>().AddAsync(entity);
    //            await _dbContext.SaveChangesAsync();
    //            return entity;
    //        }
    //        catch (DbUpdateException dbEx)
    //        {
    //            // Log database update exceptions
    //            throw new ApplicationException("Database update error occurred.", dbEx);
    //        }
    //        catch (Exception ex)
    //        {
    //            // Log general exceptions
    //            throw new ApplicationException("An error occurred while adding the entity.", ex);
    //        }
    //    }
    //    public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
    //    {
    //        try
    //        {
    //            await _dbContext.Set<T>().AddRangeAsync(entities);
    //            await _dbContext.SaveChangesAsync();
    //            return entities;
    //        }
    //        catch (DbUpdateException dbEx)
    //        {
    //            // Log database update exceptions
    //            throw new ApplicationException("Database update error occurred.", dbEx);
    //        }
    //        catch (Exception ex)
    //        {
    //            // Log general exceptions
    //            throw new ApplicationException("An error occurred while adding the entities.", ex);
    //        }
    //    }


    //    public void Update(T entity)
    //    {
    //        _dbContext.Entry(entity).State = EntityState.Modified;
    //        _dbContext.SaveChanges();
    //    }

    //    public async Task<T> UpdateAsync(T entity)
    //    {
    //        _dbContext.Entry(entity).State = EntityState.Modified;
    //        await _dbContext.SaveChangesAsync();

    //        return entity;
    //    }

    //    public void Delete(T entity)
    //    {
    //        _dbContext.Set<T>().Remove(entity);
    //        _dbContext.SaveChanges();
    //    }

    //    public async Task<T> DeleteAsync(T entity)
    //    {
    //        _dbContext.Set<T>().Remove(entity);
    //        await _dbContext.SaveChangesAsync();

    //        return entity;
    //    }
    //    public async Task<PagedResponse<IList<T>>> GetPagedListDescending(int pageNumber, int pageSize, List<Expression<Func<T, bool>>> filter = null,
    //                                               Func<IQueryable<T>, IOrderedQueryable<T>> OrderByDescending = null,
    //                                               IEnumerable<Expression<Func<T, object>>> includes = null,
    //                                               Expression<Func<T, T>> selector = null)
    //    {


    //        var query = _dbContext.Set<T>().AsQueryable();
    //        if (includes != null)
    //        {
    //            query = includes.Aggregate(query, (current, include) => current.Include(include));
    //        }


    //        if (filter != null)
    //        {
    //            foreach (var fil in filter)
    //            {
    //                query = query.Where(fil);
    //            }
    //        }
    //        int totalCount = query.Count();

    //        if (OrderByDescending != null)
    //        {
    //            query = OrderByDescending(query);
    //        }

    //        //Controlling overflow//
    //        pageNumber = (pageNumber <= 0) ? 1 : pageNumber;
    //        pageSize = (pageSize > 100 || pageSize <= 0) ? 10 : pageSize;
    //        pageSize = (pageSize > totalCount) ? totalCount : pageSize;
    //        pageSize = (totalCount == 0) ? 10 : pageSize;
    //        //Controlling overflow//

    //        if (selector != null)
    //        {
    //            query = query.Select(selector).Skip((pageNumber - 1) * pageSize).Take(pageSize);
    //        }
    //        else
    //        {
    //            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
    //        }
    //        var pageData = await query.ToListAsync();

    //        return new PagedResponse<IList<T>>(pageData, pageNumber, pageSize, totalCount);

    //    }
    //    public async Task<PagedResponse<IList<T>>> GetPagedList(int pageNumber, int pageSize, List<Expression<Func<T, bool>>> filter = null,
    //                                               Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
    //                                               IEnumerable<Expression<Func<T, object>>> includes = null,
    //                                               Expression<Func<T, T>> selector = null)
    //    {


    //        var query = _dbContext.Set<T>().AsQueryable();
    //        if (includes != null)
    //        {
    //            query = includes.Aggregate(query, (current, include) => current.Include(include));
    //        }


    //        if (filter != null)
    //        {
    //            foreach (var fil in filter)
    //            {
    //                query = query.Where(fil);
    //            }
    //        }
    //        int totalCount = query.Count();

    //        if (orderBy != null)
    //        {
    //            query = orderBy(query);
    //        }

    //        //Controlling overflow//
    //        pageNumber = (pageNumber <= 0) ? 1 : pageNumber;
    //        pageSize = (pageSize > 100 || pageSize <= 0) ? 10 : pageSize;
    //        pageSize = (pageSize > totalCount) ? totalCount : pageSize;
    //        pageSize = (totalCount == 0) ? 10 : pageSize;
    //        //Controlling overflow//

    //        if (selector != null)
    //        {
    //            query = query.Select(selector).Skip((pageNumber - 1) * pageSize).Take(pageSize);
    //        }
    //        else
    //        {
    //            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
    //        }
    //        var pageData = await query.ToListAsync();

    //        return new PagedResponse<IList<T>>(pageData, pageNumber, pageSize, totalCount);

    //    }
    //}
    public class EFRepository<T> : IRepositoryAsync<T> where T : class
    {
        protected readonly DbContext _dbContext;

        public EFRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual IQueryable<T> OdataQuery()
        {
            return _dbContext.Set<T>().AsQueryable();
        }

        public virtual T GetById(dynamic id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public virtual async Task<T> GetByIdAsync(dynamic id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public virtual async Task<T> GetByIdAsyncdual(dynamic idregion, dynamic idprovincia)
        {
            return await _dbContext.Set<T>().FindAsync(idregion, idprovincia);
        }

        public IEnumerable<T> ListAll()
        {
            return _dbContext.Set<T>().AsEnumerable();
        }

        public async Task<List<T>> ListAllAsync(IEnumerable<Expression<Func<T, object>>> includes = null)
        {
            var query = _dbContext.Set<T>().AsQueryable();
            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            return await query.ToListAsync();
        }

        public async Task<List<T>> WhereAllAsync(Expression<Func<T, bool>> where = null, Expression<Func<T, object>> include = null)
        {
            var query = _dbContext.Set<T>().AsQueryable();
            if (include != null)
            {
                query = query.Include(include);
            }

            if (where != null)
            {
                query = query.Where(where);
            }

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<List<T>> GelAllAsync(Expression<Func<T, bool>> where = null, Expression<Func<T, object>> include = null)
        {
            var query = _dbContext.Set<T>().AsQueryable();
            if (include != null)
            {
                query = query.Include(include);
            }

            if (where != null)
            {
                query = query.Where(where);
            }

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<T> WhereAsync(Expression<Func<T, bool>> where = null, IEnumerable<Expression<Func<T, object>>> includes = null)
        {
            var query = _dbContext.Set<T>().AsQueryable();
            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            return await query.Where(where).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<bool> Exists(Expression<Func<T, bool>> where = null)
        {
            return await _dbContext.Set<T>().AnyAsync(where);
        }

        public async Task<T> AddAsync(T entity)
        {
            try
            {
                await _dbContext.Set<T>().AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                return entity;
            }
            catch (DbUpdateConcurrencyException dbEx)
            {
                // Manejo de excepción de concurrencia
                throw new ApplicationException("Optimistic concurrency error occurred while adding the entity.", dbEx);
            }
            catch (DbUpdateException dbEx)
            {
                // Manejo de excepción de actualización de base de datos
                throw new ApplicationException("Database update error occurred while adding the entity.", dbEx);
            }
            catch (Exception ex)
            {
                // Manejo de otras excepciones
                throw new ApplicationException("An error occurred while adding the entity.", ex);
            }
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            try
            {
                await _dbContext.Set<T>().AddRangeAsync(entities);
                await _dbContext.SaveChangesAsync();
                return entities;
            }
            catch (DbUpdateConcurrencyException dbEx)
            {
                // Manejo de excepción de concurrencia
                throw new ApplicationException("Optimistic concurrency error occurred while adding entities.", dbEx);
            }
            catch (DbUpdateException dbEx)
            {
                // Manejo de excepción de actualización de base de datos
                throw new ApplicationException("Database update error occurred while adding entities.", dbEx);
            }
            catch (Exception ex)
            {
                // Manejo de otras excepciones
                throw new ApplicationException("An error occurred while adding entities.", ex);
            }
        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            try
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                return entity;
            }
            catch (DbUpdateConcurrencyException dbEx)
            {
                // Manejo de excepción de concurrencia
                throw new ApplicationException("Optimistic concurrency error occurred while updating the entity.", dbEx);
            }
            catch (DbUpdateException dbEx)
            {
                // Manejo de excepción de actualización de base de datos
                throw new ApplicationException("Database update error occurred while updating the entity.", dbEx);
            }
            catch (Exception ex)
            {
                // Manejo de otras excepciones
                throw new ApplicationException("An error occurred while updating the entity.", ex);
            }
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            _dbContext.SaveChanges();
        }

        public async Task<T> DeleteAsync(T entity)
        {
            try
            {
                _dbContext.Set<T>().Remove(entity);
                await _dbContext.SaveChangesAsync();
                return entity;
            }
            catch (DbUpdateConcurrencyException dbEx)
            {
                // Manejo de excepción de concurrencia
                throw new ApplicationException("Optimistic concurrency error occurred while deleting the entity.", dbEx);
            }
            catch (DbUpdateException dbEx)
            {
                // Manejo de excepción de actualización de base de datos
                throw new ApplicationException("Database update error occurred while deleting the entity.", dbEx);
            }
            catch (Exception ex)
            {
                // Manejo de otras excepciones
                throw new ApplicationException("An error occurred while deleting the entity.", ex);
            }
        }

        public async Task<PagedResponse<IList<T>>> GetPagedListDescending(int pageNumber, int pageSize, List<Expression<Func<T, bool>>> filter = null,
                                                   Func<IQueryable<T>, IOrderedQueryable<T>> OrderByDescending = null,
                                                   IEnumerable<Expression<Func<T, object>>> includes = null,
                                                   Expression<Func<T, T>> selector = null)
        {
            var query = _dbContext.Set<T>().AsQueryable();
            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            if (filter != null)
            {
                foreach (var fil in filter)
                {
                    query = query.Where(fil);
                }
            }

            int totalCount = query.Count();

            if (OrderByDescending != null)
            {
                query = OrderByDescending(query);
            }

            //Controlling overflow//
            pageNumber = (pageNumber <= 0) ? 1 : pageNumber;
            pageSize = (pageSize > 100 || pageSize <= 0) ? 10 : pageSize;
            pageSize = (pageSize > totalCount) ? totalCount : pageSize;
            pageSize = (totalCount == 0) ? 10 : pageSize;
            //Controlling overflow//

            if (selector != null)
            {
                query = query.Select(selector).Skip((pageNumber - 1) * pageSize).Take(pageSize);
            }
            else
            {
                query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            }

            var pageData = await query.ToListAsync();

            return new PagedResponse<IList<T>>(pageData, pageNumber, pageSize, totalCount);
        }

        public async Task<PagedResponse<IList<T>>> GetPagedList(int pageNumber, int pageSize, List<Expression<Func<T, bool>>> filter = null,
                                                   Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                                   IEnumerable<Expression<Func<T, object>>> includes = null,
                                                   Expression<Func<T, T>> selector = null)
        {
            var query = _dbContext.Set<T>().AsQueryable();
            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            if (filter != null)
            {
                foreach (var fil in filter)
                {
                    query = query.Where(fil);
                }
            }

            int totalCount = query.Count();

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            //Controlling overflow//
            pageNumber = (pageNumber <= 0) ? 1 : pageNumber;
            pageSize = (pageSize > 100 || pageSize <= 0) ? 10 : pageSize;
            pageSize = (pageSize > totalCount) ? totalCount : pageSize;
            pageSize = (totalCount == 0) ? 10 : pageSize;
            //Controlling overflow//

            if (selector != null)
            {
                query = query.Select(selector).Skip((pageNumber - 1) * pageSize).Take(pageSize);
            }
            else
            {
                query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            }

            var pageData = await query.ToListAsync();

            return new PagedResponse<IList<T>>(pageData, pageNumber, pageSize, totalCount);
        }
    }

}
