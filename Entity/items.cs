using System;
using Microsoft.EntityFrameworkCore;
namespace apiMyApp.Entity
{
  public record Item
  {
    public Guid Id { get; init; }
    public string Name { get; init; }

    public decimal Price { get; init; }
    public DateTimeOffset CreatedDate { get; init; }
  }
  // public class ItemContext : DbContext
  // {
  //   public DbSet<Item> Items { get; init; }
  // }
}