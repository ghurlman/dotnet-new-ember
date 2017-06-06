using JsonApiDotNetCore.Models;

namespace dotnet_new_ember
{
  public class Person : Identifiable
  {
    [Attr("name")]
    public string Name { get; set; }
  }
}