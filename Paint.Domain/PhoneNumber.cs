using Microsoft.EntityFrameworkCore;

namespace Paint.Domain
{
    [Owned]
    public class PhoneNumber
    {
        public string Number { get; set; }
        public bool Active { get; set; }
        public int PhoneNumberTypeId { get; set; }
        public PhoneNumberType PhoneNumberType { get; set; }
    }

}
