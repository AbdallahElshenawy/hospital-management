using Hospital_Management.Models;
using System.Collections.Generic;
namespace Hospital_Management.Repository
{
    public interface IRecordRepo : IRepository<Record, int>
    {
        List<Record> GetByDoctorId(string doctorId);



    }
}
