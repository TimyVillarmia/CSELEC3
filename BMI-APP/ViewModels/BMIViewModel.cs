using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMI_APP.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    class BMIViewModel
    {
        public double Height { get; set; } = 100;
        public double Weight { get; set; } = 30;

        public double BMIScore => Math.Round(Weight / Math.Pow((Height / 100), 2), 1);

       
        public string BMIResult
        {

            get
            {
                string template = "BMI: #";
                /*
                 Underweight < 18.5
                 Normal	18.5 to 25
                 Overweight	25 to 30
                 Obesity 30 to 40
                 */
                if (BMIScore <= 18.5)
                    return template.Replace("#", "Underweight");
                else if (BMIScore < 25)
                    return template.Replace("#", "Normal");
                else if (BMIScore < 30)
                    return template.Replace("#", "Overweight");
                else
                    return template.Replace("#", "Obesity");
            }
        }


    }
}
