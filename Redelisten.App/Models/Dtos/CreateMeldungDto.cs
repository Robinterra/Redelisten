public class CreateMeldungDto
{
    public string RedelistenName { get; set; } = "";
     
    public int Type { get; set; }

    public CreateMeldungDto()
    {
        
    }

    public CreateMeldungDto(string redelistenName)
    {
        RedelistenName = redelistenName;
    }
    
}