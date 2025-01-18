using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using static WizardWorks.Squares.API.Models.SquareModel;

namespace WizardWorks.Squares.API.Repositories
{

    public class JsonSquareRepository : ISquareRepository
    {
        private readonly string _filePath;
        private static readonly SemaphoreSlim _semaphore = new(1, 1);

        public JsonSquareRepository(IConfiguration configuration)
        {
            _filePath = configuration["Storage:JsonFilePath"]
                ?? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "squares.json");
        }

        public async Task<IEnumerable<Square>> GetAllAsync()
        {
            await _semaphore.WaitAsync();
            try
            {
                if (!File.Exists(_filePath))
                    return Enumerable.Empty<Square>();

                var json = await File.ReadAllTextAsync(_filePath);
                return JsonSerializer.Deserialize<IEnumerable<Square>>(json)
                       ?? Enumerable.Empty<Square>();
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task SaveAsync(IEnumerable<Square> squares)
        {
            await _semaphore.WaitAsync();
            try
            {
                var json = JsonSerializer.Serialize(squares, new JsonSerializerOptions
                {
                    WriteIndented = true
                });
                await File.WriteAllTextAsync(_filePath, json);
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}