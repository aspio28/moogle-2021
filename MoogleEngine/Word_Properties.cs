namespace MoogleEngine;
public class Word_Properties
{
    private string word;
    private int tf;
    private float idf = -1;
    private List<int> posDoc = new List<int>();

    public Word_Properties(string word, int repeticiones,int pos)
    {
        if(word == null)
           throw new ArgumentException ("The imput can't be null");
        
        this.word = word;
        this.tf = repeticiones;
        this.posDoc.Add(pos);
    }
    public void IDF(int documents, int repet)
    {
        this.idf = (float)Math.Log10(documents/repet);
    }
    public int Frecuncy
    {
        get { return this.tf; }
    }
    public string palabra
    {
        get { return this.word; }

        set{ this.word = value; }
    }
    public int AggPos
    {
        set { this.posDoc.Add(value); }
    }
    public float InvertFrecuency
    {
        get { return this.idf; }

        set { this.idf = value; }
    }
    public int pos
    {
        get{ return this.posDoc[0]; }
    }
    public float peso_TF_idf
    {
        get{ return this.idf*this.tf; }
    }
    public List<int> ArrPos
    {
        get{ return this.posDoc; }
    }
}
