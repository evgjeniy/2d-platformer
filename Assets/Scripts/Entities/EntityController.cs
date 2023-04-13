namespace Entities
{
    [System.Serializable]
    public abstract class EntityController : RegisterBehaviour
    {
        public void RegisterEntity<T1, T2>(Entity<T1, T2> entity) where T1 : EntityController where T2 : EntityState => Awake(entity);

        protected virtual void Awake<T1, T2>(Entity<T1, T2> entity) where T1 : EntityController where T2 : EntityState {}
    }
}