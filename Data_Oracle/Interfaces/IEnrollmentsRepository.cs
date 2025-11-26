using Data_Oracle.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Oracle.Interfaces
{
    public interface IEnrollmentsRepository
    {

        Enrollments GetEnrollmentsByID(int ID);

        Enrollments GetEnrollmentsByUserID(int Id);

        void Save(Enrollments enrollments);

        Enrollments GetEnrollmentsByUserIdAndOderId(int userID, int courseID);

        void SaveChange();

    }
}
