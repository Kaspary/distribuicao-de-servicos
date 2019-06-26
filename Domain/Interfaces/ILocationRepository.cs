using System;
using System.Net.Configuration;

namespace Domain.Interfaces
{
    public interface ILocationRepository
    {
        Guid Insert(Model model);
    }
}