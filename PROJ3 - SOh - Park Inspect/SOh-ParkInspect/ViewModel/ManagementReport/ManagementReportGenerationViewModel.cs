using System.IO;
using GalaSoft.MvvmLight;

namespace SOh_ParkInspect.ViewModel.ManagementReport
{
    public class ManagementReportGenerationViewModel : ViewModelBase
    {
        private string currentUri;

        //public string GetReportFile {
        //    get
        //    {
        //        var assembly = Assembly.GetExecutingAssembly();
        //        var resourceName = "SOh-ParkInspect.Control.ManagementReport.WebFiles.index.html";

        //        using (Stream stream = assembly.GetManifestResourceStream(resourceName))
        //        using (StreamReader reader = new StreamReader(stream))
        //        {
        //            string result = reader.ReadToEnd();
        //            return result;
        //        }
        //    }
        //}

        public string WebBrowserUri
        {
            get { return currentUri; }
            set
            {
                currentUri = value;
                RaisePropertyChanged("WebBrowserUri");
            }
        }

        /// <summary>
        ///     Verwijs hier naar de juiste HTML file voor dat report.
        /// </summary>
        public ManagementReportGenerationViewModel()
        {
            var curDir = Directory.GetCurrentDirectory();
            WebBrowserUri = string.Format("file:///{0}/WebFiles/index.html", curDir);
        }
    }
}