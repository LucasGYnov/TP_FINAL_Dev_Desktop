namespace GestionnaireLivresMAUI;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    // Cette méthode gère le tap sur un livre pour afficher les détails (Bonus)
    private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Models.Livre livre)
        {
            string statut = livre.Lu ? "Déjà lu" : "À lire";
            
            // On utilise DisplayAlert pour afficher la pop-up de détails
            await DisplayAlert(livre.Titre, $"Auteur: {livre.Auteur}\nGenre: {livre.Genre}\nStatut: {statut}", "OK");
            
            // On déselectionne l'item pour pouvoir recliquer dessus plus tard
            ((CollectionView)sender).SelectedItem = null;
        }
    }
}