using FakeItEasy;
using FluentAssertions;
using FluentAssertions.Extensions;
using NetworkService.DNS;
using NetworkService.Services;
using System.Net.NetworkInformation;

namespace NetworkService.Tests.ServicesTests
{
    public class NetworkServicePingTests
    {
        private readonly NetworkServicePing _ping;
        private readonly IDNS _dns;

        public NetworkServicePingTests()
        {
            // Dependencies
            _dns = A.Fake<IDNS>();

            // SUT
            _ping = new NetworkServicePing(_dns);
        }

        [Fact]
        public void NetworkServicePing_PingTest_ReturnString()
        {
            A.CallTo(() => _dns.SendDNS()).Returns(true);
            var result = _ping.PingTest();

            result.Should().Be("Success: Ping sent.");
            result.Should().Contain("Success", Exactly.Once());
        }

        [Theory]
        [InlineData(1, 2, 3)]
        [InlineData(2, 3, 5)]
        public void NetworkServicePing_PingAdd_ReturnInt(int a, int b, int c)
        {
            var result = _ping.PingAdd(a, b);

            result.Should().Be(c);
            result.Should().BeGreaterThanOrEqualTo(0);
        }

        [Fact]
        public void NetworkServicePing_LastPingDate_ReturnDate()
        {
            var result = _ping.LastPingDate();

            result.Should().BeAfter(1.January(2010));
            result.Should().BeBefore(1.January(2025));
        }

        [Fact]
        public void NetworkServicePing_GetPingOptions_RetursObject()
        {
            // Arrange
            var expectedResult = new PingOptions()
            {
                DontFragment = true,
                Ttl = 1
            };

            // Act
            var result = _ping.GetPingOptions();

            // Assert
            result.Should().BeOfType<PingOptions>();
            result.Should().BeEquivalentTo(expectedResult);
            result.Ttl.Should().Be(1);
        }

        [Fact]
        public void NetworkServicePing_MostRecentPings_RetursObject()
        {
            // Arrange
            var expectedResult = new PingOptions()
            {
                DontFragment = true,
                Ttl = 1
            };

            // Act
            var result = _ping.MostRecentPings();

            // Assert
            // result.Should().BeOfType<IEnumerable<PingOptions>>();
            result.Should().ContainEquivalentOf(expectedResult);
            result.Should().Contain(x => x.DontFragment == true);
        }
    }
}
