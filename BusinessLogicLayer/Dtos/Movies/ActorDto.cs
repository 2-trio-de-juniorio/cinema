namespace BusinessLogicLayer.Dtos
{
    public class ActorDto // replace with normal dto instead
    {
        public string Firstname {get; set;}
        public string Lastname {get; set;}

        public override string ToString()
        {
            return Firstname + " " + Lastname;
        }
    }
}