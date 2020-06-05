using System.Windows;
using finalb2020.ModelsViews;
using MahApps.Metro.Controls;

namespace finalb2020.Views
{
    public partial class SalonView : MetroWindow
    {
        private SalonViewModel model;
        public SalonView()
        {
            InitializeComponent();
            model = new SalonViewModel();
            this.DataContext=model;
        }
    }
}