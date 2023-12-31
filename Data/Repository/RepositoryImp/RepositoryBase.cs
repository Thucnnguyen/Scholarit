﻿using AlumniProject.Dto;
using Microsoft.EntityFrameworkCore;
using Scholarit.Data;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace AlumniProject.Data.Repostitory.RepositoryImp
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly ScholaritDbContext _context;
        public RepositoryBase(ScholaritDbContext context)
        {
            _context = context;
        }

        public async Task<int> CountByCondition(params Expression<Func<T, bool>>[] filters)
        {
            var query = _context.Set<T>().AsQueryable();

            foreach (var filter in filters)
            {
                query = query.Where(filter);
            }
            var entities = await query.FirstOrDefaultAsync();
            var count = await query.CountAsync();
            return count;
        }

        public async Task CreateRangeAsync(List<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public async Task<int> CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            var id = (int)_context.Entry(entity).Property("Id").CurrentValue;
            return id;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<T> FindOneByCondition(params Expression<Func<T, bool>>[] filters)
        {
            var query = _context.Set<T>().AsQueryable();

            foreach (var filter in filters)
            {
                query = query.Where(filter);
            }
            var entities = await query.FirstOrDefaultAsync();
            return entities;

        }

        public async Task<PagingResultDTO<T>> GetAllByConditionAsync(
            int pageNo,
            int pageSize,
            Expression<Func<T, bool>>[] filters,
            Expression<Func<T, object>> orderBy,
            bool descending = false
            )
        {
            var skipAmount = (pageNo - 1) * pageSize;
            var query = _context.Set<T>().AsQueryable();

            foreach (var filter in filters)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                if (descending)
                {
                    query = query.OrderByDescending(orderBy);
                }
                else { query = query.OrderBy(orderBy); }
            }

            var entities = await query.Skip(skipAmount).Take(pageSize).ToListAsync();
            var total = await query.CountAsync();
            var resultt = new PagingResultDTO<T>
            {
                CurrentPage = pageNo,
                Items = entities,
                PageSize = pageSize,
                TotalItems = total
            };
            return resultt;


        }

        public async Task<IEnumerable<T>> GetAllByConditionAsync(
            Expression<Func<T, bool>>[] filters,
            Expression<Func<T, object>> orderBy,
            bool descending = false)
        {
            var query = _context.Set<T>().AsQueryable();

            foreach (var filter in filters)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                if (descending)
                {
                    query = query.OrderByDescending(orderBy);
                }
                else { query = query.OrderBy(orderBy); }
            }

            var entities = await query.ToListAsync();

            return entities;
        }

        public async Task<T> GetByIdAsync(params Expression<Func<T, bool>>[] filters)
        {
            var query = _context.Set<T>().AsQueryable();

            foreach (var filter in filters)
            {
                query = query.Where(filter);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            var entry = _context.Entry(entity);

            _context.Set<T>().Update(entity);

            await _context.SaveChangesAsync();
        }


    }
}
