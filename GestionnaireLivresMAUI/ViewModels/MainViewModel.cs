using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using GestionnaireLivresMAUI.Models;

namespace GestionnaireLivresMAUI.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private bool _afficherUniquementLus;
        public ObservableCollection<Livre> TousLesLivres { get; set; }
        public ObservableCollection<Livre> LivresFiltrés { get; set; } = new();

        public bool AfficherUniquementLus
        {
            get => _afficherUniquementLus;
            set
            {
                _afficherUniquementLus = value;
                OnPropertyChanged();
                FiltrerLivres();
            }
        }

        public MainViewModel()
        {
            TousLesLivres = new ObservableCollection<Livre>
            {
                new Livre { Titre = "1984", Auteur = "George Orwell", Genre = "SF", Lu = true },
                new Livre { Titre = "Le Hobbit", Auteur = "Tolkien", Genre = "Fantasy", Lu = true },
                new Livre { Titre = "Dune", Auteur = "Frank Herbert", Genre = "SF", Lu = false },
                new Livre { Titre = "Sherlock", Auteur = "Conan Doyle", Genre = "Policier", Lu = true },
                new Livre { Titre = "Le Père Goriot", Auteur = "Balzac", Genre = "Roman", Lu = false }
            };
            FiltrerLivres();
        }

        private void FiltrerLivres()
        {
            var filtrés = AfficherUniquementLus 
                ? TousLesLivres.Where(l => l.Lu) 
                : TousLesLivres;

            LivresFiltrés.Clear();
            foreach (var livre in filtrés) LivresFiltrés.Add(livre);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string p = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));
    }
}