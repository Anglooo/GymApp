using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;

namespace GymApp.Views
{
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Detail, WrapInNavigationPage = true, NoHistory = true)]
    public partial class HomeView : MvxContentPage
    {
        public HomeView()
        {
            InitializeComponent();
            
        }
    }
}