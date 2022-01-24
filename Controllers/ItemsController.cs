using Microsoft.AspNetCore.Mvc;
using apiMyApp.Repositories;
using System;
using apiMyApp.Entity;
using apiMyApp.Dtos;
using System.Collections.Generic;
using System.Linq;
namespace apiMyApp.Controllers
{
  [ApiController]
  [Route("items")]
  public class ItemsController : ControllerBase
  {
    //プロパティ
    private readonly IItemsRepository repository;

    //コンストラクタ
    public ItemsController(IItemsRepository repository)
    {
      this.repository = repository;
    }
    [HttpGet]
    public IEnumerable<ItemDto> GetItems()
    {
      var items = repository.GetItems().Select(item => item.AsDto());
      return items;
    }
    [HttpGet("{id}")]
    public ActionResult<ItemDto> GetItem(Guid id)
    {
      var item = repository.GetItem(id).AsDto();
      if (item is null)
      {
        return NotFound();
      }
      //TODO:itemをitemDtoに変換するメソッドを作成する
      return item;
    }

    [HttpPost]
    //ActionResultは1つ以上の返り値を返す際に使用される。
    public ActionResult<ItemDto> CreateItem(CreateItemDto createItemDto)
    {
      Item item = new()
      {
        Id = Guid.NewGuid(),
        Name = createItemDto.Name,
        Price = createItemDto.Price,
        CreatedDate = DateTimeOffset.UtcNow
      };

      repository.CreateItem(item);
      //Postで何が生成されたかをクライエントに返す際の処理。
      return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item.AsDto());
    }

    [HttpPut("{id}")]
    //TODO: createItemDtoをUpdateItemDtoに変換
    public ActionResult<ItemDto> UpdateItem(Guid id, UpdateItemDto updateItemDto)
    {
      //バリデーション
      var item = repository.GetItem(id);
      if (item is null)
      {
        return NotFound();
      }
      Item newItem = new()
      {
        Id = id,
        Name = updateItemDto.Name,
        Price = updateItemDto.Price,
        CreatedDate = DateTimeOffset.UtcNow
      };
      repository.UpdateItem(newItem);
      return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteItem(Guid id)
    {
      var existingItem = repository.GetItem(id);
      if (existingItem is null)
      {
        return NotFound();
      }
      repository.DeleteItem(id);
      return NoContent();
    }
  }

}