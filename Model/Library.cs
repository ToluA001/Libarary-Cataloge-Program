namespace Libarary_Cataloge_Program.Model;

public class Library
{
    public int Id { get; set; } //EF key
    public string Name { get; set; }
    public string CreatorId { get; set; }

    public Library() { }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="creatorID"></param>
    public Library(string name, string creatorID)
    {
        Name = name;
        CreatorId = creatorID;
    }
}