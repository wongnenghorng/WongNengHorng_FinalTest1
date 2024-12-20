using Microsoft.Maui.Controls;

namespace Question_3
{
    public partial class AddPage : ContentPage
    {
        public AddPage()
        {
            InitializeComponent();

            // 设置 BindingContext 为 AddPageViewModel
            BindingContext = new AddPageViewModel();
        }
    }
}
