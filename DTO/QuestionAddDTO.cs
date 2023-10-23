namespace Scholarit.DTO
{
    public class QuestionAddDTO
    {
        public string Text { get; set; }
        public string? Type { get; set; }
        public string Difficult { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string? Option3 { get; set; }
        public string? Option4 { get; set; }
        public string? Option5 { get; set; }
        public int ChapterId { get; set; }
        public string Answer { get; set; }
    }
}
