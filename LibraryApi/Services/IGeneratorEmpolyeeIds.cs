using System;

namespace LibraryApi.Services
{
    public interface IGeneratorEmpolyeeIds
    {
        Guid GetNewEmployeeId();
    }
}