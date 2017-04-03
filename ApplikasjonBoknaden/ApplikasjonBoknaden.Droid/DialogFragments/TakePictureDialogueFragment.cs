using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Android.Provider;
using Android.Content.PM;
using Android.Support.V4.App;
using Android.Support.V7.Widget;
using ZXing.Mobile;
using Android.Graphics;
using ApplikasjonBoknaden.Droid.DialogFragments.CostumParent;
using RestSharp;

namespace ApplikasjonBoknaden.Droid.DialogFragments
{
    [Activity(Label = "TakePictureFragment")]
    public class TakePictureDialogueFragment : CustomDialogActivity
    {
        private enum ItemType
        {
            Book,
            Other
        };

        private enum CurrentCardType
        {
            ChooseItemTypeCard,
            TakePictureCard,
            ScanISBNcard,
            ISBNDescriptionCard
        };

        protected ValidationResponse ValidationResponder = new ValidationResponse();
        private CardView ISBNScanCard = null;
        private CardView ISBNDescriptionCard = null;
        private CardView TakePictureCard = null;
        private CardView ChooseItemTypeCard = null;
        private CardView LastCard = null;

        private List<CardView> allCards = new List<CardView>();

        public ImageView _imageView;
        private EditText EditTextISBNNumber;
        private Spinner SchoolSpinner;
        private Spinner ClassSpinner;
        private string[] schoolnamesarr1 = new string[] { "Uspesifisert", "Lunde", "Bø", "Horten" };
        private string[] ClassNamesarr = new string[] { "Uspesifisert", "IT", "Helse", "Religion" };

        
        static int REQUEST_IMAGE_CAPTURE = 1;
        private string PassedISBN = "";

        private ItemType NewitemType = ItemType.Book;

        private ImageView PrevStepImage = null;
        private Button NextStepButtonNew = null;

        private EditText ManualISBNInput = null;

        private Json.Ad NewAd = new Json.Ad();
        //private List<AdItem>


        protected override int LayoutSetter()
        {
            return Resource.Layout.DialogueFragmentTakePictureLayout;
        }

        protected override void InitiateFragment()
        {
            SetButtonValues();
            SetButtonValuesISBNScanCard();
            SetButtonValuesLastCard();
            AddAllCards();
            GoToCard(ChooseItemTypeCard);

            PopulateSpinner(SchoolSpinner, schoolnamesarr1);
            PopulateSpinner(ClassSpinner, ClassNamesarr);
        }

        private void AddAllCards()
        {
            allCards.Add(ChooseItemTypeCard);
            allCards.Add(TakePictureCard);
            allCards.Add(ISBNScanCard);
            allCards.Add(ISBNDescriptionCard);
            allCards.Add(LastCard);
        }

        /// <summary>
        /// Finds and sets the values on this activities buttons and other views
        /// </summary>
        protected void SetButtonValues()
        {
            //Gets the cards
            TakePictureCard = Dialogueview.FindViewById<CardView>(Resource.Id.cardViewPicture);
            ChooseItemTypeCard = Dialogueview.FindViewById<CardView>(Resource.Id.cardViewItemType);
            ISBNScanCard = Dialogueview.FindViewById<CardView>(Resource.Id.cardViewScanner);
            ISBNDescriptionCard = Dialogueview.FindViewById<CardView>(Resource.Id.cardViewISBNDescription);
            LastCard = Dialogueview.FindViewById<CardView>(Resource.Id.cardViewLast);

            PrevStepImage = Dialogueview.FindViewById<ImageView>(Resource.Id.PrevStepImage);
            PrevStepImage.Touch += (object sender, View.TouchEventArgs e) =>
            {
                if (e.Event.Action == MotionEventActions.Up)
                {
                    PrevStepImage.Alpha = 1.0f;
                    PrevStepImageAction();
                }
                if (e.Event.Action == MotionEventActions.Down)
                {
                    PrevStepImage.Alpha = 0.4f;
                }
            };
            NextStepButtonNew = Dialogueview.FindViewById<Button>(Resource.Id.NextStepButtonNew);
            NextStepButtonNew.Click += NextStepButtonAction;
            //Itemtype choice card
            Button setAsBookButton = Dialogueview.FindViewById<Button>(Resource.Id.SetItemAsBookButton);
            setAsBookButton.Click += SetItemAsNewBook;
            Button setAsOtherButton = Dialogueview.FindViewById<Button>(Resource.Id.SetItemAsOtherButton);
            setAsOtherButton.Click += SetItemAsNewOther;

            //Back to choose itemtype card image
       
            //Next Step button
            //Book Image
            _imageView = Dialogueview.FindViewById<ImageView>(Resource.Id.imageView1);
            if (IsThereAnAppToTakePictures())
            {
                Button button = Dialogueview.FindViewById<Button>(Resource.Id.TakePictureButton);
                CreateDirectoryForPictures();
                button.Click += TakeAPicture;
            }
        }

