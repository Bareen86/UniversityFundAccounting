namespace Corpuses.Domain
{
    public class Corpuse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int FloorsNumber { get; set; }

        public Corpuse( string name, string address, int floorsNumber )
        {
            Name = name;
            Address = address;
            FloorsNumber = floorsNumber;
        }

        public void Update( string name, string address, int floorsNumber )
        {
            Name = name;
            Address = address;
            FloorsNumber = floorsNumber;
        }   
    }
}
