using System;

namespace LibraryApi.Services
{
    public class EmpolyeeIdGenerator : IGeneratorEmpolyeeIds
    {

        public Guid GetNewEmployeeId()
        {
            return Guid.NewGuid();
        }
    }
}
