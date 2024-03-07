using NetworkService.DNS;
using System.Net.NetworkInformation;

namespace NetworkService.Services
{
    public class NetworkServicePing
    {
        private readonly IDNS _dNS;

        public NetworkServicePing(IDNS dNS)
        {
            _dNS = dNS;
        }

        public string PingTest()
        {
            var dnsSucess = _dNS.SendDNS();
            if (dnsSucess)
            {
                return "Success: Ping sent.";
            }
            else
            {
                return "Failed: Ping not sent.";
            }
        }

        public int PingAdd(int a, int b)
        {
            return a + b;
        }

        public DateTime LastPingDate()
        {
            return DateTime.Now;
        }

        public PingOptions GetPingOptions()
        {
            return new PingOptions()
            {
                DontFragment = true,
                Ttl = 1
            };
        }

        public IEnumerable<PingOptions> MostRecentPings()
        {
            IEnumerable<PingOptions> pingOptions = new List<PingOptions>()
            {
                new PingOptions()
                {
                    DontFragment = true,
                    Ttl = 1
                },
                new PingOptions()
                {
                    DontFragment = true,
                    Ttl = 1
                },
                new PingOptions()
                {
                    DontFragment = true,
                    Ttl = 1
                }
            };

            return pingOptions;
        }
    }
}
