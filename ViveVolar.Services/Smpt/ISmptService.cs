using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViveVolar.Services.Smpt
{
    public interface ISmptService
    {
        void sendEmail(string to, string subject, string body);
    }
}
