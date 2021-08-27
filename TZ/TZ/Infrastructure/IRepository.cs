using System;
using System.Collections.Generic;

namespace TZ.Infrastructure
{
    public interface IRepository<T> 
        where T : class
    {
        public void Add(T obj);
        public void DeleteByID(Guid guid);
        public T Edit(T newObj);
        public IEnumerable<T> List();
        public T GetByID(Guid guid);
        public void SaveAll();
    }
}