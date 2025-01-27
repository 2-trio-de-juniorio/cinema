namespace BusinessLogicLayer.Dtos;

public class ActorDto 
{
    public string Firstname {get; set;}
    public string Lastname {get; set;}

    public override string ToString()
    {
        return Firstname + " " + Lastname;
    }
}