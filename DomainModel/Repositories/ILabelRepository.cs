using DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Repositories
{
    public interface ILabelRepository
    {
        IEnumerable<LabelDTO> GetLabelsByCardId(string cardId);

        LabelDTO CreateLabel(LabelDTO label);

        //void UpdateLabel(LabelDTO label);

        bool DeleteLabel(string id);
    }
}
