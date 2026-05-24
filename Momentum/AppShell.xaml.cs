namespace Momentum
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            BindingContext = AppService.GetRequiredService<AppViewModel>();
        }
    }
}
