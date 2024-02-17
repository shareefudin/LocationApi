using Location.API.Common;
using Location.API.Entities;
using Location.API.Services;
using LocationApi.Tests.Fixtures;
using Moq;
using Xunit;

namespace LocationApi.Tests.Tests;

public class PharmacyServiceTests : IClassFixture<PharmacyServiceFixture>
{
    private readonly PharmacyServiceFixture _fixture;

    public PharmacyServiceTests(PharmacyServiceFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task AddPharmacyAsync_ReturnsSuccessResult()
    {
        // Arrange
        var pharmacy = new Pharmacy { Id = 1, Name = "Test Pharmacy" };
        var expectedResult = new OperationResult<Pharmacy> { Payload = pharmacy };

        _fixture.RepositoryMock.Setup(repo => repo.AddPharmacyAsync(pharmacy))
            .ReturnsAsync(pharmacy);

        var service = new PharmacyService(_fixture.LoggerMock, _fixture.RepositoryMock.Object);

        // Act
        var result = await service.AddPharmacyAsync(pharmacy);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedResult.Payload, result.Payload);
        Assert.Equal(expectedResult.Payload.Id, result.Payload.Id);
        Assert.Equal(expectedResult.Payload.Name, result.Payload.Name);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public async Task GetPharmaciesAsync_ReturnsSuccessResult()
    {
        // Arrange
        var pharmacies = new List<Pharmacy> { new Pharmacy { Id = 1, Name = "Pharmacy 1" } };
        var expectedResult = new OperationResult<List<Pharmacy>> { Payload = pharmacies };

        _fixture.RepositoryMock.Setup(repo => repo.GetPharmaciesAsync())
            .ReturnsAsync(pharmacies);
        var service = new PharmacyService(_fixture.LoggerMock, _fixture.RepositoryMock.Object);

        // Act
        var result = await service.GetPharmaciesAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedResult.Payload, result.Payload);
        Assert.Equal(expectedResult.Payload.Count(), result.Payload.Count());
        Assert.Empty(result.Errors);
    }
}