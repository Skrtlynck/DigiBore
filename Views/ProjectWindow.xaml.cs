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
            InitData(xmlpath);
            ProjectWindowName.Title = customer + " " + location;
        }

        public static async Task<string> ShowAddDialogAsync(string title)
        {
            var inputTextBox = new TextBox { AcceptsReturn = false };
            var dialog = new ContentDialog
            {
                Content = inputTextBox,
                Title = title,
                IsSecondaryButtonEnabled = true,
                PrimaryButtonText = "Maak aan",
                SecondaryButtonText = "Annuleer"
            };
            if (await dialog.ShowAsync() == ContentDialogResult.Primary)
                return inputTextBox.Text;
            else
                return "";
        }
        private void InitData(string xmlpath)
        {
            if (xmlpath == "")
            {

                    var newBoring = new TabViewItem();
                    newBoring.Header = "test";


                    Frame frame = new Frame();
                    newBoring.Content = frame;
                    frame.Navigate(typeof(BoringTabPage));

                    Content.TabItems.Add(newBoring);


            }

            else
            {

            }
        }

        private async Task<string> BoringNummerOpvragen(string title)
        {
            var vraag = new TextBox(); 
            vraag.PlaceholderText = "Boringnummer";
            var boringNummerVraag = new ContentDialog
            {
                Title = title,
                Content = vraag,
                PrimaryButtonText = "Verwijder",
                CloseButtonText = "Annuleer",
                XamlRoot = Content.XamlRoot

            };

            await boringNummerVraag.ShowAsync();

            return vraag.Text;
        }

        private async void TabView_AddTabButtonClick(TabView sender, object args)
        {
            var newBoring = new TabViewItem();
            newBoring.Header = await BoringNummerOpvragen("Wat is het boringnummer?");

            Frame frame = new Frame();
            newBoring.Content = frame;
            frame.Navigate(typeof(BoringTabPage));
            sender.TabItems.Add(newBoring);
            sender.SelectedItem = newBoring;
        }
        private async void Tabs_TabCloseRequested(TabView sender, TabViewTabCloseRequestedEventArgs args)
        {
            var deleteTabDialog = new ContentDialog
            {
                Title = "Verwijder boring",
                Content = "Ben je zeker dat je deze boring wilt verwijderen?",
                PrimaryButtonText = "Verwijder",
                CloseButtonText = "Annuleer",
                XamlRoot = Content.XamlRoot

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
                    XamlRoot = Content.XamlRoot

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
