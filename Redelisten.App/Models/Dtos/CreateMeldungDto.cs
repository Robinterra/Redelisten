public class CreateMeldungDto
{
    public string RedelistenName { get; }
     
    public int Type { get; set; }

    public CreateMeldungDto(string redelistenName)
    {
        RedelistenName = redelistenName;
    }
    
}