using System;
using System.Threading.Tasks;
using gameapi.Models;
using gameapi.Repositories;

namespace gameapi.Processors
{
    // Business logic (logic for verifying and updating the data) happens here
    public class HighScoresProcessor
    {
        private readonly IRepository _repository;
        public HighScoresProcessor(IRepository repository)
        {
            _repository = repository;
        }

        public Task<HighScore[]> GetAll(int minScore, string itemType)
        {
            Console.WriteLine("Most Common Level: " + _repository.GetCommonLevel().Result);

            return _repository.GetAllHighScores(minScore, itemType);
        }
        public Task<HighScore> Create(NewHighScore newHighScore)
        {
            HighScore highScore = new HighScore()
            {
                Items = new Item[1],
                Id = Guid.NewGuid(),
                Name = newHighScore.Name,
                Level = 1,

            };
            return _repository.CreateHighScore(highScore);
        }

        public Task<HighScore> Delete(Guid id)
        {
            return _repository.DeleteHighScore(id);
        }


        public async Task<HighScore> Update(Guid id, ModifiedHighScore modifiedHighScore)
        {
            HighScore highScore = await _repository.GetHighScore(id);
            highScore.Level = modifiedHighScore.Level;
            await _repository.UpdateHighScore(highScore);
            return highScore;
        }
    }
}