namespace MoogleEngine;
public class Tools
{
    public static void Calcular_idf(List<Vector_Documents> documents)
    {
        for(int i = 0; i < documents.Count; i++)
        {
            for(int j = 0; j < documents[i].size;j++)
            {
                documents[i][j].IDF(documents.Count,CantidadApp(documents, documents[i][j].palabra));
            }
        }
    }
    public static int CantidadApp(List<Vector_Documents> documents, string word)
    {
        int total = 0;
        for(int i = 0; i < documents.Count; i++)
        {
            if(find(documents[i], word))
                total++;
        }
        return total;
    }
    public static int CantidadRep(string[] words, string word)
    {
        int total = 0;

        for(int i = 0; i < words.Length; i++)
        {
            if(words[i] == word)
                total ++;
        }
        return total;
    }
    public static bool find(Vector_Documents words, string word)
    {
        for(int i = 0; i < words.size; i++)
        {
            if(words[i].palabra == word)
               return true;
        }
        return false;
    }
    public static int findint(Vector_Documents words, string word)
    {
        for(int i = 0; i < words.size; i++)
        {
            if(words[i].palabra == word)
               return i;
        }
        return -1;
    }
    public static bool Esta(List<Word_Properties> words, string word, int pos)
    {
        for(int i = 0; i < pos; i++)
        {
            if(words[i].palabra == word)
                return true;
        }
        return false;
    }
    public static (int,int) EstaDoble(Vector_Documents words, string word1, string word2)
    {
        (int,int) aux = (-1,-1);
        for(int i = 0; i < words.size; i++)
        {
            if(words[i].palabra == word1) aux.Item1 = i;

            if(words[i].palabra == word2) aux.Item2 = i;

            if(aux.Item1 != -1 && aux.Item2 != -1) return aux;
        }
        return aux;
    }
    public static void QuikSort(float[] scores,List<Vector_Documents> documents)
    {
        QuikSort(scores,documents, 0, scores.Length - 1);
    }
    private static void QuikSort(float[] scores,List<Vector_Documents> documents, int inicio, int fin)
    {
        if(inicio >= fin) return;

        float aux = scores[inicio];
        
        int mitad = Cortar(scores,documents, inicio, fin, aux);
        QuikSort(scores, documents, inicio, mitad);
        QuikSort(scores, documents, mitad + 1, fin);
    }
    private static int Cortar(float[] scores,List<Vector_Documents> documents, int ini, int fin, float aux)
    {
        int i = ini - 1;
        int j = fin + 1;
        while(true)
        {
            do i++; while(scores[i] > aux);
            do j--; while(scores[j] < aux);
          
            if(i >= j) return j;

            float cambio = scores[j];
            Vector_Documents auxiliar = documents[j];
            scores[j] = scores[i];
            documents[j] = documents[i];
            scores[i] = cambio;
            documents[i] = auxiliar;
        }
    }
    public static int Distancia(string texto, int pos)
    {
        int distance = 0;
        for(int i = pos; i < texto.Length; i++)
        {
            if(Char.IsLetter(texto[i])) distance ++;

            else if(distance == 0) continue;

            else break;
        }
        return distance;
    }
    public static int DistaciaInversa(string texto, int pos)
    {
        int distance = 0;
        for(int i = pos; i >= 0; i--)
        {
            if(Char.IsLetter(texto[i])) distance++;

            else if(distance == 0) continue;

            else break;
        }
        return distance;
    }
    public static string Armar_Suggestion(Vector_Documents query_trat, string query)
    {
        string suggestion = "";
        int cuenta = 0;
        string[] words = query.Split(new char[]{'!', '*', ' ', '~','^'}, StringSplitOptions.RemoveEmptyEntries);
        List<(int,int)> cambios = query_trat.cambio;
        for(int i = 0; i < cambios.Count; i++)
        {
            for(int j = 0; j < words.Length; j++)
            {
                if(j == cambios[i].Item1) 
                {
                    suggestion += query_trat[cambios[i].Item2].palabra + " ";
                    cuenta++;
                }
                else if(words[i] == " ") continue;

                else 
                {
                    suggestion += words[j] + " ";
                    cuenta ++;
                }
            }
        }
        return suggestion;
    }
}

