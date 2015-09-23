using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace RateAndShare
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

       
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }


        //Rate App Button
        private async void RateBtn_Click(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync
                (new Uri("ms-windows-store:reviewapp?appid=" + Windows.ApplicationModel.Store.CurrentApp.AppId));

        }

        //Share Method
        private void RegisterForShare()
        {
            DataTransferManager dataTransferManager = DataTransferManager.GetForCurrentView();
            dataTransferManager.DataRequested += new TypedEventHandler<DataTransferManager,
                DataRequestedEventArgs>(this.ShareLinkHandler);
        }

        private void ShareLinkHandler(DataTransferManager sender, DataRequestedEventArgs e)
        {
            DataRequest request = e.Request;
            request.Data.Properties.Title = "Title";
            request.Data.Properties.Description = "Description";
            request.Data.SetWebLink(new Uri(""+ Windows.ApplicationModel.Store.CurrentApp.LinkUri));
        }

        //Share App Link Button
        private void shareBtn_Click(object sender, RoutedEventArgs e)
        {
            RegisterForShare();
            DataTransferManager.ShowShareUI();
        }
        

        //Get more apps Button
        private async void moreAppsBtn_Click(object sender, RoutedEventArgs e)
        {
            string keyword = "Mohammed Emam";
            var uri = new Uri(string.Format(@"ms-windows-store:search?keyword={0}", keyword));
            await Windows.System.Launcher.LaunchUriAsync(uri);
        }

        //App Details on the Store Button
        private async void detailsBtn_Click(object sender, RoutedEventArgs e)
        {
            var uri = new Uri(string.Format("ms-windows-store:navigate?appid={0}", Windows.ApplicationModel.Store.CurrentApp.LinkUri));
            await Windows.System.Launcher.LaunchUriAsync(uri);
        }



    }
}
