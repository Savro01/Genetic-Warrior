# Genetic-Warrior

Des guerriers sont crée et envoyés pour combattre/survivre dans une arene (Gladiateur, Hunger games). 
Les 10 survivants sont reproduit et retourne se battre avec leur gosses. Et les 10 survivants restent à nouveau.

Condition d'arrêt : à définir (Aprés N génération ?)
Dernier combat avec les 10 meilleurs pour avoir le guerrier "parfait"

Les guerriers ont un ensemble de stat : Dégat, defense, vitesse, agilité, etc...
Des armes différente : Arc (Ajoute de la portée d'attaque), Masse (ajoute des dégat), bouclier (ajoute de la défense), etc...



Différentes stats physique (max point 50) :

Vitesse (Mouvement speed)
Dexterité (Ajout des dégat avec Arc, Lance)
Agilité (Chance d'esquive)
Force (Ajout des dégat avec Epée, Hache)
Endurance (Vitesse d'attaque)

Courage (Courage du guerrier)(Indépendant)

Différentes armes :

Epée
Hache
Arc (Guerrier attaque. Si un ennemi est trop pres, la peur augmente énormement)
Lance

Bonus bouclier ou non sur Hache, Epée et Lance

Stat de peur.
Plus il y a d'ennemi proche du guerrier, plus sa peur augmente.
Si le guerrier a une arme en defaveur  des ennemis autour sa peur augmente
Si l'ennemi a un bouclier sa peur augmente
Moins il a de pv, plus la peur augmente

Si la peur depasse le courage, le guerrier se met à fuir et s'éloigne de tout ses ennemis à portés.
Sinon, il attaque l'ennemi le plus proche



Todo : 

Instantier les 100 joueurs de base
Guerriers instantier avec des stats random + arme random. 
Génération 1 se bat, sauvegarde des 10 survivants
Préparation de la génération 2 + instantiation
Génération 2 se bat, sauvegarde des 10 survivants
Préparation de la génération n + instantiation
Génération n se bat, sauvegarde des 10 survivants
Préparation de la derniére génération + instantiation
Derniére génération se bat, sauvegarde de l'unique survivant
Affichage stats unique survivant + arme

Lancement du combat de génération via 2 boolean : Combat (qui repasse a false entre chaque génération) et CombatAuto (qui enchaine les générations)

Bonus : Add arme/bouclier dans "main" de la capsule