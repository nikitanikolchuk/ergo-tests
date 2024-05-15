using System.Windows.Markup;

namespace TestAdministration.Views.Testing;

public partial class NewPatient
{
    public NewPatient()
    {
        InitializeComponent();
        
        DatePicker.Language = XmlLanguage.GetLanguage("cs");
    }
}