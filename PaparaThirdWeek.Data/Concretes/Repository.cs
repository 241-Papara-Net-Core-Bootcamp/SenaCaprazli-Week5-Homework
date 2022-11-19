using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PaparaThirdWeek.Data.Abstracts;
using PaparaThirdWeek.Data.Context;
using PaparaThirdWeek.Domain;
using PaparaThirdWeek.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace PaparaThirdWeek.Data.Concretes
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
      public PaparaAppDbContext Context { get; }
        public Repository(PaparaAppDbContext context)
        {
            Context = context;
        }

        public void Add(T entity)
        {
            Context.Set<T>().Add(entity);
            Context.SaveChanges();
        }
        public IQueryable<T> Get()
        {
            return Context.Set<T>()
                .Where(x => (bool)!x.IsDeleted)
                .AsQueryable();
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> expression)
        {
            return Context.Set<T>()
                .Where(expression)
                .AsQueryable();
        }
        public void HardRemove(T entity)
        {
            T existData = Context.Set<T>().Find(entity.Id);
            if (existData != null)
            {
                Context.Set<T>().Remove(existData);
                Context.SaveChanges();
            }
        }

        public void Remove(int id)
        {
            T existData = Context.Set<T>().Find(id);
            if (existData != null)
            {
                existData.IsDeleted = true;
                Context.Entry(existData).State = EntityState.Modified;
                Context.SaveChanges();
            }
        }
        public void Update(T entity)
        {
            {
                Context.Update(entity);
                Context.SaveChanges();
            }
        }

    }
}
