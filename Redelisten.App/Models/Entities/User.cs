using System.Text.Json.Serialization;
using Redelisten.Data.Helper;

public class User
{

    [JsonConverter(typeof(JsonGuidConverter))]
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public User(CreateUserDto createUserDto)
    {
        this.Id = Guid.NewGuid();
        this.Name = createUserDto.Name;
    }
}