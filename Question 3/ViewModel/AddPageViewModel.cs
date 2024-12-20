using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Question_3;


    public partial class AddPageViewModel : ObservableObject
    {
        private readonly HttpClient _httpClient;

        [ObservableProperty]
        private string title;

        [ObservableProperty]
        private string body;

        public AddPageViewModel()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://jsonplaceholder.typicode.com/")
            };

            AddPostCommand = new AsyncRelayCommand(AddPostAsync);
        }

        public IAsyncRelayCommand AddPostCommand { get; }

        private async Task AddPostAsync()
        {
            if (string.IsNullOrWhiteSpace(Title) || string.IsNullOrWhiteSpace(Body))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Title and Body cannot be empty.", "OK");
                return;
            }

            try
            {
                var newPost = new PostRecord
                {
                    Title = Title,
                    Body = Body,
                    UserId = 1 // 默认的 UserId
                };

                // 发起 POST 请求
                var response = await _httpClient.PostAsJsonAsync("posts", newPost);

                if (response.IsSuccessStatusCode)
                {
                    var addedPost = await response.Content.ReadFromJsonAsync<PostRecord>();

                    // 清空输入字段
                    Title = string.Empty;
                    Body = string.Empty;

                    // 提示用户操作成功
                    await Application.Current.MainPage.DisplayAlert("Success", $"Post added successfully! (ID: {addedPost.Id})", "OK");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Failed to add post.", "OK");
                }
            }
            catch (Exception ex)
            {
                // 错误处理
                await Application.Current.MainPage.DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }
    }

