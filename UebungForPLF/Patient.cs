using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UebungForPLF
{
    internal class Patient
    {
        public int? SVNR { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public List<Audiometrie> AudiometrieList { get; set; } = new List<Audiometrie>();

        public override string? ToString()
        {
            return $"[{SVNR}] {Lastname} {Firstname}";
        }

      public string toCSV()
        {
            string Audiolist = "";
            for (int listCounter = 0; listCounter < AudiometrieList.Count; listCounter++)
            {
                Audiolist += AudiometrieList[listCounter].ToCSV(); 

            }

            return $"{SVNR};{Firstname};{Lastname}{Audiolist}";
        }


        public static bool TryParse(string line, out Patient patient)
        {
            try
            {
                string[] lineElements = line.Split(';');
                if(lineElements.Length >= 3 )
                {
                    patient = new Patient
                    {
                        SVNR = int.Parse(lineElements[0]),
                        Firstname = lineElements[1],
                        Lastname = lineElements[2]
                    };


                    if(lineElements.Length > 3 )
                    {
                        int temporaryCounter = lineElements.Length ;
                        int temporaryCounterfromZero = 3;

                        while (temporaryCounterfromZero != temporaryCounter)
                        {
                            if (Audiometrie.TryParse(lineElements[temporaryCounterfromZero], lineElements[temporaryCounterfromZero+1], lineElements[temporaryCounterfromZero+2], out Audiometrie aud))
                            {
                               patient.AudiometrieList.Add(aud);
                                temporaryCounterfromZero +=3;
                            }
                           
                            else if(temporaryCounterfromZero > temporaryCounter)
                            {
                                throw new Exception();
                            }
                            else
                            {
                                throw new Exception();
                            }


                        }
                            
                         
                    }
                    return true ;


                }
                else
                {
                    patient = null;
                    return false;
                }

            }
            catch (Exception e)
            {
                patient = null;
                return false;
            }

           
        }
    }


}
