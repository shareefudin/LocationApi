using Location.API.Repositories;
using Location.API.Services;
using Moq;

namespace LocationApi.Tests.Fixtures;

public class PharmacyServiceFixture : IDisposable
{
    public Serilog.ILogger LoggerMock { get; }
    public Mock<IPharmacyRepository> RepositoryMock { get; }

    public PharmacyService PharmacyService { get; }

    public PharmacyServiceFixture()
    {
        LoggerMock = new Mock<Serilog.ILogger>().Object;
        RepositoryMock = new Mock<IPharmacyRepository>();

        PharmacyService = new PharmacyService(LoggerMock, RepositoryMock.Object);
    }

    public void Dispose()
    {
        
    }
}
