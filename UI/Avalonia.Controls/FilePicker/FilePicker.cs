using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Platform.Storage;

namespace TheXamlGuy.UI.Avalonia.Controls
{
    public class FilePickerFilePickedEventArgs : EventArgs
    {

    }
    public class FilePicker : TemplatedControl
    {
        public static readonly StyledProperty<bool> AllowMultipleProperty =
            AvaloniaProperty.Register<FilePicker, bool>(nameof(AllowMultiple));

        public static readonly StyledProperty<IReadOnlyList<FilePickerFileType>?> FileTypeFilterProperty =
            AvaloniaProperty.Register<FilePicker, IReadOnlyList<FilePickerFileType>?>(nameof(FileTypeFilter));

        public static readonly StyledProperty<IStorageFolder?> SuggestedStartLocationProperty =
            AvaloniaProperty.Register<FilePicker, IStorageFolder?>(nameof(SuggestedStartLocation));

        public static readonly StyledProperty<string?> TitleProperty =
            AvaloniaProperty.Register<FilePicker, string?>(nameof(Title));

        public bool AllowMultiple
        {
            get { return GetValue(AllowMultipleProperty); }
            set { SetValue(AllowMultipleProperty, value); }
        }

        public IReadOnlyList<FilePickerFileType>? FileTypeFilter
        {
            get { return GetValue(FileTypeFilterProperty); }
            set { SetValue(FileTypeFilterProperty, value); }
        }

        public IStorageFolder? SuggestedStartLocation
        {
            get { return GetValue(SuggestedStartLocationProperty); }
            set { SetValue(SuggestedStartLocationProperty, value); }
        }

        public string? Title
        {
            get { return GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public event TypedEventHandler<FilePicker, FilePickerFilePickedEventArgs>? FilePicked;

        public async void Open()
        {
            if (VisualRoot is Window window)
            {
                IReadOnlyList<IStorageFile> files = await window.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions 
                { 
                    AllowMultiple = AllowMultiple,
                    FileTypeFilter = FileTypeFilter,
                    SuggestedStartLocation = SuggestedStartLocation,
                    Title = Title 
                });
            }
        }
    }
}
