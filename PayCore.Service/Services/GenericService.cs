using Microsoft.EntityFrameworkCore;
using PayCore.Core.DTOs;
using PayCore.Core.Repositories;
using PayCore.Core.Services;
using PayCore.Core.UnitOfWork;
using PayCore.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PayCore.Service.Services
{
    public class GenericService<TEntity> : IGenericService<TEntity> where TEntity : class
    {

        private readonly IGenericRepository<TEntity> _repository;
        protected readonly IUnitOfWork _unitOfWork;

        public GenericService(IGenericRepository<TEntity> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        
        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _repository.AnyAsync(expression);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _repository.GetAll().ToListAsync();
        }
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _repository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            return entity;
        }
        public async Task<TEntity> GetByIdAsync(int id)
        {
            var hasProduct = await _repository.GetByIdAsync(id);
            if (hasProduct == null)
                throw new NotFoundException($"{typeof(TEntity).Name} {(id)} not found");
            return hasProduct;
        }

        public async Task RemoveAsync(TEntity entity)
        {
            _repository.Remove(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            

            _repository.Update(entity);
            await _unitOfWork.CommitAsync();
        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression)
        {
            return _repository.Where(expression);
        }

        
    }
}
