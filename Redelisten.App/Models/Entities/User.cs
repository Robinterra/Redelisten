using System.Text.Json.Serialization;
using Redelisten.Data.Helper;

public class User
{

    [JsonConverter(typeof(JsonGuidConverter))]
    public Guid Token { get; set; }
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }

    public User(CreateUserDto createUserDto, int count)
    {
        this.Token = Guid.NewGuid();
        this.Name = createUserDto.Name;
        this.CreatedAt = DateTime.UtcNow;
        this.Id = count;
    }
}