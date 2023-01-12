using CoreAndFood.Data.Models;
using CoreAndFood.Data;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System;

namespace CoreAndFood.Repositories
{
    public class GenericRepository<T> where T : class, new()
    {
        //Veritabanı bağlantısı
        Context db = new Context();

        //Kategorileri listeleme metodu
        public List<T> TList()
        {
            return db.Set<T>().ToList();
        }

        //ekleme metodu
        public void TAdd(T prm)
        {
            db.Set<T>().Add(prm);
            db.SaveChanges();
        }

        //silme metodu
        public void TDelete(T prm)
        {
            db.Set<T>().Remove(prm);
            db.SaveChanges();
        }

        //güncelleme metodu
        public void TUpdate(T prm)
        {
            db.Set<T>().Update(prm);
            db.SaveChanges();
        }

        //Id ye göre  gösterme
        public T TGet(int id)
        {
           return db.Set<T>().Find(id);
        }

        //Arama işlemi için istediğim kolonu uygulamama yarar
        public List<T> List(Expression<Func<T, bool>> filter)
        {
            return db.Set<T>().Where(filter).ToList();
        }
    }
}
