using GymApp.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;

namespace GymApp.Views
{
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Detail, WrapInNavigationPage = true)]
    public partial class ExcersizeHistoryView : MvxContentPage
    {

        private ExcersizeHistoryViewModel viewModel;

        public ExcersizeHistoryView()
        {
            InitializeComponent();
            this.Title = "Excersize History";
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel = this.ViewModel as ExcersizeHistoryViewModel;
            ToolbarItem newExcersizeToolbarItem = new ToolbarItem("New Excersize", null,() => { viewModel.NewExcersizeCommand.Execute(); });
        }
    }
}
