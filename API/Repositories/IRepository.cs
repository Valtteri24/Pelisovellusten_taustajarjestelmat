using System;
using System.Threading.Tasks;
using gameapi.Models;

namespace gameapi.Repositories
{
    //All logic related to data-access happens on the classes that implement this
    public interface IRepository
    {
        Task<Player> CreatePlayer(Player player);
        Task<Player> GetPlayer(Guid playerId);
        Task<Player> GetPlayerByName(string name);
        Task<Player[]> GetAllPlayers(int minScore, string itemType);
        Task<Player[]> GetTopTen();
        Task<Player[]> GetBySize(int num);
        Task<Player> PushItem(Guid id, Item item);
        Task<Player> UpdatePlayer(Player player);
        Task<Player> UpdatePlayerNameAndScore(string name, string newName, int score);
        Task<Player> DeletePlayer(Guid playerId);


        Task<HighScore> CreateHighScore(HighScore highScore);
        Task<HighScore[]> GetAllHighScores(int minScore, string itemType);
        Task<HighScore> UpdateHighScore(HighScore highScore);
        Task<HighScore> DeleteHighScore(Guid highScoreId);

        Task<int> GetCommonLevel();
        Task<Item> CreateItem(Guid playerId, Item item);
        Task<Item> GetItem(Guid playerId, Guid itemId);
        Task<Item[]> GetAllItems(Guid playerId);
        Task<Item> UpdateItem(Guid playerId, Item item);
        Task<Item> DeleteItem(Guid playerId, Item item);
        Task<Item> DeleteAndAddScore(Guid playerId);
    }
}