using apiMyApp.Entity;
using System.Collections.Generic;
using System;
using System.Linq;
namespace apiMyApp.Repositories
{
  public interface IItemsRepository
  {
    IEnumerable<Item> GetItems();
    Item GetItem(Guid id);

    void CreateItem(Item item);
    void UpdateItem(Item item);

    void DeleteItem(Guid id);
  }
}