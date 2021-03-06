﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Librairie MySQL
using MySql.Data.MySqlClient;
using System.Data;

// Classes du projets
using gestionstage.Classes;


namespace gestionstage.Dao
{
    class DaoEntreprise : Dao
    {
        public static void create(Entreprise uneEntreprise)
        {
            try
            {
                open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = "INSERT INTO entreprises(id, siret, nom, adresse, cp, ville, telephone, email, commentaire, bool_envoye) VALUES (@id, @siret, @nom, @adresse, @cp, @ville, @telephone, @email, @commentaire, @bool_envoye)";

                cmd.Prepare();

                cmd.Parameters.AddWithValue("@id", uneEntreprise.Id);
                cmd.Parameters.AddWithValue("@siret", uneEntreprise.Siret);
                cmd.Parameters.AddWithValue("@nom", uneEntreprise.Nom);
                cmd.Parameters.AddWithValue("@adresse", uneEntreprise.Adresse);
                cmd.Parameters.AddWithValue("@cp", uneEntreprise.Cp);
                cmd.Parameters.AddWithValue("@ville", uneEntreprise.Ville);
                cmd.Parameters.AddWithValue("@telephone", uneEntreprise.Telephone);
                cmd.Parameters.AddWithValue("@email", uneEntreprise.Email);
                cmd.Parameters.AddWithValue("@commentaire", uneEntreprise.Commentaire);
                cmd.Parameters.AddWithValue("@bool_envoye", uneEntreprise.Bool_envoye);

                cmd.ExecuteNonQuery();

                uneEntreprise.Id = (int)cmd.LastInsertedId;

                close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());

            }
        }

        public static Entreprise readOneBySiret(string nSiret)
        {
            Entreprise lEntreprise = null;

            try
            {
                open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM entreprises WHERE siret=" + nSiret;

                MySqlDataReader res = cmd.ExecuteReader();

                res.Read();
                lEntreprise = new Entreprise(Convert.ToInt16(res["id"]), (string)res["siret"], (string)res["nom"], (string)res["adresse"], (string)res["cp"], (string)res["ville"], Convert.ToString(res["telephone"]), Convert.ToString(res["email"]), Convert.ToString(res["commentaire"]), (Boolean)res["bool_envoye"]);
                close();

                return lEntreprise;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());

                return lEntreprise;
            }
        }

        public static DataTable dtReadAll()
        {
            DataTable dtEntreprise = new DataTable();

            try
            {
                open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM entreprises";

                MySqlDataReader res = cmd.ExecuteReader();

                dtEntreprise.Load(res);

                close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());

                return dtEntreprise;
            }

            return dtEntreprise;
        }

        public static DataTable dtReadAllByBoolEnvoye()
        {
            DataTable dtEntreprise = new DataTable();

            try
            {
                open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM entreprises WHERE bool_envoye=0";

                MySqlDataReader res = cmd.ExecuteReader();

                dtEntreprise.Load(res);

                close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());

                return dtEntreprise;
            }

            return dtEntreprise;
        }

        public static DataTable dtReadAllJoin()
        {
            DataTable dtEntreprise = new DataTable();

            try
            {
                open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT entreprises.id as idEntreprises, " + //Renome entreprises.id par idEntreprises
                    "entreprises.siret, entreprises.nom, entreprises.adresse, " +
                    "entreprises.cp, entreprises.ville, entreprises.telephone, " +
                    "entreprises.email, entreprises.commentaire, " +
                    "entreprises.bool_envoye as bool_envoyeEntreprises, " + //Renome entreprises.bool_envoye par bool_envoyeEntreprises
                    "contrats.id as idContrats, contrats.typecontrat_id, " + //Renome contrats.id par idContrats
                    "contrats.formation_id, contrats.s_nom, contrats.s_prenom, " +
                    "contrats.t_nom, contrats.t_prenom, contrats.t_mail, " +
                    "contrats.t_telephone, contrats.date_debut, contrats.date_fin, " +
                    "contrats.commentaire, contrats.bool_envoye as bool_envoyeContrats, " + //Renome contrats.bool_envoye par bool_envoyeContrats
                    "contrats.appreciation, contrats.entreprise_id " +

                    "FROM entreprises " +
                    "INNER JOIN contrats " +
                    "ON contrats.entreprise_id = entreprises.id " +
                    "ORDER BY idEntreprises";

                MySqlDataReader res = cmd.ExecuteReader();

                dtEntreprise.Load(res);

                close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());

                return dtEntreprise;
            }

            return dtEntreprise;
        }

        public static Boolean update(Entreprise uneEntreprise, string ancienSiret = null)
        {
            if (ancienSiret == null)
            {   ancienSiret = uneEntreprise.Siret;  }
            try
            {
                open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "UPDATE entreprises SET siret=@siret,nom=@nom,adresse=@adresse,cp=@cp,ville=@ville,telephone=@telephone,email=@email,commentaire=@commentaire,commentaire=@commentaire,bool_envoye=@bool_envoye WHERE siret=" + ancienSiret;

                cmd.Prepare();

                cmd.Parameters.AddWithValue("@siret", uneEntreprise.Siret);
                cmd.Parameters.AddWithValue("@nom", uneEntreprise.Nom);
                cmd.Parameters.AddWithValue("@adresse", uneEntreprise.Adresse);
                cmd.Parameters.AddWithValue("@cp", uneEntreprise.Cp);
                cmd.Parameters.AddWithValue("@ville", uneEntreprise.Ville);
                cmd.Parameters.AddWithValue("@telephone", uneEntreprise.Telephone);
                cmd.Parameters.AddWithValue("@email", uneEntreprise.Email);
                cmd.Parameters.AddWithValue("@commentaire", uneEntreprise.Commentaire);
                cmd.Parameters.AddWithValue("@bool_envoye", uneEntreprise.Bool_envoye);

                cmd.ExecuteNonQuery();

                close();

                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());
                return false;
            }
        }

        public static void delete(string unSiret)
        {
            try
            {
                open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "DELETE FROM entreprises WHERE siret=" + unSiret;

                cmd.ExecuteNonQuery();

                close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());
            }
        }

        public static Boolean isExistSiret(string siret)
        {
            try
            {
                open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM entreprises WHERE siret=" + siret;

                cmd.Prepare();

                cmd.ExecuteNonQuery();

                MySqlDataReader res = cmd.ExecuteReader();

                if (res.Read())
                {
                    close();
                    return true;
                }
                else
                {
                    close();
                    return false;
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());
                return false;
            }
        }
    }
}