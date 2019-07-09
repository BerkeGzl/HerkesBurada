using Project.BLL.RepositoryPattern.InterfaceRep;
using Project.BLL.SingletonPattern;
using Project.DAL.Context;
using Project.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.RepositoryPattern.BaseRep
{
    public abstract class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected MyContext db;
        public BaseRepository()
        {
            db = DBTool.DBInstance;
        }

        protected virtual void Save()
        {
            db.SaveChanges();
        }
        public virtual void Add(T item)
        {
            db.Set<T>().Add(item);
            Save();
        }

        public virtual bool Any(Expression<Func<T, bool>> exp)
        {
            return db.Set<T>().Any(exp);
        }

        public virtual T Default(Expression<Func<T, bool>> exp)
        {
            return db.Set<T>().FirstOrDefault(exp);
        }

        public virtual void Delete(T item)
        {
            item.Status = MODEL.Enums.DataStatus.Deleted;
            item.DeletedDate = DateTime.Now;
            Save();
        }

        public virtual T GetByID(int id)
        {
            return db.Set<T>().Find(id);
        }

  public virtual object ListAnonymous(Expression<Func<T, object>> exp)
        {
            return db.Set<T>().Select(exp).ToList();
        }

        public virtual List<T> SelectActives()
        {
            return db.Set<T>().Where(x => x.Status != MODEL.Enums.DataStatus.Deleted).OrderByDescending(x => x.CreatedDate).ToList();
        }

        public virtual List<T> SelectAll()
        {
            return db.Set<T>().ToList();
        }

        public virtual List<T> SelectDeleteds()
        {
            return db.Set<T>().Where(x => x.Status == MODEL.Enums.DataStatus.Deleted).OrderByDescending(x => x.CreatedDate).ToList();
        }

        public virtual List<T> SelectModifieds()
        {
            return db.Set<T>().Where(x => x.Status == MODEL.Enums.DataStatus.Updated).OrderByDescending(x => x.CreatedDate).ToList();
        }

        public virtual void SpecialDelete(int id)
        {
            db.Set<T>().Remove(GetByID(id));
            Save();
        }

        public virtual void Update(T item)
        {
            item.Status = MODEL.Enums.DataStatus.Updated;
            T toBeUpdated = GetByID(item.ID);
            db.Entry(toBeUpdated).CurrentValues.SetValues(item);
            Save();
        }

        public virtual List<T> Where(Expression<Func<T, bool>> exp)
        {
            return db.Set<T>().Where(exp).ToList();
        }

        public virtual int GetLastAdded()
        {
            return db.Set<T>().OrderByDescending(x => x.ID).FirstOrDefault().ID;
        }
    }
}
