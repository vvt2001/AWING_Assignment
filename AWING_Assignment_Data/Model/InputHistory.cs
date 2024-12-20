namespace AWING_Assignment_Data.Model
{
    public class InputHistory : Entity
    {
        
        public required int n { get;set; }
        public required int m { get;set; }
        public required int p { get;set; }
        public required string matrix { get;set; }
        public required DateTime time { get; set; }
    }
}
