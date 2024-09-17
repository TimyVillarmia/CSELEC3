
namespace CSELE3_MIDTERM_LAB_EX_1
{
    public partial class MainPage : ContentPage
    {
        private static readonly Random rand = new Random();

        public MainPage()
        {
            InitializeComponent();
            weapon.TranslationX = -50;


        }


        private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {

            var red = sldRed.Value;
            var green = sldGreen.Value;
            var blue = sldBlue.Value;
            weapon.TranslationX = -50 + (red * 100);
            Color color = Color.FromRgb(red, green, blue);

            SetColor(color);



        }

        private void SetColor(Color color)
        {

            sldRed.Value = color.Red;
            sldGreen.Value = color.Green;
            sldBlue.Value = color.Blue;


            Container.BackgroundColor = color;
            entryHex.Text = $"{color.ToHex()}";
        }

        private void btnRandom_Clicked(object sender, EventArgs e)
        {
            var random = Color.FromRgb(rand.Next(256), rand.Next(256), rand.Next(256));            

            SetColor(random);
        }


        private void entryHex_Completed(object sender, EventArgs e)
        {

            var userHex = Color.FromArgb(((Entry)sender).Text);
            SetColor(userHex);
        }


    }

}
