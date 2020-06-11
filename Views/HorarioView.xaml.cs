using System.Windows;
using finalb2020.ModelsViews;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace finalb2020.Views
{
    public partial class HorarioView : MetroWindow
    {
        private HorarioViewModel model;
        public HorarioView()
        {
            InitializeComponent();
            model = new HorarioViewModel(DialogCoordinator.Instance);
            this.DataContext=model;
        }
    }
}