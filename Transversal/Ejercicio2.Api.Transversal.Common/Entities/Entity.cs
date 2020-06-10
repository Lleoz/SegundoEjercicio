namespace Ejercicio2.Api.Transversal.Common.Entities
{
    public abstract class Entity<TKey>
    {
        public TKey Id { get; set; }
    }
}
