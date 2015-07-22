Avant de démarrer
-----------------

- Installer Visual Studio 2013 et Resharper
- Récupération du repository
	- Fork sur GitHub (ou autre : BitBucket, FramaGit...)
	- Git clone du fork
		
Consignes
---------

- A faire individuellement
- Aucun code dans le projet Tests (les seules modifications doivent être le fait de décommenter les tests)
Pour cela, utilisez Resharper (Alt-Enter) pour créer les classes/méthodes, puis déplacez le code en dehors du projet Tests en appuyant sur Shift en même temps que le cliquer-glisser (sinon le fichier est copié et non déplacé)
- Un commit par tests avec message explicite
- On peut zapper des tests, les tests des 3 fichiers sont totalement indépendants les uns des autres, au sein d'un même fichier de test, il y a potentiellement un peu de lien entre les tests
- *A la fin, pousser vore branche de travail et donnez-moi l'URL de votre repo GitHub (ou autre si forker sur BitBucket, FramaGit...) avec le nom de la branche à récupérer*

Evaluation
----------

- Qualité du code (correctement mis en forme, i.e homogène, nommage explicite des variables...) -> NB : Ctrl-K,D remet en partie en forme + Resharper vous aide à respecter les normes de coding.
- Qualité des commits
- Nombre de tests passés
- Bonne implémentation (par ex, pour les tests Linq, inutile de faire une implémentation procédurale avec des boucles...)

Bonus
-----

Réaliser un FizzBuzz en écrivant vos propres tests unitaires (tests à mettre dans un nouveau fichier FizzBuzzTests dans la même solution)

Parcourir les entiers, puis pour chaque, s'il est divisible par 3, écrire Fizz, si divisible par 5, écrire Buzz, si divisible par les 2, écrire FizzBuzz, sinon écrire le nombre

Le bonus tiendra compte du bon usage de l'approche TDD (un commit après chaque test au vert typiquement, voir commit test rouge pour forcer le trait et montrer que les tests ont bien été écrits avant)
