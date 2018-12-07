using GymApp.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;

namespace GymApp.Views
{
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Detail, WrapInNavigationPage = true, NoHistory =true)]
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
            ToolbarItem newExcersizeToolbarItem = new ToolbarItem("New Excersize", "plus.png",() => { viewModel.NewExcersizeCommand.Execute(); });
            ToolbarItems.Clear();
            ToolbarItems.Add(newExcersizeToolbarItem);

            if (Xamarin.Forms.Application.Current.MainPage is MasterDetailPage masterDetailPage)
            {
                masterDetailPage.IsPresented = false;
            }
            else if (Xamarin.Forms.Application.Current.MainPage is NavigationPage navigationPage && navigationPage.CurrentPage is MasterDetailPage nestedMasterDetail)
            {
                nestedMasterDetail.IsPresented = false;
            }
        }
    }
}
