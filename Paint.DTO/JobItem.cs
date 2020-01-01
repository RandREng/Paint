namespace Paint.DTO
{
    public class JobItem
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public AddressDto Address { get; set; }
        public string ProjectManager { get; set; }
    }

    public class JobItem2
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public AddressDto Address { get; set; }
    }
}
