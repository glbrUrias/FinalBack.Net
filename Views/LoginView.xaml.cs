using finalb2020.Models;
using finalb2020.ModelsViews;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace finalb2020.Views
{
    public partial class LoginView :MetroWindow
    {
        private LoginModelView _ModelView;
        public LoginView(MainViewModel mainViewModel)
        {
            InitializeComponent();
            _ModelView = new LoginModelView(DialogCoordinator.Instance,mainViewModel);
            this.DataContext = _ModelView;
        }
    }
}