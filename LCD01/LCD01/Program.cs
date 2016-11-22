using System;
using Microsoft.SPOT;
using GHI.Pins;
using Microsoft.SPOT.Hardware;
using System.Threading;

namespace LCD01
{
    public class Program
    {
        #region Initialisation variables

        const byte POSITION_CURSEUR = 0x00;
        static string Phrase = DateTime.Now.Hour.ToString() + "h " + DateTime.Now.Minute.ToString() + "min";

        #region Initialisation Ecran LED
        static OutputPort D4 = new OutputPort(FEZSpider.Socket11.Pin5, true); //Affichage Ecran LED
        static OutputPort D5 = new OutputPort(FEZSpider.Socket11.Pin7, true);
        static OutputPort D6 = new OutputPort(FEZSpider.Socket11.Pin9, true);
        static OutputPort D7 = new OutputPort(FEZSpider.Socket11.Pin6, true);
        static OutputPort BacklightEcranLed = new OutputPort(FEZSpider.Socket11.Pin8, true); //Backlight de l'ecran LED
        static OutputPort Enable = new OutputPort(FEZSpider.Socket11.Pin3, true); //Enable de l'ecran LED
        static OutputPort RS = new OutputPort(FEZSpider.Socket11.Pin4, false); //RS de l'écran
        #endregion

        #endregion

        public static void Main()
        {

            InitialisationProgramme();

            #region Affichage d'une phrase qui défile
            RS.Write(true);
            AfficheChaine(Phrase);
            while (true)
	        {
	            FaireDefilerDroite();
                
            }
            #endregion

            #region Position Curseur
            //RS.Write(false);
            //PositionCurseur = 0x40;
            //SendCmd((byte)(0x80 + PositionCurseur));

            //RS.Write(true);
            //AfficheChaine("Lucas ???");
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
        public static void AfficheChaine(string chaine)
        {
            foreach (char car in chaine)
            {
                SendCmd((byte)car);
            }
        } 
        public static void FaireDefilerGauche()
        {
            RS.Write(false);
            SendCmd(0x18);
            Thread.Sleep(500);
        }
        public static void FaireDefilerDroite()
        {
            RS.Write(false);
            SendCmd(0x1C);
            Thread.Sleep(500);
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
