using System;
using System.Threading.Tasks;
using gameapi.Models;
using gameapi.Processors;
using Microsoft.AspNetCore.Mvc;

namespace gameapi.Controllers
{
    //Guid playerId will be available on all routes
    [Route("api/players/{playerId}/items")]
    public class ItemsController    {
        private ItemsProcessor _processor;
        public ItemsController(ItemsProcessor processor)        {
            _processor = processor;
        }

        [HttpGet]
        public Task<Item[]> GetAll(Guid playerId)        {
            return _processor.GetAll(playerId);
        }

        [HttpGet("{id}")]
        public Task<Item> Get(Guid playerId, Guid id)        {
            return _processor.Get(playerId, id);
        }

        [HttpPost]
        public Task<Item> Create(Guid playerId, /*[FromBody]*/NewItem item)        {
            return _processor.Create(playerId, item);
        }

        // Tehtävä 9
        [HttpDelete]
        public Task<Item> DeleteAndAddScore(Guid playerId)        {
            return _processor.DeleteAndAddScore(playerId);
        }

        [HttpDelete("{id}")]
        public Task<Item> Delete(Guid playerId, Guid id)        {
            return _processor.Delete(playerId, id);
        }

        [HttpPut("{id}")]
        public Task<Item> Update(Guid playerId, Guid id, /*[FromBody]*/ModifiedItem item)        {
            return _processor.Update(playerId, id, item);
        }
    }
}