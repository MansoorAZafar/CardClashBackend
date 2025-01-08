namespace CardClashBackend.Models;

public class Player
{ 
    public int? Id { get; set; }
    public string? email { get; set; }
    public string? password { get; set; }

    public Player() { }

    public Player(string email, string password)
    {
        this.email = email;
        this.password = password;
    }
};
