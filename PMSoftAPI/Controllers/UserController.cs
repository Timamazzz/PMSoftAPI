using AutoMapper;
using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using PMSoftAPI.Models;

namespace PMSoftAPI.Controllers;

[ApiController]
[Route("users")]
public class UserController(UserService userService, IMapper mapper) : ControllerBase
{
    private readonly UserService _userService = userService;
    private readonly IMapper _mapper = mapper;

    [HttpPost]
    public async Task<ActionResult<UserGet>> CreateUser(UserCreate user)
    {
        // Преобразование объекта UserCreate в объект Domain.Models.User
        var userDomain = _mapper.Map<User>(user);
        
        try
        {
            // Создание пользователя с использованием сервиса
            var userCreated = await _userService.CreateAsync(userDomain);
            
            // Преобразование созданного пользователя в объект UserGet
            var userGet = _mapper.Map<UserGet>(userCreated);
            
            // Возвращаем CreatedAtAction с созданным пользователем
            return CreatedAtAction(nameof(GetUser), new { id = userGet.Id }, userGet);
        }
        catch (Exception ex)
        {
            // Обработка ошибки создания пользователя
            return BadRequest($"Failed to create user: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserGet>> GetUser(int id)
    {
        try
        {
            // Получение пользователя по идентификатору с использованием сервиса
            var user = await _userService.GetByIdAsync(id);
            
            // Если пользователь не найден, возвращаем NotFound
            if (user == null)
            {
                return NotFound("User not found.");
            }
            
            // Преобразование пользователя в объект UserGet и возврат
            var userGet = _mapper.Map<UserGet>(user);
            return userGet;
        }
        catch (Exception ex)
        {
            // Обработка ошибки получения пользователя
            return BadRequest($"Failed to get user: {ex.Message}");
        }
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserGet>>> GetAllUsers()
    {
        try
        {
            // Получение всех пользователей с использованием сервиса
            var users = await _userService.GetAllAsync();

            // Преобразование списка пользователей в список объектов UserGet и возврат
            var usersGet = _mapper.Map<IEnumerable<UserGet>>(users);
            return Ok(usersGet);
        }
        catch (Exception ex)
        {
            // Обработка ошибки получения всех пользователей
            return BadRequest($"Failed to get all users: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        try
        {
            // Удаление пользователя по идентификатору с использованием сервиса
            await _userService.DeleteAsync(id);
            
            // Возврат NoContent в случае успешного удаления
            return NoContent();
        }
        catch (Exception ex)
        {
            // Обработка ошибки удаления пользователя
            return BadRequest($"Failed to delete user: {ex.Message}");
        }
    }
}