        /// <summary>
        /// Finds and sets the values on this activities buttons and other views
        /// </summary>
        protected void SetButtonValuesLastCard()
        {
            //School Selector
            SchoolSpinner = Dialogueview.FindViewById<Spinner>(Resource.Id.spinnerSchoolUpload);
            //Class Selector
            ClassSpinner = Dialogueview.FindViewById<Spinner>(Resource.Id.spinnerClassUpload);
            //ISBN number
           // EditTextISBNNumber = Dialogueview.FindViewById<EditText>(Resource.Id.editTextISBNNumber);
          //  EditTextISBNNumber.Text = PassedISBN;
          //  EditTextISBNNumber.Focusable = false;
          //  EditTextISBNNumber.SetBackgroundColor(Color.LightGreen);
        }

        private void PopulateSpinner(Spinner sp, string[] spinnerStrings)
        {
            ArrayAdapter<string> adapter1 = new ArrayAdapter<string>(this.CallerActivity, Resource.Layout.Spinner_textview, spinnerStrings);
            adapter1.SetDropDownViewResource(global::Android.Resource.Layout.SimpleSpinnerDropDownItem);
            sp.Adapter = adapter1;

        }




        //https://developer.xamarin.com/guides/android/user_interface/spinner/
        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;

            string toast = string.Format("The planet is {0}", spinner.GetItemAtPosition(e.Position));
            //Toast.MakeText(this, toast, ToastLength.Long).Show();
        }

        private CardView GetCurrentActiveCard()
        {
            foreach (CardView cv in allCards)
            {
                if (cv.Visibility == ViewStates.Visible)
                {
                    return cv;
                }
            }

            return null;
        }

        private void NextStepButtonAction(object sender, EventArgs e)
        {

            if (GetCurrentActiveCard() == ChooseItemTypeCard)
            {
                GoToCard(TakePictureCard);
            }
            else if (GetCurrentActiveCard() == TakePictureCard)
            {
                if (NewitemType == ItemType.Book)
                {
                    GoToCard(ISBNScanCard);
                }
                else
                {
                  // GoToCard(ISBNScanCard);
                }

            }
            else if (GetCurrentActiveCard() == ISBNScanCard)
            {
                GoToNextStepOfAddBook(ManualISBNInput.Text);
                //  GoToCard(TakePictureCard);
            }
            else if (GetCurrentActiveCard() == ISBNDescriptionCard)
            {
            //    GoToCard(ISBNScanCard);
            } else if (GetCurrentActiveCard() == LastCard)
            {
                UploadNewBook();
            }
        }

        private void PrevStepImageAction()
        {
            if (GetCurrentActiveCard() == ChooseItemTypeCard)
            {
                CloseFragment();
            } else if (GetCurrentActiveCard() == TakePictureCard)
            {
                GoToCard(ChooseItemTypeCard);
            } else if (GetCurrentActiveCard() == ISBNScanCard)
            {
                GoToCard(TakePictureCard);
            } else if (GetCurrentActiveCard() == ISBNDescriptionCard)
            {
                GoToCard(ISBNScanCard);
            }
            else if (GetCurrentActiveCard() == LastCard)
            {
                if (NewitemType == ItemType.Book)
                {
                    GoToCard(ISBNScanCard);
                }
                else
                {
                    // GoToCard(ISBNScanCard);
                }
            }
        }

        /// <summary>
        /// Finds and sets the values on this activities buttons and other views
        /// </summary>
        protected void SetButtonValuesISBNScanCard()
        {
            ManualISBNInput = Dialogueview.FindViewById<EditText>(Resource.Id.ManualISBNInputEditText);
            TextView ISBNDescriptionLink = Dialogueview.FindViewById<TextView>(Resource.Id.ISBNDescriptionLink);
            ISBNDescriptionLink.Clickable = true;
            ISBNDescriptionLink.Click += ShowDescriptionCard;

            ImageButton BackToScannerButton = Dialogueview.FindViewById<ImageButton>(Resource.Id.BackToScannerImageButton);
            BackToScannerButton.Click += BackToScannerFromDescription;

            Button Scanbutton = Dialogueview.FindViewById<Button>(Resource.Id.ScanISBNButton);
            Scanbutton.Click += async (sender, e) =>
            {

#if __ANDROID__
                // Initialize the scanner first so it can track the current context
                MobileBarcodeScanner.Initialize(this.CallerActivity.Application);
#endif
                var scanner = new ZXing.Mobile.MobileBarcodeScanner();
                var result = await scanner.Scan();
                if (result != null)
                {
                    Console.WriteLine("Scanned Barcode: " + result.Text);
                    GoToNextStepOfAddBook(result.Text);
                    //  TestText.Text = result.Text;
                }
            };
        }

        private void GoToNextStepOfAddBook(string isbn)
        {
            //Validates username
            ValidationResponder = InputValidator.validISBN(isbn);
            if (!ValidationResponder.Successful)
            {
                ShowToast(ValidationResponder.Information);
                return;
            }
            GoToCard(LastCard);
            //GoToCard();



            //  var dialog = new DialogFragments.AddNewBookFirstFragment();
            //  dialog.Show(this.CallerActivity.SupportFragmentManager, "dialog", this.CallerActivity);

            //  var nextActivity = new Intent(this.CallerActivity, typeof(CameraActivity));
            //  nextActivity.PutExtra(GetString(Resource.String.ISBNString), isbn);
            // StartActivity(nextActivity);
        }

