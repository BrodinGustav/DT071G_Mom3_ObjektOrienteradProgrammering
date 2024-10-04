using System.Text.Json;

//Klass för kommentarer
public class GuestBookComments
{
    public string Owner { get; set; }
    public string Comment { get; set; }
}

    //Klass för gästboken
    public class GuestBook
    {
    
    //Skapar lista som lagrar kommentarer
    public List<GuestBookComments> guestBookList {get; set;}

    public GuestBook()
    {
        //Skapar list-objekt utifrån klassen
        guestBookList = new List<GuestBookComments>();
        
        //Anropar metod fär att läsa in tidigare inlägg från JSON
        LoadComments();
    }

    //Metod för att läsa in tidigare inlägg 
    public int LoadComments()   //int för att kunna returnera antalet inlägg till Main
    {
        //Lagrar json-filen i variabel
        string fileName = "guestbook.json";

         // Variabel för att hålla räkningen av kommentarer
        int commentCount = 0;

        //Kontroll om json-fil existerar
        if(File.Exists(fileName))
        {
            //Läser in filen som sträng
            string jsonText = File.ReadAllText(fileName);

            //Deserialiserar json-strängen till en lista av objekt. Om resultatet är NULL skapas en tom lista till guestBookList-variabeln.
            guestBookList = JsonSerializer.Deserialize<List<GuestBookComments>>(jsonText) ?? new List<GuestBookComments>();
           
            //Hämta antalet kommentarer
            commentCount = guestBookList.Count; 
           
        }
        else
        {
            Console.WriteLine("Gästboken är tom");
        }
        return commentCount; //Returnera antalet inlägg
    }

    //Metod för att skapa inlägg
    public void CreateComment(string owner, string comment)
    {
        //Kontroll om input är korrekt
        if(string.IsNullOrWhiteSpace(owner) || string.IsNullOrWhiteSpace(comment))
        {
            Console.WriteLine("Var god ange ägare och text");
        }
        else
        {
            //Skapar objekt av klassen GuestBookComments
            GuestBookComments newComment = new GuestBookComments 
            { 
                Owner = owner, 
                Comment = comment 
            };

            //Lägg till kommentar-objektet till listan med struktur enligt klassen GuestBookComments konstruktor
            guestBookList.Add(newComment);
            
            //Spara inlägg till Json
            SaveComment();
        }
    }

    //Metod för att spara inlägg till JSON
    public void SaveComment()
    {
        //Serialiserar listan (guestBookList) till Json-sträng. Gör strängen mer läsbar med WriteIntended.
        string json = JsonSerializer.Serialize(guestBookList, new JsonSerializerOptions {WriteIndented = true });

        //Json-strängen lagras i filen guestbook.json.
        File.WriteAllText("guestbook.json", json);
    }

    //Metod för att radera inlägg
    public void DeleteComment(int index)
    {
        //Kontroll om deleteIndex är inom marginalen för listan
        if(index >= 0 && index < guestBookList.Count)
        {
             //Ta bort inlägget
            guestBookList.RemoveAt(index);
           
            //Spara uppdaterad lista
            SaveComment();
           
        }
        else if (index < 0)
        {
            Console.WriteLine("OBS! Angivet inlägg finns ej");
        }
    else
    {
        Console.WriteLine("------------------");
        Console.WriteLine("Ogiltig inmatning. Vänligen ange ett giltigt index.");
    }
    }
    
    //Metod för att visa alla inlägg
    public void DisplayComments()
    {
        //Kontroll om inlägg finns
        if(guestBookList.Count > 0)
        {
              Console.WriteLine("\n--- Gästbokens Inlägg ---");
                        Console.WriteLine("------------------");
            
            //Loopar igenom samtliga inlägg i listan
            for(int i = 0; i < guestBookList.Count; i++)
            {
                //Utskrift
                var comment = guestBookList[i];
                Console.WriteLine($"[{i + 1}] Ägare: {comment.Owner} - {comment.Comment} ");
            }
        }
        else
        {
            Console.WriteLine("Gästboken är tom.");
        }
    }
}