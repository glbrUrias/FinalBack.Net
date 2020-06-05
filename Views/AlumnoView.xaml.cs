using System.Windows;
using finalb2020.ModelsViews;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace finalb2020.Views
{
    public partial class AlumnoView : MetroWindow
    {
        private AlumnoViewModel model;
        public AlumnoView()
        {
            InitializeComponent();
            model = new AlumnoViewModel(DialogCoordinator.Instance);
            this.DataContext=model;
        }
    }
}