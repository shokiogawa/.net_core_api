using apiMyApp.Entity;
using System.Collections.Generic;
using System;
using System.Linq;
using apiMyApp.Repositories.db;
using MySql.Data.MySqlClient;
namespace apiMyApp.Repositories
{
  public class ItemRepositoryImp : IItemsRepository
  {
    private readonly MysqlDb mysqlDb;
    public ItemRepositoryImp(MysqlDb mysqlDb)
    {
      this.mysqlDb = mysqlDb;
    }
    // インターフェース
    public IEnumerable<Item> GetItems()
    {
      List<Item> items = new List<Item> { };
      try
      {
        using (var cmd = mysqlDb.DBConnect())
        {
          cmd.CommandText = "SELECT * FROM items";
          // 括弧内の処理が終わったら破棄される。
          using (var reader = cmd.ExecuteReader())
          {
            while (reader.Read())
            {
              items.Add(new Item
              {
                Id = reader.GetGuid("public_item_id"),
                Name = reader.GetString("name"),
                Price = reader.GetInt32("price")
              });
            }
          }
        }
        return items;
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        throw e;
      }
    }

    public Item GetItem(Guid id)
    {
      //該当の値があってもなくてもreturnする。SingleOrDerfault
      Item item = new Item();
      using (var cmd = mysqlDb.DBConnect())
      {
        cmd.CommandText = "SELECT * FROM items WHERE public_item_id = @public_item_id";
        cmd.Parameters.AddWithValue("@public_item_id", id);
        using (var reader = cmd.ExecuteReader())
        {
          while (reader.Read())
          {

            item = new Item
            {
              Id = reader.GetGuid("public_item_id"),
              Name = reader.GetString("name"),
              Price = reader.GetInt32("price")
            };
          }
        }
      }
      return item;
    }

    public void CreateItem(Item item)
    {
      try
      {
        //データベースオープン&コネクト
        using (var cmd = mysqlDb.DBConnect())
        {
          //トランザクション開始(sql文が1つなため必要ないが練習として記述。)
          var transaction = cmd.Connection.BeginTransaction();
          cmd.CommandText = "INSERT INTO items(public_item_id, name, price) VALUES(@public_item_id, @name, @price)";
          cmd.Parameters.AddWithValue("@public_item_id", item.Id);
          cmd.Parameters.AddWithValue("@name", item.Name);
          cmd.Parameters.AddWithValue("@price", item.Price);
          var result = cmd.ExecuteNonQuery();
          if (result != 1)
          {
            Console.WriteLine("Unable to create data");
            //失敗したら元の状態にロールバック
            transaction.Rollback();
          }
          //成功したらコミット(データを保存)
          transaction.Commit();
        }
      }
      catch (Exception e)
      {
        throw e;
      }
    }

    public void UpdateItem(Item item)
    {
      try
      {
        using (var cmd = mysqlDb.DBConnect())
        {
          var transaction = cmd.Connection.BeginTransaction();
          cmd.CommandText = "UPDATE items SET name = @name, price = @price WHERE public_item_id = @public_item_id";
          cmd.Parameters.AddWithValue("@name", item.Name);
          cmd.Parameters.AddWithValue("@price", item.Price);
          cmd.Parameters.AddWithValue("@public_item_id", item.Id);
          //変更したテーブル内の行数を返す。
          var result = cmd.ExecuteNonQuery();
          if (result != 1)
          {
            Console.WriteLine("Update failed");
            transaction.Rollback();
          }
          transaction.Commit();
        }
      }
      catch (Exception e)
      {

        throw e;
      }
    }

    public void DeleteItem(Guid id)
    {
      try
      {
        using (var cmd = mysqlDb.DBConnect())
        {
          var transaction = cmd.Connection.BeginTransaction();
          cmd.CommandText = "DELETE FROM items WHERE public_item_id = @public_item_id";
          cmd.Parameters.AddWithValue("@public_item_id", id);
          var result = cmd.ExecuteNonQuery();
          if (result != 1)
          {
            Console.WriteLine("Delete un success");
            transaction.Rollback();
          }
          transaction.Commit();
        }
      }
      catch (Exception e)
      {
        throw e;
      }
    }
  }
}