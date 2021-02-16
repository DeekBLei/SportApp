
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Gui
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewTeam : Page
    {
       // private ViewModelNewTeam viewModelNewTeam;
        public NewTeam()
        {
            this.InitializeComponent();
          
           // DataContext = new ViewModelNewTeam();

        }

       


        //private void SpelersList_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //    // ListView listView = (ListView)sender;

        //    var clickedMenuItem = (Speler)e.ClickedItem; // Typecast the clicked menu item back to the content type of each list view item

        //    // Key line is here:
        //    //var item = listView.Items.IndexOf(clickedMenuItem);
        //  viewModelNewTeam.NewTeam.TeamLeden.Add(clickedMenuItem);

        //}

        //private void TeamLeden_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //    var clickedMenuItem = (Speler)e.ClickedItem; // Typecast the clicked menu item back to the content type of each list view item

        //    // Key line is here:
        //    //var item = listView.Items.IndexOf(clickedMenuItem);
        //    viewModelNewTeam.NewTeam.TeamLeden.Remove(clickedMenuItem);
        //}

        //private void CoachList_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //    var clickedMenuItem = (Coach)e.ClickedItem; // Typecast the clicked menu item back to the content type of each list view item

        //    // Key line is here:
        //    //var item = listView.Items.IndexOf(clickedMenuItem);
        //    viewModelNewTeam.NewTeam.Coaches.Add(clickedMenuItem);
        //}
    }
}
