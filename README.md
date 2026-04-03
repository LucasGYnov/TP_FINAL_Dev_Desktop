# TP FINAL - Développement Desktop .NET (C\#)

### PARTIE 1 — WinForms
* **C1 :** `![Image C1](captures/C1.png)` 
*Interface principale générée par code affichant la liste des livres et les contrôles de saisie.*
* **C2 :** `![Image C2](captures/C2.png)` 
*Démonstration de la validation stricte avec un MessageBox listant toutes les erreurs du formulaire.*
* **C3 :** `![Image C3](captures/C3.gif)` 
*Mode édition activé suite à un double-clic sur un livre, remplissant automatiquement les champs.*

---

### PARTIE 2 — WPF + SQLite
* **C4 :** `![Image C4](captures/C4.png)`
*Vue principale WPF utilisant le pattern MVVM avec affichage des statistiques dynamiques en bas de page.*
* **C5 :** `![Image C5](captures/C5.png)`
*Recherche en temps réel filtrant instantanément la liste sur le terme "fan".*
* **C6 :** `![Image C6](captures/C6.gif)`
*Formulaire de modification synchronisé avec le livre sélectionné dans le DataGrid.*
* **C7 :** `![Image C7](captures/C7.png)`
*Sécurité à la suppression via une demande de confirmation utilisateur.*
* **C8 :** `![Image C8](captures/C8.gif)`
*Vérification de la persistance des données dans le fichier SQLite après redémarrage.*

---

### PARTIE 3 — MAUI
* **C9 :** `![Image C9](captures/C9.gif)` 
*Interface MAUI avec CollectionView, badges colorés par genre et filtre "Livres lus" fonctionnel.*

---

### PARTIE 4 — Bonus

**Réponse Q1 :** Les termes manquants pour la classe RelayCommand sont : **ICommand**, **execute**, **true**, **CanExecuteChanged**.
**Réponse Q2 :** La fonctionnalité d'export CSV est déjà intégrée au projet WPF dès le début du développement.

* **C10 :** `![Image C10](captures/C10.png)`
*Aperçu du fichier "livres.csv" généré avec le format Titre;Auteur;Annee;Genre;Lu.*