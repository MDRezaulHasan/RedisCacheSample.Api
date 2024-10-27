namespace RedisCacheSample.Api.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }=String.Empty;
    public int UserLevel  { get; set; }
}