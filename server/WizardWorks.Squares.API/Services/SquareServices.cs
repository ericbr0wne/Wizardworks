using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WizardWorks.Squares.API.Repositories;
using static WizardWorks.Squares.API.Models.SquareModel;

namespace WizardWorks.Squares.API.Services
{
public class SquareService : ISquareService
{
    private readonly ISquareRepository _repository;
    private readonly Random _random = new();

    public SquareService(ISquareRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Square>> GetSquaresAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Square> AddSquareAsync()
    {
        var squares = (await _repository.GetAllAsync()).ToList();
        var newSquare = GenerateNewSquare(squares);
        
        squares.Add(newSquare);
        await _repository.SaveAsync(squares);
        
        return newSquare;
    }

    private Square GenerateNewSquare(IList<Square> existingSquares)
    {
        var sideLength = (int)Math.Ceiling(Math.Sqrt(existingSquares.Count + 1));
        var position = CalculateNextPosition(existingSquares.Count, sideLength);
        var color = GenerateRandomColor(existingSquares.LastOrDefault()?.Color);

        return new Square
        {
            Id = existingSquares.Count + 1,
            Position = position,
            Color = color
        };
    }

    private Position CalculateNextPosition(int currentCount, int sideLength)
    {
        return new Position
        {
            X = currentCount % sideLength,
            Y = currentCount / sideLength
        };
    }

    private string GenerateRandomColor(string? previousColor)
    {
        string[] colors = { "#FF0000", "#00FF00", "#0000FF", "#FFFF00", "#FF00FF", "#00FFFF" };
        string newColor;
        
        do
        {
            newColor = colors[_random.Next(colors.Length)];
        } while (newColor == previousColor);

        return newColor;
    }
}
}