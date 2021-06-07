using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoAspNetCore.DomainCore.Base
{
    public interface IRepository<TEntity, TKey> : IDisposable where TEntity : class
    {
        Task Atualizar(TEntity entity);
        Task Excluir(TEntity entity);
        Task ExcluirPorId(TKey id);
        Task Inserir(TEntity entity);
        Task<IEnumerable<TEntity>> SelecionarTodos(Expression<Func<TEntity, bool>> quando = null);
        Task<TEntity> SelecionarPorId(TKey id);
        Task<int> SaveAsync();
    }
}
