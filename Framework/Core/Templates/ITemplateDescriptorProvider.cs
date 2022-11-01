namespace TheXamlGuy.Framework.Core
{
    public interface ITemplateDescriptorProvider
    {
        ITemplateDescriptor? Get(string name);

        ITemplateDescriptor? Get(Type type);

        ITemplateDescriptor? Get<T>();
    }
}