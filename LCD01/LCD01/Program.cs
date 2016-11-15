using System;
using Microsoft.SPOT;
using GHI.Pins;
using Microsoft.SPOT.Hardware;
using System.Threading;

namespace LCD01
{
    public class Program
    {
        //Initialisation variables


        //Ecran LED
        static OutputPort D4 = new OutputPort(FEZSpider.Socket11.Pin5, true); //Affichage Ecran LED
        static OutputPort D5 = new OutputPort(FEZSpider.Socket11.Pin7, true);
        static OutputPort D6 = new OutputPort(FEZSpider.Socket11.Pin9, true);
        static OutputPort D7 = new OutputPort(FEZSpider.Socket11.Pin6, true);
        static OutputPort BacklightEcranLed = new OutputPort(FEZSpider.Socket11.Pin8, true); //Backlight de l'ecran LED
        static OutputPort Enable = new OutputPort(FEZSpider.Socket11.Pin3, true); //Enable de l'ecran LED
        static OutputPort RS = new OutputPort(FEZSpider.Socket11.Pin4, false); //RS de l'�cran

        public static void Main()
        {
            
            //Traitement

            //Initialisation programme
            Thread.Sleep(40);
            SendCmd(0x33);
            SendCmd(0x32);
            SendCmd(0x0C);
            SendCmd(0x01);

            //Affichage de 'a'
            RS.Write(true);
            SendCmd((byte)'A');
            SendCmd((byte)'d');
            SendCmd((byte)'r');
            SendCmd((byte)'i');
            SendCmd((byte)'e');
            SendCmd((byte)'n');

            
    

        }
        public static void EnableSequence() //Methode pour faire fonctionner le LCD
        {
            Enable.Write(true); 
            Enable.Write(false);

        }
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
    }
}
