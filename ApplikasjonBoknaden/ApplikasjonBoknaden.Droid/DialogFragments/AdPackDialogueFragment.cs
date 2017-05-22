using Android.App;
using Android.Views;
using Android.Widget;
using ApplikasjonBoknaden.Droid.DialogFragments.CostumParent;
using ApplikasjonBoknaden.Json;

namespace ApplikasjonBoknaden.Droid.DialogFragments
{
    [Activity(Label = "AdPackDialogueFragmen")]
    public class AdPackDialogueFragment : CustomDialogFragment
    {
        private Ad _AdPack;
        protected GridLayout AdDisplayer;
        protected ProgressBar _ProgressBar;
        private ImageView BackImageView = null;
        private ImageView MoreInfomageView = null;

        public void Show(Android.Support.V4.App.FragmentManager manager, string tag, CustomFragmentActivity caller, Ad ad)
        {
            this.CallerActivity = caller;
            _AdPack = ad;
            base.Show(manager, tag);
        }

        protected override int LayoutSetter()
        {
            return Resource.Layout.AdPackLayout;
        }

        protected override void InitiateFragment()
        {
            AdDisplayer = Dialogueview.FindViewById<GridLayout>(Resource.Id.tableLayout1);

            BackImageView = Dialogueview.FindViewById<ImageView>(Resource.Id.AdPack_BackImage_view);
            BackImageView.Touch += (object sender, View.TouchEventArgs e) =>
            {
                if (e.Event.Action == MotionEventActions.Up)
                {
                    BackImageView.Alpha = 1.0f;
                    CloseFragment();
                }
                if (e.Event.Action == MotionEventActions.Down)
                {
                    BackImageView.Alpha = 0.4f;
                }
            };

            MoreInfomageView = Dialogueview.FindViewById<ImageView>(Resource.Id.AdPack_MoreInfo_Imageview);
            MoreInfomageView.Touch += (object sender, View.TouchEventArgs e) =>
            {
                if (e.Event.Action == MotionEventActions.Up)
                {
                    BackImageView.Alpha = 1.0f;
                    ShowMoreInfo();
                }
                if (e.Event.Action == MotionEventActions.Down)
                {
                    BackImageView.Alpha = 0.4f;
                }
            };

            AddLoadingSign();
            AddItems(_AdPack);
        }

        private void ShowMoreInfo()
        {

        }

        private void AddLoadingSign()
        {
            _ProgressBar = new ProgressBar(Context);
            _ProgressBar.LayoutParameters = new RelativeLayout.LayoutParams(RelativeLayout.LayoutParams.MatchParent, RelativeLayout.LayoutParams.MatchParent);
            AdDisplayer.AddView(_ProgressBar);
        }

        private void AddItems(Ad ad)
        {
            foreach (Json.Aditem a in ad.aditems)
            {
                AdItemClasses.AdMiniature AdPackDisplay1 = new AdItemClasses.AdMiniature(Context, AdDisplayer, a, ad);
                AdPackDisplay1.InitiateShowInterestButton("Meld interesse",false);
                
                AdPackDisplay1.GetButton().Click += delegate {
                   // ShowAdPack(a);
                };
            }
            _ProgressBar.Visibility = ViewStates.Gone;
        }
    }
}