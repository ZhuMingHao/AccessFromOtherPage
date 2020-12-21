using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AccessFromOtherPage
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MessagePage : Page
    {
        public MessagePageViewModel MessageViewModel { get; set; }

        public MessagePage()
        {
            MessageViewModel = new MessagePageViewModel();

            this.InitializeComponent();
            this.Loaded += MessagePage_Loaded;

            // Always use the cached page
            // this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        private void MessagePage_Loaded(object sender, RoutedEventArgs e)
        {
            var list = new List<string>() { "ms-appx:///Assets/img.png",
                "ms-appx:///Assets/img.png" ,
                "ms-appx:///Assets/img.png" ,
                "ms-appx:///Assets/img.png" };

            var items = new Dictionary<string, List<string>>() { { "Hello", list }, { "Some", list }, { "To", list } };   
            var groups = from c in items
                         group c by c.Key;

            this.cvs.Source = groups;
            this.cvs.IsSourceGrouped = true;
        }

        private void Message1_Checked(object sender, RoutedEventArgs e)
        {
            RootPageViewModel.Instance.MainStatusContent = "dd";
            // MainPage.Instance.RootViewModel.MainStatusContent = "Message 1 Selected";
        }

        private void Message1_Unchecked(object sender, RoutedEventArgs e)
        {
            MainPage.Instance.RootViewModel.MainStatusContent = "Message 1 De-Selected";
        }


    }
}
