using Calculeur_Age.Views;

namespace Calculeur_Age;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        // Déclare la route : sans cette ligne, GoToAsync lève une exception "route inconnue".
        Routing.RegisterRoute(nameof(ResultatPage), typeof(ResultatPage));
    }
}
