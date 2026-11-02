using Counter.Services;
using Counter.Models;

namespace Counter;

public partial class MainPage : ContentPage
{
    private List<CounterItem> counters = new List<CounterItem>();
    private CounterStorage storage = new CounterStorage();

    public MainPage()
    {
        InitializeComponent();

        counters = storage.LoadCounters();

        foreach (CounterItem counter in counters)
        {
            CreateCounterView(counter);
        }
    }

    private void AddCounter_Clicked(object sender, EventArgs e)
    {
        string name = NameEntry.Text;

        if (string.IsNullOrWhiteSpace(name))
        {
            DisplayAlert("Error", "Enter counters name.", "OK");
            return;
        }

        int initialValue = 0;

        if (!int.TryParse(ValueEntry.Text, out initialValue))
        {
            DisplayAlert("Error", "Invalid value.", "OK");
            return;
        }

        string colorName = ColorPicker.SelectedItem.ToString();

        CounterItem newCounter = new CounterItem(name, initialValue, initialValue, colorName);

        counters.Add(newCounter);

        CreateCounterView(newCounter);
        storage.SaveCounters(counters);

        NameEntry.Text = "";
        ValueEntry.Text = "0";
    }

    private void CreateCounterView(CounterItem counter)
    {
        Label nameLabel = new Label
        {
            Text = counter.Name,
            FontSize = 20,
            FontAttributes = FontAttributes.Bold
        };

        Label valueLabel = new Label
        {
            Text = counter.Value.ToString(),
            FontSize = 24,
            VerticalOptions = LayoutOptions.Center
        };

        Button minusButton = new Button
        {
            Text = "-",
            FontSize = 20,
            BindingContext = counter,
            BackgroundColor = Colors.Red,
            TextColor = Colors.White
        };

        Button plusButton = new Button
        {
            Text = "+",
            FontSize = 20,
            BindingContext = counter,
            BackgroundColor = Colors.Green,
            TextColor = Colors.White
        };

        Button resetButton = new Button
        {
            Text = "Reset",
            FontSize = 20,
            BindingContext = counter,
            BackgroundColor = Colors.Red,
            TextColor = Colors.White
        };

        Button deleteButton = new Button
        {
            Text = "Delete",
            FontSize = 20,
            BindingContext = counter,
            BackgroundColor = Colors.Red,
            TextColor = Colors.White
        };

        minusButton.Clicked += DecreaseCounter_Clicked;
        plusButton.Clicked += IncreaseCounter_Clicked;
        resetButton.Clicked += ResetCounter_Clicked;
        deleteButton.Clicked += DeleteButton_Clicked;

        HorizontalStackLayout controls = new HorizontalStackLayout
        {
            Spacing = 10,
            Children =
            {
                valueLabel,
                minusButton,
                plusButton,
                resetButton,
                deleteButton
            }
        };

        VerticalStackLayout counterLayout = new VerticalStackLayout
        {
            Padding = 10,
            Spacing = 5,
            Children =
            {
                nameLabel,
                controls
            }
        };

        Border counterBorder = new Border
        {
            Stroke = Colors.Gray,
            StrokeThickness = 1,
            Padding = 5,
            Background = new SolidColorBrush(GetCounterColor(counter.ColorName)),
            Content = counterLayout
        };

        CountersLayout.Children.Add(counterBorder);
    }

    private Color GetCounterColor(string colorName)
    {
        switch (colorName)
        {
            case "Blue":
                return Colors.DarkBlue;

            case "Green":
                return Colors.DarkGreen;

            case "Red":
                return Colors.DarkRed;

            case "Purple":
                return Colors.Magenta;

            default:
                return Colors.DarkGray;
        }
    }

    private void IncreaseCounter_Clicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;

        CounterItem counter = (CounterItem)button.BindingContext;

        counter.Value++;

        HorizontalStackLayout row = (HorizontalStackLayout)button.Parent;

        Label valueLabel = (Label)row.Children[0];

        valueLabel.Text = counter.Value.ToString();

        storage.SaveCounters(counters);
    }

    private void DecreaseCounter_Clicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;

        CounterItem counter = (CounterItem)button.BindingContext;

        counter.Value--;

        HorizontalStackLayout row = (HorizontalStackLayout)button.Parent;

        Label valueLabel = (Label)row.Children[0];

        valueLabel.Text = counter.Value.ToString();

        storage.SaveCounters(counters);
    }

    private void ResetCounter_Clicked(Object sender, EventArgs e)
    {
        Button button = (Button)sender;

        CounterItem counter = (CounterItem)button.BindingContext;

        counter.Value = counter.InitialValue;

        HorizontalStackLayout row = (HorizontalStackLayout)button.Parent;

        Label valueLabel = (Label)row.Children[0];

        valueLabel.Text = counter.Value.ToString();

        storage.SaveCounters(counters);
    }

    private void DeleteButton_Clicked(Object sender, EventArgs e)
    {
        Button button = (Button)sender;

        CounterItem counter = (CounterItem)button.BindingContext;

        counters.Remove(counter);

        Border counterBorder = (Border)button.Parent.Parent.Parent;

        CountersLayout.Children.Remove(counterBorder);

        storage.SaveCounters(counters);
    }
}