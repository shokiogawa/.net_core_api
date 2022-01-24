using apiMyApp.Dtos;
using apiMyApp.Entity;
namespace apiMyApp
{
  public static class Extention
  {
    //拡張メソッド(staticメソッドをインスタンスメソッドと同じ形式で呼び出せる)
    public static ItemDto AsDto(this Item item)
    {
      return new ItemDto
      {
        Id = item.Id,
        Name = item.Name,
        Price = item.Price,
        CreatedDate = item.CreatedDate
      };
    }
  }

}