using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GestionnaireLivresWinForms.Models;

namespace GestionnaireLivresWinForms
{
    public partial class Form1 : Form
    {
        // --- Déclaration des contrôles ---
        private TextBox txtTitre = null!;
        private TextBox txtAuteur = null!;
        private TextBox txtAnnee = null!;
        private ComboBox cmbGenre = null!;
        private CheckBox chkLu = null!;
        private Button btnAjouter = null!;
        private Button btnModifier = null!;
        private Button btnSupprimer = null!;
        private ListBox lstLivres = null!;

        private List<Livre> bibliotheque = new List<Livre>();

        public Form1()
        {
            this.Text = "Gestionnaire de Livres";
            this.Size = new Size(850, 500);
            this.StartPosition = FormStartPosition.CenterScreen;

            InitialiserInterface();
            LierEvenements();
        }

        // --- Construction de l'interface ---
        private void InitialiserInterface()
        {
            Label lblTitre = new Label { Text = "Titre :", Location = new Point(20, 20), AutoSize = true };
            txtTitre = new TextBox { Location = new Point(120, 20), Width = 200 };

            Label lblAuteur = new Label { Text = "Auteur :", Location = new Point(20, 60), AutoSize = true };
            txtAuteur = new TextBox { Location = new Point(120, 60), Width = 200 };

            Label lblAnnee = new Label { Text = "Année :", Location = new Point(20, 100), AutoSize = true };
            txtAnnee = new TextBox { Location = new Point(120, 100), Width = 100 };

            Label lblGenre = new Label { Text = "Genre :", Location = new Point(20, 140), AutoSize = true };
            cmbGenre = new ComboBox { Location = new Point(120, 140), Width = 200, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbGenre.Items.AddRange(new string[] { "Roman", "SF", "Fantasy", "Policier", "Autre" });

            chkLu = new CheckBox { Text = "Lu ?", Location = new Point(120, 180), AutoSize = true };

            btnAjouter = new Button { Text = "Ajouter", Location = new Point(20, 230), Size = new Size(100, 35) };
            btnModifier = new Button { Text = "Modifier", Location = new Point(130, 230), Size = new Size(100, 35) };
            btnSupprimer = new Button { Text = "Supprimer", Location = new Point(240, 230), Size = new Size(100, 35), Enabled = false };

            lstLivres = new ListBox { Location = new Point(360, 20), Size = new Size(450, 400), HorizontalScrollbar = true };

            this.Controls.AddRange(new Control[] {
                lblTitre, txtTitre, lblAuteur, txtAuteur, lblAnnee, txtAnnee, lblGenre, cmbGenre, chkLu,
                btnAjouter, btnModifier, btnSupprimer, lstLivres
            });
        }

        // --- Branchement des événements ---
        private void LierEvenements()
        {
            btnAjouter.Click += BtnAjouter_Click;
            btnModifier.Click += BtnModifier_Click;
            btnSupprimer.Click += BtnSupprimer_Click;
            lstLivres.SelectedIndexChanged += LstLivres_SelectedIndexChanged;
            lstLivres.DoubleClick += LstLivres_DoubleClick;
        }

        // --- Logique et Validation ---
        private string ValiderFormulaire()
        {
            string erreurs = "";

            if (string.IsNullOrWhiteSpace(txtTitre.Text) || txtTitre.Text.Trim().Length < 2)
                erreurs += "- Le titre doit contenir au moins 2 caractères.\n";

            if (string.IsNullOrWhiteSpace(txtAuteur.Text) || txtAuteur.Text.Trim().Length < 2)
                erreurs += "- L'auteur doit contenir au moins 2 caractères.\n";

            if (!int.TryParse(txtAnnee.Text, out int annee) || annee < 1800 || annee > DateTime.Now.Year)
                erreurs += $"- L'année doit être un nombre valide entre 1800 et {DateTime.Now.Year}.\n";

            if (cmbGenre.SelectedItem == null)
                erreurs += "- Vous devez sélectionner un genre.\n";

            return erreurs;
        }

        private void ViderFormulaire()
        {
            txtTitre.Clear();
            txtAuteur.Clear();
            txtAnnee.Clear();
            cmbGenre.SelectedIndex = -1;
            chkLu.Checked = false;
            lstLivres.ClearSelected();
        }

        private void ActualiserListe()
        {
            lstLivres.Items.Clear();
            foreach (var livre in bibliotheque)
            {
                lstLivres.Items.Add(livre);
            }
        }

        // --- Actions des boutons ---
        private void BtnAjouter_Click(object? sender, EventArgs e)
        {
            string erreurs = ValiderFormulaire();
            if (!string.IsNullOrEmpty(erreurs))
            {
                MessageBox.Show("Veuillez corriger les erreurs suivantes :\n\n" + erreurs, "Erreur de validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Livre nouveauLivre = new Livre
            {
                Titre = txtTitre.Text.Trim(),
                Auteur = txtAuteur.Text.Trim(),
                Annee = int.Parse(txtAnnee.Text),
                Genre = cmbGenre.SelectedItem?.ToString() ?? "Autre",
                Lu = chkLu.Checked
            };

            bibliotheque.Add(nouveauLivre);
            ActualiserListe();
            ViderFormulaire();
        }

        private void LstLivres_SelectedIndexChanged(object? sender, EventArgs e)
        {
            btnSupprimer.Enabled = lstLivres.SelectedItem != null;
        }

        private void LstLivres_DoubleClick(object? sender, EventArgs e)
        {
            if (lstLivres.SelectedItem is Livre livreSelectionne)
            {
                txtTitre.Text = livreSelectionne.Titre;
                txtAuteur.Text = livreSelectionne.Auteur;
                txtAnnee.Text = livreSelectionne.Annee.ToString();
                cmbGenre.SelectedItem = livreSelectionne.Genre;
                chkLu.Checked = livreSelectionne.Lu;
            }
        }

        private void BtnModifier_Click(object? sender, EventArgs e)
        {
            if (lstLivres.SelectedItem is Livre livreSelectionne)
            {
                string erreurs = ValiderFormulaire();
                if (!string.IsNullOrEmpty(erreurs))
                {
                    MessageBox.Show("Veuillez corriger les erreurs suivantes :\n\n" + erreurs, "Erreur de validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                livreSelectionne.Titre = txtTitre.Text.Trim();
                livreSelectionne.Auteur = txtAuteur.Text.Trim();
                livreSelectionne.Annee = int.Parse(txtAnnee.Text);
                livreSelectionne.Genre = cmbGenre.SelectedItem?.ToString() ?? "Autre";
                livreSelectionne.Lu = chkLu.Checked;

                ActualiserListe();
                ViderFormulaire();
            }
        }

        private void BtnSupprimer_Click(object? sender, EventArgs e)
        {
            if (lstLivres.SelectedItem is Livre livreSelectionne)
            {
                var confirmation = MessageBox.Show($"Êtes-vous sûr de vouloir supprimer '{livreSelectionne.Titre}' ?", "Confirmation de suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question); 

                if (confirmation == DialogResult.Yes)
                {
                    bibliotheque.Remove(livreSelectionne);
                    ActualiserListe();
                    ViderFormulaire();
                }
            }
        }
    }
}