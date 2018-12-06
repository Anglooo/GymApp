using GymApp.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;

namespace GymApp.Views
{
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Root, WrapInNavigationPage = false)]
    public partial class RootView : MvxMasterDetailPage<RootViewModel>
    {
        public RootView()
        {
            InitializeComponent();
            this.BackgroundColor = Color.Blue;
        }
    }
}