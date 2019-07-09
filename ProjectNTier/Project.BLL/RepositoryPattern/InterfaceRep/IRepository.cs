using Project.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.RepositoryPattern.InterfaceRep
{
    public interface IRepository<T> where T:BaseEntity
    {
        void Add(T item);
        void Delete(T item);
        void Update(T item);
        List<T> SelectAll();
        T GetByID(int id);
        List<T> SelectActives();
        object ListAnonymous(Expression<Func<T, object>> exp);
        T Default(Expression<Func<T, bool>> exp);
        List<T> Where(Expression<Func<T, bool>> exp);
        bool Any(Expression<Func<T, bool>> exp);
        List<T> SelectDeleteds();
        List<T> SelectModifieds();
        void SpecialDelete(int id);
    }
}
