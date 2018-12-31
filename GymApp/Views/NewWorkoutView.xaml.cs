using GymApp.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;

namespace GymApp.Views
{
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Detail, WrapInNavigationPage = true, NoHistory = true)]
    public partial class NewWorkoutView : MvxContentPage
    {
        public NewWorkoutView()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (this.ViewModel != null)
            {
                (this.ViewModel as NewWorkoutViewModel).SaveWorkout(true);

                NewWorkoutViewModel viewModel = this.ViewModel as NewWorkoutViewModel;
                ToolbarItem newExcersizeToolbarItem = new ToolbarItem("Add Excersize", "plus.png", () => { viewModel.NewExcersizeCommand.Execute(); });
                ToolbarItems.Clear();
                ToolbarItems.Add(newExcersizeToolbarItem);
            }

            ExcersizeListView.SelectedItem = null;

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
