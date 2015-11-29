using System;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace LiveTileEditor
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly TileUpdater _tileUpdater;

        public MainPage()
        {
            InitializeComponent();

            _tileUpdater = TileUpdateManager.CreateTileUpdaterForApplication();
            _tileUpdater.Clear();
            _tileUpdater.EnableNotificationQueue(false);
        }

        private async void UpdateButton_OnClick(object sender, RoutedEventArgs e)
        {
            var notificationXml = new XmlDocument();

            try
            {
                notificationXml.LoadXml(TileTemplateBox.Text);
            }
            catch (Exception)
            {
                var message = new MessageDialog("Can't load xml");

                await message.ShowAsync();

                return;
            }

            var notification = new TileNotification(notificationXml);

            _tileUpdater.Update(notification);
        }

        private void ClearButton_OnClick(object sender, RoutedEventArgs e)
        {
            _tileUpdater.Clear();
        }

        private void ToggleQueueButton_OnChecked(object sender, RoutedEventArgs e)
        {
            _tileUpdater.EnableNotificationQueue(true);
        }

        private void ToggleQueueButton_OnUnchecked(object sender, RoutedEventArgs e)
        {
            _tileUpdater.EnableNotificationQueue(false);
        }
    }
}