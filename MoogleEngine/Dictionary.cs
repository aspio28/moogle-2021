namespace MoogleEngine;
public class Dictionary
{
    private List<Word_Properties> diccionario = new List<Word_Properties>();
    
    public Dictionary(List<Vector_Documents> documents)
    {
        int pos = 0;

        for(int i = 0 ; i < documents.Count; i++)
        {
            for(int j = 0; j < documents[i].size; j++)
            {
                if(!Tools.Esta(diccionario, documents[i][j].palabra, pos))
                {
                    diccionario.Add(documents[i][j]);
                    pos++;
                }
            }
        }
       
    } 
    public void Asig_Idf(Word_Properties word)
    {
        for(int i = 0; i < this.diccionario.Count; i++)
        {
            if(this.diccionario[i].palabra == word.palabra)
                word.InvertFrecuency = this[i].InvertFrecuency;
        }
    }
    public Word_Properties this[int i]
    {
        get { return this.diccionario[i]; }
    } 
    public int size
    {
        get { return this.diccionario.Count; }
    }
}