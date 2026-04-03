# TP FINAL - Développement Desktop .NET (C\#)

## PARTIE 1 — WinForms

**C1 :**![](./captures/C1.png)

*Mini-description : Interface principale créée entièrement par code affichant la liste des livres et les contrôles de gestion.*

**C2 :**![](./captures/C2.png)
*Mini-description : Validation stricte affichant un MessageBox regroupant toutes les erreurs de saisie (titre, auteur, année, genre).*

**C3 :**![](./captures/C3.gif)
*Mini-description : Mode édition activé par double-clic, pré-remplissant le formulaire avec les données du livre sélectionné.*

-----

## PARTIE 2 — WPF + SQLite : Application MVVM complète

**C4 :**![](./captures/C4.png)
*Mini-description : Vue principale WPF sous pattern MVVM incluant le panneau de statistiques dynamiques et le bouton d'export.*

**C5 :**![](./captures/C5.png)
*Mini-description : Système de recherche en temps réel filtrant la liste instantanément lors de la saisie (ex: "fan").*

**C6 :**![](./captures/C6.gif)
*Mini-description : Formulaire de modification synchronisé avec l'élément sélectionné dans le DataGrid.*

**C7 :**![](./captures/C7.png)
*Mini-description : Fenêtre de confirmation de sécurité avant la suppression d'une entrée dans la base SQLite.*

**C8 :**![](./captures/C8.gif)
*Mini-description : Démonstration de la persistance des données après redémarrage de l'application.*

-----

## PARTIE 3 — MAUI : Application mobile-ready

**C9 :**![](./captures/C9.gif)
*Mini-description : Interface multi-plateforme avec CollectionView, badges de couleurs par genre et filtre des livres lus.*

-----

## PARTIE 4 — Bonus

**Réponse Q1 :** Les termes manquants pour la classe RelayCommand sont : **ICommand**, **execute**, **true**, **CanExecuteChanged**.

**Réponse Q2 :** La fonctionnalité d'export CSV est intégrée au projet WPF, permettant de sauvegarder la liste au format `Titre;Auteur;Annee;Genre;Lu`.

**C10 :**![](./captures/C10.png)
*Mini-description : Aperçu du fichier "livres.csv" généré avec le séparateur point-virgule dans le dossier de l'application.*