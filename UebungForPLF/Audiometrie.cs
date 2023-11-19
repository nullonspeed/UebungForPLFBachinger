using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UebungForPLF
{
    internal class Audiometrie
    {
        public DateTime dateOfTest { get; set; }
        public int dezibelRight { get; set; }
        public int dezibelLeft { get; set; }

        public override string? ToString()
        {
            return $"Audiometrie: {dateOfTest.ToShortDateString()}";
        }

        internal string ToCSV()
        {
            return $";{dateOfTest.ToShortDateString()};{dezibelRight};{dezibelLeft}";
        }


        public static bool TryParse(string el1, string el2, string el3, out Audiometrie aud)
        {
            try
            {
               
                
                    aud = new Audiometrie
                    {
                        dateOfTest = DateTime.Parse(el1),
                        dezibelRight = int.Parse(el2),
                        dezibelLeft = int.Parse(el3)
                    };
                
                    return true;
            }
            catch
            {
                aud = null;
                return false;
            }
        }

       
    }
}
