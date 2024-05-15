using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio;
using NAudio.Wave;

namespace Zasobnil
{
    internal class Zasobnik
    {
        
        public int MaxPocetNaboju;
        public int AktualniPocet;
        public string raze = "5.56x45mm";
        public List<Naboj> Naboje;

        public Zasobnik(int pocet) {
            this.MaxPocetNaboju = pocet;
            this.AktualniPocet = pocet;
            this.Naboje = new List<Naboj>();
            for(int i = 0; i < this.MaxPocetNaboju; i++)
            {
                Naboj naboj = new Naboj(this.raze);
                Naboje.Add(naboj);
            }
        }

        public void vystrelitNaboj()
        { 
            if(Naboje.Count > 0 ) { 
                Naboje.RemoveAt(0);
                this.AktualniPocet = this.AktualniPocet - 1;
                var audioFile = new AudioFileReader("./Sounds/shoot.wav");       
                using (var outputDevice = new WaveOutEvent())
                {
                    outputDevice.Init(audioFile);
                    outputDevice.Play();
                    while (outputDevice.PlaybackState == PlaybackState.Playing)
                    {
                        Console.WriteLine("Probíhá střelba");
                        Thread.Sleep(1000);
                    }
                }
                Console.WriteLine("Nový počet nábojů je {0}/{1}", this.AktualniPocet, this.MaxPocetNaboju);
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Máš prázdný zásobník!");
            Console.ReadKey();
            return;
        }

        public void vypisNaboje()
        {
            var table = new Table();

            table.AddColumn("Serial Number");
            table.AddColumn("Raze");

            table.Expand();


            foreach (Naboj naboj in Naboje)
            {
                (string, string) info = naboj.infoNaboj();
                string sn = info.Item1;
                string raze = info.Item2;
                table.AddRow(sn, raze);
            }

            AnsiConsole.Write(table);

            Console.ReadKey();
        }


        public void zmenitRazi(string novaRaze)
        {
            foreach(Naboj naboj in Naboje)
            {
                naboj.typRaze = novaRaze;   
            }
            Console.WriteLine("Nová ráže je {0}", novaRaze);
            Console.ReadKey();
        }

        public void doplnit(int pocet)
        {
            for (int i = pocet; i >= 0; i--)
            {
                if (this.AktualniPocet < this.MaxPocetNaboju)
                {
                    Naboj novyNaboj = new Naboj(this.raze);
                    this.Naboje.Add(novyNaboj);
                    var audioFile = new AudioFileReader("./Sounds/shoot.wav");
                    using (var outputDevice = new WaveOutEvent())
                    {
                        outputDevice.Init(audioFile);
                        outputDevice.Play();
                        while (outputDevice.PlaybackState == PlaybackState.Playing)
                        {
                            Console.WriteLine("Dobíjím {0}. náboj", i);
                            Thread.Sleep(1000);
                        }
                    }
                    this.AktualniPocet++;

                } else
                {
                    Console.WriteLine("Zásobník je plně doplněný");
                    Console.ReadKey();
                    break;
                }
            }
        }
    } 
}
