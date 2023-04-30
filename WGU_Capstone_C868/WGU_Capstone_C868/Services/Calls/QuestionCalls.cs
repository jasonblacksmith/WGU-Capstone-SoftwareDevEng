using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WGU_Capstone_C868.Services.Interfaces;

namespace WGU_Capstone_C868.Services.Calls
{
    public class QuestionCalls : IQuestionCalls
    {
        public Question question;
        public ObservableCollection<Question> questions = new();

        //Creates and adds new Question record to DB
        public async Task<Question> AddQuestionAsync(Question question)
        {
            Question AddQuestion = question;
            try
            {
                _ = await SqLiteDataService.Db.InsertAsync(AddQuestion);
                return AddQuestion;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        //Returns the desired Question record from the DB
        public async Task<Question> GetQuestionAsync(int pk)
        {
            try
            {
                Question GetQuestion = await SqLiteDataService.Db.GetAsync<Question>(pk);
                return GetQuestion;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        //Returns an ObservableCollection of all Questions int the table
        public async Task<ObservableCollection<Question>> GetQuestionsAsync()
        {
            List<Question> Questions = await SqLiteDataService.Db.Table<Question>().ToListAsync();
            foreach (Question Question in Questions)
            {
                questions.Add(Question);
            }
            return questions;
        }

        //Removes or Deletes the desired Question record from the DB
        public async Task<bool> RemoveQuestionAsync(Question question)
        {
            try
            {
                _ = await SqLiteDataService.Db.DeleteAsync<Question>(question.QuestionId);

                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        //Updates the desired Question in the Question table in the DB
        public async Task<Question> UpdateQuestionAsync(Question question)
        {
            Question UpdateQuestion = question;
            try
            {
                _ = await SqLiteDataService.Db.UpdateAsync(UpdateQuestion);
                return UpdateQuestion;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }
    }
}
