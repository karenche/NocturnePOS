using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Foundation.Diagnostics;
using Windows.Storage;

namespace Nocturne.App.Helpers
{
    /// <summary>
    /// Handles app logging. Logging messages are saved to installation_folder/MyLogFile folder in *.etl files.
    /// Installation folder can be opened from the App: in lower left corner is a button for it.
    /// *.etl files can be converted to human readable format in the command line with the following command:
    /// tracerpt.exe log_file_name.etl -of XML -o readable_log.xml
    /// </summary>
    class Logger
    {
        private LoggingChannel _logChannel;
        private LoggingSession _logSession;
        private StorageFolder _logUploadFolder;
        static private Logger _logger;

        private const int DAYS_TO_DELETE = 15;
        public const string LOG_SESSION_RESROUCE_NAME = "LogSession";


        public async void InitiateLogger()
        {
            _logChannel = new LoggingChannel("MySampleChannel", new LoggingChannelOptions(new Guid("{6A59B51C-8E78-474A-A892-27443122DE38}")));
            _logSession = new LoggingSession("MySample Session");

            _logSession.AddLoggingChannel(_logChannel);

            await RegisterUnhandledErrorHandler();
        }


        /// <summary> 
        /// Maintains singleton object  
        /// </summary> 
        /// <returns></returns> 
        static public Logger GetLogger()
        {
            if (_logger == null)
            {
                _logger = new Logger();
            }
            return _logger;
        }

        public void LogMessage(string message)
        {
            _logChannel.LogMessage(message);
            //var filename = DateTime.Now.ToString("yyyyMMdd-HHmmssTzz") + ".etl"; 
            var filename = DateTime.Now.ToString("yyyyMMdd") + ".etl";
            var logSaveTast = _logSession
                .SaveToFileAsync(_logUploadFolder, filename)
                .AsTask();

            logSaveTast.Wait();
        }


        private async Task RegisterUnhandledErrorHandler()
        {
            _logUploadFolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("MyLogFile",
                CreationCollisionOption.OpenIfExists);

            CoreApplication.UnhandledErrorDetected += CoreApplication_UnhandledErrorDetected;

        }

        /// <summary> 
        /// Any  uncaught exceptions are thrown to here 
        /// </summary> 
        /// <param name="sender"></param> 
        /// <param name="e"></param> 
        private void CoreApplication_UnhandledErrorDetected(object sender, UnhandledErrorDetectedEventArgs e)
        {
            try
            {
                _logChannel.LogMessage("Caught the exception");
                e.UnhandledError.Propagate();
            }
            catch (Exception ex)
            {
                //logChannel.LogMessage(string.Format("UnhandledErro: 0x{0:X})", ex.HResult), LoggingLevel.Critical); 
                _logChannel.LogMessage(string.Format("Effor Message: {0}", ex.Message));

                if (_logSession != null)
                {
                    //var filename = DateTime.Now.ToString("yyyyMMdd-HHmmssTzz") + ".etl"; 
                    var filename = DateTime.Now.ToString("yyyyMMdd") + ".etl";
                    var logSaveTast = _logSession
                        .SaveToFileAsync(_logUploadFolder, filename)
                        .AsTask();

                    logSaveTast.Wait();
                }


                // throw; 
            }
        }

        /// <summary> 
        /// Deelete the files based on the days mentioned 
        /// </summary> 

        public async void Deletefile()
        {
            try
            {

                var logFiles = await _logUploadFolder.GetFilesAsync();

                foreach (var logFile in logFiles)
                {
                    if ((DateTime.Now - logFile.DateCreated).Days > DAYS_TO_DELETE)
                    {
                        await logFile.DeleteAsync();
                    }


                }
            }
            catch (Exception ex)
            {
                _logChannel.LogMessage(ex.Message);

            }
        }

    }
}
