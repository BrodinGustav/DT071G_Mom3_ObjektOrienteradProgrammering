using System.Collections;

class Program
{
    static void Main (string[] args)
    {
        //Skapa objekt av GuestBook
        GuestBook guestBook = new GuestBook();

        bool isValid = false;       //Flagga som kontrollerar om input är korrekt

        //Do-while loop för kontroll av input
        do
        {
                // Huvudmenyn
            Console.Clear();                        //Skärmen skrivs om efter varje genomfört menyval
            Console.WriteLine("Gustavs Gästbok");
            Console.WriteLine("------------------");
            Console.WriteLine("1. Skriv i gästboken");
            Console.WriteLine("2. Ta bort inlägg");
            Console.WriteLine("X. Avsluta");
            Console.WriteLine("------------------");

            //Anropar metod för att visa inlägg
            guestBook.DisplayComments();

            //Lagrar input
            string choice = Console.ReadLine();
    
    try
    {
        //Kontroll om input kan konverteras till heltal
            if(int.TryParse(choice, out int option))
            {
                switch(option)
                {
                    //Skapa inlägg
                    case 1:
                    Console.Clear();
                     Console.Write("Ange ägare: ");
                        string owner = Console.ReadLine();
                        Console.Write("Ange kommentar: ");
                        string comment = Console.ReadLine();
                     

                    //Kontroll om input är korrekt
                            if (string.IsNullOrWhiteSpace(owner) || string.IsNullOrWhiteSpace(comment))
                            {
                                Console.WriteLine("Var god ange både ägare och kommentar.");
                            }
                            else
                            {
                                //Anropar metod för att skapa inlägg
                                guestBook.CreateComment(owner, comment);
                            }         
                                                
                        break;
                
                    //Radera inlägg
                    case 2:
                    Console.Clear();
                    //Anropar LoadComment för att läsa in antalet kommentarer
                     int loadedCount = guestBook.LoadComments();

                        // Kontroll om det finns inlägg att radera
                        if (loadedCount == 0)
                    {
                        Console.WriteLine("Det finns inga inlägg att radera.");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Ange inlägg som önskas raderas: ");
                    }

                    //Kontroll om input kan konverteras till heltal
                    if(int.TryParse(Console.ReadLine(), out int deleteIndex))
                    {
                        // Kontrollera om index är giltigt
                         if (deleteIndex > 0 && deleteIndex <= loadedCount) 
                         {
                            guestBook.DeleteComment(deleteIndex - 1);       //-1 justerar index till 0 för array
                         }
                                           
                                else
                                {
                                    Console.WriteLine("Ogiltigt index. Vänligen ange ett giltigt index.");
                                }
                    }
                                    //Kontroll om input är annat än heltal
                                    else
                                    {
                                        Console.WriteLine("Ogiltig inmatning. Vänligen ange ett giltigt index.");
                                    }
                    break;
                       
                   default:
                        Console.WriteLine("Ogiltigt val, försök igen.");
                        break;
                }

            }
            
            //Val att avsluta programmet
            else if (choice?.ToUpper() == "X")
            {
                isValid = true;  // Avslutar programmet
            }
                else
                {
                    Console.WriteLine("Ogiltig inmatning, var god ange 1, 2 eller X.");
                }

            Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
            Console.ReadKey();
           
    }
    catch (ArgumentException e)
    {
        Console.WriteLine($"Fel, {e.Message} Var god ange text");
    }
    catch (Exception e)
    {
        Console.WriteLine("------------------");
        Console.WriteLine($"Ett oväntat fel inträffade: {e.Message}");
    }
      continue;  // Tar användaren tillbaka till huvudmenyn

        } while (!isValid); // Loopen fortsätter tills input är korrekt

        Console.WriteLine("Programmet är avslutat.");
    }
}