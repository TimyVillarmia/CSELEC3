using UnitConverter.MVVM.ViewModels;

namespace UnitConverter.MVVM.Views;

public partial class ConverterView : ContentPage
{
	public ConverterView()
	{
		InitializeComponent();
	}

    private void Picker_SelectedIndexChanged(object sender, EventArgs e)
    {
        var viewModel = (ConverterViewModel)BindingContext;

        viewModel.Convert();
    }
}