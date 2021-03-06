﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Using perso
using gestionstage.Properties; // Personnalisation des couleurs
using gestionstage.Dao;
using gestionstage.Classes;
using MetroFramework;

namespace gestionstage.Forms
{
    public partial class AddEntreprise : MetroFramework.Forms.MetroForm
    {
        List<string> lsError = new List<string>();

        public AddEntreprise()
        {
            InitializeComponent();
            this.StyleManager = mSMAddEntreprise;
            this.StyleManager.Style = (MetroFramework.MetroColorStyle)Settings.Default.Style;
        }

        // --------------------------------------------------------------------
        // Bouttons et Cliques :
        // --------------------------------------------------------------------
        private void mLinkBack_Click(object sender, EventArgs e)
        {
            ListEntreprise MainFormListEntreprise = new ListEntreprise();
            MainFormListEntreprise.Show();
            this.Close();
        }

        private void mButtonAddEntreprise_Click(object sender, EventArgs e)
        {
            CheckErrorSiret();
            CheckErrorNom();
            CheckErrorAdresse();
            CheckErrorCP();
            CheckErrorCity();
            CheckErrorTelephone();
            CheckErrorEmail();

            if (lsError.Count == 0)
            {
                DaoEntreprise.create(new Entreprise(mTxBEntrepriseSiret.Text, mTxBEntrepriseName.Text, mTxBEntrepriseAdresse.Text, mTxBEntreprisePostalCode.Text, mTxBEntrepriseCity.Text, mTxBEntrepriseTelephone.Text, mTxBEntrepriseEmail.Text, mTxBCommentaire.Text, false));
                ListEntreprise MainFormListEntreprise = new ListEntreprise();
                MainFormListEntreprise.Show();
                this.Close();
            }
            else
            {
                // TODO : Si trop d'erreurs, elles ne sont pas toutes affiché, trouver un moyen de faire un ascenseur vertical.
                string lesErreurs = "";
                foreach (string erreur in lsError)
                {
                    lesErreurs += erreur + "\n";
                }
                MetroMessageBox.Show(this, lesErreurs, "Erreur");
                lsError.Clear();
            }
        }

        // --------------------------------------------------------------------
        // Fonctions de vérification des champs avant l'ajout de l'entreprise :
        // --------------------------------------------------------------------
        private void CheckErrorSiret()
        {
            if (mTxBEntrepriseSiret.Text == "")
            {
                lsError.Add("Champ \"Siret\" vide");
            }
            else if (!(IsNumeric(mTxBEntrepriseSiret.Text)))
            {
                lsError.Add("Champ \"Siret\" n'est pas composé de 14 chiffres");
            }
            else if (IsNumeric(mTxBEntrepriseSiret.Text) && mTxBEntrepriseSiret.Text.Length != 14)
            {
                lsError.Add("Champ \"Siret\" doit être composé de 14 chiffres");
            }
        }
        private void CheckErrorNom()
        {
            if (mTxBEntrepriseName.Text == "")
            {
                lsError.Add("Champ \"nom\" vide");
            }
        }
        private void CheckErrorAdresse()
        {
            if (mTxBEntrepriseAdresse.Text == "")
            {
                lsError.Add("Champ \"Adresse\" vide");
            }
        }
        private void CheckErrorCP()
        {
            if (mTxBEntreprisePostalCode.Text == "")
            {
                lsError.Add("Champ \"Code Postale\" vide");
            }
            else if (!(IsNumeric(mTxBEntreprisePostalCode.Text)))
            {
                lsError.Add("Champ \"Code Postale\" n'est pas composé de chiffres");
            }
            else if (IsNumeric(mTxBEntreprisePostalCode.Text) && mTxBEntreprisePostalCode.Text.Length != 5) 
            {
                lsError.Add("Champ \"Code Postale\" doit être composé de 5 chiffres");
            }
        }
        private void CheckErrorCity()
        {
            if (mTxBEntrepriseCity.Text == "")
            {
                lsError.Add("Champ \"Ville\" vide");
            }
        }
        private void CheckErrorTelephone()
        {
            if (mTxBEntrepriseTelephone.Text == "")
            {
                //lsError.Add("Champ \"Téléphone\" vide"); // Commenté car peut être null
            }
            else if (!(IsNumeric(mTxBEntrepriseTelephone.Text)))
            {
                lsError.Add("Champ \"Téléphone\" n'est pas composé de chiffres");
            }
            else if (IsNumeric(mTxBEntrepriseTelephone.Text) && mTxBEntrepriseTelephone.Text.Length != 10)
            {
                lsError.Add("Champ \"Téléphone\" doit être composé de 10 chiffres");
            }
        }
        private void CheckErrorEmail()
        {
            if (mTxBEntrepriseEmail.Text == "")
            {
                //lsError.Add("Champ \"E-mail\" vide"); // Commenté car peut être null
            }
            if ((mTxBEntrepriseEmail.Text.IndexOf("@") == -1) || (mTxBEntrepriseEmail.Text.IndexOf(".") == -1))
            {
                lsError.Add("Champ \"E-mail\" n'est pas un E-mail valide");
            }
        }

        // --------------------------------------------------------------------
        // Fonctions privé :
        // --------------------------------------------------------------------
        // Renvois true si la chaine passé en paramètre est numérique
        private bool IsNumeric(string someNumber)
        {
            long test;
            if (Int64.TryParse(someNumber, out test))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
