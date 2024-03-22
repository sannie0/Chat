namespace WebApiDemo;

public class ChatInterface
{
    public int Id { get; set; }
    public string ChatName { get; set; }

    public List<User> Users{ get; set; }
}