
namespace Hospital.Interfaces
{
    public interface IBed
    {
        int Id { get; set;}
        string Name { get; set;}
        decimal RatePerDay { get; set;}
        string BedType { get; set;}
    }

    public interface IDoctor
    {
        int Id { get; set; }
        string Name { get; set; }
        string Address { get; set; }
        string Phone { get; set; }
    }

    public interface IPatient
    {
        int Id { get; set; }
        string Name { get; set; }
        string Address { get; set; }
        System.DateTime DateOfBirth { get; set; }
        string Phone { get; set; }
        string EmergencyContact { get; set; }
        System.DateTime DateOfRegistration { get; set; }
    }


    public interface IVisit
    {
        int Id { get; set;}
        string Name { get; set; }
        string Address { get; set; }
        bool isInPatient { get; set; }
        string DrName { get; set; }
        string BedName { get; set; }
        System.DateTime DateOfVisit { get; set; }
        System.DateTime DateOfDischarge { get; set; }
        string Symptoms { get; set; }
        string Disease { get; set; }
        string Treatment { get; set; }
    }

    public interface IVisitSearch
    {
        string Name { get; set; }
        System.DateTime DateOfVisit { get; set; }
        System.DateTime? DateOfDischarge { get; set; }
    }


}
