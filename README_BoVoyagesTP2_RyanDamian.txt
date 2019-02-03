GTM Formation DotNET Projet 2 C#
BoVoyage
Ryan MARTIN et Damian McCANN
 
L'application console ci-joint a été conçu pour BoVoyages. 
Cette application a pour but d'améliorer le suivi de voyages et suivi de clientèle de BoVoyage.
L'application permet aussi aux clients de BoVoyage de gérer leurs voyages.
 
Une base de données a été crée avec Microsoft SQL Server 2017.
 
System Requirements :
 - Microsoft SQL Server 2017 et Microsoft SQL Server tools 17
 - Microsoft Visual Studio 2017

Installation : 
1) Création base de données
	Copier le contenu du script BoVoyagesTP2RD - Create Database and Create and Load Tables.sql sur Microsoft Server Management Tools.
	Le script installera à la fois la base de données, le vues, et les procédures stockées.

2) Exécution sur C# 
	Lancer l'application BoVoyages.exe qui se trouve dans RyanDamian > Source > C# > BoVoyages > Bin > Debug
	

L'application se présente en cascade de menus décrits ci-dessous :

Menu Principal
1. Menu Voyage
	1. Liste de voyages disponible (Récupérer une liste des voyages disponible de la base de données)
	2. Réserver un voyage (Création d'un DossierReservation et les objets Client (si nécessaire), Participants,
						   et la création de lien entre DossierReservation et Participants, 
						   et entre DossierReservation et Assurance (si nécessaire))
	3. Supprimer un Voyage Réservation (Suppression d'un DossierReservation et les objets Participants correspondant, 
										 en respectant la cohérence de la base des données)
2. Menu Commercial
	1. Gérer les voyages
		1. Liste de voyages disponible (Récupérer une liste des voyages disponible de la base de données)
		2. Supprimer les voyages périmes (Suppression de toutes les Voyages ou la date de départ est inferieure à aujourd'hui, 
										  et les références correspondantes.)
		3. Ajouter les voyages (pas encore implémente)
	2. Gérer les clients
		1. Lister les clients (pas encore implémente)
		2. Faire une synthèse mensuelle (pas encore implémente)
		3. Promouvoir les voyages disponibles par mail (pas encore implémente)
		4. Envoyer le questionnaire de satisfaction par mail (pas encore implémente)



Fichiers joint :
Diagramme relationnelle de base de données : SGBDR.JPG
