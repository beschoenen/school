# Framework voor KMint

Deze repository bevat een klein opstartproject voor KMINT. Het maakt gebruik van CMake als build system.

# Gebruik

## Afhankelijkheden

Dit project vereist een C++14 compiler (recente versies van `g++` en `clang++` voldoen) en SDL2 en SDL_image versie 2 of hoger. Deze afhankelijkheden kun je je installeren met je favoriete package manager.

## Compileren

Onderstaande commando's laten zien hoe je de code compileert

```
$ cmake -H. -Bbuild
$ cmake --build build -- -j3
```

Je kunt de `3` vervangen door het aantal processorkernen in je systeem + 1. Voor een quad-core systeem zou je dus `-j5` gebruiken in plaats van `-j3`

## Draaien

Het gecompileerde programma verwacht de map `resources` en het bestand `graph.txt` in de huidige directory te vinden. Als je bovenstaande commando's hebt utigevoerd om het project te compileren kun je het programma als volgt starten:

```
$ ./build/src/KMintFramework 
```

# Het bestand `graph.txt`

`graph.txt` bevat een beschrijving van een graaf. Het bestand bestaat uit twee delen gescheiden door een witregel.

Het eerste deel beschrijft de locatie van de knopen van de graaf. Elke regel bevat een x- en een y-coordinaat. Het gehele veld heeft een afmeting van 1280 bij 720 pixels. Houd hier rekening mee! De knoop op regel 1, krijgt automatisch index 0; de knoop op regel 2 index 1 enzovoorts.

Het tweede deel bevat de zijden van de graaf. Elke regel bestaat uit drie velden gescheiden door spaties. De eerste twee velden staan respectievelijk voor de bron- en doelknoop. Het derde veld bevat een gewicht. Je zou bijvoorbeeld een zwaarder gewicht kunnen toekennen aan een verbinding door een moeras.

# Auteur

Dit project is geschreven door Jeroen de Haas voor gebruik in de module KMINT binnen de opleiding Informatica aan Avans Hogeschool.