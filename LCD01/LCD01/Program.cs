using System;
using Microsoft.SPOT;
using GHI.Pins;
using Microsoft.SPOT.Hardware;
using System.Threading;
using GHI.Processor;

namespace LCD01
{
    public class Program
    {
        #region Initialisation variables

        public static DateTime DT;
        public static byte PositionCurseur = 0x40;


        #region Initialisation Ecran LED
        static OutputPort D4 = new OutputPort(FEZSpider.Socket11.Pin5, true); //Affichage Ecran LED
        static OutputPort D5 = new OutputPort(FEZSpider.Socket11.Pin7, true);
        static OutputPort D6 = new OutputPort(FEZSpider.Socket11.Pin9, true);
        static OutputPort D7 = new OutputPort(FEZSpider.Socket11.Pin6, true);
        static OutputPort BacklightEcranLed = new OutputPort(FEZSpider.Socket11.Pin8, true); //Backlight de l'ecran LED
        static OutputPort Enable = new OutputPort(FEZSpider.Socket11.Pin3, true); //Enable de l'ecran LED
        static OutputPort RS = new OutputPort(FEZSpider.Socket11.Pin4, false); //RS de l'écran
        #endregion


        public static void Main()
        {
            
            InitialisationProgramme();

            #region Affichage d'une phrase qui défile //Désactivé
            //RS.Write(true);
            //AfficheChaine("");
            //while (true)
            //{
            //    FaireDefilerDroite();

            //}
            #endregion 

            #region Initialisation Heure
            DT = new DateTime(2016, 11, 29, 15, 48, 0);
            RealTimeClock.SetDateTime(DT);
            DT = RealTimeClock.GetDateTime();
            #endregion



        }
        public static void EnableSequence()
        {
            Enable.Write(true); 
            Enable.Write(false);

        } //Methode pour faire fonctionner le LCD
        public static void SendCmd(byte Value)
        {
            D7.Write((Value & 0x80) == 0x80);
            D6.Write((Value & 0x40) == 0x40);
            D5.Write((Value & 0x20) == 0x20);
            D4.Write((Value & 0x10) == 0x10);
            EnableSequence();
            Thread.Sleep(1);
            D7.Write((Value & 0x08) == 0x08);
            D6.Write((Value & 0x04) == 0x04);
            D5.Write((Value & 0x02) == 0x02);
            D4.Write((Value & 0x01) == 0x01);
            EnableSequence();
            Thread.Sleep(1);
        }
        public static void FaireDefiler(string Choix)
        {
            RS.Write(false);
            if (Choix == "gauche")
            {
                SendCmd(0x18);
            }
            else if (Choix == "droite")
            {
                SendCmd(0x1C);
            }
            else
            {
                SendCmd(0x1C);
            }
            
            Thread.Sleep(500);
        }
        public static void AfficheChainePosition(string chaine, byte position)
        {
            RS.Write(false);
            SendCmd((byte)(0x80 + position));

            RS.Write(true);
            foreach (char car in chaine)
            {
                SendCmd((byte)car);
            }   
        }
        public static void InitialisationProgramme()
        {
            Thread.Sleep(40);
            SendCmd(0x33);
            SendCmd(0x32);
            SendCmd(0x0C);
            SendCmd(0x01);
        }
    }
}
