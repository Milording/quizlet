using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;
using Quizlet.Models;
using Quizlet.ViewModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Quizlet.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AllCardsPage : Page
    {
        public AllCardsPage()
        {
            this.InitializeComponent();
        }
        
        private void backrectangle_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var rect = sender as Grid;
            var grid = rect.Parent as Grid;
            var values = grid.Resources.Values;
            var msb = values.Last() as Storyboard;
            msb.Begin();
        }

        private void frontrectangle_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var rect = sender as Grid;
            var grid = rect.Parent as Grid;
            var values = grid.Resources.Values;
            var msb = values.First() as Storyboard;
            msb.Begin();

        }
        
    }
}
