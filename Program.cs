using System.Globalization;
using System.Text;

namespace Labo_pendu
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Optionnel : utiliser des mots de longueur différentes, l'option deviner le mot complet et de rejouer
            //truc : string leMot ==> leMot[1] = e
            //char[] reponseEnLettres = reponse.ToCharArray();
            //string ReponseEnString = new string(reponseEnLettres);

            Random random = new();
            int numero = 0;
            int nbEssais = 8;

            string[] choixMots = File.ReadLines("C:\\Users\\flore\\Documents\\Projets_Cegep_Informatique\\Projets Programmation 1\\Laboratoires\\Labo_pendu\\mots\\mots.txt").ToArray();

            int choixMenu = 0;
            bool valide = false;

            //Mise en place du mot à deviner
            string leMot = "";
            numero = random.Next(0, choixMots.Length);
            leMot = choixMots[numero];
            char[] leMotEnLettres = leMot.ToCharArray();

            //Longueur de la réponse
            string reponse = "";
            for (int j = 0; j < leMot.Length; j++)
            {
                reponse += "_";
            }

            //Variables réponse
            char[] reponseEnLettres = reponse.ToCharArray();
            string reponseEnString = new string(reponseEnLettres);
            char lettreJoueur = 'a';
            string deviner = "";
            bool lettrePresente = false;
            string lettresPasPresentes = "";

            //Intro
            Console.WriteLine("Bienvenue au jeu du bonhomme pendu!");
            Console.WriteLine("Un mot a été choisi! Devinez-en ses lettres, sinon le bonhomme sera pendu!");

            menu();

            //Jeu
            while(!valide)
            {
                int.TryParse(Console.ReadLine(), out choixMenu);
                if (choixMenu == 1)
                {
                    Console.WriteLine("Entrez une lettre");
                    char.TryParse(Console.ReadLine(), out lettreJoueur);

                    lettrePresente = verifierLettre(leMotEnLettres, lettreJoueur);
                    if (lettrePresente == true)
                    {
                        Console.WriteLine("Cette lettre est dans le mot!");

                        for (int i = 0; i < leMotEnLettres.Length; i++)
                        {
                            if (leMotEnLettres[i] == lettreJoueur)
                            {
                                reponseEnLettres[i] = lettreJoueur;
                                reponseEnString = new string(reponseEnLettres);
                            }
                        }
                        display(reponseEnString, lettresPasPresentes, nbEssais);
                    }
                    else
                    {
                        Console.WriteLine("Cette lettre n'est pas dans le mot!");
                        lettresPasPresentes += lettreJoueur + ", ";
                        nbEssais -= 1;
                        if (nbEssais == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("Il ne vous reste plus d'essais! Le bonhomme a été pendu!");
                            Console.ResetColor();
                            Console.Write("La réponse était : ");
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.Write(leMot + "\n");
                            Console.ResetColor();
                            valide = true;
                        }
                        else
                        {
                            display(reponseEnString, lettresPasPresentes, nbEssais);
                        }
                    }
                }
                else if (choixMenu == 2)
                {
                    Console.WriteLine("Écrivez un mot");
                    deviner = Console.ReadLine();
                    if (deviner == leMot)
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("Vous avez gagné!");
                        Console.ResetColor();
                        valide = true;
                    }
                    else
                    {
                        Console.WriteLine("Vous avez perdu!");
                        Console.Write("La réponse était : ");
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write(leMot + "\n");
                        Console.ResetColor();
                        valide = true;
                    }
                }
                else if (choixMenu == 3)
                {
                    valide = true;
                }
                
            }
        }

        static void menu()
        {
            Console.WriteLine("\nEntrez un numéro pour choisir ce que vous allez faire!");
            Console.WriteLine("  1. Deviner une lettre");
            Console.WriteLine("  2. Donner une réponse (Un essai)");
            Console.WriteLine("  3. Arrêter");
        }

        static bool verifierLettre(char[] mot, char lettre)
        {
            bool present = false;
            
            for (int i = 0; i < mot.Length; i++)
            {
                if (mot[i] == lettre)
                {
                    present = true;
                }
            }
            return present;
        }

        static void display(string reponse, string invalides, int essais)
        {
            Console.Clear();
            Console.WriteLine("\nEntrez un numéro pour choisir ce que vous allez faire!");
            Console.WriteLine("  1. Deviner une lettre");
            Console.WriteLine("  2. Donner une réponse (Un essai)");
            Console.WriteLine("  3. Arrêter");
            Console.WriteLine("\nVotre réponse : " + reponse);
            Console.Write("Lettres invalides : ");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write(invalides);
            Console.ResetColor();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write(essais);
            Console.ResetColor();
            Console.Write(" essais restants! \n");
        }
    }
}