namespace MoogleEngine;
public static class Moogle
{
    static List<Vector_Documents> documents = Corpus.Run();
    static Dictionary diccionario = new Dictionary(documents);
    public static SearchResult Query(string query) 
    {
        Operadores ope = new Operadores(query);
        Vector_Documents query_Trat = Corpus.Tratar_Query(query, diccionario);
        float[] scores = Calcula_score.Similitud(documents, query_Trat);
        Tools.QuikSort(scores, documents);
        if(ope.Hay) ope.AppOperadores(documents,scores);

        List<SearchItem> items = new List<SearchItem>();
        for(int i = 0; i < documents.Count; i++)
        {
            if(scores[i] > 0)
                items.Add(new SearchItem(documents[i].title,Snippet.Snip(documents[i],query_Trat),scores[i]));
        }
        string suggestion = "";
        if(items.Count != 0) 
            suggestion = Tools.Armar_Suggestion(query_Trat,query);

        if(items.Count == 0) items.Add(new SearchItem("Niguna coincidencia","",0));

        return new SearchResult(items, suggestion);
    }
}
