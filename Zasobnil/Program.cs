using Spectre.Console;
using System.Runtime.CompilerServices;
using Zasobnil;

int size = 0;
bool isRunning = true;

size = AnsiConsole.Ask<int>("Jaká je velikost zásobníků? ");



while (!(size > 0) || !(size <= 60))
{
    size = AnsiConsole.Ask<int>("Velikost musí být v rozsahu 0 až 60: ");
}

Zasobnik zasobnik = new Zasobnik(size);

do
{
    Console.Clear();
    Console.WriteLine("Pro nejlepší zážitek si zapni zvuky");
    var option = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .Title("Co chceš udělat? ")
            .AddChoices(new[]
            {
                "Zkontrolovat zásobník",
                "Vystřelit náboj",
                "Dobít zásobník",
                "Změnit ráži"
               }));

    switch(option)
    {
        case "Zkontrolovat zásobník":
            zasobnik.vypisNaboje();
            break;
        case "Vystřelit náboj":
            zasobnik.vystrelitNaboj();
            break;
        case "Dobít zásobník":
            int novyNaboje = AnsiConsole.Ask<int>("Kolik nábojů chceš dobít? ");
            zasobnik.doplnit(novyNaboje);
            break;
        case "Změnit ráži":
            string raze = AnsiConsole.Ask<string>("Jaká je nová ráže");
            zasobnik.zmenitRazi(raze);
            break;
    }
} while (isRunning);