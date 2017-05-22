
namespace ApplikasjonBoknaden.Droid.ViewPageExpanders
{
    class ChatCatalog : CustomCatalog
    {
        new protected static PageFrag[] treeBuiltInCatalog = {
            new PageFrag { LayoutID = Resource.Layout.ChatPageSellingLayout,
                           Header = "Selger" },
            new PageFrag { LayoutID = Resource.Layout.ChatPageSellingLayout,
                           Header = "Kjøper" },
        };

        public ChatCatalog()
        {
            Pages = treeBuiltInCatalog;
        }
    }
}