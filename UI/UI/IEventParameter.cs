namespace TheXamlGuy.UI;

public interface IEventParameter
{
    List<object> GetParameters(EventArgs args);
}