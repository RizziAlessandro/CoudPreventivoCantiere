using System;
using System.Collections.Generic;
using System.Text;
using DBRepository.Models;

namespace DBRepository
{
    public interface IInterventionRepository : IRepositoryBase<Intervention, int>
    {
        IEnumerable<Intervention>GetElectricalInterventions();
    }
}
