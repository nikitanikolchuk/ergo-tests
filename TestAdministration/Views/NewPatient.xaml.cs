using System.Windows.Markup;

namespace TestAdministration.Views;

public partial class NewPatient
{
    public NewPatient()
    {
        InitializeComponent();
        
        DatePicker.Language = XmlLanguage.GetLanguage("cs");
    }
}