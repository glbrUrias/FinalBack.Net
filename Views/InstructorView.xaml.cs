using System.Windows;
using finalb2020.ModelsViews;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace finalb2020.Views
{
    public partial class InstructorView : MetroWindow
    {
        private InstructorViewModel model;
        public InstructorView()
        {
            InitializeComponent();
            model = new InstructorViewModel(DialogCoordinator.Instance);
            this.DataContext=model;
        }
    }
}