using Aip.Instance.Backend.Api.Common.Services;

using FakeItEasy;

using FluentAssertions;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


namespace Aip.Instance.Backend.Tests.Unit;

public class BytesToStringTests {
  public static TheoryData<long, string> Cases = new() {
    { 0L, "0 Б" },
    { 1000L, "1000 Б" },
    { 1023L, "1023 Б" },
    { 1024L, "1 КБ" },
    { 10000L, "9.8 КБ" },
    { 10240L, "10 КБ" },
    { 10490000L, "10 МБ" },
    { 1074000000L, "1 ГБ" },
    { 10740000000L, "10 ГБ" },
  };

  [Theory]
  [MemberData(nameof(Cases))]
  public void WhenBytes_ReturnStringRepr(long bytes, string expected) {
    // Arrange
    var env = A.Fake<IHostEnvironment>();
    var logger = A.Fake<ILogger<StaticFileService>>();
    var service = new StaticFileService(env, logger);

    // Act
    var result = service.BytesToString(bytes);

    // Assert
    result.Should().Be(expected);
  }
}