
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
    public sealed partial class ViewEditTeams : Page
    {
       
        public ViewEditTeams()
        {
            this.InitializeComponent();


            this.DataContext = new ViewModelViewEditTeams();
        }


        //private void TeamsList_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //    ListView listView = (ListView)sender;
        //    var clickedMenuItem = (Team)e.ClickedItem; // Typecast the clicked menu item back to the content type of each list view item

        //    // Key line is here:
        //    var item = listView.Items.IndexOf(clickedMenuItem);
        //    viewModelViewEditTeams.CurrentTeam = clickedMenuItem;
        //}
        //private void SpelersList_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //    ListView listView = (ListView)sender;
        //    var clickedMenuItem = (Speler)e.ClickedItem; // Typecast the clicked menu item back to the content type of each list view item

        //    // Key line is here:
        //    var item = listView.Items.IndexOf(clickedMenuItem);
        //    viewModelViewEditTeams.AddCurrentSpeler = clickedMenuItem;
        //}





    }
}
