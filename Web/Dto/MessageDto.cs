namespace Web.Dto
{
    public class MessageDto
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public int UserId { get; set; }

        public int ChatId { get; set; }

        public DateTime Date { get; set; }
    }
}
