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
        private readonly string[] _colors = { "#FF0000", "#00FF00", "#0000FF", "#FFFF00", "#FF00FF", "#00FFFF" };

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
            var position = FindNextAvailablePosition(existingSquares, sideLength);
            var color = GenerateRandomColor(existingSquares);

            return new Square
            {
                Id = existingSquares.Count + 1,
                Position = position,
                Color = color
            };
        }

        private Position FindNextAvailablePosition(IList<Square> existingSquares, int sideLength)
        {
            for (int y = 0; y < sideLength; y++)
            {
                for (int x = 0; x < sideLength; x++)
                {
                    if (!existingSquares.Any(s => s.Position.X == x && s.Position.Y == y))
                    {
                        return new Position { X = x, Y = y };
                    }
                }
            }

            return new Position
            {
                X = 0,
                Y = sideLength
            };
        }

        private string GenerateRandomColor(IList<Square> existingSquares)
        {
            var recentColors = existingSquares
                .OrderByDescending(s => s.Id)
                .Take(_colors.Length - 1)
                .Select(s => s.Color)
                .ToList();

            var availableColors = _colors.Except(recentColors).ToList();

            if (!availableColors.Any())
            {
                availableColors = _colors.ToList();
            }

            return availableColors[_random.Next(availableColors.Count)];
        }
    }
}