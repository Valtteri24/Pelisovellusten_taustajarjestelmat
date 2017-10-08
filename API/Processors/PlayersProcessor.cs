using System;
using System.Threading.Tasks;
using gameapi.Models;
using gameapi.Repositories;

namespace gameapi.Processors
{
    // Business logic (logic for verifying and updating the data) happens here
    public class PlayersProcessor
    {
        private readonly IRepository _repository;
        public PlayersProcessor(IRepository repository)
        {
            _repository = repository;
        }

        public Task<Player[]> GetAll(int minScore, string itemType)
        {
            Console.WriteLine("Most Common Level: " + _repository.GetCommonLevel().Result);

            return _repository.GetAllPlayers(minScore, itemType);
        }

        public Task<Player> Get(Guid id)
        {
            return _repository.GetPlayer(id);
        }
        public Task<Player> GetByName(string name)
        {
            return _repository.GetPlayerByName(name);
        }
        public Task<Player> Create(NewPlayer newPlayer)
        {
            Player player = new Player()
            {
                Items = new Item[1],
                Id = Guid.NewGuid(),
                Name = newPlayer.Name,
                Level = 1,

            };
            return _repository.CreatePlayer(player);
        }

        public Task<Player> Delete(Guid id)
        {
            return _repository.DeletePlayer(id);
        }


        public async Task<Player> Update(Guid id, ModifiedPlayer modifiedPlayer)
        {
            Player player = await _repository.GetPlayer(id);
            player.Level = modifiedPlayer.Level;
            await _repository.UpdatePlayer(player);
            return player;
        }
        public async Task<Player> UpdatePlayerNameAndScore(string name, string newName, int score)
        {
            return await _repository.UpdatePlayerNameAndScore(name, newName, score);
        }
        public async Task<Player> PushItem(Guid id, string type, int itemLevel)
        {
            var item = new Item()
            {
                Id = Guid.NewGuid(),
                Price = 10,
                Level = itemLevel,
                Type = type,
            };

            return await _repository.PushItem(id, item);
        }
        public async Task<Player[]> GetTopTen()
        {
            return await _repository.GetTopTen();
        }
        public async Task<Player[]> GetBySize(int num){
            return await _repository.GetBySize(num);
        }



    }
}