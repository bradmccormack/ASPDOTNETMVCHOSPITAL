using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Interfaces
{
    public interface IDoctorRepository
    {
        IDoctor GetDoctor(int id);
        List<IDoctor> GetDoctors();
    }

    public interface IPatientRepository
    {
        IPatient GetPatient(int id);
        bool UpdatePatient(IPatient patient);
        List<IPatient> GetPatients();
        IVisit GetVisit(int id);
        List<IVisit> GetVisits();
    }

    public interface IBedRepository
    {
        IBed GetBed(int id);
        List<IBed> GetBeds();
    }

}
