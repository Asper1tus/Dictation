namespace Dictation.ViewModels.HelpPages
{
    using Dictation.Helpers;
    using Microsoft.Toolkit.Uwp.Extensions;
    using Windows.ApplicationModel;

    public class AboutViewModel : Observable
    {
        private string versionDescription;

        public AboutViewModel()
        {
            VersionDescription = GetVersionDescription();
        }

        public string VersionDescription
        {
            get => versionDescription;

            set => Set(ref versionDescription, value);
        }

        private string GetVersionDescription()
        {
            var appName = "AppDisplayName".GetLocalized();
            var package = Package.Current;
            var packageId = package.Id;
            var version = packageId.Version;

            return $"{appName} - {version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
        }
    }
}
