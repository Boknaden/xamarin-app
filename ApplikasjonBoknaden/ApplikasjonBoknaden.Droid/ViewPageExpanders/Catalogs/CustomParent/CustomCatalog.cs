using Android.App;
using Android.Views;

namespace ApplikasjonBoknaden.Droid.ViewPageExpanders
{
    class CustomCatalog
    {
        protected PageFrag[] Pages;
        protected static PageFrag[] treeBuiltInCatalog = {
            new PageFrag { LayoutID = Resource.Layout.UserPageSoldItemsLayout,
                           Header = "Solgte varer" },
            new PageFrag { LayoutID = Resource.Layout.UserPageSoldItemsLayout,
                           Header = "Til salgs" },
        };

        public CustomCatalog()
        {
            Pages = treeBuiltInCatalog;
        }

        // Reead only indexer used for accessing pages
        public PageFrag this[int i] {
            get { return Pages[i]; }
        }

        // Returns the number of pages in the catalog:
        public int NumOfPages {
            get { return Pages.Length; }
        }
    }

    public class PageFrag : Fragment
    {
        // Layout ID for this page
        public int LayoutID;
        // Header text for this page
        public string Header;
        // The layout for this page
        public View PageView = null;
        
        /// <summary>
        /// Returns the layout for this page
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        public View GetView(ViewGroup container)
        {
            PageView = LayoutInflater.From(container.Context).Inflate(LayoutID, container, false);
            return PageView;
        }

    }
}