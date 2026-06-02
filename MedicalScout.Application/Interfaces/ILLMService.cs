using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MedicalScout.Application.Interfaces
{
    public interface ILLMService
    {
        Task<string> ChatAsync(string message);
    }
}
