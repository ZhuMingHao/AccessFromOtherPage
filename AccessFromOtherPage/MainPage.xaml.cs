using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AccessFromOtherPage
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {




        public RootPageViewModel RootViewModel { get; set; }

        public MainPage()
        {
            var ss = Singletion.Instance;
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
            Instance = this;
            RootViewModel = RootPageViewModel.Instance;
        }

        public static MainPage Instance;


        public void NotifyUser(string strMessage)
        {
            // If called from the UI thread, then update immediately.
            // Otherwise, schedule a task on the UI thread to perform the update.
            if (Dispatcher.HasThreadAccess)
            {
                RootViewModel.MainStatusContent = strMessage;
            }
            else
            {
                var task = Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    RootViewModel.MainStatusContent = strMessage;

                });
            }
        }



        private void nvTopLevelNav_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                contentFrame.Navigate(typeof(MessagePage));

                RootViewModel.MainStatusContent = "Settings_Page";
            }
            else
            {
                var navItemTag = args.InvokedItemContainer.Tag.ToString();

                RootViewModel.MainStatusContent = navItemTag;

                switch (navItemTag)
                {


                    case "Message_Page":
                        contentFrame.Navigate(typeof(MessagePage));
                        break;
                }

            }
        }

        private void MyTextBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
          var code =  KeyCode.KeyCodeToUnicode(e.Key);
            // var ca = (ushort)e.Key;
            InfoText.Text = code;
        }
    }

    public class Singletion
    {
        private static readonly Lazy<Singletion> lazy = new Lazy<Singletion>(() => new Singletion());
        private Singletion()
        {

        }
        public static Singletion Instance
        {
            get
            {
                return lazy.Value;
            }
        }

    }

    internal class KeyCode
    {
        /// <summary>
        /// Helps to get the UniCode based on input keys.
        /// </summary>
        /// <param name="key">input key.</param>
        /// <returns>Unicode.</returns>
        internal static string KeyCodeToUnicode(VirtualKey key)
        {
            byte[] keyboardState = new byte[255];
            bool keyboardStateStatus = GetKeyboardState(keyboardState);



            if (!keyboardStateStatus)
            {
                return string.Empty;
            }



            uint virtualKeyCode = (uint)key;
            uint scanCode = MapVirtualKey(virtualKeyCode, 0);
            IntPtr inputLocaleIdentifier = GetKeyboardLayout(0);



            StringBuilder result = new StringBuilder();
            int unicode = ToUnicodeEx(virtualKeyCode, scanCode, keyboardState, result, (int)5, 0, inputLocaleIdentifier);



            return result.ToString();
        }

        [DllImport("user32.dll", ExactSpelling = true)]
        private static extern bool GetKeyboardState(byte[] lpKeyState);



        [DllImport("user32.dll")]
        private static extern uint MapVirtualKey(uint uCode, uint uMapType);



        [DllImport("user32.dll")]
        private static extern IntPtr GetKeyboardLayout(uint idThread);



        [DllImport("user32.dll")]
        private static extern int ToUnicodeEx(uint wVirtKey, uint wScanCode, byte[] lpKeyState, [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pwszBuff, int cchBuff, uint wFlags, IntPtr dwhkl);

    }
}
