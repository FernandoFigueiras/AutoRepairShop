﻿using AutoRepairShop.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity
    {
        private readonly DataContext _context;

        public GenericRepository(DataContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await SaveAllAsync();
        }



        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>()
                .Remove(entity);
            await SaveAllAsync();
        }




        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Set<T>()
                .AllAsync(e => e.Id == id);
        }




        public IQueryable<T> GetAll()
        {
            return _context.Set<T>().AsNoTracking();
        }




        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>()
                .FirstOrDefaultAsync(e => e.Id == id);
        }




        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>()
                 .Update(entity);

            await SaveAllAsync();
        }




        private async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
