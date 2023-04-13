using System.Collections.Generic;
using System.Linq;

namespace Entities.Player
{
    public class PlayerInventory
    {
        private readonly List<ICollectableItem> _items = new ();

        public void Add(ICollectableItem item) => _items.Add(item);

        public bool Remove(ICollectableItem item) => _items.Remove(item);
        
        public void Remove(int index)
        {
            if (index < 0 || index >= _items.Count) return;
            _items.Remove(_items[index]);
        }

        public bool Contains(ICollectableItem item) => _items.Contains(item);

        public T Find<T>() where T : class, ICollectableItem => _items.FirstOrDefault(item => item is T) as T;
    }

    public interface ICollectableItem {}
}