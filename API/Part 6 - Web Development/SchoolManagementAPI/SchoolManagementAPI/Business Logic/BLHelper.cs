using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagementAPI.Business_Logic
{
    /// <summary>
    /// Helper class for managing periodic updates.
    /// </summary>
    public class BLHelper
    {
        private Timer timer;

        /// <summary>
        /// Starts the periodic update process.
        /// </summary>
        public void StartUpdate()
        {
            int initialDelay = 0;
            int interval = 30 * 1000;

            // Set up a timer to trigger the UpdateFile method at regular intervals.
            timer = new Timer(UpdateFile, null, initialDelay, interval);
        }

        /// <summary>
        /// Callback method for the timer to trigger an asynchronous file update.
        /// </summary>
        /// <param name="state">State object (not used in this case).</param>
        private async void UpdateFile(object state)
        {
            // Trigger the asynchronous file update.
            await UpdateFileAsync();
        }

        /// <summary>
        /// Asynchronously updates the file data.
        /// </summary>
        private async Task UpdateFileAsync()
        {
            // Invoke the static method to update file data.
            BLUser.UpdateFileData();
        }
    }
}