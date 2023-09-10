using System.Collections.Generic;
using l12_nosql.Models;

namespace l12_nosql.Repositories;

public interface ICoffeeRepository
{
    Task<IEnumerable<Coffee>> GetAllCoffee();
    Task<Coffee?> GetCoffeeById(string coffeeId);
    Task<Coffee> CreateCoffee(Coffee newCoffee);
    Task<Coffee?> UpdateCoffee(Coffee newCoffee);
    Task DeleteCoffeeById(string coffeeId);

}