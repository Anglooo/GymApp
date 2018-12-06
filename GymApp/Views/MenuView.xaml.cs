using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;


namespace GymApp.Views
{
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Master, WrapInNavigationPage = false)]
    public partial class MenuView : MvxContentPage
    {
        public MenuView()
        {
            InitializeComponent();
            BackgroundColor = Color.FromHex("#2196F3");
            Title = "Menu";
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }
    }
}
