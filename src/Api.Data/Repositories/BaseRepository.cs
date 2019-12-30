using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Data.Contexts;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repositories {
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity {
        protected readonly MyContext _context;
        private DbSet<T> _dataset;

        public BaseRepository (MyContext context) {
            _context = context;
            _dataset = _context.Set<T>();
        }
        
        public async Task<bool> ExistAsync(Guid id)
        {
          try {
               return await _dataset.AnyAsync (obj => obj.Id.Equals (id));
            } catch (Exception e) {
                throw e;
            }
        }

        public async Task<T> InsertAsync (T item) {
            try {
                if (item.Id == Guid.Empty) item.Id = Guid.NewGuid ();
                item.CreatedAt = DateTime.UtcNow;
                _dataset.Add (item);
                await _context.SaveChangesAsync ();
                return item;
            } catch (Exception e) {
                throw e;
            }
        }

        public async Task<T> UpdateAsync (T item) {
            try {
                var model = await _dataset.SingleOrDefaultAsync (obj => obj.Id.Equals (item.Id));

                if (model == null) return null;

                item.Updated = DateTime.UtcNow;
                item.CreatedAt = model.CreatedAt;

                _context.Entry (model).CurrentValues.SetValues (item);

                await _context.SaveChangesAsync ();
                return item;
            } catch (Exception e) {
                throw e;
            }
        }

        public async Task<bool> DeleteAsync (Guid id) {
            try {
                var model = await _dataset.SingleOrDefaultAsync (obj => obj.Id.Equals (id));

                if (model == null) return false;

                _dataset.Remove (model);

                await _context.SaveChangesAsync ();
                return true;
            } catch (Exception e) {
                throw e;
            }
        }

        public async Task<T> SelectAsync (Guid id) {
            try {
                var model = await _dataset.SingleOrDefaultAsync (obj => obj.Id.Equals (id));

                if (model == null) return null;

                _dataset.Remove (model);

                await _context.SaveChangesAsync ();
                return model;
            } catch (Exception e) {
                throw e;
            }
        }

        public async Task<IEnumerable<T>> SelectAsync () {
            try {
                var model = await _dataset.ToListAsync ();
                if (model == null) return null;
                await _context.SaveChangesAsync ();
                return model;
            } catch (Exception e) {
                throw e;
            }
        }
    }
}