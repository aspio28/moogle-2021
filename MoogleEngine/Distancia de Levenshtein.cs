namespace MoogleEngine;
public class Distancia_de_Levenshtein
{
    public static int LD(string word1, string word2)
    {
        int total = 0;
        int aux1 = word1.Length;
        int aux2 = word2.Length;
        int[,] m = new int[aux1 + 1, aux2 + 1];

        for(int i = 0; i <= aux1; i++)
        {
            m[i,0] = i;
        }
        for (int i = 0; i <= aux2; i++)        
        {
            m[0,i] = i; 
        }
        for(int i = 1; i <= aux1; i++)
        {
            for(int j = 1; j <= aux2; j++)
            {
                total = (word1[i - 1] == word2[j - 1])? 0:1;
                m[i,j] = Math.Min(Math.Min(m[i-1, j] + 1, m[i, j-1] + 1),m[i-1,j-1] + total);
            }
        }
        return m[aux1,aux2];
    }
}
