using Microsoft.EntityFrameworkCore;

namespace Cadastramento {
    internal class EntityFrameworkCollectionView<T> {
        private DbContext db;

        public EntityFrameworkCollectionView(DbContext db) {
            this.db = db;
        }
    }
}