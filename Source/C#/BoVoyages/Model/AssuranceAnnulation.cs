using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVoyages.Model
{
    class AssuranceAnnulation : Assurance
    {
        public AssuranceAnnulation() {}

        public AssuranceAnnulation(int assuranceId, ASSURANCE_TYPE assuranceType, string assuranceNom, string assuranceDescription): 
                                   base(assuranceId, assuranceType, assuranceNom, assuranceDescription) {}

    }
}
