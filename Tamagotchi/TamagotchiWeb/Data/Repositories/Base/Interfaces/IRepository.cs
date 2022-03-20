//
//  Copyright 2021
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.

namespace TamagotchiWeb.Data.Repositories.Base.Interfaces;

public interface IRepository<TEntity> where TEntity : class, new()
{
    IUnitOfWork UnitOfWork { get; }

    void Remove(TEntity entity);

    void RemoveRange(IEnumerable<TEntity> entities);

    Task<TEntity> AddAsync(TEntity newEntity, CancellationToken cancellationToken = default);

    Task AddRangeAsync(IEnumerable<TEntity> newEntities);

    TEntity Update(TEntity entity);

    IQueryable<TEntity> GetReadOnlyQuery();

    IQueryable<TEntity> GetChangeTrackingQuery();

    Task<TEntity> FindByIdAsync(params object[] id);
}