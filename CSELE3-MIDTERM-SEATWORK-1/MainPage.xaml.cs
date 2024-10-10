using System;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

namespace CSELE3_MIDTERM_SEATWORK_1
{

    public partial class MainPage : ContentPage
    {
        double TotalPerPerson;
        double TipPercentage = 0.00;
        double TipPerPerson;
        double SubTotalPerPerson;
        int count = 1;

        double Bill;
        int CustomTip = 0;



        public MainPage()
        {
            InitializeComponent();
            txtNoPersons.Text = count.ToString();
        }


        private void btnPlus_Clicked(object sender, EventArgs e)
        {
            txtNoPersons.Text = $"{count++}";
            Calculate();

        }

        private void txtBill_Completed(object sender, EventArgs e)
        {
            Entry bill = (Entry)sender;
            Bill = Convert.ToDouble(bill.Text);
            Debug.WriteLine(Bill);
            Calculate();
        }

        bool CustomTipState = false;
        private void Button_Clicked(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            if (btn.Text == "10%")
            {
                TipPercentage = .10;
            }
            else if (btn.Text == "15%")
            {
                TipPercentage = .15;
            }
            else if(btn.Text == "20%")
            {
                TipPercentage = .20;
            }
            else
            {
                //
                CustomTipState = !CustomTipState;
                customTipContainer.IsVisible = CustomTipState;

                TipPercentage = 0;
                CustomTip = 0;

                sldTip.IsEnabled = !CustomTipState!;
                btn10.IsEnabled = !CustomTipState;
                btn15.IsEnabled = !CustomTipState;
                btn20.IsEnabled = !CustomTipState;
               
            }

            Calculate();
        }

        private void txtTip_Completed(object sender, EventArgs e)
        {
            Entry entry = (Entry)sender;


            CustomTip = Convert.ToInt32(entry.Text);
            Calculate();
        }

        private void sldTip_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            TipPercentage = sldTip.Value;
            lblTip.Text = $"Tip: {TipPercentage.ToString("0")}%";
            Calculate();
        }

        private void btnMinus_Clicked(object sender, EventArgs e)
        {
            if (count > 1)
            {
                count--;

            }
            txtNoPersons.Text = $"{count}";
            Calculate();

        }

        private void txtNoPersons_Completed(object sender, EventArgs e)
        {
            Entry entry = (Entry)sender;

            if (entry.Text == "0")
            {
                count = 1;
                return;
            }
            count = Convert.ToInt32(entry.Text);
            Calculate();
        }

        public void Calculate()
        {
            SubTotalPerPerson = Bill / count;
            TipPerPerson = (CustomTip + (Bill * TipPercentage / 100)) / count;
            TotalPerPerson = SubTotalPerPerson + TipPerPerson;

            Debug.WriteLine(SubTotalPerPerson);
            Debug.WriteLine(TipPerPerson);
            Debug.WriteLine(TotalPerPerson);
            txtNoPersons.Text = $"{count}";


            lblSubTotal.Text = SubTotalPerPerson.ToString("0.00");
            lblTipPerPerson.Text = TipPerPerson.ToString("0.00");
            lblTotalPerPerson.Text = TotalPerPerson.ToString("0.00");

        }


    }

}
