namespace Scholarit.DTO
{
    public class QuizQuestionUpdateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public int ChapterId { get; set; }
        public List<int> QuestionIdList { get; set; }
    }
}
