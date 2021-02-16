using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using ViewModelService;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Gui.Views;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Gui
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RootGrid : Grid
    {
        Frame currentFrame;
        Frame filters = new Frame();
        private ViewModelNavigation viewModelNavigation;
        public RootGrid(Frame currentView)
        {
            InitializeComponent();
            //this.filters = currentfilters;
            this.currentFrame = currentView;
           // this.filters = globalFilters;
            viewModelNavigation = new ViewModelNavigation();
            this.DataContext = viewModelNavigation;
            this.filters.Navigate(typeof(GlobalFilters));
            Grid.SetRow(this.filters, 1);
            // Add the Frame to the third row of the Grid
            Grid.SetRow(this.currentFrame, 2);
            this.Children.Add(this.currentFrame);
            this.Children.Add(this.filters);
            //Navigatio delegates -- Moet nog anders volgens MvvM

          
            this.viewModelNavigation.GaNaarSpelersDelegate = () => this.currentFrame.Navigate(typeof(SpelersPage));
            this.viewModelNavigation.GaNaarMainPageDelegate = () => this.currentFrame.Navigate(typeof(MainPage));
            this.viewModelNavigation.GaNaarViewEditTeamsDelegate = () => this.currentFrame.Navigate(typeof(ViewEditTeams));
            this.viewModelNavigation.GaNaarViewEditWedstrijdSchemaDelegate = () => this.currentFrame.Navigate(typeof(Views.ViewEditWedstrijdSchema));
            this.viewModelNavigation.GaNaarCoachesDelegate = () => this.currentFrame.Navigate(typeof(CoachesPage));
            this.currentFrame.Navigated += Frame_Navigated;
        }

        void Frame_Navigated(object sender, NavigationEventArgs e)
        {
            if (this.currentFrame != null)
            {
                // Keep the enabled/disabled state of the buttons relevant
                this.BackButton.IsEnabled = this.currentFrame.CanGoBack;
                this.ForwardButton.IsEnabled = this.currentFrame.CanGoForward;
            }
        }

        void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.currentFrame != null && this.currentFrame.CanGoBack)
                this.currentFrame.GoBack();
        }

        void ForwardButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.currentFrame != null && this.currentFrame.CanGoForward)
                this.currentFrame.GoForward();
        }
    }
}
