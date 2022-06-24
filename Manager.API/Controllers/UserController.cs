using AutoMapper;
using Manager.API.Utilities;
using Manager.API.ViewModels;
using Manager.Core.Exceptions;
using Manager.Services.DTO;
using Manager.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Manager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController: ControllerBase
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
        }

        [HttpPost] 
        [Authorize]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] CreateUserViewModel createUser)
        {
            try
            {
                var userDTO = mapper.Map<UserDTO>(createUser);
                var userCreated = await userService.Create(userDTO);
                return Ok(new ResultViewModel{Message="Usuário criado com sucesso",
                                             Success=true,
                                             Data=userCreated});
            }
            catch (DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }

        [HttpPut] 
        [Authorize]
        [Route("update")]
        public async Task<IActionResult> update([FromBody] UpdateUserViewModel updateUser)
        {
            try
            {
                var userDTO = mapper.Map<UserDTO>(updateUser);
                var userUpdated = await userService.Update(userDTO);
                return Ok(new ResultViewModel{Message="Usuário atualizado com sucesso",
                                             Success=true,
                                             Data=userUpdated});
            }
            catch (DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }

        [HttpDelete] 
        [Authorize]
        [Route("remove/{id}")]
        public async Task<IActionResult> Remove(long id)
        {
            try
            {
                
                await userService.Remove(id);
                return Ok(new ResultViewModel{Message="Usuário removido com sucesso",
                                             Success=true,
                                             Data=null});
            }
            catch (DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Responses.DomainErrorMessage(ex.Message));
            }
        }

        [HttpGet] 
        [Authorize]
        [Route("get/{id}")]
        public async Task<IActionResult> Get(long id)
        {
            try
            {
                
                var user = await userService.Get(id);
                if(user == null)
                    return NotFound(new ResultViewModel{Message="Usuário não encontrado",
                                             Success=false,
                                             Data=null});
                return Ok(new ResultViewModel{Message="Usuário encontrado com sucesso",
                                             Success=true,
                                             Data=user});
            }
            catch (DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }

        [HttpGet] 
        [Authorize]
        [Route("get-all")]
        public async Task<IActionResult> Get()
        {
            try
            {
                
                var allUsers = await userService.Get();

                return Ok(new ResultViewModel{Message="Usuários listados com sucesso",
                                             Success=true,
                                             Data=allUsers});
            }
            catch (DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }

        [HttpGet]
        [Authorize] 
        [Route("get-by-email")]
        public async Task<IActionResult> GetByEmail([FromQuery] string email)
        {
            try
            {
                
                var user = await userService.GetByEmail(email);

                if(user == null) 
                    return NotFound(new ResultViewModel
                                        {Message="Nenhum usuário encontrado pelo e-mail informado",
                                             Success=false,
                                             Data=null
                                        });

                return Ok(new ResultViewModel{Message="Usuario foi encontrado com sucesso",
                                             Success=true,
                                             Data=user});
            }
            catch (DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }

        [HttpGet] 
        [Authorize]
        [Route("search-by-name")]
        public async Task<IActionResult> SearchByName([FromQuery] string name)
        {
            try
            {
                
                var allUser = await userService.SearchByName(name);

                if(allUser.Count == 0) 
                    return NotFound(new ResultViewModel
                                        {Message="Nenhum usuário encontrado com o nome informado",
                                             Success=false,
                                             Data=null
                                        });

                return Ok(new ResultViewModel{Message="Usuario foi encontrado com sucesso",
                                             Success=true,
                                             Data=allUser});
            }
            catch (DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }

        [HttpGet] 
        [Authorize]
        [Route("search-by-email")]
        public async Task<IActionResult> SearchByEMail([FromQuery] string email)
        {
            try
            {
                
                var allUser = await userService.SearchByEmail(email);

                if(allUser.Count == 0) 
                    return NotFound(new ResultViewModel
                                        {Message="Nenhum usuário encontrado com o email informado",
                                             Success=false,
                                             Data=null
                                        });

                return Ok(new ResultViewModel{Message="Usuario foi encontrado com sucesso",
                                             Success=true,
                                             Data=allUser});
            }
            catch (DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }

    }
}