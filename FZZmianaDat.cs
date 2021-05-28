using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hydra;
using System.Windows;
using System.Drawing;

[assembly: CallbackAssemblyDescription("CG39_FZ_Date", "Dodaje button do edycji dat na zatwierdzonym dokumencie.", "CG39", "1.0", "2020.2.0.0", "26-05-2021")]
namespace FZZmianaDat
{
    public static class ZmianaDat
    {
        public static void DodajButtonEdycji(ClaWindow window, Rectangle bounds)
        {
            ClaWindow header = window.AllChildren["?TabNaglowek"];
            ClaWindow button = header.Children.Add(ControlTypes.button);
            button.Visible = true;
            button.Enabled = true;
            button.TextRaw = "Edytuj daty";
            button.Bounds = bounds;
            button.OnAfterAccepted += (Procedures ProcId2, int ControlId2, Events Event2) =>
            {
                foreach (ClaWindow a in window.AllChildren)
                {
                    if (a.Id == 238 || a.Id == 239 || a.Id == 240 || a.Id == 241)
                    {
                        a.Enabled = true;
                        a.Visible = true;
                    }

                }
                return true;
            };
        }

        public static Rectangle PobierzKoordynatyDatyWplywu(ClaWindow window)
        {
            Rectangle bounds = new Rectangle(0,0,0,0);
            foreach (ClaWindow a in window.AllChildren)
            {
                if (a.Id == 238)
                {
                    bounds = a.Bounds;
                }
            }
            return bounds;
        }
    }
    // podpięcie się pod procedurę FZ Spinacza - format dokumentu: (S)FZ-xxx
    [SubscribeProcedure(Procedures.TrN_FZSpinacz, "TrN_FZSpinacz")]
    public class ZmianaDatFZS : Callback
    {

        public override void Init()
        {
            AddSubscription(true, 0, Events.OpenWindow, otworzEdycje);
        }

        public bool otworzEdycje(Procedures ProcId, int ControlId, Events Event)
        {
            Rectangle bound = ZmianaDat.PobierzKoordynatyDatyWplywu(base.GetWindow());
            ZmianaDat.DodajButtonEdycji(base.GetWindow(), new Rectangle(bound.X - 50, bound.Y, 50, 15));
            return true;
        }

        public override void Cleanup()
        {
            
        }
    }
    [SubscribeProcedure(Procedures.TrN_FZ, "TrN_FZ")]
    public class ZmianaDatFZ : Callback
    {
        public override void Init()
        {
            AddSubscription(true, 0, Events.OpenWindow, otworzEdycje);
        }

        public bool otworzEdycje(Procedures ProcId, int ControlId, Events Event)
        {
            Rectangle bound = ZmianaDat.PobierzKoordynatyDatyWplywu(base.GetWindow());
            ZmianaDat.DodajButtonEdycji(base.GetWindow(), new Rectangle(bound.X - 50, bound.Y, 50, 15));
            return true;
        }

        public override void Cleanup()
        {

        }
    }
}
