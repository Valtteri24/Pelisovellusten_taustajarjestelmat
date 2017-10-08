using System;
using System.Threading.Tasks;
using gameapi.Models;
using gameapi.Repositories;

namespace gameapi.Processors
{
    // Business logic (logic for verifying and updating the data) happens here
    public class ItemsProcessor
    {
        private IRepository _repository;

        public ItemsProcessor(IRepository repository)
        {
            _repository = repository;
        }

        public Task<Item[]> GetAll(Guid playerId)
        {
            return _repository.GetAllItems(playerId);
        }

        public Task<Item> Get(Guid playerId, Guid id)
        {
            return _repository.GetItem(playerId, id);
        }

        public Task<Item> Create(Guid playerId, NewItem newItem)
        {
            var item = new Item()
            {
                Id = Guid.NewGuid(),
                Price = 10,
                Level = newItem.Level,
                Type = newItem.Type,
            };
            return _repository.CreateItem(playerId, item);
        }

        public async Task<Item> Delete(Guid playerId, Guid id)
        {
            Item item = await _repository.GetItem(playerId, id);
            await _repository.DeleteItem(playerId, item);
            return item;
        }
        public async Task<Item> DeleteAndAddScore(Guid playerId)
        {
            return await _repository.DeleteAndAddScore(playerId);
        }

        public async Task<Item> Update(Guid playerId, Guid id, ModifiedItem modifiedItem)
        {
            Item item = await _repository.GetItem(playerId, id);
            item.Price = modifiedItem.Price;
            await _repository.UpdateItem(playerId, item);
            return item;
        }
    }
}