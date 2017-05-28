using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using Android.Provider;
using Android.Content.PM;
using Android.Support.V7.Widget;
using ZXing.Mobile;
using Android.Graphics;
using ApplikasjonBoknaden.Droid.DialogFragments.CostumParent;
using ApplikasjonBoknaden.Droid.AdItemClasses;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using ApplikasjonBoknaden.Json;
using System.Net.Http;
using System.Threading.Tasks;
using ApplikasjonBoknaden.Droid.SavedValues;

namespace ApplikasjonBoknaden.Droid.DialogFragments
{
    [Activity(Label = "TakePictureFragment")]
    public class AddNewAdPackDialogueFragment : CustomDialogFragment
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

        private Json.Ad _Ad = null;

        private AdMiniature _SelectedAdminiature;
        private List<AdMiniature> _AllAdMiniatures = new List<AdMiniature>();
        private Json.Aditem _SelectedProduct;
        private List<Json.Aditem> _AdItems = new List<Json.Aditem>();

        protected ValidationResponse ValidationResponder = new ValidationResponse();
        //Main card
        private CardView AdsCard = null;
        private EditText AdTitelText;
        private EditText AdDescriptionText;
        //ISBN-scanner card
        private CardView ISBNScanCard = null;
        //ISbn description card
        private CardView ISBNDescriptionCard = null;
        //Take picture card
        private CardView TakePictureCard = null;
        public ImageView _imageView;
        //Choose type of item card
        private CardView ChooseItemTypeCard = null;
        //Last card
        private CardView LastCard = null;
        private EditText ProductNameText;
        private EditText ProductDescriptionText;
        private EditText ProductPriceText;
        private EditText AdPackNameText;
        private EditText AdPackDescriptionText;

        private CardView PublishCard = null;

        private List<CardView> allCards = new List<CardView>();

        //private EditText EditTextISBNNumber;
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
        private LinearLayout AdDisplayer;
        private Json.Ad NewAd = new Json.Ad();
        private Bitmap CurrentProductImage;

        private List<Java.IO.File> _images;

        //private List<AdItem>


        protected override int LayoutSetter()
        {
            return Resource.Layout.DialogueFragmentTakePictureLayout;
        }

        protected override void InitiateFragment()
        {
            _Ad = new Json.Ad();
            _Ad.aditems = new List<Json.Aditem>();
            _Ad.user = new Json.User();
            _Ad.user.username = SavedValues.UserValues.GetValueFromToken(CallerActivity.sP, AndroidJsonHelpers.AndroidJsonHelper.UserValuesEnums.username);
            _Ad.user.firstname = SavedValues.UserValues.GetValueFromToken(CallerActivity.sP, AndroidJsonHelpers.AndroidJsonHelper.UserValuesEnums.firstname);
            _Ad.user.lastname = SavedValues.UserValues.GetValueFromToken(CallerActivity.sP, AndroidJsonHelpers.AndroidJsonHelper.UserValuesEnums.lastname);

            SetButtonValues();
            SetButtonValuesISBNScanCard();
            SetButtonValuesLastCard();
            SetButtonValuesAdCard();
            AddAllCards();
            GoToCard(AdsCard);

            PopulateSpinner(SchoolSpinner, schoolnamesarr1);
            PopulateSpinner(ClassSpinner, ClassNamesarr);
        }

        private void AddAllCards()
        {
            allCards.Add(AdsCard);
            allCards.Add(ChooseItemTypeCard);
            allCards.Add(TakePictureCard);
            allCards.Add(ISBNScanCard);
            allCards.Add(ISBNDescriptionCard);
            allCards.Add(LastCard);
            allCards.Add(PublishCard);
        }


