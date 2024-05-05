using APBD6Kox.Models;
using APBD6Kox.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD6Kox.Controllers;
[ApiController]
[Route("api/animals")]
public class AnimalController : ControllerBase
{
    private readonly IAnimalService _animalService;

    public AnimalController(IAnimalService animalService)
    {
        _animalService = animalService;
    }
    [HttpGet]
    public IActionResult GetAnimals(string orderBy = "name")
    {
        IEnumerable<Animal> animals = _animalService.GetAnimals(orderBy);
        return Ok(animals);
    }
    [HttpPost]
    public IActionResult AddAnimal(Animal animal)
    {
        int effectedCount = _animalService.AddAnimal(animal);
        return StatusCode(StatusCodes.Status201Created);
    }
    [HttpPut]
    public IActionResult UpdateAnimal(Animal animal)
    {
        int effectedCount = _animalService.UpdateAnimal(animal);
        return NoContent();
    }
    
    
    [HttpDelete]
    public IActionResult DeleteAnimal(int id)
    {
        int effectedCount = _animalService.DeleteAnimal(id);
        return NoContent();
    }
    
    
}