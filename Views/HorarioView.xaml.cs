using System.Windows;
using finalb2020.ModelsViews;
using MahApps.Metro.Controls;

namespace finalb2020.Views
{
    public partial class HorarioView : MetroWindow
    {
        private HorarioViewModel model;
        public HorarioView()
        {
            InitializeComponent();
            model = new HorarioViewModel();
            this.DataContext=model;
        }
    }
}