        /// <summary>
        /// Finds and sets the values on this activities buttons and other views
        /// </summary>
        protected void SetButtonValues()
        {
            //Gets the cards
            AdsCard = Dialogueview.FindViewById<CardView>(Resource.Id.cardViewAds);
            TakePictureCard = Dialogueview.FindViewById<CardView>(Resource.Id.cardViewPicture);
            ChooseItemTypeCard = Dialogueview.FindViewById<CardView>(Resource.Id.cardViewItemType);
            ISBNScanCard = Dialogueview.FindViewById<CardView>(Resource.Id.cardViewScanner);
            ISBNDescriptionCard = Dialogueview.FindViewById<CardView>(Resource.Id.cardViewISBNDescription);
            LastCard = Dialogueview.FindViewById<CardView>(Resource.Id.cardViewLast);
            PublishCard = Dialogueview.FindViewById<CardView>(Resource.Id.cardViewPublish);

            //LastCard
            ProductNameText = Dialogueview.FindViewById<EditText>(Resource.Id.ProductNameText);
            ProductDescriptionText = Dialogueview.FindViewById<EditText>(Resource.Id.ProductDescriptionText);
            ProductPriceText = Dialogueview.FindViewById<EditText>(Resource.Id.ProductPriceText);
            AdPackNameText = Dialogueview.FindViewById<EditText>(Resource.Id.AdPackNameText);
            AdPackDescriptionText = Dialogueview.FindViewById<EditText>(Resource.Id.AdPackDescriptionText);

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
            NextStepButtonNew.Click += delegate {
                NextStepButtonAction();
            };
            //Itemtype choice card
            Button setAsBookButton = Dialogueview.FindViewById<Button>(Resource.Id.SetItemAsBookButton);
            // setAsBookButton.Click += SetItemAsNewBook;
     
            setAsBookButton.Click += delegate {
                NewitemType = ItemType.Book;
                NextStepButtonAction();
            };


            Button setAsOtherButton = Dialogueview.FindViewById<Button>(Resource.Id.SetItemAsOtherButton);

            setAsOtherButton.Click += delegate {
                NewitemType = ItemType.Other;
                NextStepButtonAction();
            };

            Button addNewProduktButton = Dialogueview.FindViewById<Button>(Resource.Id.NewProductButton);
            addNewProduktButton.Click += AddNewProdukt;
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
        }

        private void DeleteAdItem(Aditem item, AdMiniature adMiniature)
        {
            if (adMiniature == null)
            {
                System.Diagnostics.Debug.WriteLine("Den er null...");

            }else
            {
                System.Diagnostics.Debug.WriteLine("Den er ikke null");
            }

            for (int i = 0; i < AdDisplayer.ChildCount; i++)
            {
                if (_AllAdMiniatures[i] == adMiniature)
                {
                    _AllAdMiniatures.Remove(_AllAdMiniatures[i]);
                    System.Diagnostics.Debug.WriteLine("Fjerner 1");
                    AdDisplayer.RemoveView(AdDisplayer.GetChildAt(i));
                }
            } 
        

        adMiniature.RemoveAllViews();
            _SelectedAdminiature = adMiniature;
            AdDisplayer.RemoveView(adMiniature.RootView);
            // AdDisplayer.RemoveAllViewsInLayout();
           // AdDisplayer.RemoveAllViews();

            foreach (Aditem a in _Ad.aditems)
            {
                if (item == a)
                {
                    _Ad.aditems.Remove(a);
                    return;
                }
            }

         
        }

     

        private void AddNewProdukt(object sender, EventArgs e)
        {
            _imageView.SetImageBitmap(null);
      
            ProductNameText.Text = "";
            ProductDescriptionText.Text = "";
            ProductPriceText.Text = "";

            GoToCard(ChooseItemTypeCard);
            Json.Aditem newProdukt = new Json.Aditem();
       
            
            _Ad.aditems.Add(newProdukt);
            
            _SelectedProduct = newProdukt;
            AdItemClasses.AdMiniature newAdminiature = new AdItemClasses.AdMiniature(Context, AdDisplayer, newProdukt, _Ad);
            _AllAdMiniatures.Add(newAdminiature);
            _SelectedAdminiature = newAdminiature;
            _SelectedAdminiature.InitiateShowInterestButton("Slett", true);
            _SelectedAdminiature.GetShowInterestButton().Click += delegate {
                DeleteAdItem(newProdukt, newAdminiature);
                _SelectedAdminiature.RemoveFromParent();
            };
            _SelectedAdminiature.GetButton().Click += delegate {
                ShowProduct(newProdukt, newAdminiature);
            };


            //  fillList(_Ad);
        }

        private void ShowProduct(Json.Aditem product, AdItemClasses.AdMiniature adminiature)
        {
            SetProductInfo(product);
            _SelectedProduct = product;
            _SelectedAdminiature = adminiature;
            GoToCard(ChooseItemTypeCard);
        }

        private void SetProductInfo(Json.Aditem product)
        {
            ProductNameText.Text = product.text;
            ProductDescriptionText.Text = product.description;
            ProductPriceText.Text = product.price.ToString();
            if (product.isbn != null)
            {
                ManualISBNInput.Text = product.isbn.ToString();
            }
            else
            {
                ManualISBNInput.Text = "";
            }
          
            int height = Resources.DisplayMetrics.HeightPixels;
            int width = _imageView.Height;

            if (product.image != null)
            {
                try
                {
                    Bitmap myBitmap = BitmapFactory.DecodeByteArray(ObjectToByteArray(product.image), height, width);
                    CurrentProductImage = myBitmap;
                    _imageView.SetImageBitmap(myBitmap);
                }
                catch
                {
                    _imageView.SetImageBitmap(null);
                }
            }
        }

