using GymApp.Model;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;

namespace GymApp.Views
{
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Detail, WrapInNavigationPage = true, NoHistory = true)]
    public partial class TemplateView : MvxContentPage
    {
        TemplateViewModel viewModel;

        public TemplateView()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel = this.ViewModel as TemplateViewModel;
            ToolbarItem newExcersizeToolbarItem = new ToolbarItem("New Template", "plus.png", () => { viewModel.ShowNewTemplateCommand.Execute(); });
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

        public async void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            WorkoutTemplate templateSelected = (sender as ListView).SelectedItem as WorkoutTemplate;

            string title = "What Would You like to do?";
            string cancel = "Cancel";
            const string startWorkout = "Start Workout";
            const string edit = "Edit";
            const string delete = "Delete";
            string action = await DisplayActionSheet(title,cancel, null, startWorkout,edit,delete);
            switch (action)
            {
                case edit:
                    await viewModel.EditTemplate(templateSelected);
                    break;
                case delete:
                    //Do Delete
                case startWorkout:
                    //Do Start Worout
                default:
                    break;
            }
        }
    }
}
