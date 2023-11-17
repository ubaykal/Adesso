namespace WorldLeague.Entities;

public class Team : BaseEntity
{
    public string Name { get; set; }
    public string Country { get; set; }
    public char Group { get; set; }
    public Guid DrawerId { get; set; }
    public Drawer Drawer { get; set; }
}