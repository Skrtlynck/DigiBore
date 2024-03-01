using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;

namespace DigiBore.Views
{

    public sealed partial class ProjectWindow : Window
    {
        public ProjectWindow(string xmlpath, string number, string customer, string location)
        {
            InitializeComponent();
            ExtendsContentIntoTitleBar = true;
            SetTitleBar(AppTitleBar);
            TitleBar.Text = number + " " + customer + " " + location;
        }

        private void TabView_AddTabButtonClick(TabView sender, object args)
        {

            var newBoring = new TabViewItem();
            newBoring.Header = "Boringnaam";

            //
            //Frame frame = new Frame();
            //newBoring.Content = frame;
            //frame.Navigate(typeof(Page1));

            sender.TabItems.Add(newBoring);
        }
        private async void Tabs_TabCloseRequested(TabView sender, TabViewTabCloseRequestedEventArgs args)
        {
            var deleteTabDialog = new ContentDialog
            {
                Title = "Verwijder boring",
                Content = "Ben je zeker dat je deze boring wilt verwijderen?",
                PrimaryButtonText = "Verwijder",
                CloseButtonText = "Annuleer",
                XamlRoot = Content.XamlRoot // Why do I need to explicitly set this ?!

            };

            ContentDialogResult resultaat = await deleteTabDialog.ShowAsync();
            if (resultaat == ContentDialogResult.Primary)
            {
                var deleteBoringDialog = new ContentDialog
                {
                    Title = "Verwijder boring",
                    Content = "Ben je HEEL zeker dat je deze boring DEFINITIEF wilt verwijderen?",
                    PrimaryButtonText = "Verwijder definitief",
                    CloseButtonText = "Annuleer",
                    XamlRoot = Content.XamlRoot // Why do I need to explicitly set this ?!

                };
                ContentDialogResult resultaat2 = await deleteBoringDialog.ShowAsync();
                if (resultaat2 == ContentDialogResult.Primary)
                {
                    sender.TabItems.Remove(args.Tab);
                }
            }
        }
    }
    
}
