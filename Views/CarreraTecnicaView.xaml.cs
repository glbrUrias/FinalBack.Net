using System.Windows;
using finalb2020.ModelsViews;
using MahApps.Metro.Controls;

namespace finalb2020.Views
{
    public partial class CarreraTecnicaView : MetroWindow
    {
        private CarreraTecnicaViewModel model;
        public CarreraTecnicaView()
        {
            InitializeComponent();
            model = new CarreraTecnicaViewModel();
            this.DataContext=model;
        }
    }
}