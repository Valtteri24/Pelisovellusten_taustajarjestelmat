using System;
using System.Threading.Tasks;
using gameapi.Exceptions;
using gameapi.Models;
using gameapi.ModelValidation;
using gameapi.Processors;
using Microsoft.AspNetCore.Mvc;

namespace gameapi.Controllers{

    [Route("game/players")]
    public class PlayersController : Controller    {
        private PlayersProcessor _processor;
        public PlayersController(PlayersProcessor processor)        {
            _processor = processor;
        }

        
        [HttpGet("{name}")]
        public Task<Player> Get(string name)        {
            return _processor.GetByName(name);
        }
        [HttpPost]
        [ValidateModel]
        public Task<Player> Create(NewPlayer player)        {
            return _processor.Create(player);
        }


        [HttpGet]
        public Task<Player[]> GetAll(int minScore, string itemType)        {
            return _processor.GetAll(minScore, itemType);
        }


        [HttpGet("{num:int}")]
        public Task<Player[]> GetBySize(int num)        {
            return _processor.GetBySize(num);
        }
        [HttpGet("{id:guid}")]
        public Task<Player> Get(Guid id)        {
            return _processor.Get(id);
        }


        [HttpPut("{name:minlength(1)}")]
        public Task<Player> UpdatePlayerNameAndScore(string name, string newName, int score)        {
            return _processor.UpdatePlayerNameAndScore(name, newName, score);
        }


        [HttpPost("{id}")]
        public Task<Player> PushItem(Guid id, string type, int level)        {
            return _processor.PushItem(id, type, level);
        }


        [HttpGet("{asd:bool}")]
        public Task<Player[]> GetTopTen()        {
            return _processor.GetTopTen();
        }

        [HttpDelete("{id}")]
        public Task<Player> Delete(Guid id)        {
            return _processor.Delete(id);
        }

        [HttpPut("{id:guid}")]
        public Task<Player> Update(Guid id, ModifiedPlayer player)        {
            return _processor.Update(id, player);
        }
    }
}