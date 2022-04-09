using System.Collections.Generic;
using AutoMapper;
using CommandService.Data;
using CommandService.Dtos;
using CommandService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommandService.Controllers
{
    [Route("api/c/platforms/{platformId}/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandRepo repository;
        private readonly IMapper mapper;

        public CommandsController(ICommandRepo repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetCommandsForPlatform(int platformId)
        {
            System.Console.WriteLine($"--> Hit GetCommandsForPlatform: {platformId}");

            if(!this.repository.PlatformExists(platformId))
            {
                return NotFound();
            }

            var commands = this.repository.GetCommandsForPlatform(platformId);

            return Ok(this.mapper.Map<IEnumerable<CommandReadDto>>(commands));
        }

        [HttpGet("{commandId}", Name = "GetCommandForPlatform")]
        public ActionResult<CommandReadDto> GetCommandForPlatform(int platformId, int commandId)
        {
            System.Console.WriteLine($"--> Hit GetCommandForPlatform: {platformId} / {commandId}");

            if(!this.repository.PlatformExists(platformId))
            {
                return NotFound();
            }

            var command = this.repository.GetCommand(platformId, commandId);
            
            if(command == null)
            {
                return NotFound();
            }

            return Ok(this.mapper.Map<CommandReadDto>(command));
        }

        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommandForPlatform(int platformId, CommandCreateDto commandDto)
        {
            System.Console.WriteLine($"--> Hit CreateCommandForPlatform: {platformId}");

            if(!this.repository.PlatformExists(platformId))
            {
                return NotFound();
            }

            var command = this.mapper.Map<Command>(commandDto);

            this.repository.CreateCommand(platformId, command);
            this.repository.SaveChanges();

            var commandReadDto = this.mapper.Map<CommandReadDto>(command);

            return CreatedAtRoute(nameof(GetCommandForPlatform),
                new {platformId = platformId, commandId = commandReadDto.Id}, commandReadDto);
        }
    }
}