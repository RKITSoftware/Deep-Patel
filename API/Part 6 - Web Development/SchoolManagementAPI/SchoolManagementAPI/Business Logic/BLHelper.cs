using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagementAPI.Business_Logic
{
    public class BLHelper
    {
        private Timer timer;

        public void StartUpdate()
        {
            int initialDelay = 0;
            int interval = 30 * 1000;

            timer = new Timer(UpdateFile, null, initialDelay, interval);
        }

        private async void UpdateFile(object state)
        {
            await UpdateFileAsync();
        }

        private async Task UpdateFileAsync()
        {
             BLUser.UpdateFileData();
        }
    }
}