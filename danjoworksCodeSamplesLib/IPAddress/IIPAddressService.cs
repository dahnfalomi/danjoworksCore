using danjoworksCoreLibrary.IPAddress.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace danjoworksCoreLibrary.IPAddress
{
    public interface IIPAddressService
    {
        Task<IPFindResponse> GetIPAddressLocation();
    }
}
