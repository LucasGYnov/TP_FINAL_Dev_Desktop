namespace GestionnaireLivresMAUI;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Models.Livre livre)
        {
            string statut = livre.Lu ? "Déjà lu" : "À lire";
            
            await DisplayAlert(livre.Titre, $"Auteur: {livre.Auteur}\nGenre: {livre.Genre}\nStatut: {statut}", "OK");
            
            ((CollectionView)sender).SelectedItem = null;
        }
    }
}