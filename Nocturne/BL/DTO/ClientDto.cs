namespace Nocturne.BL.DTO
{
    public class ClientDto
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string IdCode { get; set; }
        public string Fullname
        {
            get
            {
                return string.Format("{0} {1}", Name, Surname);
            }
        }
    }
}