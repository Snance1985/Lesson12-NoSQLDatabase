using l12_nosql.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace l12_nosql.Repositories;

public class NoSqlCoffeeRepository : ICoffeeRepository
{
    private readonly IMongoCollection<Coffee> _coffee;

    public NoSqlCoffeeRepository(IOptions<CoffeeDatabaseSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        var database = client.GetDatabase(settings.Value.DatabaseName);

        _coffee = database.GetCollection<Coffee>(settings.Value.CoffeeCollectionName);
    }

    public async Task<IEnumerable<Coffee>> GetAllCoffee()
{
    var allCoffee = await _coffee.FindAsync(coffee => true);
    return allCoffee.ToList();
}

public async Task<Coffee?> GetCoffeeById(string coffeeId)
{
    var coffeeFound = await _coffee.FindAsync<Coffee>(coffee => coffee.Id == coffeeId);
    return coffeeFound.FirstOrDefault();
}

public async Task<Coffee> CreateCoffee(Coffee newCoffee)
{
    await _coffee.InsertOneAsync(newCoffee);
    return newCoffee;
}

public async Task<Coffee?> UpdateCoffee(Coffee newCoffee)
{
    await _coffee.ReplaceOneAsync(coffee => coffee.Id == newCoffee.Id, newCoffee);
    return newCoffee;
}

public async Task DeleteCoffeeById(string coffeeId)
{
    await _coffee.DeleteOneAsync(coffee => coffee.Id == coffeeId);
}

}
