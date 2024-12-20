namespace WongNengHorng_FinalTest
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();


            slider1.ValueChanged += (s, e) =>
            {
                label1.Text = e.NewValue.ToString("0"); 
            };
        }
    }
}
