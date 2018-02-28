using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TAL
{
	class MainClass
	{

		// Forme canonique pour la lemmatisation
		private List<Pair<string[], int>> cleCanonique;
		private Dictionary<int, string> canonique;

		// Liste des catégories morpho syntaxiques utilisé pour l'étiquetage
		private List<Pair<string[], int>> cleCategorieMorphoSyntaxique;
		private Dictionary<int, string> categorieMorphoSyntaxique;

		// Liste des grammaires de phrase qui corresponde à une phrase
		private List<string[]> PhraseGrammaire;

		private List<Pair<string[], int>> cleKeyWord;
		private Dictionary<int, string> keyWord;

		public static void Main(string[] args)
		{
			MainClass c = new MainClass();
			string[] textentry = { "Salut!", "Tu as mangé quoi au petit déjeuner?",  "Le nutella, c'est trop bon!" };
			Console.WriteLine("Texte d'entré : " + c.afficheTableau(textentry));
			for (int i = 0; i < textentry.Length; i++)
			{
				string resultatChatBot = c.chatbot(textentry[i]);
				Console.WriteLine("\nRésultat du chatbot : \n" + resultatChatBot);
			}
			Console.ReadKey();
		}

		private void initialiseDictionnaire()
		{
			cleCanonique = new List<Pair<string[], int>>();
			canonique = new Dictionary<int, string>();

			string[] aux1 = { "Salut", "Bonjour", "Yo" };
			Pair<string[], int> c1 = new Pair<string[], int>(aux1, 1);
			cleCanonique.Add(c1);

			string[] aux2 = { "ai", "as", "a", "avons", "avez", "ont" };
			Pair<string[], int> c2 = new Pair<string[], int>(aux2, 2);
			cleCanonique.Add(c2);

			string[] aux3 = { "mange", "manges", "mangeons", "mangez", "mangent", "mangé" };
			Pair<string[], int> c3 = new Pair<string[], int>(aux3, 3);
			cleCanonique.Add(c3);

			string[] aux4 = { "suis", "es", "est", "sommes", "êtes", "sont", "c'est" };
			Pair<string[], int> c4 = new Pair<string[], int>(aux4, 4);
			cleCanonique.Add(c4);

			canonique.Add(1, "bonjour");
			canonique.Add(2, "avoir");
			canonique.Add(3, "manger");
			canonique.Add(4, "être");

			cleCategorieMorphoSyntaxique = new List<Pair<string[], int>>();
			categorieMorphoSyntaxique = new Dictionary<int, string>();


			string[] eaux1 = { "petit", "bon" }; // Adjectif
			Pair<string[], int> e1 = new Pair<string[], int>(eaux1, 1);
			cleCategorieMorphoSyntaxique.Add(e1);

			string[] eaux2 = { "trop" }; // Adverbe
			Pair<string[], int> e2 = new Pair<string[], int>(eaux2, 2);
			cleCategorieMorphoSyntaxique.Add(e2);

			string[] eaux3 = { }; // Conjonction
			Pair<string[], int> e3 = new Pair<string[], int>(eaux3, 3);
			cleCategorieMorphoSyntaxique.Add(e3);

			string[] eaux4 = { "au", "Le" }; // Déterminant
			Pair<string[], int> e4 = new Pair<string[], int>(eaux4, 4);
			cleCategorieMorphoSyntaxique.Add(e4);

			string[] eaux5 = { "bonjour", "Salut" }; // Interjection
			Pair<string[], int> e5 = new Pair<string[], int>(eaux5, 5);
			cleCategorieMorphoSyntaxique.Add(e5);

			string[] eaux6 = { "déjeuner", "nutella" }; // Nom
			Pair<string[], int> e6 = new Pair<string[], int>(eaux6, 6);
			cleCategorieMorphoSyntaxique.Add(e6);

			string[] eaux7 = { }; // Préposition
			Pair<string[], int> e7 = new Pair<string[], int>(eaux7, 7);
			cleCategorieMorphoSyntaxique.Add(e7);

			string[] eaux8 = { "Tu" }; // Pronom
			Pair<string[], int> e8 = new Pair<string[], int>(eaux8, 8);
			cleCategorieMorphoSyntaxique.Add(e8);

			string[] eaux9 = {"avoir","être", "manger" }; // Verbe
			Pair<string[], int> e9 = new Pair<string[], int>(eaux9, 9);
			cleCategorieMorphoSyntaxique.Add(e9);

			string[] eaux10 = { "quoi" }; // Inconnu
			Pair<string[], int> e10 = new Pair<string[], int>(eaux10, 10);
			cleCategorieMorphoSyntaxique.Add(e10);

			categorieMorphoSyntaxique.Add(1, "ADJ"); // Adjectif
			categorieMorphoSyntaxique.Add(2, "ADV"); // Adverbe
			categorieMorphoSyntaxique.Add(3, "CONJ"); // Conjonction
			categorieMorphoSyntaxique.Add(4, "DET"); // Déterminant
			categorieMorphoSyntaxique.Add(5, "INTER"); // Interjection
			categorieMorphoSyntaxique.Add(6, "NOM"); // Nom
			categorieMorphoSyntaxique.Add(7, "PREPO"); // Préposition
			categorieMorphoSyntaxique.Add(8, "PRO"); // Pronom
			categorieMorphoSyntaxique.Add(9, "VERB"); // Verbe
			categorieMorphoSyntaxique.Add(10, "INCO"); // Inconnu


			PhraseGrammaire = new List<string[]>();
			string[] pgaux1 = { "INTER" };
			string[] pgaux2 = { "PRO" , "VERB", "VERB", "INCO", "DET", "ADJ", "NOM" };
			string[] pgaux3 = { "DET", "NOM", "VERB", "ADV", "ADJ"};
			/*string[] pgaux4 = { };
			string[] pgaux5 = { };
			string[] pgaux6 = { };
			string[] pgaux7 = { };*/

			PhraseGrammaire.Add(pgaux1);
			PhraseGrammaire.Add(pgaux2);
			PhraseGrammaire.Add(pgaux3);
			/*PhraseGrammaire.Add(pgaux4);
			PhraseGrammaire.Add(pgaux5);
			PhraseGrammaire.Add(pgaux6);
			PhraseGrammaire.Add(pgaux7);*/
			

			cleKeyWord = new List<Pair<string[], int>>();

			string[] kaux1 = { "bonjour", "Salut" }; 
			Pair<string[], int> k1 = new Pair<string[], int>(kaux1, 1);
			cleKeyWord.Add(k1);

			string[] kaux2 = { "manger"}; 
			Pair<string[], int> k2 = new Pair<string[], int>(kaux2, 2);
			cleKeyWord.Add(k2);

			string[] kaux3 = { "petit", "déjeuner" }; // Faiblesse visible de mon exemple et de mon code
			Pair<string[], int> k3 = new Pair<string[], int>(kaux3, 3);
			cleKeyWord.Add(k3);

			string[] kaux4 = { "nutella" }; 
			Pair<string[], int> k4 = new Pair<string[], int>(kaux4, 4);
			cleKeyWord.Add(k4);

			string[] kaux5 = { "bon" };
			Pair<string[], int> k5 = new Pair<string[], int>(kaux5, 5);
			cleKeyWord.Add(k5);

			keyWord = new Dictionary<int, string>();
			keyWord.Add(1, "Salutation");
			keyWord.Add(2, "Manger");
			keyWord.Add(3, "Matin");
			keyWord.Add(4, "Sucrerie");
			keyWord.Add(5, "Aimer");
		}

		public string chatbot(string entry)
		{
			// Initialisation des dictionnaires nécessaire à l'execution du chatbot
			initialiseDictionnaire();

			// Les prétraitements : segmentation (corpus -> tokens)
			List<Pair<List<string[]>, char>> entryPreTraiter = pretraitement(entry);

			// Les prétraitements : lemmatisation (forme canonique) hashmap
			List<Pair<List<string[]>, char>> entryLemmaliser = lemmatisation(entryPreTraiter);

			// Les prétraitements : étiquetage (bonne catégorie morpho-syntaxique (verbe, nom, ...) hashmap 
			List<Pair<List<Pair<string[], string[]>>, char>> entryEtiqueter = etiquetage(entryLemmaliser);

			// Analyse syntaxique (analyse descendante : on vérifie si la phrase correspond à une grammaire prédéfinie 
			//(hors contexte car pas le temps de rajouter un contexte)
			for (int i = 0; i < entryEtiqueter.Count; i++)
			{
				if (! analyseDescendante(entryEtiqueter.ElementAt(i)))
				{
					return "Je ne comprends pas votre phrase. La syntaxe de celle ci m'est inconnu.";
				}
			}

			// Analyse sémantique (recherche du sens d'une phrase hors contexte)
			// Algo : on associe un  mot clé à chaque token de la phrase
			List<List<string>> KWords = new List<List<string>>();
			for (int i = 0; i < entryEtiqueter.Count; i++)
			{
				KWords.Add(analyseSemantique(entryEtiqueter.ElementAt(i).getFirst()));
				Console.Write("Phrase "+ i + " : " +afficheList(KWords.ElementAt(i))+"\n");
			}

			for (int i = 0; i < KWords.Count; i++)
			{
				if (KWords.ElementAt(i).Count == 0)
				{
					return "Je n'ai pas compris ta phrase. Apprend moi de nouveaux mots clefs";
				}
			}

			string result = "Je ne sais pas quoi répondre, désolé";
			// Création de la réponse (Renvoi la réponse qui correspond à l'ensemble de mots clefs trouvé.)
			for (int i = 0; i < KWords.Count; i++)
			{
				result = reponse(KWords.ElementAt(i), entryEtiqueter.ElementAt(i).getSecond());
			}
			return result;
		}

		public string reponse(List<string> keyWords, char ponctuation)
		{
			string rep = "";
			// Pour chaque mot clé, on le remplace par un mot correspondant 
			List<string> words = new List<string>();
			for (int i = 0; i < keyWords.Count; i++)
			{
				for (int j = 0; j < keyWord.Keys.Count; j++)
				{
					if (keyWord[keyWord.Keys.ElementAt(j)].Equals(keyWords.ElementAt(i)))
					{
						string[] motsPossibles = null;
						for (int k = 0; k < cleKeyWord.Count; k++)
						{
							if (cleKeyWord.ElementAt(k).getSecond() == keyWord.Keys.ElementAt(j))
							{
								motsPossibles = cleKeyWord.ElementAt(k).getFirst();
							}
						}

						Random rand = new Random();
						int val = rand.Next(0, motsPossibles.Length - 1);
						words.Add(motsPossibles[val]);
					}
				}
			}

			Console.Write(afficheList(words)+"\n");

			List<string> morpho = new List<string>();
			for (int i = 0; i < words.Count; i++)
			{
				int cle = -1;
				for (int j = 0; j < cleCategorieMorphoSyntaxique.Count; j++)
				{
					
					if (cleCategorieMorphoSyntaxique.ElementAt(j).getFirst().Contains(words.ElementAt(i)))
					{
						cle = cleCategorieMorphoSyntaxique.ElementAt(j).getSecond();
						break;
					}


				}

				if (cle != -1)
						{
						morpho.Add(categorieMorphoSyntaxique[cle]);
						}
						else
						{
							
						morpho.Add(categorieMorphoSyntaxique[10]);
						}
					
			}

			Console.Write(afficheList(morpho)+"\n");

			// Puis on trouve un type de phrase contenant tous les mots 
			//private List<string[]> PhraseGrammaire;
			int indice = -1;
			for (int i = 0; i < PhraseGrammaire.Count; i++)
			{
				bool taux = true;
				for (int j = 0; j < morpho.Count; j++)
				{
					if (!PhraseGrammaire.ElementAt(i).Contains(morpho.ElementAt(j)))
					{
						taux = false;
						break;
					}
				}
				if (taux)
				{
					indice = i;
					break;
				}
			}

			Console.Write(afficheTableau(PhraseGrammaire.ElementAt(indice))+"\n");


			//On ajoute les mots qui manquent et on les met dans le bonne ordre. Et on envoi.
			for (int i = 0; i < PhraseGrammaire.ElementAt(indice).Length; i++)
			{
				if (morpho.Contains(PhraseGrammaire.ElementAt(indice).ElementAt(i)))
				{
					int pos = -1;
					for (int j = 0; j < morpho.Count; j++)
					{
						if (morpho.ElementAt(j).Equals(PhraseGrammaire.ElementAt(indice).ElementAt(i)))
						{
							pos = j;
							break;
						}
					}

					if (pos == -1)
					{
						Console.Write("Je ne connais pas de " + PhraseGrammaire.ElementAt(i) + " -_- \n");
					}
					else
					{
						rep += words.ElementAt(pos) + " ";
					}
				}
				else
				{
					int key = -1;
					switch (PhraseGrammaire.ElementAt(indice).ElementAt(i))
					{
						case "ADJ" :
							key = 1;
							break;
						case "ADV" :
							key = 2;
							break;
						case "CONJ" :
							key = 3;
							break;
						case "DET" :
							key = 4;
							break;
						case "INTER" :
							key = 5;
							break;
						case "NOM" :
							key = 6;
							break;
						case "PREPO" :
							key = 7;
							break;
						case "PRO" :
							key = 8;
							break;
						case "VERB" :
							key = 9;
							break;
						case "INCO" :
							key = 10;
							break;
					}

					for (int j = 0; j < cleCategorieMorphoSyntaxique.Count; j++)
					{
						if (cleCategorieMorphoSyntaxique.ElementAt(j).getSecond() == key)
						{
							Random rand = new Random();
							int r = rand.Next(0, cleCategorieMorphoSyntaxique.ElementAt(j).getFirst().Length - 1);
							rep += cleCategorieMorphoSyntaxique.ElementAt(j).getFirst()[r] + " ";
						}
					}
				}
			}
			return rep;
		}

		public string afficheList(List<string> list)
		{
			string aux = "";
			for (int i = 0; i < list.Count; i++)
			{
				aux += "'";
				aux += list.ElementAt(i);
				aux += "' ";
			}
			return aux;
		}

		public List<string> analyseSemantique(List<Pair<string[], string[]>> entryEtiqueter)
		{
			List<string> list = new List<string>();
			List<string> aux = new List<string>();
			for (int i = 0; i < entryEtiqueter.Count; i++)
			{
				for (int j = 0; j < entryEtiqueter.ElementAt(i).getFirst().Length; j++)
				{
					aux.Add(entryEtiqueter.ElementAt(i).getFirst()[j]);
				}
			}

			for (int i = 0; i < aux.Count; i++)
			{
				int cle = -1;
				for (int j = 0; j < cleKeyWord.Count; j++)
				{
					if (cleKeyWord.ElementAt(j).getFirst().Contains(aux.ElementAt(i)))
					{
						cle = cleKeyWord.ElementAt(j).getSecond();
						break;
					}
				}
				Console.Write(cle);
				if (cle != -1)
				{
					if (!list.Contains(keyWord[cle]))
					{
						list.Add(keyWord[cle]);
					}
				}
			}

			return list;
		}

		public bool analyseDescendante(Pair<List<Pair<string[], string[]>>, char> phrase)
		{
			int taille = 0;
			for (int i = 0; i < phrase.getFirst().Count; i++)
			{
				taille += phrase.getFirst().ElementAt(i).getFirst().Length;
			}

			string[] aux = new string[taille];
			int cpt = 0;
			for (int i = 0; i < phrase.getFirst().Count; i++)
			{
				for (int j = 0; j < phrase.getFirst().ElementAt(i).getFirst().Length; j++)
				{
					aux[cpt] = phrase.getFirst().ElementAt(i).getSecond()[j];
					cpt++;
				}
			}


			for (int i = 0; i < PhraseGrammaire.Count; i++)
			{
				bool trouver = true;
				if (aux.Length == PhraseGrammaire.ElementAt(i).Length)
				{
					for (int j = 0; j < PhraseGrammaire.ElementAt(i).Length; j++)
					{
						if (PhraseGrammaire.ElementAt(i)[j] != aux[j])
						{
							trouver = false;
							break;
						}
					}
					if (trouver)
					{ 
						return true;
					}
				}
			}
			return false;
		}

		public List<Pair<List<Pair<string[], string[]>>, char>> etiquetage(List<Pair<List<string[]>, char>> entryLemmaliser)
		{
			Console.Write("\n------------------------- Morpho Syntaxique ------------------------\n");
			List<Pair<List<Pair<string[], string[]>>, char>> entryEtiquetage = new List<Pair<List<Pair<string[], string[]>>, char>>();
			//corpus
			for (int i = 0; i < entryLemmaliser.Count; i++)
			{
				//phrase
				Pair<List<string[]>, char> phraseLem = entryLemmaliser.ElementAt(i);
				List<Pair<string[], string[]>> aux = new List<Pair<string[], string[]>>();
				for (int j = 0; j < phraseLem.getFirst().Count; j++)
				{
					// Préposition
					string[] prepolemma = phraseLem.getFirst().ElementAt(j);
					string[] prepomorpho = new string[prepolemma.Length];
					for (int k = 0; k < prepolemma.Length; k++)
					{
						string token = prepolemma[k];
						int cle = -1;
						//Recherche de la bonne clé
						for (int n = 0; n < cleCategorieMorphoSyntaxique.Count; n++)
						{
							if (cleCategorieMorphoSyntaxique.ElementAt(n).getFirst().Contains(token))
							{
								cle = cleCategorieMorphoSyntaxique.ElementAt(n).getSecond();
								break;
							}
						}

						if (cle != -1)
						{
							prepomorpho[k] = categorieMorphoSyntaxique[cle];
						}
						else
						{
							
							prepomorpho[k] = categorieMorphoSyntaxique[10];
						}
					}
					Pair<string[], string[]> paux = new Pair<string[], string[]>(prepolemma, prepomorpho);
					aux.Add(paux);
				}
				Pair<List<Pair<string[], string[]>>, char> phraseMormo = new Pair<List<Pair<string[], string[]>>, char>(aux, phraseLem.getSecond());
				entryEtiquetage.Add(phraseMormo);
			}

			Console.Write("\n\nTokens vs Morpho Syntaxique :\n");
			for (int i = 0; i < entryEtiquetage.Count; i++)
			{
				string debugProposition = "Phrase " + i + ":\n";
				for (int j = 0; j < entryEtiquetage.ElementAt(i).getFirst().Count; j++)
				{
					string[] tokens = entryEtiquetage.ElementAt(i).getFirst().ElementAt(j).getFirst();
					string[] morpho = entryEtiquetage.ElementAt(i).getFirst().ElementAt(j).getSecond();
					Console.Write("\tProposition " + j + ": " + afficheTableauMorpho(tokens, morpho) + "\n");
				}
			}

			return entryEtiquetage;
		}


		public List<Pair<List<string[]>, char>> lemmatisation(List<Pair<List<string[]>, char>> entryPreTraiter)
		{
			Console.Write("\n------------------------- Lemmatisation  ------------------------\n");
			for (int i = 0; i < entryPreTraiter.Count; i++)
			{
				for (int j = 0; j < entryPreTraiter.ElementAt(i).getFirst().Count; j++)
				{
					string[] proposition = entryPreTraiter.ElementAt(i).getFirst().ElementAt(j);
					for (int k = 0; k < proposition.Length; k++)
					{
						int cle = -1;
						for (int p = 0; p < cleCanonique.Count; p++)
						{
							if (cleCanonique.ElementAt(p).getFirst().Contains(proposition[k]))
							{
								cle = cleCanonique.ElementAt(p).getSecond();
								break;
							}
						}

						if (cle != -1)
						{
							entryPreTraiter.ElementAt(i).getFirst().ElementAt(j)[k] = canonique[cle];
						}
					}

				}

			}

			Console.Write("\n\nTokens Canonique :\n");
			for (int i = 0; i < entryPreTraiter.Count; i++)
			{
				string debugProposition = "Phrase " + i + ":\n";
				for (int j = 0; j < entryPreTraiter.ElementAt(i).getFirst().Count; j++)
				{
					Console.Write("\tProposition " + j + ": " + afficheTableau(entryPreTraiter.ElementAt(i).getFirst().ElementAt(j)) + "\n");
				}
			}

			return entryPreTraiter;
		}

		public List<Pair<List<string[]>, char>> pretraitement(string entry)
		{
			Console.Write("\n---------------- Pretraitement -------------------\n");

			// Gestion du '.' 
			List<Pair<string, char>> phrasesDeclaratif = new List<Pair<string, char>>();
			string[] separatorCorpus = entry.Split(new char[1] { '.' });
			for (int i = 0; i < separatorCorpus.Length; i++)
			{
				phrasesDeclaratif.Add(new Pair<string, char>(separatorCorpus[i], '.'));
			}

			// Gestion du '!'
			List<Pair<string, char>> phrasesExclamative = new List<Pair<string, char>>();
			for (int i = 0; i < phrasesDeclaratif.Count; i++)
			{
				string[] auxExclamative = phrasesDeclaratif.ElementAt(i).getFirst().Split(new char[1] { '!' });
				for (int j = 0; j < auxExclamative.Length; j++)
				{
					if (!auxExclamative[j].Equals(""))
					{
						if (auxExclamative.Length == 1)
						{
							phrasesExclamative.Add(phrasesDeclaratif.ElementAt(i));
						}
						else if (j == (auxExclamative.Length - 2))
						{
							phrasesExclamative.Add(new Pair<string, char>(auxExclamative[j], '!'));
						}
						else
						{
							phrasesExclamative.Add(new Pair<string, char>(auxExclamative[j], phrasesDeclaratif.ElementAt(i).getSecond()));
						}
					}
				}
			}

			// Gestion du '?'
			List<Pair<string, char>> phrasesInterrogatif = new List<Pair<string, char>>();
			for (int i = 0; i < phrasesExclamative.Count; i++)
			{
				string[] auxInterrogatif = phrasesExclamative.ElementAt(i).getFirst().Split(new char[1] { '?' });
				for (int j = 0; j < auxInterrogatif.Length; j++)
				{
					if (!auxInterrogatif.Equals(""))
					{
						if (auxInterrogatif.Length == 1)
						{
							phrasesInterrogatif.Add(phrasesExclamative.ElementAt(i));
						}
						else if (j == (auxInterrogatif.Length - 2))
						{
							phrasesInterrogatif.Add(new Pair<string, char>(auxInterrogatif[j], '?'));
						}
						else
						{
							phrasesInterrogatif.Add(new Pair<string, char>(auxInterrogatif[j], phrasesExclamative.ElementAt(i).getSecond()));
						}
					}
				}
			}


			//Debug Phrases 
			Console.Write("Debug séparateur phrase : \n");
			for (int i = 0; i < phrasesInterrogatif.Count; i++)
			{
				Console.Write("Phrase " + i + ": " + phrasesInterrogatif.ElementAt(i).getFirst() + " " + phrasesInterrogatif.ElementAt(i).getSecond() + "\n");
			}


			List<Pair<List<string[]>, char>> tokens = new List<Pair<List<string[]>, char>>();
			for (int i = 0; i < phrasesInterrogatif.Count; i++)
			{
				List<string[]> tab = new List<string[]>();
				string[] auxProposition = phrasesInterrogatif.ElementAt(i).getFirst().Split(new char[1] { ',' });
				for (int j = 0; j < auxProposition.Length; j++)
				{
					if (!auxProposition[j].Equals(""))
					{
						string[] aux = auxProposition[j].Split(new char[1] { ' ' });
						List<string> laux = new List<string>();
						for (int k = 0; k < aux.Length; k++)
						{
							if (!aux[k].Equals(""))
							{
								laux.Add(aux[k]);
							}
						}
						tab.Add(laux.ToArray());
					}
				}
				tokens.Add(new Pair<List<string[]>, char>(tab, phrasesInterrogatif.ElementAt(i).getSecond()));
			}

			// Debug Token
			Console.Write("\n\nTokens :\n");
			for (int i = 0; i < tokens.Count; i++)
			{
				string debugProposition = "Phrase " + i + ":\n";
				for (int j = 0; j < tokens.ElementAt(i).getFirst().Count; j++)
				{
					Console.Write("\tProposition " + j + ": " + afficheTableau(tokens.ElementAt(i).getFirst().ElementAt(j)) + "\n");
				}
			}

			return tokens;
		}

		// Transforme un string[] en string (uniquement pour le debug)
		public string afficheTableau(string[] tab)
		{
			string result = "";
			for (int i = 0; i < tab.Length; i++)
			{
				result += "'";
				result += tab[i];
				result += "'";
			}
			return result;
		}

		// Transforme un string[] en string (uniquement pour le debug)
		public string afficheTableauMorpho(string[] tab, string[] morpho)
		{
			string result = "";
			for (int i = 0; i < tab.Length; i++)
			{
				result += "'";
				result += tab[i];
				result += "'";
				result += "'[";
				result += morpho[i];
				result += "]";
			}
			return result;
		}
	}
}

