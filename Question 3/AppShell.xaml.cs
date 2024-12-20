namespace Question_3
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("AddPage", typeof(AddPage));

        }
    }
}
