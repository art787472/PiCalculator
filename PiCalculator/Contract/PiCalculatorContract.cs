using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiCalculator.Contract
{
   
        public interface IPiCalcPresenter
        {
            IPiCalcView View { get; set; }
            void Calculate(long size);
        }

        public interface IPiCalcView
        {
            IPiCalcPresenter Presenter { get; set; }
            void CalculateFinish(PiMissionModel model);
        }
    
}