        private byte[] ObjectToByteArray(object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        private void fillList(Json.Ad ad)
        {
            foreach (Json.Aditem a in _Ad.aditems)
            {
                AdItemClasses.AdMiniature AdPackDisplay1 = new AdItemClasses.AdMiniature(Context, AdDisplayer, a, ad);

            }
        }

        public void setProductImage()
        {
            int height = Resources.DisplayMetrics.HeightPixels;
            int width = _imageView.Height;
            App.bitmap = App._file.Path.LoadAndResizeBitmap(width, height);

            if (App.bitmap != null)
            {
                CurrentProductImage = App.bitmap;
                _imageView.SetImageBitmap(App.bitmap);
                _SelectedAdminiature.setImage(App.bitmap);
                _SelectedProduct.image = new AdItemImage();
                _SelectedProduct.image.imagefile = App._file;
                _SelectedProduct.image.file = App.bitmap;
                //_SelectedProduct.image.bitmapimage = App.bitmap;
                //App.bitmap.CompressAsync(Bitmap.CompressFormat.Png, 85, outStream);
                App.bitmap = null;
            }
        }

        // private async void GetNewestAdsFromDatabase()
        //  {
        //  Json.RootObject root = await JsonDownloader.GetItemsFromDatabase();
        //   if (root != null)
        //   {
        //      AddItems(root);
        //    }

        //   }

        private async Task UploadImages(Ad ad)
        {
            foreach (Aditem ai in ad.aditems)
            {
                int i = 1;

                if (ai.image.file != null)
                {
                    //string Response = 
                        await Task.Run(() => Json.JsonUploader.upload(SavedValues.UserValues.GetSavedToken(CallerActivity.sP), ai.image));
                //    System.Diagnostics.Debug.WriteLine(Response + "Dette er resultatet ny");
                }

                // System.Net.Http.HttpResponseMessage Response = await Task.Run(() => Json.JsonUploader.AddNewImage(SavedValues.UserValues.GetSavedToken(CallerActivity.sP), ai.image));
                // PoppupDialogueFragment APDF = new PoppupDialogueFragment();
                //  if (Response.IsSuccessStatusCode)
                // {
                //     APDF.Show(CallerActivity.SupportFragmentManager, "dialog", CallerActivity, "Lastet opp bilde", true);
                // }
                // else
                // {
                //     APDF.Show(CallerActivity.SupportFragmentManager, "dialog", CallerActivity, "Noe gikk galt med bilde", false);
                //  }
 
                ai.image.imageid = i;
            }

            //return i;
        }

        private void AddItems(Json.RootObject root)
        {
            foreach (Json.Ad a in root.ads)
            {
                AdItemClasses.AdMiniature AdPackDisplay1 = new AdItemClasses.AdMiniature(Context, AdDisplayer, a);
            }
        }

        protected void SetButtonValuesAdCard()
        {
           AdDisplayer = Dialogueview.FindViewById<LinearLayout>(Resource.Id.AdDisplayer);
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

        private void NextStepButtonAction()
        {

            if (GetCurrentActiveCard() == ChooseItemTypeCard)
            {
                if (NewitemType == ItemType.Book)
                {
                    GoToCard(ISBNScanCard);
                }
                else
                {
                    //GoToCard(LastCard);
                    GoToCard(TakePictureCard);
                }

            
            }
            else if (GetCurrentActiveCard() == AdsCard)
            {
                ValidationResponder = InputValidator.validGeneralName(AdPackNameText.Text);
                if (!ValidationResponder.Successful)
                {
                    ShowToast(ValidationResponder.Information);
                    return;
                }
                ValidationResponder = InputValidator.validGeneralDescription(AdPackDescriptionText.Text);
                if (!ValidationResponder.Successful)
                {
                    ShowToast(ValidationResponder.Information);
                    return;
                }
                Publish(_Ad);
                CloseFragment();

                //GoToCard(PublishCard);
            }
            else if (GetCurrentActiveCard() == TakePictureCard)
            {
                ValidationResponder = InputValidator.validAdItemImage(CurrentProductImage);
                if (!ValidationResponder.Successful)
                {
                    ShowToast(ValidationResponder.Information);
                    return;
                }

                GoToCard(LastCard);
            }
            else if (GetCurrentActiveCard() == ISBNScanCard)
            {
                _SelectedProduct.isbn = ManualISBNInput.Text;

                GoToNextStepOfAddBook(ManualISBNInput.Text);
                //  GoToCard(TakePictureCard);
            }
            else if (GetCurrentActiveCard() == ISBNDescriptionCard)
            {
                //    GoToCard(ISBNScanCard);
            }
            else if (GetCurrentActiveCard() == LastCard)
            {
                ValidationResponder = InputValidator.validAdItemPrice(ProductPriceText.Text);
                if (!ValidationResponder.Successful)
                {
                    ShowToast(ValidationResponder.Information);
                    return;
                }

                ValidationResponder = InputValidator.validGeneralDescription(ProductDescriptionText.Text);
                if (!ValidationResponder.Successful)
                {
                    ShowToast(ValidationResponder.Information);
                    return;
                }

                ValidationResponder = InputValidator.validGeneralName(ProductNameText.Text);
                if (!ValidationResponder.Successful)
                {
                    ShowToast(ValidationResponder.Information);
                    return;
                }


                _SelectedProduct.text = ProductNameText.Text;
                _SelectedProduct.description = ProductDescriptionText.Text;
                _SelectedProduct.price = Int32.Parse(ProductPriceText.Text);
                _SelectedAdminiature.SetValues(_SelectedProduct);
                GoToCard(AdsCard);
                // _SelectedProduct.
                //UploadNewBook();
            }
            else if (GetCurrentActiveCard() == PublishCard)
            {
               // _Ad.adname = name
               Publish(_Ad);
                Console.WriteLine("Publishes");
            }
        }


        private void PrevStepImageAction()
        {
            if (GetCurrentActiveCard() == ChooseItemTypeCard)
            {
                GoToCard(AdsCard);
            }
            else if (GetCurrentActiveCard() == AdsCard)
            {
                CloseFragment();
            }
            else if (GetCurrentActiveCard() == TakePictureCard)
            {
                if (NewitemType == ItemType.Book)
                {
                    GoToCard(ISBNScanCard);
                }
                else
                {
                    GoToCard(ChooseItemTypeCard);
                }
            } else if (GetCurrentActiveCard() == ISBNScanCard)
            {
                GoToCard(ChooseItemTypeCard);
            } else if (GetCurrentActiveCard() == ISBNDescriptionCard)
            {
                GoToCard(ISBNScanCard);
            }
            else if (GetCurrentActiveCard() == LastCard)
            {
                    GoToCard(TakePictureCard);

            }else if (GetCurrentActiveCard() == PublishCard)
            {
                GoToCard(AdsCard);
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
            ValidationResponder = InputValidator.validISBN(isbn);
            if (!ValidationResponder.Successful)
            {
                ShowToast(ValidationResponder.Information);
                return;
            }
            GoToCard(TakePictureCard);
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
                NextStepButtonNew.Text = "Neste";
            }
            else if (GetCurrentActiveCard() == ISBNScanCard)
            {
                NextStepButtonNew.Visibility = ViewStates.Visible;
                NextStepButtonNew.Text = "Neste";
            }
            else if (GetCurrentActiveCard() == ISBNDescriptionCard)
            {
                NextStepButtonNew.Visibility = ViewStates.Gone;

            } else if (GetCurrentActiveCard() == AdsCard)
            {
                NextStepButtonNew.Text = "Publiser";
               // GetNewestAdsFromDatabase();
                NextStepButtonNew.Visibility = ViewStates.Visible;
            }
            else if (GetCurrentActiveCard() == PublishCard)
            {
                NextStepButtonNew.Text = "Publiser";
                // GetNewestAdsFromDatabase();
                NextStepButtonNew.Visibility = ViewStates.Visible;
            }
        }

        private async void Publish(Ad ad)
        {
            await UploadImages(ad);


            if (AdPackNameText != null && AdPackDescriptionText != null)
            {
                ad.adname = AdPackNameText.Text;
                ad.text = AdPackDescriptionText.Text;
            }
            else
            {
                ad.adname = "Fak";
                ad.text = "Shit";
            }

            HttpResponseMessage resp = await JsonUploader.AddNewAdPack(SavedValues.UserValues.GetSavedToken(CallerActivity.sP), ad);
            PoppupDialogueFragment APDF = new PoppupDialogueFragment();
            if (resp.IsSuccessStatusCode)
            {
                APDF.Show(CallerActivity.SupportFragmentManager, "dialog", CallerActivity, "Lastet opp", true);
            }else
            {
                APDF.Show(CallerActivity.SupportFragmentManager, "dialog", CallerActivity, "Noe gikk galt", false);
            }
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
    }
}