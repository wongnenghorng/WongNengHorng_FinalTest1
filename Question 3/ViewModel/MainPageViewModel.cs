using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Question_3;
using System.Collections.ObjectModel;
using System.Text.Json;

public partial class MainPageViewModel : ObservableObject
{
    private readonly HttpClient _httpClient;

    // Posts 数据集合
    public ObservableCollection<PostRecord> Posts { get; } = new();

    public MainPageViewModel()
    {
        // 初始化 HttpClient
        _httpClient = new HttpClient { BaseAddress = new Uri("https://jsonplaceholder.typicode.com/") };

        // 命令绑定
        LoadPostsCommand = new RelayCommand(async () => await LoadPosts());
        NavigateToAddCommand = new RelayCommand(async () => await NavigateToAddPage());
        DeletePostCommand = new RelayCommand<PostRecord>(async (post) => await DeletePost(post));
    }

    // 命令
    public IRelayCommand LoadPostsCommand { get; }
    public IRelayCommand NavigateToAddCommand { get; }
    public IRelayCommand DeletePostCommand { get; }

    // 加载 Posts 数据
    private async Task LoadPosts()
    {
        try
        {
            var response = await _httpClient.GetStringAsync("posts");
            var posts = JsonSerializer.Deserialize<List<PostRecord>>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            if (posts != null)
            {
                Posts.Clear();
                foreach (var post in posts)
                {
                    Posts.Add(post);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching posts: {ex.Message}");
        }
    }

    // 导航到 AddPage
    private async Task NavigateToAddPage()
    {
        await Shell.Current.GoToAsync(nameof(AddPage));
    }

    // 删除指定的帖子，并弹出确认框
    private async Task DeletePost(PostRecord post)
    {
        bool isConfirmed = await Shell.Current.DisplayAlert("Confirm Delete", "Are you sure you want to delete this post?", "Yes", "No");

        if (isConfirmed)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"posts/{post.Id}");
                if (response.IsSuccessStatusCode)
                {
                    Posts.Remove(post);  // 移除本地数据中的帖子
                }
                else
                {
                    Console.WriteLine("Failed to delete post");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting post: {ex.Message}");
            }
        }
    }
}
