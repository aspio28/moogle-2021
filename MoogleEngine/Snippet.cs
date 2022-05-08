using System.Text.RegularExpressions;
namespace MoogleEngine;
public class Snippet
{
    public static string Snip(Vector_Documents document, Vector_Documents query)
    {
        int aux = -1;
        for(int i = 0; i < query.size; i++)
        {
            aux = Tools.findint(document,query[i].palabra);
            if(aux >= 0) break;    
        }
        StreamReader reader = new StreamReader(document.ruta);
        string texto = reader.ReadToEnd();
        string[] words = Regex.Split(texto, @"\W+");
        if(aux >= 0)
        {
            aux = document[aux].pos;
    
            return CrearSnippet(words, aux);
        }
        return "Query's not found";
    }
    public static string CrearSnippet(string[] words, int pos)
    {
        string aux = "";
        for(int i = (pos - 8 >= 0) ? pos -8 : 0; i <= ((pos + 8 < words.Length) ? pos + 8 : words.Length); i++)
        {
            aux += words[i]+" "; 
        }
        return aux;
    } 
}
    
