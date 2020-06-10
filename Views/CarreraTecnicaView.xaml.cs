using System.Windows;
using finalb2020.ModelsViews;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace finalb2020.Views
{
    public partial class CarreraTecnicaView : MetroWindow
    {
        private CarreraTecnicaViewModel model;
        public CarreraTecnicaView()
        {
            InitializeComponent();
            model = new CarreraTecnicaViewModel(DialogCoordinator.Instance);
            this.DataContext=model;
        }
    }
}