using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using gameapi.Models;
using gameapi.mongodb;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace gameapi.Repositories
{
    //Gets from and updates data to MongoDb 
    public class MongoDbRepository : IRepository
    {
        private IMongoCollection<Player> _collection;
        public MongoDbRepository(MongoDBClient client)
        {
            //Getting the database with name "game2"
            IMongoDatabase database = client.GetDatabase("game");

            //Getting collection with name "players"
            _collection = database.GetCollection<Player>("players");
        }

        public async Task<Item> CreateItem(Guid playerId, Item item)
        {
            var filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
            var cursor = await _collection.FindAsync(filter);
            var player = cursor.First();

            for (int i = 0; i < player.Items.Length; i++)
            {
                if (player.Items[i] == null)
                {
                    var filter1 = Builders<Player>.Filter.Eq(p => p.Items[i], null);
                    var update = Builders<Player>.Update.Set(x => x.Items[i], item);
                    var result = await _collection.UpdateOneAsync(filter, update);
                    break;
                }
                else if (i == player.Items.Length - 1)
                {

                    var update = Builders<Player>.Update.Push(x => x.Items, item);
                    await _collection.UpdateOneAsync(filter, update);
                }


            }
            return item;
        }

        public async Task<Player> CreatePlayer(Player player)
        {
            await _collection.InsertOneAsync(player);
            return player;
        }

        public async Task<Item> DeleteItem(Guid playerId, Item item)
        {
            var filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
            var cursor = await _collection.FindAsync(filter);
            var player = cursor.First();

            for (int i = 0; i < player.Items.Length; i++)
            {
                if (player.Items[i] != null)
                {
                    if (player.Items[i].Id == item.Id)
                    {
                        var filter1 = Builders<Player>.Filter.Eq(p => p.Items[i], item);
                        var update = Builders<Player>.Update.Set(x => x.Items[i], null);
                        var result = await _collection.UpdateOneAsync(filter, update);
                        break;
                    }
                }



            }
            return item;
        }

        public async Task<Player> DeletePlayer(Guid playerId)
        {
            var result = await _collection.FindAsync(a => a.Id == playerId);
            await _collection.DeleteOneAsync(a => a.Id == playerId);
            return null;
        }

        public async Task<Item[]> GetAllItems(Guid playerId)
        {

            var filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
            var cursor = await _collection.FindAsync(filter);
            var player = cursor.First();

            int itemCount = player.Items.Length;
            Item[] item = new Item[itemCount];

            for (int i = 0; i < player.Items.Length; i++)
            {
                item[i] = player.Items[i];
            }
            return item;
        }

        public async Task<Player[]> GetAllPlayers(int minScore, string itemType)
        {
            var countfilter = Builders<Player>.Filter.Empty;
            int playerCount = (int)_collection.Count(countfilter);
            int counter = 0;
            Player[] player = new Player[playerCount];

            if (minScore == 0 && itemType == null)
            {
                var filter = Builders<Player>.Filter.Empty;
                var cursor = await _collection.FindAsync(filter);
                while (await cursor.MoveNextAsync())
                {
                    IEnumerable<Player> batch = cursor.Current;
                    foreach (Player document in batch)
                    {
                        player[counter] = document;
                        counter++;
                    }

                }
            }
            else if (minScore > 0)
            {

                FilterDefinition<Player> filter = Builders<Player>.Filter.Gte("Score", minScore);
                var cursor = await _collection.FindAsync(filter);
                while (await cursor.MoveNextAsync())
                {
                    IEnumerable<Player> batch = cursor.Current;
                    foreach (Player document in batch)
                    {
                        player[counter] = document;
                        counter++;
                    }

                }

            }
            else if (itemType != null)
            {
                FilterDefinition<Player> filter = Builders<Player>.Filter.Eq("Items.Type", itemType);
                var cursor = await _collection.FindAsync(filter);
                while (await cursor.MoveNextAsync())
                {
                    IEnumerable<Player> batch = cursor.Current;
                    foreach (Player document in batch)
                    {
                        player[counter] = document;
                        counter++;
                    }

                }
            }



            return player;
        }

        public async Task<Item> GetItem(Guid playerId, Guid itemId)
        {
            var filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
            var cursor = await _collection.FindAsync(filter);
            var player = cursor.First();

            for (int i = 0; i < player.Items.Length; i++)
            {
                if (player.Items[i] != null)
                {
                    if (player.Items[i].Id == itemId)
                        return player.Items[i];

                }

            }
            return null;
        }

        public async Task<Player> GetPlayer(Guid playerId)
        {
            var filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
            var cursor = await _collection.FindAsync(filter);
            var player = await cursor.FirstAsync();
            return player;
        }
        public async Task<Player> UpdatePlayerNameAndScore(string name, string newName, int score)
        {
            FilterDefinition<Player> filter1;
            if (newName != null)
            {
                var filter = Builders<Player>.Filter.Eq(p => p.Name, name);
                var update = Builders<Player>.Update.Set("Name", newName);
                await _collection.UpdateOneAsync(filter, update);
                filter1 = Builders<Player>.Filter.Eq(p => p.Name, newName);
            }
            else
            {
                var filter = Builders<Player>.Filter.Eq(p => p.Name, name);
                var update = Builders<Player>.Update.Inc("Score", score);
                await _collection.UpdateOneAsync(filter, update);
                filter1 = Builders<Player>.Filter.Eq(p => p.Name, name);
            }

            //testi
            var cursor = await _collection.FindAsync(filter1);
            var player1 = await cursor.FirstAsync();

            return player1;
        }



        public async Task<Player> GetPlayerByName(string name)
        {
            var filter = Builders<Player>.Filter.Eq(p => p.Name, name);
            var cursor = await _collection.FindAsync(filter);
            var player = await cursor.FirstAsync();
            return player;
        }

        public async Task<Item> UpdateItem(Guid playerId, Item item)
        {
            var filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
            var cursor = await _collection.FindAsync(filter);
            var player = cursor.First();


            for (int i = 0; i < player.Items.Length - 1; i++)
            {
                if (player.Items[i] != null)
                {
                    if (player.Items[i].Id == item.Id)
                    {

                        var filter1 = Builders<Player>.Filter.Eq(p => p.Items[i].Id, item.Id);
                        var update = Builders<Player>.Update.Set(x => x.Items[i].Price, item.Price);
                        var result = await _collection.UpdateOneAsync(filter, update);
                        return player.Items[i];
                    }
                }

            }
            return null;
        }

        public async Task<Player> UpdatePlayer(Player player)
        {
            var filter = Builders<Player>.Filter.Eq(p => p.Id, player.Id);
            await _collection.ReplaceOneAsync(filter, player);
            return player;
        }

        public async Task<Player> PushItem(Guid id, Item item)
        {
            var filter = Builders<Player>.Filter.Eq(p => p.Id, id);
            var update = Builders<Player>.Update.Push(x => x.Items, item);
            await _collection.UpdateOneAsync(filter, update);

            var cursor = _collection.Find(filter);
            var player = cursor.First();
            return player;
        }

        public async Task<Item> DeleteAndAddScore(Guid playerId)
        {
            var filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
            var cursor = await _collection.FindAsync(filter);
            var player = await cursor.FirstAsync();

            for (int i = 0; i < player.Items.Length; i++)
            {
                if (player.Items[i] != null)
                {

                    var item = player.Items[i];
                    var update = Builders<Player>.Update.PopFirst(x => x.Items);
                    var result = await _collection.UpdateOneAsync(filter, update);

                    var update2 = Builders<Player>.Update.Inc("Score", 50);
                    var result2 = await _collection.UpdateOneAsync(filter, update2);
                    return item;

                }

            }
            return null;
        }
        public async Task<int> GetCommonLevel()
        {
            var aggregate = _collection.Aggregate().Project(new BsonDocument { { "Level", 1 } })
            .Group(new BsonDocument { { "_id", "$Level" }, { "Count", new BsonDocument("$sum", 1) } })
            .Sort(new BsonDocument { { "Count", -1 } }).Limit(1);
            if (aggregate.Any())
            {
                BsonDocument result = await aggregate.FirstAsync();
                return result["_id"].ToInt32();
            }
            else
                return 0;
        }
        public async Task<Player[]> GetTopTen()
        {

            var filter = Builders<Player>.Filter.Empty;
            var list = await _collection.Find(filter).Sort(Builders<Player>.Sort.Descending("Score")).ToListAsync();
            Player[] player = new Player[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                player[i] = list[i];
                if (i == 9)
                    break;
            }
            return player;


        }
        public async Task<Player[]> GetBySize(int num)
        {
            var filter = Builders<Player>.Filter.Size(p => p.Items, num);
            var cursor = await _collection.FindAsync(filter);
            var list = cursor.ToList();
            Player[] player = new Player[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                player[i] = list[i];
            }
            return player;

        }



    }
}
