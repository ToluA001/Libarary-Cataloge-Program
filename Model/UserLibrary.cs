namespace Libarary_Cataloge_Program.Model;

public class UserLibrary
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public string LibId { get; set; }
    public string Role { get; set; }

    public UserLibrary(string userId, string libId, string role)
    {
        UserId = userId;
        LibId = libId;
        Role = role;
    }
}