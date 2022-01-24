using System;
using MySql.Data.MySqlClient;
namespace apiMyApp.Repositories.db
{
  public class MysqlDb
  {
    private static readonly string server = "net-db";
    private static readonly string database = "mysql";
    private static readonly string user = "user";
    private static readonly string pass = "secret";
    private static readonly string charset = "utf8";
    private static readonly string dsn = string.Format("Server={0};Database={1};Uid={2};Pwd={3};Charset={4}", server, database, user, pass, charset);

    public MySqlConnection mySqlConnection;
    public MysqlDb()
    {
      this.mySqlConnection = new MySqlConnection(dsn);
      mySqlConnection.Open();
    }
    public MySqlCommand DBConnect()
    {
      MySqlCommand cmd = mySqlConnection.CreateCommand();
      return cmd;
    }
  }
}