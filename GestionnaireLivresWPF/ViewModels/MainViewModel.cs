using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using GestionnaireLivresWPF.Data;
using GestionnaireLivresWPF.Models;

namespace GestionnaireLivresWPF.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly LivreRepository _repository;

        // --- Collections ---
        public ObservableCollection<Livre> Livres { get; set; } = new ObservableCollection<Livre>();

        // --- Propriétés du formulaire ---
        private string _titre = "";
        public string Titre { get => _titre; set => SetProperty(ref _titre, value); }

        private string _auteur = "";
        public string Auteur { get => _auteur; set => SetProperty(ref _auteur, value); }

        private int _annee = DateTime.Now.Year;
        public int Annee { get => _annee; set => SetProperty(ref _annee, value); }

        private string _genre = "Autre";
        public string Genre { get => _genre; set => SetProperty(ref _genre, value); }

        private bool _lu;
        public bool Lu { get => _lu; set => SetProperty(ref _lu, value); }

        // --- Sélection et Recherche ---
        private Livre? _livreSelectionne;
        public Livre? LivreSelectionne
        {
            get => _livreSelectionne;
            set
            {
                if (SetProperty(ref _livreSelectionne, value) && value != null)
                {
                    // Remplissage du formulaire lors de la sélection
                    Titre = value.Titre;
                    Auteur = value.Auteur;
                    Annee = value.Annee;
                    Genre = value.Genre;
                    Lu = value.Lu;
                }
            }
        }

        private string _termeRecherche = "";
        public string TermeRecherche
        {
            get => _termeRecherche;
            set
            {
                if (SetProperty(ref _termeRecherche, value))
                {
                    EffectuerRecherche(); // Recherche en temps réel
                }
            }
        }

        // --- Statistiques ---
        private int _totalLivres;
        public int TotalLivres { get => _totalLivres; set => SetProperty(ref _totalLivres, value); }

        private int _livresLus;
        public int LivresLus { get => _livresLus; set => SetProperty(ref _livresLus, value); }

        private double _pourcentageLus;
        public double PourcentageLus { get => _pourcentageLus; set => SetProperty(ref _pourcentageLus, value); }

        // --- Commandes ---
        public ICommand AjouterCommand { get; }
        public ICommand ModifierCommand { get; }
        public ICommand SupprimerCommand { get; }
        public ICommand ExporterCsvCommand { get; } // Bonus 2

        public MainViewModel()
        {
            _repository = new LivreRepository();

            AjouterCommand = new RelayCommand(_ => AjouterLivre(), _ => PeutAjouter());
            ModifierCommand = new RelayCommand(_ => ModifierLivre(), _ => LivreSelectionne != null);
            SupprimerCommand = new RelayCommand(_ => SupprimerLivre(), _ => LivreSelectionne != null);
            ExporterCsvCommand = new RelayCommand(_ => ExporterCsv());

            ActualiserListe();
        }

        // --- Méthodes ---
        private void EffectuerRecherche()
        {
            var resultats = string.IsNullOrWhiteSpace(TermeRecherche) 
                ? _repository.GetAll() 
                : _repository.GetByRecherche(TermeRecherche);
                
            Livres.Clear();
            foreach (var livre in resultats)
            {
                Livres.Add(livre);
            }
            CalculerStatistiques();
        }

        private void ActualiserListe()
        {
            EffectuerRecherche(); // Charge tout si le terme de recherche est vide
        }

        private void CalculerStatistiques()
        {
            // On calcule sur la totalité de la base pour avoir des stats globales
            var tousLesLivres = _repository.GetAll();
            TotalLivres = tousLesLivres.Count;
            LivresLus = tousLesLivres.Count(l => l.Lu);
            PourcentageLus = TotalLivres > 0 ? Math.Round((double)LivresLus / TotalLivres * 100, 2) : 0;
        }

        private bool PeutAjouter()
        {
            return !string.IsNullOrWhiteSpace(Titre) && Titre.Length >= 2 &&
                   !string.IsNullOrWhiteSpace(Auteur) && Auteur.Length >= 2 &&
                   Annee >= 1800 && Annee <= DateTime.Now.Year &&
                   !string.IsNullOrWhiteSpace(Genre);
        }

        private void AjouterLivre()
        {
            var nouveau = new Livre { Titre = Titre, Auteur = Auteur, Annee = Annee, Genre = Genre, Lu = Lu };
            _repository.Add(nouveau);
            ViderFormulaire();
            ActualiserListe();
        }

        private void ModifierLivre()
        {
            if (LivreSelectionne == null || !PeutAjouter()) return;

            LivreSelectionne.Titre = Titre;
            LivreSelectionne.Auteur = Auteur;
            LivreSelectionne.Annee = Annee;
            LivreSelectionne.Genre = Genre;
            LivreSelectionne.Lu = Lu;

            _repository.Update(LivreSelectionne);
            ViderFormulaire();
            ActualiserListe();
        }

        private void SupprimerLivre()
        {
            if (LivreSelectionne != null)
            {
                var result = MessageBox.Show($"Supprimer '{LivreSelectionne.Titre}' ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    _repository.Delete(LivreSelectionne.Id);
                    ViderFormulaire();
                    ActualiserListe();
                }
            }
        }

        private void ViderFormulaire()
        {
            Titre = "";
            Auteur = "";
            Annee = DateTime.Now.Year;
            Genre = "Autre";
            Lu = false;
            LivreSelectionne = null;
        }

        // --- Bonus 2 : Export CSV ---
        private void ExporterCsv()
        {
            try
            {
                var csv = new StringBuilder();
                csv.AppendLine("Titre;Auteur;Annee;Genre;Lu");
                var tousLesLivres = _repository.GetAll();
                
                foreach (var l in tousLesLivres)
                {
                    csv.AppendLine($"{l.Titre};{l.Auteur};{l.Annee};{l.Genre};{l.Lu}");
                }
                
                File.WriteAllText("livres.csv", csv.ToString());
                MessageBox.Show("Export CSV réussi dans le dossier de l'application !", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de l'export : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}