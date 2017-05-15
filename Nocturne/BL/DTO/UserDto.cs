namespace Nocturne.BL.DTO
{
    public class UserDto
    {
        public const string Administrator = "Administrator";
        public const string Worker = "Worker";

        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string RegCode { get; set; }
        public bool IsActive { get; set; }     
        public string[] UserRoles { get; set; }
    }
}
