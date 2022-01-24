using apiMyApp.Entity;
using System.Collections.Generic;
using System;
using System.Linq;
namespace apiMyApp.Repositories
{
  public class InMemItemsRepository : IItemsRepository
  {
    private readonly List<Item> items = new()
    {
      new Item { Id = Guid.NewGuid(), Name = "Naruto", Price = 9, CreatedDate = System.DateTimeOffset.UtcNow },
      new Item { Id = Guid.NewGuid(), Name = "Bleatch", Price = 12, CreatedDate = System.DateTimeOffset.UtcNow },
      new Item { Id = Guid.NewGuid(), Name = "One Piece", Price = 18, CreatedDate = System.DateTimeOffset.UtcNow }
    };
    // インターフェース
    public IEnumerable<Item> GetItems()
    {
      return items;
    }

    public Item GetItem(Guid id)
    {
      //該当の値があってもなくてもreturnする。SingleOrDerfault
      return items.Where(item => item.Id == id).SingleOrDefault();
    }

    public void CreateItem(Item item)
    {
      items.Add(item);
    }

    public void UpdateItem(Item item)
    {
      var index = items.FindIndex(exItem => exItem.Id == item.Id);
      items[index] = item;
    }

    public void DeleteItem(Guid id)
    {
      var itemIndex = items.FindIndex(exItem => exItem.Id == id);
      items.RemoveAt(itemIndex);
    }
  }
}