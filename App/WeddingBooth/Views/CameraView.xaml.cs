
namespace WeddingBooth.Views;

public partial class CameraView
{
    public CameraView()
    {
        InitializeComponent();
        Loaded += CameraView_Loaded;    
    }

    private void CameraView_Loaded(object sender, System.Windows.RoutedEventArgs e)
    {
        CameraPreview.StartAsync();
    }
}
