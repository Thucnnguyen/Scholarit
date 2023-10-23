namespace Scholarit.DTO
{
    public class QuestionDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string? Type { get; set; }
        public string Difficult { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string? Option3 { get; set; } = String.Empty;
        public string? Option4 { get; set; } = String.Empty ;
        public string? Option5 { get; set; } = String.Empty;
        public int ChapterId { get; set; }
    }
}
