namespace Scholarit.DTO
{
    public class UserUpdateDTO
    {
        
        public string FullName { get; set; }
        public DateTime Dob { get; set; }
        public string Address { get; set; }
        public string Hobby { get; set; }
        public DateTime LastLogin { get; set; }
        public int LearnHourPerDay { get; set; }
        public string Strength { get; set; }
        public string AvatarUrl { get; set; }
    }
}
