using System.ComponentModel;
using System.Diagnostics;

namespace hangman
{
    public partial class MainPage : ContentPage, INotifyPropertyChanged
    {
        #region UI Properties
        public string Spotlights
        {
            get => spotlights;
            set
            {
                spotlights = value;
                OnPropertyChanged();
            }

        }

        public List<char> Letters
        {
            get => letters;
            set
            {
                letters = value;
                OnPropertyChanged();
            }
        }

        public string Message
        {
            get => message;
            set
            {
                message = value;
                OnPropertyChanged();
            }
        }

        public string GameStatusImage
        {
            get => gameStatusImage;
            set
            {
                gameStatusImage = value;
                OnPropertyChanged();
            }
        }

        public string GameStatus
        {
            get => gameStatus; set
            {
                gameStatus = value;
                OnPropertyChanged();
            }
        }
        public string CurrentImage
        {
            get => currentImage;
            set
            {
                currentImage = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Fields
        public Dictionary<string, string[]> words = new Dictionary<string, string[]>
        {
            { "Animals", new string[]
                {
                    "elephant", "giraffe", "kangaroo", "dolphin", "tiger",
                    "penguin", "octopus", "panda", "rhinoceros", "leopard",
                    "hippopotamus", "chimpanzee", "armadillo", "crocodile", "koala",
                    "albatross", "bison", "buffalo", "capybara", "zebra"
                }
            },
            { "Fruits", new string[]
                {
                    "apple", "banana", "orange", "strawberry", "pineapple",
                    "grapes", "mango", "papaya", "watermelon", "peach",
                    "plum", "cherry", "apricot", "avocado", "blueberry",
                    "coconut", "dragonfruit", "fig", "grapefruit", "kiwi"
                }
            },
            { "Countries", new string[]
                {
                    "canada", "brazil", "france", "germany", "india",
                    "japan", "australia", "nigeria", "egypt", "italy",
                    "argentina", "mexico", "china", "russia", "turkey",
                    "greece", "peru", "thailand", "vietnam", "kenya"
                }
            },
            { "Space", new string[]
                {
                    "planet", "galaxy", "asteroid", "comet", "nebula",
                    "black hole", "supernova", "meteor", "orbit", "solar system",
                    "milky way", "saturn", "jupiter", "mars", "venus",
                    "gravity", "telescope", "constellation", "astronaut", "rocket"
                }
            },
            { "Professions", new string[]
                {
                    "doctor", "engineer", "teacher", "lawyer", "chef",
                    "artist", "scientist", "architect", "pilot", "nurse",
                    "firefighter", "dentist", "musician", "writer", "pharmacist",
                    "mechanic", "carpenter", "electrician", "veterinarian", "plumber"
                }
            }
        };

        public List<string> categories => words.Keys.ToList();
        public event PropertyChangedEventHandler propertyChanged;
        //List<string> words = new List<string>(){
        //    "python",
        //    "javascript",
        //    "maui",
        //    "csharp",
        //    "mongodb",
        //    "sql",
        //    "xaml",
        //    "word",
        //    "excel",
        //    "powerpoint",
        //    "code",
        //    "hotreload",
        //    "snippets"
        //};
        string answer = "";
        private string message = "";
        private string spotlights;
        List<char> guessed = new List<char>();
        private List<char> letters = new List<char>();
        int mistakes = 0;
        int maxWrong = 6;
        private string gameStatus;
        private string gameStatusImage = "mistake0.png";
        private string currentImage = "img0.jpg";
        public string currentCategory;

        #endregion


        public MainPage()
        {
            InitializeComponent();
            Letters.AddRange("abcdefghijklmanopqrustuvwxyz");
            BindingContext = this;

            CategoryPicker.SelectedIndex = 0;
            currentCategory = CategoryPicker.SelectedItem.ToString();
            PickWord();
            CalculateWord(answer, guessed);
            UpdateStatus();
            CurrentImage = "img0.jpg";
            gameStatusImage = "mistake0.png";


        }

        #region Game Engine
        private void PickWord()
        {
            answer = words[currentCategory][new Random().Next(words.Count)];
            Debug.WriteLine(answer);
        }
        private void CalculateWord(string answer, List<char> guessed)
        {
            var temp = answer.Select(x => (guessed.IndexOf(x) >= 0 ? x : '_')).ToArray();
            Spotlights = string.Join(" ", temp);
        }
        #endregion

        private void UpdateStatus()
        {
            GameStatus = $"{mistakes}/{maxWrong}";
            GameStatusImage = $"mistake{mistakes}.png";

        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var btn = sender as Button;
            if (btn != null)
            {
                var letter = btn.Text;
                btn.IsEnabled = false;
                HandleGuess(letter[0]);
            }
        }

        private void HandleGuess(char letter)
        {
            if (guessed.IndexOf(letter) == -1)
            {
                guessed.Add(letter);
            }
            if (answer.IndexOf(letter) >= 0)
            {
                CalculateWord(answer, guessed);
                CheckIfGameWon();
            }
            else if (answer.IndexOf(letter) == -1)
            {
                mistakes++;
                UpdateStatus();
                CheckIfGameLost();
                CurrentImage = $"img{mistakes}.jpg";
            }
        }

        private void CheckIfGameLost()
        {
            if (mistakes >= maxWrong)
            {
                Message = "You Lost!!";
                DisableLetters();
            }
        }

        private void DisableLetters()
        {
            foreach (var children in LettersContainer.Children)
            {
                var btn = children as Button;
                if (btn != null)
                {
                    btn.IsEnabled = false;
                }
            }
        }

        private void CheckIfGameWon()
        {
            if (Spotlights.Replace(" ", "") == answer)
            {
                Message = "You win!!";
                DisableLetters();
            }
        }

        private void Reset_Clicked(object sender, EventArgs e)
        {
            mistakes = 0;
            guessed = new List<char>();
            CurrentImage = "img0.jpg";
            PickWord();
            CalculateWord(answer, guessed);
            Message = "";
            UpdateStatus();
            EnableLetters();
        }

        private void EnableLetters()
        {
            foreach (var children in LettersContainer.Children)
            {
                var btn = children as Button;
                if (btn != null)
                {
                    btn.IsEnabled = true;
                }
            }
        }

        private void CategoryPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentCategory = CategoryPicker.SelectedItem.ToString();
            mistakes = 0;
            guessed = new List<char>();
            CurrentImage = "img0.png";
            PickWord();
            CalculateWord(answer, guessed);
            Message = "";
            UpdateStatus();
            EnableLetters();

            HintBtn.IsEnabled = true;
        }

        private void HintBtn_Clicked(object sender, EventArgs e)
        {
            char hint = GetHint();
            if (mistakes < 4)
            {
                HandleGuess(hint);
                foreach (var children in LettersContainer.Children)
                {
                    var btn = children as Button;
                    if (btn!.Text[0] == hint)
                    {
                        btn.IsEnabled = false;
                    }
                }
                mistakes += 2;
                UpdateStatus();
                CurrentImage = $"img{mistakes}.png";
                if (mistakes == 4)
                {
                    HintBtn.IsEnabled = false;
                }
            }
        }

        private char GetHint()
		{
			List<char> charHints = new List<char>();
            foreach(char x in answer)
			{
				if (!spotlights.Contains(x))
				{
					charHints.Add(x);
				}
			}

			return charHints[new Random().Next(0, charHints.Count)];
        }
    
    }

}
