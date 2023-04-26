namespace WGU_Capstone_C868.Services.Interfaces
{
    public interface IQuestionCalls
    {
        Task<Question> AddQuestionAsync(Question question);
        Task<bool> RemoveQuestionAsync(Question question);
        Task<Question> GetQuestionAsync(int pk);
        Task<Question> UpdateQuestionAsync(Question question);
        Task<ObservableCollection<Question>> GetQuestionsAsync();
    }
}