        /// <summary>
        /// Goes through all cards and disables all exept the card that is given.
        /// </summary>
        /// <param name="goToCard"></param>
        private void GoToCard(CardView goToCard)
        {
            foreach (CardView c in allCards)
            {
                if (c != null)
                {

                    if (c == goToCard)
                    {
                        c.Visibility = ViewStates.Visible;
                    }
                    else if(c.Visibility == ViewStates.Visible)
                    {
                        c.Visibility = ViewStates.Gone;
                    }
                }
            }

            if (GetCurrentActiveCard() == ChooseItemTypeCard)
            {
                NextStepButtonNew.Visibility = ViewStates.Gone;
            }
            else if (GetCurrentActiveCard() == TakePictureCard)
            {
                NextStepButtonNew.Visibility = ViewStates.Visible;
            }
            else if (GetCurrentActiveCard() == ISBNScanCard)
            {
                NextStepButtonNew.Visibility = ViewStates.Visible;
            }
            else if (GetCurrentActiveCard() == ISBNDescriptionCard)
            {
                NextStepButtonNew.Visibility = ViewStates.Gone;
            }

            ShowToast("New card");
        }


        private void SetItemAsNewBook(object sender, EventArgs eventArgs)
        {
            NewitemType = ItemType.Book;
            GoToCard(TakePictureCard);
        }

        private void SetItemAsNewOther(object sender, EventArgs eventArgs)
        {
            NewitemType = ItemType.Other;
        }

        private void BackToScannerFromDescription(object sender, EventArgs e)
        {
            GoToCard(ISBNScanCard);
        }

        private void ShowDescriptionCard(object sender, EventArgs e)
        {
            GoToCard(ISBNDescriptionCard);
        }

        private void dispatchTakePictureIntent()
        {
            Intent takePictureIntent = new Intent(MediaStore.ActionImageCapture);
            if (IsThereAnAppToTakePictures())
            {
                StartActivityForResult(takePictureIntent, REQUEST_IMAGE_CAPTURE);
            }
        }

        private void CreateDirectoryForPictures()
        {
            App._dir = new Java.IO.File(
                Android.OS.Environment.GetExternalStoragePublicDirectory(
                    Android.OS.Environment.DirectoryPictures), "CameraAppDemo");
            if (!App._dir.Exists())
            {
                App._dir.Mkdirs();
            }
        }

        private bool IsThereAnAppToTakePictures()
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            IList<ResolveInfo> availableActivities =
                this.CallerActivity.PackageManager.QueryIntentActivities(intent, PackageInfoFlags.MatchDefaultOnly);
            return availableActivities != null && availableActivities.Count > 0;
        }

        private void TakeAPicture(object sender, EventArgs eventArgs)
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            App._file = new Java.IO.File(App._dir, String.Format("myPhoto_{0}.jpg", Guid.NewGuid()));
            intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(App._file));
            StartActivityForResult(intent, 0);
        }

        private void AdNewAdd()
        {
            var client = new RestClient("http://146.185.164.20:57483/");

            Json.Ad newad = new Json.Ad();
            // newad.userid = "1";
            newad.universityid = 1;
            newad.courseid = 1;
            newad.adname = "Nye boker!";
            newad.text = "Text";
            Json.Aditem newbook = new Json.Aditem();
            // newbook.userid = "1";
            newbook.price = 250;
            newbook.text = "Funker fra app!";

            newad.aditems.Add(newbook);

            var request = new RestRequest("ads", Method.POST);
            //request.AddObject(newbook);

            //request.AddObject("'");
            request.AddJsonBody(newad);
            // request.AddBody("'");
            //  request.AddJsonBody(newbook);
            //  request.AddBody("'");
            // request.AddBody(newad);
            //  request.AddObject(newad);

            request.AddHeader("boknaden-verify", ApplikasjonBoknaden.Droid.AndroidJsonHelpers.AndroidJsonHelper.CleansedToken(SavedValues.UserValues.GetSavedToken(CallerActivity.sP)));
            System.Diagnostics.Debug.WriteLine(request.Parameters);

            //  RestResponse response = client.Execute(request);
            // var content = response.Content;

            client.ExecuteAsync(request, response => {
                Console.WriteLine(response.Content);
            });
        }


        // private void GoToAddNewOther()
        //  {
        //      AddNewItemDialogueFragment nextFragment = new DialogFragments.AddNewItemDialogueFragment();
        //      nextFragment.Show(this.CallerActivity.SupportFragmentManager, "dialog", this.CallerActivity);
        //}

        private async void UploadNewBook()
        {
            ShowToast("Registrerer");
            await Json.JsonUploader.RegisterNewBookAd();
            //CloseFragment();
            //  await JsonDownloader.RegisterNewUser(WritenUsername, WrittenEmail, WritenFirstname, WritenLastname, WritenPassword);
        }
    }



    public class NewBook
    {
        string userid = "";
        string universityid = "";
        string courseid = "";
        string adname = "";

        string text = "";
        string pinned = "";
        string deleted = "";
    }
}