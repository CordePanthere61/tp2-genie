
### Section 2

#### Problemes logiques
- Il est possible de renommer un fichier avec un nom existant.
- Meme chose pour les dossiers.

- Lorsqu'on cree un nouveau fichier avec le meme nom qu'un folder, il est impossible d'aller get le fichier creer avec le nom, il retourne le folder
- On peut creer des folders/fichiers avec des characteres speciaux

- Si j'essaie de get un fichier dans un folder inexistant, il plante. Par contre, si j'essaie d'aller chercher un fichier inexistant dans un folder existant, non.
    - Par exemple :
        - ["Root", "non-existant-folder", "file"] -> Erreur (NullReferenceException)
        - ["Root", "existant-folder", "non-existant-file"] -> Pas d'erreur, retourne seulement un component null.


### 


#### Questions prof

- Demander a will si necessaire de faire le get a chaque fois pour vraiment contenir les 3 "A" du unit testing ou bien utiliser variables globales pour ameliorer performances.