namespace Calculeur_Age.ViewModels;

// Contient l'ÉTAT de l'écran et les ACTIONS possibles.
public class CalculateurViewModel : BaseViewModel
{
    // Champs privés : la vraie donnée.
    private string _nom = "";
    private DateTime _dateNaissance = DateTime.Today.AddYears(-20);
    private string _resultat = "";
    private string _message = "";          // AJOUT 1 : Majeur / Mineur
    private bool _resultatVisible;

    // Propriétés publiques : ce que le XAML voit.
    public string Nom
    {
        get => _nom;
        set
        {
            if (SetField(ref _nom, value))
                CalculerCommand.Rafraichir();
        }
    }

    public DateTime DateNaissance
    {
        get => _dateNaissance;
        set
        {
            if (SetField(ref _dateNaissance, value))
            {
                // AJOUT 3 : la date a changé, on réévalue si elle est dans le futur
                OnPropertyChanged(nameof(DateFuture));
                CalculerCommand.Rafraichir();
            }
        }
    }

    public string Resultat
    {
        get => _resultat;
        set => SetField(ref _resultat, value);
    }

    // AJOUT 1 : texte "Majeur" ou "Mineur"
    public string Message
    {
        get => _message;
        set => SetField(ref _message, value);
    }

    public bool ResultatVisible
    {
        get => _resultatVisible;
        set => SetField(ref _resultatVisible, value);
    }

    // AJOUT 3 : propriété calculée, vraie si la date de naissance est dans le futur
    public bool DateFuture => DateNaissance.Date > DateTime.Today;

    // Liées aux Button.Command dans le XAML.
    public RelayCommand CalculerCommand { get; }
    public RelayCommand EffacerCommand { get; }   // AJOUT 2

    public CalculateurViewModel()
    {
        // AJOUT 3 : le bouton est aussi grisé si la date est dans le futur
        CalculerCommand = new RelayCommand(
            Calculer,
            () => !string.IsNullOrWhiteSpace(Nom) && !DateFuture);

        EffacerCommand = new RelayCommand(Effacer);
    }

    // Logique métier : aucun contrôle d'interface ici.
    private void Calculer()
    {
        int age = DateTime.Today.Year - DateNaissance.Year;
        if (DateNaissance.Date > DateTime.Today.AddYears(-age)) age--;

        Resultat = $"{Nom}, vous avez {age} ans";
        Message = age >= 18 ? "Majeur" : "Mineur";   // AJOUT 1
        ResultatVisible = true;
    }

    // AJOUT 2 : remet tous les champs à zéro
    private void Effacer()
    {
        Nom = "";
        DateNaissance = DateTime.Today.AddYears(-20);
        Resultat = "";
        Message = "";
        ResultatVisible = false;
    }
}
