using Microsoft.EntityFrameworkCore;
using ProjetoAspNetCore.Data.ORM;
using ProjetoAspNetCore.Domain.Entities;
using ProjetoAspNetCore.DomainCore.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoAspNetCore.Repository.Base
{
    public abstract class RepositoryGeneric<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class, new()
    {
        protected CursoDbContext _context;

        protected RepositoryGeneric(CursoDbContext context)
        {
            this._context = context;
        }

        public virtual async Task Atualizar(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await this.SaveAsync();
        }

        public virtual async Task Excluir(TEntity entity)
        {
            this._context.Entry(entity).State = EntityState.Deleted;
            await this.SaveAsync();
        }

        public virtual async Task ExcluirPorId(TKey id)
        {
            TEntity entity = await this.SelecionarPorId(id);
            await this.Excluir(entity);
        }

        public virtual async Task Inserir(TEntity entity)
        {
            this._context.Set<TEntity>().Add(entity);
            await this.SaveAsync();
        }

        public async Task<TEntity> SelecionarPorId(TKey id)
        {
            return await this._context.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task<IEnumerable<TEntity>> SelecionarTodos(Expression<Func<TEntity, bool>> quando = null)
        {
            if (quando == null)
            {
                return await this._context.Set<TEntity>().AsNoTracking().ToListAsync();
            }
            return await this._context.Set<TEntity>().AsNoTracking().Where(quando).ToListAsync();
        }

        public void Dispose()
        {
            _context?.DisposeAsync();
        }

        public virtual async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
