// WizardWorks.Squares.Tests/Services/SquareServiceTests.cs
using Moq;
using Xunit;
using WizardWorks.Squares.API.Services;
using WizardWorks.Squares.API.Repositories;
using WizardWorks.Squares.API.Models;
using static WizardWorks.Squares.API.Models.SquareModel;

public class SquareServiceTests
{
    private readonly Mock<ISquareRepository> _mockRepository;
    private readonly ISquareService _squareService;

    public SquareServiceTests()
    {
        _mockRepository = new Mock<ISquareRepository>();
        _squareService = new SquareService(_mockRepository.Object);
    }

    [Fact]
    public async Task AddSquare_FirstSquare_ShouldBeAtOrigin()
    {
        // Arrange
        _mockRepository.Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(new List<Square>());

        // Act
        var result = await _squareService.AddSquareAsync();

        // Assert
        Assert.Equal(0, result.Position.X);
        Assert.Equal(0, result.Position.Y);
        Assert.NotNull(result.Color);
    }

    [Fact]
    public async Task AddSquare_SecondSquare_ShouldBeNextToFirst()
    {
        // Arrange
        var existingSquare = new Square
        {
            Id = 1,
            Position = new Position { X = 0, Y = 0 },
            Color = "#FF0000"
        };

        _mockRepository.Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(new List<Square> { existingSquare });

        // Act
        var result = await _squareService.AddSquareAsync();

        // Assert
        Assert.Equal(1, result.Position.X);
        Assert.Equal(0, result.Position.Y);
        Assert.NotEqual(existingSquare.Color, result.Color);
    }

    [Fact]
    public async Task AddSquare_Colors_ShouldNeverBeTheSameAsLastSquare()
    {
        // Arrange
        var existingSquares = new List<Square>
        {
            new Square
            {
                Id = 1,
                Position = new Position { X = 0, Y = 0 },
                Color = "#FF0000"
            }
        };

        _mockRepository.Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(existingSquares);

        // Act
        var newSquare = await _squareService.AddSquareAsync();

        // Assert
        Assert.NotEqual(existingSquares.Last().Color, newSquare.Color);
    }

    [Fact]
    public async Task AddSquare_FourSquares_ShouldFormTwoByTwoGrid()
    {
        // Arrange
        var existingSquares = new List<Square>
        {
            new Square { Id = 1, Position = new Position { X = 0, Y = 0 }, Color = "#FF0000" },
            new Square { Id = 2, Position = new Position { X = 1, Y = 0 }, Color = "#00FF00" },
            new Square { Id = 3, Position = new Position { X = 0, Y = 1 }, Color = "#0000FF" }
        };

        _mockRepository.Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(existingSquares);

        // Act
        var result = await _squareService.AddSquareAsync();

        // Assert
        Assert.Equal(1, result.Position.X);
        Assert.Equal(1, result.Position.Y);
    }
}