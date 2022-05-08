namespace MoogleEngine;
public class Vector_Documents
{
    private List<Word_Properties> document = new List<Word_Properties>();
    private float Norma; 
    private string Title; 
    private string Path;
    private List<(int,int)> CambioPos_OnlyQuery = new List<(int,int)>();
    public Vector_Documents(string filename, string title, string path)
    {
        string[] words = Tratar_Data.Tokenizacion(filename);
        for(int i = 0; i < words.Length; i++)
        {
            if(!Tools.Esta(this.document, words[i], document.Count ))
            {
                Word_Properties aux = new Word_Properties(words[i], Tools.CantidadRep(words, words[i]), i);
                document.Add(aux);
            }
            else
            {
                int aux = Tools.findint(this, words[i]);
                this[aux].AggPos = i;
            }
        }
        this.Title = title.Remove(title.Length - 4); 
        this.Path = path;
    }
    public int size
    {
        get { return this.document.Count; }
    }
    public Word_Properties this[int i]
    {
        get { return this.document[i]; }
    }
    public float SC
    {
        get { return this.Norma; }
    }
    public string title
    {
        get { return this.Title; } 
    }
    public string ruta
    {
        get { return this.Path; }
    }
    public List<(int,int)> cambio
    {
        get { return this.CambioPos_OnlyQuery; }
    }
    public (int,int) AggCambio
    {
        set { this.CambioPos_OnlyQuery.Add(value); }
    }
    public void Remove(int pos)
    {
        this.document.RemoveAt(pos);
    }
    public void Purge()
    {
        for(int i = 0; i < this.size; i++)
        {
            if(document[i].InvertFrecuency <= (float)Math.Log10(5/4) && (document[i].InvertFrecuency >= 0)) 
            {
                document.RemoveAt(i);
                i--;
            }
        }
    }
    public void Sim_Cos()
    {
        float parcial = 0;
        for(int i = 0; i < this.size; i++)
        {
            parcial += (float)Math.Pow(this[i].peso_TF_idf,2);
        }
        this.Norma = (float)Math.Sqrt(parcial);
    }
}
